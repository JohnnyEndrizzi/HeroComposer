using DummyFiles;
using HeroComposer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUser : MonoBehaviour {

    private int Cash = 0;
    private List<GameItems> UserItems = new List<GameItems> { };

	public int AddCash (int add) {
        return 0;
	}

	public int RemoveCasts (int remove) {
        return 0;
    }

    public List<GameItems> GetItemList()
    {
        return new List<GameItems> { };
    }

    public int AddItem(GameItems add)
    {
        return 0;
    }

    public int RemoveItem(GameItems remove)
    {
        return 0;
    }
}
