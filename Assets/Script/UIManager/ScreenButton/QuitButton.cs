using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // When the user is clicking on the Quit button of the TitleMenu
    // We quit the Game directly
    public void OnClick_TitleMenu()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    // When the user is clicking on the Quit button of the EndGame
    //Before quitting the game, we save the progress of the current sessions
    public void OnClick_EndGame()
    {
        DataPersistence.instance.PushData();

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    
}
