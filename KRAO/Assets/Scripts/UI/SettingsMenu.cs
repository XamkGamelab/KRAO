using UnityEngine;
using UnityEngine.UI;

/* Create a working settings menu
* Settings don't have to be loaded or saved.
*/

public class SettingsMenu : MonoBehaviour
{
    private Dropdown graphicsDropdown => GetComponentInChildren<Dropdown>();

}

/*
 * Look into making two separate graphics modes (Low and High). Should affect Lighting settings,
 * max texture resolution settings. Unity can automate some of it, but some things
 * (dynamic lights in scene) might have to be programmed by hand.
 * 
 * Look into alternative ways (to PlayerPrefs) of saving and loading user preferences and potentially
 * how many lessons the player has completed. You don't have to create anything yet, just look into
 * the possibilities and assess if the functionality is worth the cost in time.
 */