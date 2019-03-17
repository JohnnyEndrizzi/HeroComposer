using UnityEngine;
using UnityEngine.UI;

public class CharMenuBtn : MonoBehaviour {
    //Menu cell for Units in Inventory

    [SerializeField]
    private Image highlighter = null;
    private Text btnText;

    //Image to display to
    public Image image = null;

    public Character character;

    //Called before start
    private void Awake()
    {
        image = GetComponent<Image>();
        btnText = GetComponentInChildren<Text>();
    }

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});

        try { highlighter.enabled = false; } catch { }

        btnText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        btnText.fontSize = 16;
    }

    //Display character sprite in portrait
    public void DisplayCharacter(Character character)
    {
        if (character != null)
        {
            this.character = character;
            Sprite characterSprite = Resources.Load<Sprite>(this.character.sprite);
            image.sprite = characterSprite;
            Color c = image.color;
            c.a = 1;
            image.color = c;

            btnText.text = character.name;
        }
        else
        {
            RemoveCharacter();
        }

    }

    //Remove character sprite from portrait
    public void RemoveCharacter()
    {
        character = null;
        image.sprite = null;
        Color c = image.color;
        c.a = 0;
        image.color = c;

        btnText.text = "";
    }

    //Toggle Highlighting on/off  TODO
    public void ToggleHigh() 
    {
        Debug.Log(highlighter.enabled.ToString() + " High");
        if (highlighter.enabled) {
            highlighter.enabled = false;
        }
        else {
            highlighter.enabled = true;
        }
    }

    void OnClick() //Gets clicked
    {
        FindObjectOfType<InvController>().LoadInv(character);
    }
}