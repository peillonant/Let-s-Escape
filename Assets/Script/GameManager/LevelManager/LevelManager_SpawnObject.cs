using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_SpawnObject : MonoBehaviour
{

    [SerializeField] GameObject go_BlockLock;
    [SerializeField] GameObject go_Obstacle_Type1;

    // Function called by the PlatformManager to check and trigger the creation of Charact or Object on the Platform
    public void CreationObject(GameObject go_newPlatform)
    {
        if (CheckSpawnCharact())
        {
            SpawnCharactOnPlatform(go_newPlatform);
        }

        CheckSpawnObjectOnPlatform(go_newPlatform);
    }

    // Function to check if we have to add the new Charact on the platform
    // First check if we are on the correct level to trigger the event
    // Then check if the charact has been already or not added on the game
    bool CheckSpawnCharact()
    {
        int i_CurrentLevel = LevelManager.instance.GetLevel();
        int i_NumberOfCharactLocked = GameObject.Find("Character/Locked").transform.childCount;

        if (i_CurrentLevel == 1 && i_NumberOfCharactLocked == 3)
            return true;
        
        else if ( i_CurrentLevel == 10 && i_NumberOfCharactLocked == 2)
            return true;

        else if (i_CurrentLevel == 15 && i_NumberOfCharactLocked == 1)
            return true;

        else
            return false;
    }

    // Function to add the Charact on the Platform created
    void SpawnCharactOnPlatform(GameObject go_newPlatform)
    {
        // First we create the block above the newCharact that will generate the Spring Joint
        GameObject go_NewBlockLock = Instantiate(go_BlockLock, GameObject.Find("Block").transform);
        go_NewBlockLock.transform.position = new Vector3(go_newPlatform.transform.position.x, 0, 0);

        // Retrieve the Charact that will added to the Game
        GameObject go_NewCharact = GameObject.Find("Character/Locked").transform.GetChild(0).gameObject;

        go_NewCharact.GetComponent<SpringJoint>().connectedBody = go_NewBlockLock.GetComponent<Rigidbody>();
        
        go_NewCharact.GetComponent<CharactProperties>().SetButtonBlockLinked(go_NewBlockLock);
        
        go_NewCharact.transform.position = new Vector3(go_newPlatform.transform.position.x, 1, 0);

        go_NewCharact.transform.SetParent(GameObject.Find("Character/Unlocked").transform);

        go_NewCharact.SetActive(true);
    }

    // Function to check if we have to add a Object on the platform
    // Check if we are on the correct level to trigger the event
    void CheckSpawnObjectOnPlatform(GameObject go_newPlatform)
    {
        int i_CurrentLevel = LevelManager.instance.GetLevel();
        int i_SpwanRate = UnityEngine.Random.Range(1, 11);

        if (i_CurrentLevel == 2 && i_SpwanRate == 1)
        {
            SpawnObjectOnPlatform(go_newPlatform);
        }
        else if (i_CurrentLevel >= 9 && i_CurrentLevel <= 12 && i_SpwanRate >= 1 && i_SpwanRate <= 2)
        {
            SpawnObjectOnPlatform(go_newPlatform);
        }
        else if (i_CurrentLevel >=13 && i_CurrentLevel <= 20 && i_SpwanRate >= 1 && i_SpwanRate <= 4)
        {
            SpawnObjectOnPlatform(go_newPlatform);
        }

       
    }

    // Function to add an Obstacle on the Platform created
    // First, make a random to see if we put in the front (0) or in the back (1) of the platform
    // Then, position it regarding the result
    void SpawnObjectOnPlatform(GameObject go_newPlatform)
    {
        int i_RandomFrontBack = UnityEngine.Random.Range(0,2);
        float f_Position_x;
        
        GameObject go_newObstacle = Instantiate(go_Obstacle_Type1);
        go_newObstacle.transform.SetParent(go_newPlatform.transform);

        if (i_RandomFrontBack == 0)
        {
            f_Position_x = go_newPlatform.transform.position.x - go_newPlatform.GetComponent<PlatformProperty>().GetSizePlatform() + 0.15f;
        }
        else
        {
            f_Position_x = go_newPlatform.transform.position.x + go_newPlatform.GetComponent<PlatformProperty>().GetSizePlatform() - 0.15f;
        }

        //go_newObstacle.transform.position = new Vector3(f_Position_x, 0.62f, -0.375f);
        go_newObstacle.transform.position = new Vector3(f_Position_x, 0.62f, -2);
    }


}
