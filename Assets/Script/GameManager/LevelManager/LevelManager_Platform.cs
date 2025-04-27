using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager_Platform : MonoBehaviour
{
    [SerializeField] private List <GameObject> go_Platforms = new List<GameObject>();
    [SerializeField] private List <Material> materials = new List<Material>();

    // Function that send which Platform will be created by the Spawner regarding the current level
    public GameObject GetPlatformTemplate()
    {
        int i_tmp_level = LevelManager.instance.GetLevel();
        int i_RandomNumber;
        
        if (i_tmp_level >= 1 && i_tmp_level <= 5)
        {
            // Spawn just Large Platform
            return go_Platforms[0];
        }
        else if ( i_tmp_level == 6)
        {
            // Spawn Large or Medium Platform (Spawn rate 2/3 - 1/3)
            i_RandomNumber = UnityEngine.Random.Range(1,4);

            if (i_RandomNumber == 1 || i_RandomNumber == 2) 
                return go_Platforms[0];
            else
                return go_Platforms[1];
        }   
        else if (i_tmp_level == 7)
        {   
            // Spawn Large or Medium Platform (Spawn rate 1/3 - 2/3)
            i_RandomNumber = UnityEngine.Random.Range(1,4);

            if (i_RandomNumber == 1) 
                return go_Platforms[0];
            else
                return go_Platforms[1];
        }
        else if (i_tmp_level >= 8 && i_tmp_level <= 10)
        {
            // Spawn Large or Medium Platform (Spawn rate 1/4 - 3/4)
            i_RandomNumber = UnityEngine.Random.Range(1,5);

            if (i_RandomNumber == 1) 
                return go_Platforms[0];
            else
                return go_Platforms[1];
        }
        else if (i_tmp_level == 11)
        {
            // Spawn Large or Medium or Small Platform (Spawn rate 1/4 - 2/4 - 1/4)
            i_RandomNumber = UnityEngine.Random.Range(1,5);

            if (i_RandomNumber == 1) 
                return go_Platforms[0];
            else if (i_RandomNumber == 4)
                return go_Platforms[2];
            else
                return go_Platforms[1];
        }
        else if (i_tmp_level == 12)
        {
            // Spawn Large or Medium or Small Platform (Spawn rate 2/10 - 5/10 - 3/10)
            i_RandomNumber = UnityEngine.Random.Range(1,11);

            if (i_RandomNumber == 1 || i_RandomNumber == 2) 
                return go_Platforms[0];
            else if (i_RandomNumber >= 3 && i_RandomNumber <= 7) 
                return go_Platforms[1];
            else
                return go_Platforms[2];
        }
        else if (i_tmp_level == 13)
        {
            // Spawn Large or Medium or Small Platform (Spawn rate  1/10 - 5/10 - 4/10)
            i_RandomNumber = UnityEngine.Random.Range(1,11);

            if (i_RandomNumber == 1) 
                return go_Platforms[0];
            else if (i_RandomNumber >= 2 && i_RandomNumber <= 6) 
                return go_Platforms[1];
            else
                return go_Platforms[2];
        }
        else if (i_tmp_level == 14 || i_tmp_level == 15)
        {
            // Spawn Medium or Small or ExtraSmall Platform (Spawn rate  5/10 - 4/10 - 1/10)
            i_RandomNumber = UnityEngine.Random.Range(1,11);

            if (i_RandomNumber >= 1 && i_RandomNumber <= 5) 
                return go_Platforms[1];
            else if (i_RandomNumber >= 6 && i_RandomNumber <= 9) 
                return go_Platforms[2];
            else
                return go_Platforms[3];
        }
        else if (i_tmp_level == 16 || i_tmp_level == 17)
        {
            // Spawn Medium or Small or ExtraSmall Platform (Spawn rate  3/10 - 5/10 - 2/10)
            i_RandomNumber = UnityEngine.Random.Range(1,11);

            if (i_RandomNumber >= 1 && i_RandomNumber <= 3) 
                return go_Platforms[1];
            else if (i_RandomNumber >= 4 && i_RandomNumber <= 8) 
                return go_Platforms[2];
            else
                return go_Platforms[3];
        }
        else if (i_tmp_level == 18)
        {
            // Spawn Medium or Small or ExtraSmall Platform (Spawn rate 1/10 - 6/10 - 3/10)
            i_RandomNumber = UnityEngine.Random.Range(1,11);

            if (i_RandomNumber == 1) 
                return go_Platforms[1];
            else if (i_RandomNumber >= 2 && i_RandomNumber <= 7) 
                return go_Platforms[2];
            else
                return go_Platforms[3];
        }
        else if (i_tmp_level == 19)
        {
            // Spawn Small or ExtraSmall Platform (Spawn rate  6/10 - 4/10)
            i_RandomNumber = UnityEngine.Random.Range(1,11);

            if (i_RandomNumber >= 1 && i_RandomNumber <= 6) 
                return go_Platforms[2];
            else
                return go_Platforms[3];
        }
        else if (i_tmp_level == 20)
        {
            // Spawn Small or ExtraSmall Platform (Spawn rate  2/10 - 8/10)
            i_RandomNumber = UnityEngine.Random.Range(1,11);

            if (i_RandomNumber == 1 || i_RandomNumber == 2) 
                return go_Platforms[2];
            else
                return go_Platforms[3];
        }
        else
        {
            // Spawn Large Platform
            return go_Platforms[0];
        }
    }

    // Function that send which Gap need to be used to spawn platform regarding the current level
    public float GetPlatformGap()
    {
        int i_tmp_level = LevelManager.instance.GetLevel();
        
        if (i_tmp_level == 1)
        {
            return 5f;
        }
        else if (i_tmp_level == 2)
        {
            return UnityEngine.Random.Range(5f, 7.5f);
        }
        else if (i_tmp_level == 3)
        {
            return UnityEngine.Random.Range(5f, 10f);
        }
        else
        {
            return UnityEngine.Random.Range(5f, 15f);
        }
    }

    // Function that send back the Materials to be used on the new platform regarding the current level
    public Material GetPlatformMaterial()
    {
        int i_tmp_level = LevelManager.instance.GetLevel() % 4;

        if (i_tmp_level == 1)
        {
            return materials[0];
        }
        else if (i_tmp_level == 2)
        {
            return materials[1];
        }
        else if (i_tmp_level == 3)
        {
            return materials[2];
        }
        else
        {
            return materials[3];
        }
    }
}
