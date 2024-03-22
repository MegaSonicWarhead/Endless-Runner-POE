using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_BT : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
        //Application.Quit();
    }
}
