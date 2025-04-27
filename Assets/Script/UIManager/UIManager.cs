using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Launch the persistence of the gameObject
    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        // end of new code

        instance = this;
    }
    
    [SerializeField] private GameObject go_ScoreDisplay;
    [SerializeField] private GameObject go_MultiplierDisplay;
    [SerializeField] private GameObject go_LevelDisplay;

    public void UpdateScoreDisplay(int i_Score)
    {
        go_ScoreDisplay.GetComponent<TextMeshProUGUI>().SetText("Score: " + i_Score);
    }

    public void UpdateMultiplierDisplay(int i_Multiplier)
    {
        go_MultiplierDisplay.GetComponent<TextMeshProUGUI>().SetText("Alive: " + i_Multiplier);
    }

    public void UpdateLevelDisplay(int i_Level)
    {
        go_LevelDisplay.GetComponent<TextMeshProUGUI>().SetText("Level: " + i_Level);
    }

    // Function call when Game is launch to hide Title Screen and display Score GameObject
    public void StartGameDisplay()
    {

        GameObject go_Canvas = GameObject.Find("Main Canvas");

        // Hide the Title Screen
        go_Canvas.transform.GetChild(2).gameObject.SetActive(false);

        // Display Score
        go_Canvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Function call when Game is ended to hide Score GameObject and display EndGame GameObject
    public void EndGameDisplay()
    {
        GameObject go_Canvas = GameObject.Find("Main Canvas");

        // Hide Score
        go_Canvas.transform.GetChild(1).gameObject.SetActive(false);

        // Display End Game Screen
        go_Canvas.transform.GetChild(3).gameObject.SetActive(true);

        GetComponent<EndGameDisplay>().UpdateEndGameDisplay();
    }

    public void UpdateStatisticScreen()
    {
        GameObject go_Canvas = GameObject.Find("Main Canvas");

        // Hide the Title Screen
        go_Canvas.transform.GetChild(2).gameObject.SetActive(false);

        // Display Score
        go_Canvas.transform.GetChild(4).gameObject.SetActive(true);

        GetComponent<StatisticScreenDisplay>().UpdateStat();
    }

    public void BackToMenuFromStat()
    {
        GameObject go_Canvas = GameObject.Find("Main Canvas");

        // Hide the Title Screen
        go_Canvas.transform.GetChild(2).gameObject.SetActive(true);

        // Display Score
        go_Canvas.transform.GetChild(4).gameObject.SetActive(false);
    }
}
