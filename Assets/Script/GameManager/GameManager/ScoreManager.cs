using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

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

    GameObject go_MainCamera;
    GameObject go_CharactAlive;

    [SerializeField] private int i_Score = 0;
    [SerializeField] private int i_Multiplier = 1;
    [SerializeField] private int f_PreviousPositionX = 10;

    public int GetScore() { return i_Score; }
    
    void Start()
    {
        go_MainCamera = GameObject.Find("Main Camera");
        go_CharactAlive = GameObject.Find("Character/Alive");
    }

    void Update()
    {
        ComputeScore();
        ComputeMultiplier();
    }

    // Function to compute the Score
    // How we compute it => Check that camera has a X coordinate above the f_PreviousPositionX
    // If so, we take the difference between these coordinate * difficulty_Level * Number_of_Charact
    // Then, we update f_PreviousPositionX with this new position
    // Else, do nothing
    void ComputeScore()
    {
        if ((int)go_MainCamera.transform.position.x > f_PreviousPositionX)
        {
            float f_level_Multiplier = 1 + (LevelManager.instance.GetLevel()/10);

            i_Score +=  (int)(((int)go_MainCamera.transform.position.x - f_PreviousPositionX) * f_level_Multiplier * i_Multiplier);

            f_PreviousPositionX = (int)go_MainCamera.transform.position.x;

            UIManager.instance.UpdateScoreDisplay(i_Score);
        }        
    }

    // Function to compute the multiplier
    void ComputeMultiplier()
    {
        i_Multiplier = go_CharactAlive.transform.childCount;

        UIManager.instance.UpdateMultiplierDisplay(i_Multiplier);
    }

}
