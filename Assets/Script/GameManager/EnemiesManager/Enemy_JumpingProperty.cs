using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_JumpingProperty : MonoBehaviour
{
    [SerializeField] private bool b_isJumping;

    [SerializeField] private float f_JumpingRate = 2;

    public void SetJumpingRate(float f_newJumpingRate) { f_JumpingRate = f_newJumpingRate;}

    // Update is called once per frame
    void Update()
    {
        // First condition, when the Camera is near the Jumping Object, trigger the Jump
        if (transform.position.x - GameObject.Find("Main Camera").transform.position.x < 15 && !b_isJumping)
        {
            TriggerJump();
        }

        if (transform.position.y < -9 && b_isJumping)
        {
            TriggerStopJump();
        } 
    }

    // Function that trigger the jump
    // First, Remove the boolean is Kinematic and add the Use Gravity
    // Second, Apply the the AddForce impulse to have the Jumping effect
    // Thrid, change the bool isJumping to avoid to trigger too many time the jump
    private void TriggerJump()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;

        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 1750, ForceMode.Impulse);

        b_isJumping = true;
    }


    // Function that trigger the stop Jump
    // First, Remove the Use Gravity and put back the is Kinematic
    // Second, change the bool isJumping to allow to trigger the jump again after few seconds
    private void TriggerStopJump()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        gameObject.transform.position = new Vector3(transform.position.x, -9f, transform.position.z);

        StartCoroutine(TimerBeforeNextJump());
    }

    IEnumerator TimerBeforeNextJump()
    {
        yield return new WaitForSeconds(f_JumpingRate);
        b_isJumping = false;
    }

}
