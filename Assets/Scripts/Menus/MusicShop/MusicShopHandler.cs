using UnityEngine;

public class MusicShopHandler : MonoBehaviour {
    //Item details panel
    private static ItemDetailsPanel itemDetailsPanel;
    //Store instance of currently displayed item
    private static MusicShopItem displayedStoreItem;

    //Audio
    private static AudioSource audioSource;
    private AudioClip errorSound;
    private AudioClip regularPurchase;
    private AudioClip limitedTimePurchase;
    private static AudioClip selectItem;

    // Use this for initialization
    void Start () {
        //Audio 
        audioSource = GetComponent<AudioSource>();
        errorSound = (AudioClip)Resources.Load("SoundEffects/shop_not_enough_money");
        regularPurchase = (AudioClip)Resources.Load("SoundEffects/shop_purchase_regular_item");
        limitedTimePurchase = (AudioClip)Resources.Load("SoundEffects/shop_purchase_special_item");
        selectItem = (AudioClip)Resources.Load("SoundEffects/inventory_pick_up_item");

        //Item details panel
        itemDetailsPanel = FindObjectOfType<ItemDetailsPanel>();
    }

    //Display item in itemDetailsPanel
    public static void DisplayItemDetails(MusicShopItem musicShopItem)
    {
        //Display item 
        itemDetailsPanel.DisplayItem(musicShopItem.item);
        //Keep track of which item in store is displayed
        displayedStoreItem = musicShopItem;
        //Sound effect
        audioSource.PlayOneShot(selectItem, 0.7f);
    }   

    //Purchase displayed item
    public void PurchaseItem()
    {
        //Get item
        Item item = itemDetailsPanel.item;
        //If player can afford the item
        if (GameManager.Instance.gameDataManager.GetAvailableMoney() >= item.cost)
        {
            //Remove item from details panel
            itemDetailsPanel.RemoveItem();
            //Adjust avaialable money by cost of item
            itemDetailsPanel.UpdateAvailableMoney(-item.cost);
            //Add item to player's inventory
            GameManager.Instance.gameDataManager.AddItemToInventory(item);
            //Save inventory
            GameManager.Instance.gameDataManager.SaveInventory();
            //Remove item from store list
            Destroy(displayedStoreItem.gameObject);
            //Sound effect
            audioSource.PlayOneShot(regularPurchase, 0.7f);
        }
        //Player can't afford the item
        else
        {
            //Sound effect
            audioSource.PlayOneShot(errorSound, 0.7f);
        }
    }	
}
