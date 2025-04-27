using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_FlyingProperty : MonoBehaviour
{
    [SerializeField] float f_TargetPositionX;
    [SerializeField] float f_speed;
    [SerializeField] Vector3 v3_TargetPosition;
    [SerializeField] GameObject go_WarningDisplayTemplate;
    GameObject go_WarningDisplayLinked;

    public void SetSpeedFlyingEnemy(float f_newSpeed) { f_speed = f_newSpeed; }

    void Start()
    {
        f_TargetPositionX = GameObject.Find("Main Camera").transform.position.x - 50;
        v3_TargetPosition = new(f_TargetPositionX, transform.position.y, 0);
    }
    
    // Manage the movement to the left of the Camera
    // When the Flying Enemy is at the position of the target => Delete the GameObject
    void Update()
    {
        if (transform.position.x > f_TargetPositionX)
            transform.position = Vector3.MoveTowards(transform.position, v3_TargetPosition, f_speed * Time.deltaTime);
        else
            RemoveFlyingEnemy();
    }

    private void RemoveFlyingEnemy()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager_SpawnEnemies>().SetFlyingAlreadyExist(false);
        Destroy(gameObject);
    }
}
