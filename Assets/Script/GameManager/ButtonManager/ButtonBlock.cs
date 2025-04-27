using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    // Function called by the Coroutine to Remove the CanvasButton
    public void RemoveButtonBlock()
    {
        Destroy(gameObject);
    }

    // Function to Update the Local Canvas of the Button Block to display the Key to keep press to have the Block
    public void SetLetterToKeep(string s_newLetter)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(s_newLetter);
    }

}
