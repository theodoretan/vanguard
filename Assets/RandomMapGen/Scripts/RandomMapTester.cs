using UnityEngine;
using System.Collections;

public class RandomMapTester : MonoBehaviour {

	[Header("Map Dimensions")]
	public int mapWidth = 20;
	public int mapHeight = 20;

	[Space]
	[Header("Vizualize Map")]
	public GameObject mapContainer;
	public GameObject tilePrefab;
	public Vector2 tileSize = new Vector2(16,16);

	[Space]
	[Header("Map Sprites")]
	public Texture2D islandTexture;
	public Texture2D fowTexture;

	[Space]
	[Header("Player")]
	public GameObject playerPrefab;
	public GameObject player;
	public int distance = 3;
	[Range(0, .9f)]
	public float randomBattleOdds = .3f;

	[Space]
	[Header("Actor Templates")]
	public Actor playerTemplate;
	public Actor monsterTemplate;

	[Space]
	[Header("Decorate Map")]
	[Range(0, .9f)]
	public float erodePercent = .5f;
	public int erodeIterations = 2;
	[Range(0, .9f)]
	public float treePercent = .3f;
	[Range(0, .9f)]
	public float hillPercent = .2f;
	[Range(0, .9f)]
	public float mountainsPercent = .1f;
	[Range(0, .9f)]
	public float townPercent = .05f;
	[Range(0, .9f)]
	public float monsterPercent = .1f;
	[Range(0, .9f)]
	public float lakePercent = .05f;

	public Map map;

	private int tmpX;
	private int tmpY;
	private Sprite[] islandTileSprites;
	private Sprite[] fowTileSprites;
	private BattleWindow battleWindow;
	private Actor playerActor;
	private StatsWindow statsWindow;

	// Use this for initialization
	// void Start () {
	// 	// islandTileSprites = Resources.LoadAll<Sprite> (islandTexture.name);
	// 	// fowTileSprites = Resources.LoadAll<Sprite> (fowTexture.name);

	// 	Reset ();
	// }

	public WindowManager windowManager {
		get {
			return GenericWindow.manager;
		}
	}

	public void Reset(){
		islandTileSprites = Resources.LoadAll<Sprite> (islandTexture.name);
		fowTileSprites = Resources.LoadAll<Sprite> (fowTexture.name);

		map = new Map ();
		MakeMap ();
		StartCoroutine (AddPlayer ());
	}

	public void Shutdown() {
		ClearMapContainer();
	}

	IEnumerator AddPlayer(){
		yield return new WaitForEndOfFrame ();
		CreatePlayer ();

	}

	
	public void MakeMap(){
		map.NewMap (mapWidth, mapHeight);
		map.CreateIsland (
			erodePercent,
			erodeIterations,
			treePercent,
			hillPercent,
			mountainsPercent,
			townPercent,
			monsterPercent,
			lakePercent
		);
		CreateGrid ();
		CenterMap (map.castleTile.id);
	}

	void CreateGrid(){
		ClearMapContainer ();

		var total = map.tiles.Length;
		var maxColumns = map.columns;
		var column = 0;
		var row = 0;

		for (var i = 0; i < total; i++) {

			column = i % maxColumns;

			var newX = column * tileSize.x;
			var newY = -row * tileSize.y;

			var go = Instantiate (tilePrefab);
			go.name = "Tile " + i;
			go.transform.SetParent (mapContainer.transform);
			go.transform.position = new Vector3 (newX, newY, 0);

			DecorateTile (i);

			if (column == (maxColumns - 1)) {
				row++;
			}

		}

	}

	private void DecorateTile(int tileID){
		var tile = map.tiles [tileID];
		var spriteID = tile.autotileID;
		var go = mapContainer.transform.GetChild (tileID).gameObject;

		if (spriteID >= 0) {
			var sr = go.GetComponent<SpriteRenderer> ();

			if (tile.visited) {
				sr.sprite = islandTileSprites [spriteID];
			} else {

				tile.CalculateFoWAutotileID ();
				sr.sprite = fowTileSprites [Mathf.Min(tile.fowAutotileID, fowTileSprites.Length - 1)];
			}
		}
	}

