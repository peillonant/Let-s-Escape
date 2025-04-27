using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharactProperties : MonoBehaviour
{
    private GameObject go_ButtonCanvasLinked;
    private GameObject go_ButtonBlockLinked;
    [SerializeField] private bool b_CharactIsOnPlatform;
    [SerializeField] private GameObject go_LineRendererLinked;


    /*********************************************** ENCAPSULATION FUNCTION ******************************************************/
    public GameObject GetButtonCanvasLinked() { return go_ButtonCanvasLinked; }
    public GameObject GetLineRendererLinked() { return go_LineRendererLinked;}
    public void SetButtonCanvasLinked(GameObject new_ButtonCanvasLinked) { go_ButtonCanvasLinked = new_ButtonCanvasLinked; }
    public bool GetCharactIsOnPlatform() { return b_CharactIsOnPlatform; }
    public void SetCharactIsOnPlatform( bool b_newCharactIsOnPlatform) { b_CharactIsOnPlatform = b_newCharactIsOnPlatform;}
    public void SetButtonBlockLinked(GameObject new_ButtonBlockLinked) { go_ButtonBlockLinked = new_ButtonBlockLinked; }
    
    /*****************************************************************************************************************************/

    void Update()
    {
        if (gameObject.transform.parent.name == "Alive" && GameManager.instance.GetGameStart())
        {
            if (gameObject.transform.position.y < -8)
            {
                CharactDeath();
            }

            // Condition to generate the jump for the character
            if (b_CharactIsOnPlatform && Input.GetKeyDown(KeyCode.Space))
            {
                CharactJump();
            }
        }
    }

    // Function that put the Charact to the Death folder
    void CharactDeath()
    {
        gameObject.transform.SetParent(transform.parent.parent.GetChild(1), false);
        gameObject.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.position = new(0,0,0);
        gameObject.SetActive(false);
    }

    // Function to create the Jumping effect
    void CharactJump()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500, ForceMode.Impulse);
        gameObject.GetComponent<CharactAnimation>().TriggerJump();
    }

    // Function to link the Block to the Character
    public void SetSpringJoint(GameObject go_Button)
    {
        gameObject.GetComponent<SpringJoint>().connectedBody = go_Button.GetComponent<Rigidbody>();

        go_LineRendererLinked.GetComponent<LR_LineController>().ActiveLine(go_Button);

        if (gameObject.GetComponent<SpringJoint>().autoConfigureConnectedAnchor)
            gameObject.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
    }

    // Function to check when a Character is on the platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            b_CharactIsOnPlatform = true;
            gameObject.GetComponent<CharactAnimation>().TriggerCushion();
        }    

        if (gameObject.transform.parent.name == "Unlocked" && collision.gameObject.CompareTag("Charact"))
        {
            UnlockNewCharact();
        }
    }

    // Function to check when a Character is not anymore on the platform
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            b_CharactIsOnPlatform = false;
        }    
    }


    // Function that Unlock the new Charact
    void UnlockNewCharact()
    {
        // First, change the parent to pass it on Alive folder
        gameObject.transform.SetParent(GameObject.Find("Character/Alive").transform);

        //EnableSetJoint();

        // Now destroy the block that maintain the charact unlocked
        Destroy(go_ButtonBlockLinked);
    }

    // Function that Disable Set Joint Component
    void EnableSetJoint()
    {
        SpringJoint sp_Component = gameObject.AddComponent(typeof(SpringJoint)) as SpringJoint;
        sp_Component.spring = 150;
        sp_Component.damper = 10;
    }
}
