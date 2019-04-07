using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanel : MonoBehaviour {
    //Item currently displayed
    public Item item { get; private set; }

    //Available money
    [SerializeField]
    private Text availableMoney;

    //Item information
    [SerializeField]
    private new Text name;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Image icon;

    //Purchase button
    [SerializeField]
    private Button purchaseButton;

    //Used for initialization
    private void Start()
    {
        availableMoney.text = "Money: $" + GameManager.Instance.gameDataManager.GetAvailableMoney();
        purchaseButton.interactable = false;
        Color c = icon.color;
        c.a = 0;
        icon.color = c;
    }

    //Update available money after a purchase/sale
    public void UpdateAvailableMoney(int amount)
    {
        GameManager.Instance.gameDataManager.UpdateAvailableMoney(amount);
        availableMoney.text = "Money: $" + GameManager.Instance.gameDataManager.GetAvailableMoney();
    }

    //Display item in item details panel
    public void DisplayItem(Item item)
    {
        this.item = item;
        name.text = this.item.name;
        string desc = this.item.description;

        //Row Names
        string[] statName = new string[] { "Level", "HP   ", "ATK  ", "DEF  ", "RCV  ", "MGC  " };
        int[] itemStats = new int[]
        {
            item.level,
            item.hp,
            item.atk,
            item.def,
            item.rcv,
            item.mgc
        };

        for (int i = 0; i < itemStats.Length; i++)
        {
            if (itemStats[i] != 0)
            {
                desc += "\n" + statName[i] + ": " + ((Math.Sign(itemStats[i]) > 0) ? "<color=green><size=20>+" : "<color=red><size=20>-") + itemStats[i] + "</size></color>";
            }
        }
        description.text = desc;

        purchaseButton.GetComponentInChildren<Text>().text = "Buy: $"+this.item.cost.ToString();
        icon.sprite = Resources.Load<Sprite>(this.item.sprite);
        purchaseButton.interactable = true;
        Color c = icon.color;
        c.a = 1;
        icon.color = c;
    }

    //Remove item from details panel
    public void RemoveItem()
    {
        this.item = null;
        name.text = "";
        description.text = "";
        purchaseButton.GetComponentInChildren<Text>().text = "Select an Item!";
        icon.sprite = null;
        purchaseButton.interactable = false;
        Color c = icon.color;
        c.a = 0;
        icon.color = c;
    }
}
