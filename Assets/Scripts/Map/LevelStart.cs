using System.Collections;
using UnityEngine;

public class LevelStart : Interactable {

    public string songName = "";
    public DialogueTrigger dialogueTrigger;

    string difficulty = "Normal";

    public override void Interact()
    {
        //Start cutscene
        if (dialogueTrigger != null)
        {
            StartCoroutine(DisplayCutscene());
        }
        else
        {
            StartLevel();
        }
    }

    private void StartLevel()
    {
        /* Functional Requirement
       * ID: 8.2-1
       * Description: The player must be able to choose a level.
       * 
       * Creates a path to the selected song using the provided name and difficulty, and saves it in ApplicationModel */
        //Assets.Scripts.MainMenu.ApplicationModel.songName = songName;
        //Assets.Scripts.MainMenu.ApplicationModel.songPathName = songName + "_" + difficulty;
        GameManager.Instance.gameDataManager.SetActiveLevel(songName);
        GameManager.Instance.sceneManager.SwitchSceneWithCurtains("main", false);
    }

    IEnumerator DisplayCutscene()
    {
        dialogueTrigger.TriggerDialogue();
        yield return new WaitUntil(() => GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueBoxAnimator.GetBool("IsOpen") == false);
        StartLevel();
    }
}
