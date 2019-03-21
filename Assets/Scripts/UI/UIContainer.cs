using UnityEngine;

public class UIContainer : MonoBehaviour {
    //Singleton instance
    public static UIContainer Instance { get; private set; }

    //Called before start
    void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            //Save singleton instance
            Instance = this;
            //Dont destroy between scenes
            DontDestroyOnLoad(gameObject);
        }
        //Add this UI's elements to already existing UI container
        else if(Instance != this)
        {
            Transform newParent = Instance.transform;
            Transform oldParent = this.transform;
            while (oldParent.childCount > 0)
            {
                oldParent.GetChild(0).SetParent(newParent, false);
            }
            //Move curtain to the bottom of the UI container
            if (newParent.Find("Curtain"))
            {
                newParent.Find("Curtain").SetAsLastSibling();
            }
            Destroy(this.gameObject);
        }
    }

    //Clear all UI layers except for the curtain
    public void ClearUILayers()
    {
        foreach (Transform child in this.transform)
        {
            if (!child.GetComponent<Curtain>())
            {
                Destroy(child.gameObject);
            }
        }
    }
}
