using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    GameObject go_CharactLived;
    bool b_GameEnd;
    bool b_GameStart;

    float f_timeToInvoke = 5;

    /*********************************************** ENCAPSULATION FUNCTION ******************************************************/
    public void SetTimeToInvoke(float f_newTimeToInvoke) { f_timeToInvoke = f_newTimeToInvoke; }
    public bool GetGameStart() { return b_GameStart; } 

    /*****************************************************************************************************************************/


    public void Start()
    {
        go_CharactLived = GameObject.Find("Character/Alive");

        DataPersistence.instance.LoadData();

        if (DataPersistence.instance.GetTryAgain())
        {
            UIManager.instance.StartGameDisplay();
            StartGame();
        }
    }

    public void StartGame()
    {
        b_GameStart = true;
        Invoke("CallButtonManager", 1); 
    }

    void Update()
    {
        if (go_CharactLived != null)   
        {
            if(go_CharactLived.transform.childCount == 0)
            {
                EndGame();
            }
        }    
    }

    // Function that trigger the end of the game
    void EndGame()
    {
        if (!b_GameEnd)
            DataPersistence.instance.SetTopScoreX(ScoreManager.instance.GetScore());

        b_GameEnd = true;
        UIManager.instance.EndGameDisplay();
    }


    // Function that manage the creation of Button on the Canvas
    void CallButtonManager()
    {
       ButtonManager.instance.StartSpwaning();

       if (!b_GameEnd)
            Invoke("CallButtonManager", f_timeToInvoke);
    }

}
