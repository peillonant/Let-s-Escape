using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

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

    [SerializeField] private Vector3 v3_newPosition;
    [SerializeField] private float f_speed;

    void Start()
    {
        v3_newPosition = transform.localPosition;
    }

    void Update()
    {
        CameraMotion();
        transform.position = Vector3.MoveTowards(transform.position, v3_newPosition, f_speed * Time.deltaTime);
    }

    // Function to move the Camera to the right by following the charact the farest from the camera
    void CameraMotion()
    {      
        GameObject go_CharactFarest = FindFarthestCharacter();

        if (go_CharactFarest != null)
            GenerateNewTargetPositonCamera(go_CharactFarest);
    }
    
    // Function to find the Charact farthest to the left of the camera
    GameObject FindFarthestCharacter()
    {
        GameObject go_CharactLive = GameObject.Find("Character/Alive");

        if (go_CharactLive.transform.childCount > 0)
        {
            GameObject go_FarthestCharact = go_CharactLive.transform.GetChild(0).gameObject;
            float f_DistanceX = go_FarthestCharact.transform.position.x - gameObject.transform.position.x;


            if (go_CharactLive.transform.childCount > 1)
            {
                for (int i = 1; i < go_CharactLive.transform.childCount; i++)
                {
                    float f_tmp_DistanceX = go_CharactLive.transform.GetChild(i).position.x - gameObject.transform.position.x;

                    if (f_tmp_DistanceX < f_DistanceX)
                    {
                        f_DistanceX = f_tmp_DistanceX;
                        go_FarthestCharact = go_CharactLive.transform.GetChild(1).gameObject;
                    }
                }
            }
        
            return go_FarthestCharact;
        }
        else
            return null;
    }

    // Function to update the Position that Camera has to target
    void GenerateNewTargetPositonCamera(GameObject go_CharactFarest)
    {
        Vector3 v3_tmpPosition = new(go_CharactFarest.transform.position.x + 10,transform.position.y,transform.position.z);

        if (v3_newPosition == v3_tmpPosition)
        {
            return;
        }
        else
        {
            v3_newPosition = v3_tmpPosition;
        }
    }
}