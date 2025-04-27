using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    void Update()
    {
        if (gameObject.activeSelf && gameObject.name.Equals("TryAgainButton"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnClick_EndGame();
            }
        }
    }

    // Function called by the Button on the TitleMenu
    public void OnClick_TitleMenu()
    {
        UIManager.instance.StartGameDisplay();
        GameManager.instance.StartGame();
    }

    // Function called by the Button "Try Again" on the EndGame Menu
    public void OnClick_EndGame()
    {
        DataPersistence.instance.SetTryAgain(true);

        DataPersistence.instance.PushData();

        SceneManager.LoadScene("Game");
    }

}
