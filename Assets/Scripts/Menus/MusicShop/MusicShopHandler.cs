using UnityEngine;
using UnityEngine.UI;

public class MusicShopHandler : MonoBehaviour {
    //Item details panel
    [SerializeField]
    private Image itemDetailsPanel;

    //Audio
    private AudioSource audioSource;
    private AudioClip errorSound;
    private AudioClip regularPurchase;
    private AudioClip limitedTimePurchase;
    private AudioClip selectItem;

    // Use this for initialization
    void Start () {
        //Audio 
        audioSource = GetComponent<AudioSource>();
        errorSound = (AudioClip)Resources.Load("SoundEffects/shop_not_enough_money");
        regularPurchase = (AudioClip)Resources.Load("SoundEffects/shop_purchase_regular_item");
        limitedTimePurchase = (AudioClip)Resources.Load("SoundEffects/shop_purchase_special_item");
        selectItem = (AudioClip)Resources.Load("SoundEffects/inventory_pick_up_item");

        //Display available amount of money
        itemDetailsPanel.transform.Find("MoneyAmount").GetComponentInChildren<Text>().text = "Money: $" + GameManager.Instance.gameDataManager.GetAvailableMoney();
    }

    //Display image in itemDetailsPanel
    public static void DisplayItemDetails()
    {

    }

    //Purchase displayed item
    public void PurchaseItem()
    {

    }	
}
