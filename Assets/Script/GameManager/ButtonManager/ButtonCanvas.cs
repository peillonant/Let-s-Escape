using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCanvas : MonoBehaviour
{
    [SerializeField] private GameObject go_ButtonBlock;
    [SerializeField] private GameObject go_CharactLinked;

    [SerializeField] private float f_TimerDespawn = 7;
    [SerializeField] private string s_LetterToClick;
    [SerializeField] private bool b_ToKeep;
    [SerializeField] private KeyCode kc_KeyCodeToClick;

    /*********************************************** ENCAPSULATION FUNCTION ******************************************************/
    public void SetTimerDespawn(float f_newTimerDespawn) {f_TimerDespawn =  f_newTimerDespawn;}
    public string GetLetterToClick() {return s_LetterToClick;}
    public void SetButtonBlock(GameObject go_newButtonBlock) {go_ButtonBlock =go_newButtonBlock;}
    public void SetKeyCodeToClick(KeyCode newKeycode) {kc_KeyCodeToClick = newKeycode;}
    public GameObject GetCharactLinked() {return go_CharactLinked;}
    
    /*****************************************************************************************************************************/

    public void Update()
    {
        if (Input.GetKey(kc_KeyCodeToClick))
        {
            if (!b_ToKeep)
            {
                ButtonManager.instance.CreateNewButtonBlock(gameObject,gameObject.GetComponent<RectTransform>().position);
                
                go_CharactLinked.GetComponent<CharactAnimation>().TriggerSwingFront();


                RemoveDisplayButtonCanvas();
            }
            b_ToKeep = true;
            
        }
        else if (Input.GetKeyUp(kc_KeyCodeToClick))
        {
            RemoveButtonCanvas();
        }
    }

    public void LaunchTimer()
    {
        StartCoroutine(TimerBeforeRemoveIt());
    }

    // Wait X seconds before removing the ButtonCanvas from the screen
    IEnumerator TimerBeforeRemoveIt()
    {
        yield return new WaitForSeconds(f_TimerDespawn);
        if(!b_ToKeep)
        {
            RemoveButtonCanvas();
        }
    }

    // Function called by the Coroutine to Remove the CanvasButton
    private void RemoveButtonCanvas()
    {
        if(go_ButtonBlock != null)
        {
            go_ButtonBlock.GetComponent<ButtonBlock>().RemoveButtonBlock();
            go_CharactLinked.GetComponent<CharactProperties>().GetLineRendererLinked().GetComponent<LR_LineController>().DeactivateLine();
            go_CharactLinked.GetComponent<CharactAnimation>().TriggerReleaseSwing();
        }

        Destroy(gameObject);
        ButtonManager.instance.DecrementI_ButtonCanvas();
    }

    // Function called to remove the display of the ButtonCanvas on the camera when the Block is created
    public void RemoveDisplayButtonCanvas()
    {
        Image image_buttonCanvas = GetComponent<Image>();
        image_buttonCanvas.enabled = false;

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Function to Set which letter need to be called to trigger the Spawn of the Block
    public void SetLetterToClick(string s_newLetterToClick) 
    { 
        s_LetterToClick = s_newLetterToClick;

        UpdateLetterDisplayed();
    }
    
    // Function to update which letter is displayed
    private void UpdateLetterDisplayed()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(s_LetterToClick);
    }

    // Function to set the CharactLinked to the Button and also Add the Material
    public void SetCharactLinked(GameObject new_CharactLinked) 
    { 
        go_CharactLinked = new_CharactLinked;
        gameObject.GetComponent<Image>().material = new_CharactLinked.GetComponent<SkinnedMeshRenderer>().materials[1];    
    }


}
