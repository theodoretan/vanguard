using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindow : GenericWindow {

    public int sprite1 = 0;
    public int sprite2 = 0;
    public int sprite3 = 0;

    public Image CharacterImage1;
    public Image CharacterImage2;
    public Image CharacterImage3;

    public Sprite slime;
    public Sprite knight;

	[Space]
	[Header("Actor Templates")]
	public Actor playerTemplate;
	public Actor monsterTemplate;

    public void SwitchImage1(){
        if (sprite1 == 0) {
            CharacterImage1.sprite = knight;
            sprite1++;
        } else {
            CharacterImage1.sprite = slime;
            sprite1--;
        }
        CharacterImage1.SetNativeSize();
    }

    public void Start() {
		var connect = ConnectSocket.Instance;

		connect.GetCharacters ();
    }

    public void SwitchImage2() {
        if (sprite2 == 0) {
            CharacterImage2.sprite = knight;
            sprite2++;
        } else {
            CharacterImage2.sprite = slime;
            sprite2--;
        }
        CharacterImage2.SetNativeSize();
    }

    public void SwitchImage3() {
        if (sprite3 == 0) {
            CharacterImage3.sprite = knight;
            sprite3++;
        } else {
            CharacterImage3.sprite = slime;
            sprite3--;
        }
        CharacterImage3.SetNativeSize();
    }

    public void SubmitCharacters() {

		Actor a1 = sprite1 == 1 ? playerTemplate.Clone<Actor> () : monsterTemplate.Clone<Actor>();
		Actor a2 = sprite2 == 1 ? playerTemplate.Clone<Actor> () : monsterTemplate.Clone<Actor>();
		Actor a3 = sprite3 == 1 ? playerTemplate.Clone<Actor> () : monsterTemplate.Clone<Actor>();
        
		a1.ResetHealth ();
		a2.ResetHealth ();
		a3.ResetHealth ();

		var connect = ConnectSocket.Instance;

		connect.SetCharacters (a1,a2,a3);
		// Change 0 into PlayerActor
        // Change 1 into SlimeActor
        // Submit to Database
//        OnNextWindow();
    }

	public void SetCharacters(JSONObject a1, JSONObject a2, JSONObject a3) {
		sprite1 = Int32.Parse(a1["id"].str);
		sprite2 = Int32.Parse(a2["id"].str);
		sprite3 = Int32.Parse(a3["id"].str);

		CharacterImage1.sprite = sprite1 == 1 ? knight : slime;
		CharacterImage2.sprite = sprite2 == 1 ? knight : slime;
		CharacterImage3.sprite = sprite3 == 1 ? knight : slime;

		CharacterImage1.SetNativeSize ();
		CharacterImage2.SetNativeSize ();
		CharacterImage3.SetNativeSize ();
	}

    public void BackButton() {
        OnPreviousWindow();
    }

	public void NextWindow() {
		OnNextWindow ();
	}

}
