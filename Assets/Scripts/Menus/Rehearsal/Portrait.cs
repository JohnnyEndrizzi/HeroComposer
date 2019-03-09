using UnityEngine;
using UnityEngine.UI;

public class Portrait : ItemDragHandler {

    //Image displayed in portrait
    private Image image = null;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    //Display character sprite in portrait
    public void DisplayCharacter(Character character)
    {
        Sprite characterSprite = Resources.Load<Sprite>(character.sprite);
        image.sprite = characterSprite;
        Color c = image.color;
        c.a = 1;
        image.color = c;
    }

    //Remove character sprite from portrait
    public void RemoveCharacter()
    {
        image.sprite = null;
        Color c = image.color;
        c.a = 0;
        image.color = c;
    }
}
