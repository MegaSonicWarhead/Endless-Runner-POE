using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings_BT : MonoBehaviour
{
    public GameObject SettingsPopUpMenu;
    public GameObject StartGameBT;
    public GameObject SettingsBT;
    public GameObject exitBT;
    // Start is called before the first frame update
    public void Settings()
    {
        
        SettingsPopUpMenu.SetActive(true);
        StartGameBT.SetActive(false);
        SettingsBT.SetActive(false);
        exitBT.SetActive(false);
        
    }
}
