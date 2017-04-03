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
        // Get From Database;
        // use name is determine int id
        // set spriteid
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
        // Change 0 into PlayerActor
        // Change 1 into SlimeActor
        // Submit to Database
        OnNextWindow();
    }

    public void BackButton() {
        OnPreviousWindow();
    }

}
