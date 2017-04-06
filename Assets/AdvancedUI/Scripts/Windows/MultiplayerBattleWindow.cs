using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerBattleWindow : GenericWindow {

    public GameObject actionsGroup;

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

    public override void Open() {
        base.Open();
    }

    public void CalculateDamage(JSONObject opponent) {
        // check speed
        var mySpeed = Player1.speed;
        var oppSpeed = Int32.Parse(opponent["speed"].str);

        if (mySpeed > oppSpeed) {
            // we first
            Opp1.DecreaseHealth(Player1.attack);
            if (!Opp1.alive) {
                // they dead and game over
                StartCoroutine(OnBattleOver());
            }

            Player1.DecreaseHealth(Opp1.attack);

            if (!Player1.alive) {
                // we dead, gameover
                StartCoroutine(OnBattleOver());
            }
        } else {
            // we second
            Player1.DecreaseHealth(Opp1.attack);

            if (!Player1.alive) {
                // we dead, gameover
                StartCoroutine(OnBattleOver());
            }

            Opp1.DecreaseHealth(Player1.attack);
            if (!Opp1.alive) {
                // they dead and game over
                StartCoroutine(OnBattleOver());
            }
        }

        this.OpponentHP.text = Opp1.health + "/" + Opp1.maxHealth;
        this.PlayerHP.text = Player1.health + "/" + Player1.maxHealth;

        // enable attack/run key
        actionsGroup.SetActive(true);
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

    IEnumerator OnBattleOver() {
        var message = (Player1.alive ? Player1.name : Opp1.name) + " has won the battle";

        // display message

        // wait for stuff
        yield return new WaitForSeconds(0.5f);

        // update server
        var socket = ConnectSocket.Instance;

        socket.UpdateScore(Player1.alive);

    }
}