	public void CreatePlayer(){
		player = Instantiate (playerPrefab);
		player.name = "Player";
		player.transform.SetParent (mapContainer.transform);

		var controller = player.GetComponent<MapMovementController> ();
		controller.map = map;
		controller.tileSize = tileSize;
		controller.tileActionCallback += TileActionCallback;

		var moveScript = Camera.main.GetComponent<MoveCamera> ();
		moveScript.target = player;

		controller.MoveTo (map.castleTile.id);

		playerActor = playerTemplate.Clone<Actor>();
		playerActor.ResetHealth();

		statsWindow = windowManager.Open((int) Windows.StatsWindow - 1, false) as StatsWindow;
		statsWindow.target = playerActor;
		statsWindow.UpdateStats();
	}

	void TileActionCallback(int type){

		var tileID = player.GetComponent<MapMovementController> ().currentTile;
		VisitTile (tileID);

		// var messageWindow = windowManager.Open((int) Windows.MessageWindow - 1, false) as MessageWindow;
		// messageWindow.text = "On tile type" + type;

		switch (type) {
			case 19:
				DisplayMessage("you have entered a town");
				break;
			case 20:
				DisplayMessage("you have entered a castle");
				break;

			case 21:
				StartBattle();
				break;
			default:
				var chance = Random.Range(0, 1f);

				if (chance < randomBattleOdds) {
					StartBattle();
				}
				break;
			
		}

	}

	void DisplayMessage(string text) {
		var messageWindow = windowManager.Open((int) Windows.MessageWindow - 1, false) as MessageWindow;
		messageWindow.text = text;
	}

	void ClearMapContainer(){

		var children = mapContainer.transform.GetComponentsInChildren<Transform> ();
		for (var i = children.Length - 1; i > 0; i--) {
			Destroy (children [i].gameObject);
		}

	}

	void CenterMap(int index){

		var camPos = Camera.main.transform.position;
		var width = map.columns;

		PosUtil.CalculatePos (index, width, out tmpX, out tmpY);

		camPos.x = tmpX * tileSize.x;
		camPos.y = -tmpY * tileSize.y;
		Camera.main.transform.position = camPos;

	}

	void VisitTile(int index){

		int column, newX, newY, row = 0;

		PosUtil.CalculatePos (index, map.columns, out tmpX, out tmpY);

		var half = Mathf.FloorToInt (distance / 2f);
		tmpX -= half;
		tmpY -= half;

		var total = distance * distance;
		var maxColumns = distance - 1;

		for (int i = 0; i < total; i++) {

			column = i % distance;

			newX = column + tmpX;
			newY = row + tmpY;

			PosUtil.CalculateIndex (newX, newY, map.columns, out index);

			if (index > -1 && index < map.tiles.Length) {
				var tile = map.tiles [index];
				tile.visited = true;
				DecorateTile (index);

				foreach (var neighbor in tile.neighbors) {

					if (neighbor != null) {

						if (!neighbor.visited) {

							neighbor.CalculateFoWAutotileID ();
							DecorateTile (neighbor.id);
						}

					}
				}
			}

			if (column == maxColumns) {
				row++;
			}
		}

	}

	public void StartBattle() {
		var monsterActor = monsterTemplate.Clone<Actor>();
		monsterActor.ResetHealth();

		battleWindow = windowManager.Open((int) Windows.BattleWindow - 1, false) as BattleWindow;
		battleWindow.battleOverCallback += BattleOver;
		battleWindow.StartBattle(playerActor, monsterActor);
		TogglePlayerMovement(false);
	}

	public void EndBattle() {
		battleWindow.Close();
		TogglePlayerMovement(true);
	}

	private void TogglePlayerMovement(bool value) {
		player.GetComponent<MapMovementController>().enabled = value;
		Camera.main.GetComponent<MoveCamera>().enabled = value;
	}

	private void BattleOver(bool playerWin) {
		EndBattle();

		if (!playerWin) {
			Destroy(player);
			playerActor = null;
			StartCoroutine(ExitGame());

			// var messageWindow = windowManager.Open((int)Windows.MessageWindow -1 , false) as MessageWindow;
			// messageWindow.text = "The game is over.";
		} else {
			var tileID = player.GetComponent<MapMovementController>().currentTile;
			var tile = map.tiles[tileID];

			if (tile.autotileID == 21) {
				tile.autotileID = 22;
				DecorateTile(tileID);
			}
		}
	}

	IEnumerator ExitGame() {
		DisplayMessage("The game is over.");

		yield return new WaitForSeconds(1.5f);

		windowManager.Open((int)Windows.GameOverWindow - 1);
	}
}
