using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameDisplay : MonoBehaviour
{
    public void UpdateEndGameDisplay()
    {
        GameObject go_Canvas = GameObject.Find("Main Canvas").transform.GetChild(3).GetChild(3).gameObject;
        int i_score = ScoreManager.instance.GetScore();
        int i_HighScore = DataPersistence.instance.GetHighScore();
        int i_NumberTries = DataPersistence.instance.GetNumberTries() + 1;
        int i_Meters = (int)GameObject.Find("Main Camera").transform.position.x - 10;
        int i_Position = DataPersistence.instance.GetIndexTopScore(i_score) + 1;

        // Change the Text for the Score
        go_Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Score: " + i_score);

        // Change the Text for the Position

        // If the score is the new HighScore
        if (i_Position == 1)
        {   
            go_Canvas.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Top: " + i_Position);
            go_Canvas.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        }
        else if (i_Position == 0)
        {
            go_Canvas.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Not on the Top 10");
            go_Canvas.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            go_Canvas.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().SetText("(Score Above: " + DataPersistence.instance.GetTopScoreX(9) + ")");
        }
        else
        {
            go_Canvas.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Top: " + i_Position);
            go_Canvas.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            go_Canvas.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().SetText("(Score Above: " + DataPersistence.instance.GetTopScoreX(i_Position-2) + ")");
        }

        // Change the Text for the HighScore
        if (i_score > i_HighScore)
        {
            go_Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("High Score: " + i_score);
            go_Canvas.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            go_Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("High Score: " + i_HighScore);
            go_Canvas.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        }

        // Change the Text of meter covered
        go_Canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText("Meters covered: " + i_Meters);
        if (DataPersistence.instance.GetMetersCovered() < i_Meters)
        {
            go_Canvas.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            go_Canvas.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
        }

        // Change the Text of number of try
        go_Canvas.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText("Tries: " + i_NumberTries); 
    }
}
