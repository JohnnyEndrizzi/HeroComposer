using UnityEngine;

public class PlayDoor : Door {
    /* Functional Requirement
    * ID: 8.2-1
    * Description: The player must be able to choose a level.
    * 
    * Creates a path to the selected song using the provided name and difficulty, and saves it in ApplicationModel */
    protected new void OnMouseDown()
    {
        GameObject.Find("SongSelectionHandler").GetComponent<SongSelectionHandler>().OpenSongSelectionMenu();
    }
}
