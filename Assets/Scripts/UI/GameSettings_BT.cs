using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings_BT : MonoBehaviour
{
    public GameObject GameSettingsPopUpMenu;
    public GameObject MainMenuBT;
    public GameObject RestartBT;
    public GameObject QuitBT;
    public GameObject SettingsBT;
    // Start is called before the first frame update
    public void GameSettings()
    {

        GameSettingsPopUpMenu.SetActive(true);
        MainMenuBT.SetActive(false);
        RestartBT.SetActive(false);
        QuitBT.SetActive(false);
        SettingsBT.SetActive(false);

    }
}
