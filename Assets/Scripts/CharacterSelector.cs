using UnityEngine;
using System.Collections;

public class CharacterSelector : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerSpawnPosition = new Vector3(0, 1, -7);
    public Character[] characters;

    //public GameObject characterSelectPanel;
    //public GameObject abilityPanel;

    public void OnCharacterSelect(int characterChoice)
    {
        //characterSelectPanel.SetActive(false);
        //abilityPanel.SetActive(true);
        Character selectedCharacter = characters[characterChoice];

        player.GetComponent<SpriteRenderer>().sprite = selectedCharacter.sprite;

        GameObject spawnedPlayer = Instantiate(player, playerSpawnPosition, Quaternion.identity) as GameObject;
        //WeaponMarker weaponMarker = spawnedPlayer.GetComponentInChildren<WeaponMarker>();
        //AbilityCoolDown[] coolDownButtons = GetComponentsInChildren<AbilityCoolDown>();
        //for (int i = 0; i < coolDownButtons.Length; i++)
        //{
        //    coolDownButtons[i].Initialize(selectedCharacter.characterAbilities[i], weaponMarker.gameObject);
        //}
    }
}