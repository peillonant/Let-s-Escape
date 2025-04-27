using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

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

    [SerializeField] GameObject go_MainCamera;
    [SerializeField] private int i_Level = 1;

    public int GetLevel() { return i_Level; }

    void Update()
    {
        CheckIncreaseLevel();
    }

    // Function to check if the Camera is above the threshold to increase the difficulty
    // Compute : Camera.x /(100 * level)
    // If the result is above 1, then we increase the level by 1
    void CheckIncreaseLevel()
    {
        int i_Target = 100 * i_Level;
        
        if (go_MainCamera.transform.position.x / i_Target > 1)
        {
            i_Level++;
            UIManager.instance.UpdateLevelDisplay(i_Level);
        }
    }

    

}
