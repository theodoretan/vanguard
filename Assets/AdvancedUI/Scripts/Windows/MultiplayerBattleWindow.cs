using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerBattleWindow : GenericWindow {

    public GameObject actionsGroup;
    public bool yourturn;
    public bool finishedRound = false;

	public Text OpponentHP;
	public Text PlayerHP;

	public Actor SlimeTemplate;
	public Actor KnightTemplate;

	public Sprite SlimeImageTemplate;
	public Sprite KnightImageTemplate;

	public Image PlayerImage;
	public Image OppImage;

	private Actor Player1;
	private Actor Opp1;

    public RectTransform playerRect;
    public RectTransform monsterRect;

    private ShakeManager shakeManager;

    protected override void Awake() {
        shakeManager = GetComponent<ShakeManager>();
        base.Awake();
    }

    public override void Open() {
        base.Open();
    }

    public void CalculateDamage(JSONObject data) {

        if (data["first"].str == "you") {
            // we first
            Opp1.DecreaseHealth(Player1.attack);
            shakeManager.Shake(monsterRect, 1f, 2);
            yourturn = false;
        } else {
            Player1.DecreaseHealth(Opp1.attack);
            shakeManager.Shake(playerRect, 1f, 2);
            yourturn = true;
        }

        StartCoroutine(UpdateStats());
    }

    public void SetupBattle (JSONObject player, JSONObject opp){

//        Debug.Log(player);
//        Debug.Log(opp);
        Debug.Log("Setting up battle!");
		this.Player1 = Int32.Parse(player ["character1"]["id"].str) == 1 	? KnightTemplate.Clone<Actor>() : SlimeTemplate.Clone<Actor>();
		this.Opp1 = Int32.Parse(opp ["character1"]["id"].str) == 1 ? KnightTemplate.Clone<Actor>() : SlimeTemplate.Clone<Actor>();

		this.Player1.ResetHealth ();
		this.Opp1.ResetHealth ();

		this.OpponentHP.text = Opp1.health+"/"+Opp1.maxHealth;
		this.PlayerHP.text = Player1.health + "/" + Player1.maxHealth;

		this.PlayerImage.sprite = Int32.Parse (player ["character1"]["id"].str) == 1 ? KnightImageTemplate : SlimeImageTemplate;
		this.OppImage.sprite = Int32.Parse (opp ["character1"]["id"].str) == 1 ? KnightImageTemplate : SlimeImageTemplate;

		this.PlayerImage.SetNativeSize ();
		this.OppImage.SetNativeSize ();
	}


	public void Attack(){
		// Attack
		Debug.Log("Attack Pressed");

        finishedRound = false;

        // disable attack/run key
        actionsGroup.SetActive(false);

        var socket = ConnectSocket.Instance;

        socket.MonsterInformation(Player1);

	}

	public void SpecialAttack(){
		// Special Attack
		Debug.Log("Special Attack Pressed");
	}

	public void Defense(){
		// Defense
		Debug.Log("Defense Pressed");
	}

	public void Run(){
		// Disconnect and go back to menu
		Debug.Log("Surrendering Pressed");
		var socket = ConnectSocket.Instance;

		socket.Disconnect ();
		manager.Open ((int) Windows.MenuWindow - 1);
	}

    public void NextAction() {
        if (yourturn) {
            Opp1.DecreaseHealth(Player1.attack);
            shakeManager.Shake(monsterRect, 1f, 2);

        } else {
            Player1.DecreaseHealth(Opp1.attack);
            shakeManager.Shake(playerRect, 1f, 2);

        }
        finishedRound = true;
        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats() {
        yield return new WaitForSeconds(0.5f);
        this.OpponentHP.text = Opp1.health + "/" + Opp1.maxHealth;
        this.PlayerHP.text = Player1.health + "/" + Player1.maxHealth;

        yield return new WaitForSeconds(1);

        if (!Player1.alive || !Opp1.alive) {
            // they dead and game over
            StartCoroutine(OnBattleOver());
        } else {
            if (finishedRound) {
                // go do actions
                actionsGroup.SetActive(true);
            } else {
                NextAction();
            }
        }
    }

    IEnumerator OnBattleOver() {
        var message = (Player1.alive ? Player1.name : Opp1.name) + " has won the battle";

        // display message
        actionsGroup.SetActive(true);

        // wait for stuff
        yield return new WaitForSeconds(0.5f);

        // update server
        var socket = ConnectSocket.Instance;

        socket.UpdateScore(Player1.alive);

    }
}
