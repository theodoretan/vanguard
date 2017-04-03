using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerBattleWindow : GenericWindow {

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

    public void SetupBattle (JSONObject player, JSONObject opp){

        Debug.Log(player);
        Debug.Log(opp);
        Debug.Log("Setting up battle!");
		this.Player1 = Int32.Parse(player ["id"].str) == 1 	? KnightTemplate.Clone<Actor>() : SlimeTemplate.Clone<Actor>();
		this.Opp1 = Int32.Parse(opp ["id"].str) == 1 ? KnightTemplate.Clone<Actor>() : SlimeTemplate.Clone<Actor>();

		this.OpponentHP.text = Opp1.health+"/"+Opp1.maxHealth;
		this.PlayerHP.text = Player1.health + "/" + Player1.maxHealth;

		this.PlayerImage.sprite = Int32.Parse (player ["id"].str) == 1 ? KnightImageTemplate : SlimeImageTemplate;
		this.OppImage.sprite = Int32.Parse (opp ["id"].str) == 1 ? KnightImageTemplate : SlimeImageTemplate;
	}


	public void Attack(){
		// Attack
		Debug.Log("Attack Pressed");
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
	}

}
