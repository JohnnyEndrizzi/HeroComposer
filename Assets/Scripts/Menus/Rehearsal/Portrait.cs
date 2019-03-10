using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Portrait : MonoBehaviour{

    //Image displayed in portrait
    private Image image = null;
    private Character character = null;

    //Called before start
    private void Awake()
    {
        image = GetComponent<Image>();
        RemoveCharacter();
    }

    //Display character sprite in portrait
    public void DisplayCharacter(Character character)
    {
        this.character = character;
        Sprite characterSprite = Resources.Load<Sprite>(this.character.sprite);
        image.sprite = characterSprite;
        Color c = image.color;
        c.a = 1;
        image.color = c;
    }

    //Remove character sprite from portrait
    public void RemoveCharacter()
    {
        character = null;
        image.sprite = null;
        Color c = image.color;
        c.a = 0;
        image.color = c;
    }
}
