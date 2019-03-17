using UnityEngine.EventSystems;

public class MusicShopItem : UIItem {
    //When item is clicked in shop
    public override void OnPointerClick(PointerEventData eventData)
    {
        //Display details
        MusicShopHandler.DisplayItemDetails(this);
    }
}
