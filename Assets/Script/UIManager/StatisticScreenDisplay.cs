using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticScreenDisplay : MonoBehaviour
{
    public void UpdateStat()
    {
        GameObject go_Canvas = GameObject.Find("Main Canvas").transform.GetChild(4).GetChild(2).gameObject;

        int i_NumberTries = DataPersistence.instance.GetNumberTries();
        int i_Meters = (int)GameObject.Find("Main Camera").transform.position.x - 10;
        int i_NumberOfTopScore = DataPersistence.instance.GetNumberTopScore();

        // Function to update the list of 10 top score
        for (int i = 0; i < i_NumberOfTopScore; i++)
        {
            int i_tmp = i+1;
            go_Canvas.transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().SetText("Top " + i_tmp + ": " + DataPersistence.instance.GetTopScoreX(i));
        }

        // Function to put at 0 the line (Used for the rest of data)
        for (int i = i_NumberOfTopScore; i < 10; i++)
        {
            int i_tmp = i+1;
            go_Canvas.transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().SetText("Top " + i_tmp + ": 0");
        }

        // Function to update the numbor of Meters
        go_Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Meters covered: " + i_Meters);

        // Function to update the number of tries 
        go_Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("Tries: " + i_NumberTries); 

    }
}
