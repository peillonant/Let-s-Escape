using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence instance;

    // Launch the persistence of the gameObject
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private bool b_TryAgain = false;
    [SerializeField] private int i_HighScore = 0;
    [SerializeField] private int i_NumberTries = 0;
    [SerializeField] private int i_MetersCovered = 0;
    [SerializeField] private List <int> i_Top10Scores = new List<int>();

    /*********************************************** ENCAPSULATION FUNCTION ******************************************************/
    public bool GetTryAgain() { return b_TryAgain; }
    public void SetTryAgain(bool b_newTryAgain) { b_TryAgain = b_newTryAgain;}
    
    public int GetHighScore() { return i_HighScore;}
    public void SetHighScore(int i_newHighScore) { i_HighScore = i_newHighScore;}

    public int GetNumberTries() { return i_NumberTries;}
    public void SetNumberTries(int i_newTries) { i_NumberTries = i_newTries;}

    public int GetMetersCovered() { return i_MetersCovered;}
    public void SetMetersCovered(int i_newMetersCovered) { i_MetersCovered = i_newMetersCovered;}

    public int GetTopScoreX(int i_indexTopScore) { return i_Top10Scores[i_indexTopScore]; }
    public int GetNumberTopScore() { return i_Top10Scores.Count; }

    // Function that Retrieve the index of the Score if the score is on the List
    public int GetIndexTopScore(int i_ScoreToCheck) 
    {
        if(i_Top10Scores.Contains(i_ScoreToCheck))
        {
            return i_Top10Scores.IndexOf(i_ScoreToCheck);
        }
        else
            return -1;
    }

    // Function to add the new score on the list and push the last item out of the list
    public void SetTopScoreX(int i_addTopScore) 
    {
        for (int i = 0; i < i_Top10Scores.Count; i++)
        {
            if (i_addTopScore > i_Top10Scores[i])
            {
                i_Top10Scores.Insert(i, i_addTopScore);
                break;
            }
        }

        if (!i_Top10Scores.Contains(i_addTopScore) && i_Top10Scores.Count < 10)
        {
            i_Top10Scores.Add(i_addTopScore);
        }

        if (i_Top10Scores.Count > 10)
        {
            i_Top10Scores.RemoveAt(i_Top10Scores.Count - 1);
        }
    }
    /*****************************************************************************************************************************/

    [System.Serializable]
    class SaveData
    {
        // Variable that need to be stored
        public int i_HighScore;
        public int i_NumberTries;
        public int i_MetersCovered;
        public List<int> i_Top10Scores;
    }

    public void WriteData()
    {
        SaveData data = new SaveData();

        // Variables that need to be stored
        data.i_HighScore = i_HighScore;
        data.i_NumberTries = i_NumberTries;
        data.i_MetersCovered = i_MetersCovered;
        data.i_Top10Scores = i_Top10Scores;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Variable that need to be stored
            i_HighScore = data.i_HighScore;
            i_NumberTries = data.i_NumberTries;
            i_MetersCovered = data.i_MetersCovered;
            i_Top10Scores = data.i_Top10Scores;
        }
    }


    public void PushData()
    {
        if (ScoreManager.instance.GetScore() > i_HighScore)
        {
            i_HighScore = ScoreManager.instance.GetScore();
        }

        if ((int)GameObject.Find("Main Camera").transform.position.x - 10 > i_MetersCovered)
        {
            i_MetersCovered = (int) GameObject.Find("Main Camera").transform.position.x;
        }

        i_NumberTries++;

        WriteData();
    }


    public void ResetData()
    {
        i_HighScore = 0;
        i_NumberTries = 0;
        i_MetersCovered = 0;
        i_Top10Scores.Clear();

        WriteData();
    }



}
