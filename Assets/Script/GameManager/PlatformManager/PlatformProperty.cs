using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProperty : MonoBehaviour
{
    [SerializeField] private int i_NbrCharactOnPlatform = 0;
    [SerializeField] private float f_SizePlatform;
    [SerializeField] private bool b_PlatformIsUsed;

    /*********************************************** ENCAPSULATION FUNCTION ******************************************************/
    public bool GetPlatformIsUsed() { return b_PlatformIsUsed; }
    public float GetSizePlatform() { return f_SizePlatform; }

    /*****************************************************************************************************************************/

    void Update()
    {
        CheckIsPlatformUsed();
        CheckPlatformPosition();
    }

    // Function to check if the platform is far from the Main Camera on X to trigger the destroy
    // First, check if the Platform is the first one on the PlatformParent
    // Computation: Scale.x of the platform + Gap between the platform and the next
    // Then compaire this calcul with the position X of the Camera
    void CheckPlatformPosition()
    {
        if (gameObject.transform.GetSiblingIndex() == 0)
        {

            if (GameObject.Find("Main Camera").GetComponent<Transform>().position.x - gameObject.GetComponent<Transform>().position.x > 50)
            {
                Destroy(gameObject);
            }
        }
    }

    // Function to check if the platform is used by a charact
    void CheckIsPlatformUsed()
    {
        if (i_NbrCharactOnPlatform > 0)
        {
            b_PlatformIsUsed = true;
        }
        else
        {
            b_PlatformIsUsed = false;
        }
    }

    // Function to check when a Character is on the platform
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Charact"))
        {
            i_NbrCharactOnPlatform++;
        }   
    }

    // Function to check when a Character is not anymore on the platform
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Charact"))
        {
            i_NbrCharactOnPlatform--;
        }
    }
}
