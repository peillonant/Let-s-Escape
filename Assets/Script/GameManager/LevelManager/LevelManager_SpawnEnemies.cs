using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject go_FlyingEnemyTemplate;
    [SerializeField] private List <GameObject> go_JumpingEnemyTemplates;
    bool b_FlyingObjectAlreadyExist;
    bool b_JumpingObjectAlreadyExist;
    
    public void SetFlyingAlreadyExist(bool b_newFlyingObject) { b_FlyingObjectAlreadyExist = b_newFlyingObject;}
    public void SetJumpingAlreadyExist(bool b_newJumpingObject) { b_JumpingObjectAlreadyExist = b_newJumpingObject;}

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.GetLevel() > 1 && !b_JumpingObjectAlreadyExist)
        {
            
            // if (CheckSpawnEnemy())
            // {
            //     CreationAllowedJumpingEnemy();
            // }
            // else
            // {
            //     b_JumpingObjectAlreadyExist = true;
            //     StartCoroutine(TimerBeforeNextCreationCheck(2, "Jumping"));
            // }

            // if (CheckSpawnEnemy() && !b_FlyingObjectAlreadyExist)
            // {
            //     CreateFlyingEnemy();
            // }

            // if (CheckSpawnEnemy() && !b_JumpingObjectAlreadyExist)
            // {
            //     CreationAllowedJumpingEnemy();
            // }
        }

        if (LevelManager.instance.GetLevel() >= 10 && LevelManager.instance.GetLevel() < 15
            && !b_FlyingObjectAlreadyExist)
        {
            if (CheckSpawnEnemy())
            {
                CreateFlyingEnemy();
            }
        }
        else if (LevelManager.instance.GetLevel() >= 15 && LevelManager.instance.GetLevel() < 17
                && (!b_FlyingObjectAlreadyExist || !b_JumpingObjectAlreadyExist))
        {
            if (CheckSpawnEnemy())
            {
                CreateFlyingEnemy();
            }
            else if (CheckSpawnEnemy())
            {
                CreationAllowedJumpingEnemy();
            }   
        }
        else if (LevelManager.instance.GetLevel() >= 17 
                && !b_FlyingObjectAlreadyExist && !b_JumpingObjectAlreadyExist)
        {
            if (CheckSpawnEnemy())
            {
                CreateFlyingEnemy();
                CreationAllowedJumpingEnemy();
            }   
        }

    }

    // Function that check the current level and change the spawn rate regarding this
    // Return true when we have to create the enemy
    private bool CheckSpawnEnemy()
    {
        int i_SpwanRate = UnityEngine.Random.Range(0, 6);
        if (LevelManager.instance.GetLevel() < 15)
        {
            if (i_SpwanRate == 1)
                return true;
            else
                return false;
        }
        else if (LevelManager.instance.GetLevel() >= 15 && LevelManager.instance.GetLevel() < 18 )
        {
            if (i_SpwanRate < 3)
                return true;
            else
                return false;
        }
        else if (LevelManager.instance.GetLevel() >= 18)
        {
            if (i_SpwanRate < 4)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    // Function that instantiate the Flying Object
    // Creation of the Flying object with x+30 than the Camera and a random position on the Y
    private void CreateFlyingEnemy()
    {
        float f_PositionX = GameObject.Find("Main Camera").transform.position.x + 30;
        float f_PositionY = UnityEngine.Random.Range(1f, 10f);

        GameObject go_NewFyingEnemy = Instantiate(go_FlyingEnemyTemplate);
        go_NewFyingEnemy.transform.SetParent(GameObject.Find("Enemies").transform);
        go_NewFyingEnemy.transform.position = new(f_PositionX,f_PositionY,0);

        b_FlyingObjectAlreadyExist = true;
    }

    // Function that trigger the boolean on the PlatformManager that allow the script to call the CreateJumpingEnemy after the creation of the next platform
    private void CreationAllowedJumpingEnemy()
    {
        PlatformManager.instance.SetJumpingObjectCreationAllowed(true);
    }

    // Function that instantiate the Jumping Object
    // Creation of the Jumping Object between the two last platform
    // After finding the position, call the creation Methode to the JumpObject script to add all information needed on the Property
    public void CreateJumpingEnemy()
    {
        GameObject go_PlatformParent = GameObject.Find("PlatformParent");
        int i_indexLastChill = go_PlatformParent.transform.childCount - 1;

        // First, we compute the gap between the X position of the two last platforms
        float f_gap = go_PlatformParent.transform.GetChild(i_indexLastChill).position.x - go_PlatformParent.transform.GetChild(i_indexLastChill-1).position.x;

        // Second, we remove to the previous gap the size of both platform
        f_gap -= go_PlatformParent.transform.GetChild(i_indexLastChill).GetComponent<PlatformProperty>().GetSizePlatform() + go_PlatformParent.transform.GetChild(i_indexLastChill-1).GetComponent<PlatformProperty>().GetSizePlatform();

        // Third, create the starting position of the jumping Object
        float f_PositionX =  go_PlatformParent.transform.GetChild(i_indexLastChill).position.x - go_PlatformParent.transform.GetChild(i_indexLastChill).GetComponent<PlatformProperty>().GetSizePlatform() - (f_gap/2);

        GameObject go_NewJumpingEnemy = RetrieveTemplateJumpingEnemy(f_gap);

        go_NewJumpingEnemy.GetComponent<Transform>().localPosition = new Vector3(f_PositionX, -6, 0);


        b_JumpingObjectAlreadyExist = true;        
    }

    // Function that instantiate the JumpingEnemy regarding the Gap and the current level and also random generation
    private GameObject RetrieveTemplateJumpingEnemy(float f_gap)
    {
        GameObject go_NewJumpingEnemy;

        go_NewJumpingEnemy = Instantiate(go_JumpingEnemyTemplates[0]);
        go_NewJumpingEnemy.transform.SetParent(GameObject.Find("Enemies").transform);

        return go_NewJumpingEnemy;
    }

     // Wait X seconds before allowing a new Check for creation
    IEnumerator TimerBeforeNextCreationCheck(float f_newTimeCheck, string s_EnemyType)
    {
        yield return new WaitForSeconds(f_newTimeCheck);
        if (s_EnemyType == "Flying")
            b_FlyingObjectAlreadyExist = false;
        else if (s_EnemyType == "Jumping")
            b_JumpingObjectAlreadyExist = false;
    }

    
}
