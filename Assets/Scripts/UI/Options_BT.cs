using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_BT : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject player;

    // Start is called before the first frame update
    public void Options()
    {
        OptionsMenu.SetActive(true);
        player.SetActive(false);
    }
}
