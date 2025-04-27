using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

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

    [SerializeField] private GameObject go_Button_Canvas;
    [SerializeField] private GameObject go_Button_Block;
    [SerializeField] private int i_buttonCanvas = 0;
    string[] s_Alphabet = new string[26] {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
    List <KeyCode> keyCodes = new List <KeyCode> {KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,KeyCode.H,KeyCode.I,KeyCode.J,
                                                    KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,
                                                    KeyCode.U,KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z};

    public void DecrementI_ButtonCanvas() {i_buttonCanvas--;}


    /****************************************************************************/
     /* This function is called by the GameManager every X secondes             */
     /* Check first how many character is on the Game currently lived            */
     /* If more than 1, then check how many button_Canvas is already created    */
     /* If less button than character, then create the button                    */
     /* else do nothing                                                         */
     /***************************************************************************/
    public void StartSpwaning()
    {
        // Check first the number of character lived
        if (GameObject.Find("Character/Alive").transform.childCount > 0)
        {
            int tmp = GameObject.Find("Character/Alive").transform.childCount - i_buttonCanvas;

            if (tmp > 0)
            {
                for (int i = 0; i < tmp; i++) 
                {
                    CreateNewButtonCanvas();
                }
            }
        }
    }

    /****************************************************************************/
     /* This function to create the Button on the Canvas                        */
     /* Find which Area can received the Button_Canvas                          */
     /* Generate a random number to select an Area Available                    */
     /* Then Generate the position randomly on the Area                         */
     /***************************************************************************/
    private void CreateNewButtonCanvas()
    {
        int i_index_AreaAvailable = GenerateIndexAvailable();

        if (i_index_AreaAvailable >= 0)
        {
            InstantiateButtonCanvas(i_index_AreaAvailable, GeneratePosition(), GeneratePosition());
        }
    }

    // Function to generate the Button_Block 
    public void CreateNewButtonBlock(GameObject go_ButtonCanvas, Vector3 v_position)
    {
        GameObject go_Button = Instantiate(go_Button_Block, GameObject.Find("Block").transform);
        go_Button.GetComponent<RectTransform>().localPosition = v_position;
        go_Button.GetComponent<MeshRenderer>().material = go_ButtonCanvas.GetComponent<ButtonCanvas>().GetCharactLinked().GetComponent<SkinnedMeshRenderer>().materials[1];
        go_Button.GetComponent<ButtonBlock>().SetLetterToKeep(go_ButtonCanvas.GetComponent<ButtonCanvas>().GetLetterToClick());

        go_ButtonCanvas.GetComponent<ButtonCanvas>().SetButtonBlock(go_Button);
        go_ButtonCanvas.GetComponent<ButtonCanvas>().GetCharactLinked().GetComponent<CharactProperties>().SetSpringJoint(go_Button);
    }

    // Function to generate the Index Available for the new button
    private int GenerateIndexAvailable()
    {
        int i_index = -1;
        int i_cpt = 0;

        GameObject go_CanvasArea = GameObject.Find("Main Canvas/AreaButton");

        while (i_index == -1 && i_cpt < 50)
        {
            i_index = Random.Range(0, go_CanvasArea.transform.childCount);
            
            if (go_CanvasArea.transform.GetChild(i_index).childCount > 0)
            {
                i_index = -1;
            }

            i_cpt++;
        }

        if (i_index >= 0)
            return i_index;
        else
            return -1;
    }

    // Function to gerenate the Random position of this button on the Area
    private int GeneratePosition()
    {
        return Random.Range(-60, 61);
    }

    // Function to instantiate the Button_Canvas
    private void InstantiateButtonCanvas(int i_index_AreaAvailable, int i_position_x, int i_position_y)
    {
        GameObject go_CanvasArea = GameObject.Find("Main Canvas/AreaButton").transform.GetChild(i_index_AreaAvailable).gameObject;
        GameObject go_Button = Instantiate(go_Button_Canvas, go_CanvasArea.transform);
        go_Button.GetComponent<RectTransform>().localPosition = new Vector3(i_position_x,i_position_y,0);

        go_Button.GetComponent<ButtonCanvas>().SetTimerDespawn(7);

        UpdateLetterToClick(go_Button);

        go_Button.GetComponent<ButtonCanvas>().LaunchTimer();

        LinkButtonToCharact(go_Button);

        i_buttonCanvas++;
    }

    // Function to link the CanvasButton to the Charact
    void LinkButtonToCharact(GameObject go_ButtonCanvas)
    {
        GameObject go_CharactLive = GameObject.Find("Character/Alive");

        // Find the charac that did not have a ButtonCanvas linked
        for (int i = 0; i < go_CharactLive.transform.childCount; i++)
        {
            if (go_CharactLive.transform.GetChild(i).GetComponent<CharactProperties>().GetButtonCanvasLinked() == null)
            {
                // Now we linked the ButtonCanvas Created to the Charact
                go_CharactLive.transform.GetChild(i).GetComponent<CharactProperties>().SetButtonCanvasLinked(go_ButtonCanvas);
                go_ButtonCanvas.GetComponent<ButtonCanvas>().SetCharactLinked(go_CharactLive.transform.GetChild(i).gameObject);

                return;
            }
        }
    }

    // Function to Generate the new LetterToClick
    private void UpdateLetterToClick(GameObject go_Button)
    {
        int i_cpt = 0;
        string s_letter = "!";
        bool b_Continue = false;
        int i_indexAlphabet;

        GameObject go_CanvasArea = GameObject.Find("Main Canvas/AreaButton");

        do
        {           
            i_indexAlphabet = Random.Range (0,26);
            string s_tmp = s_Alphabet[i_indexAlphabet];

            for (int i = 0; i < go_CanvasArea.transform.childCount; i++)
            {
                if (go_CanvasArea.transform.GetChild(i).childCount > 0)
                {
                    if (go_CanvasArea.transform.GetChild(i).GetChild(0).GetComponent<ButtonCanvas>().GetLetterToClick() == s_tmp)
                    {
                        b_Continue = true;
                    }
                    else
                        b_Continue = false;
                }   
            }

            if (b_Continue == true)
                i_cpt++;
            else
                s_letter = s_tmp;

        }while (i_cpt < 50 && b_Continue);

        if (i_cpt == 50 || s_letter == "!")
        {
            Debug.Log("The counter for find the letter has been exceed => " + i_cpt);

            go_Button.GetComponent<ButtonCanvas>().SetLetterToClick("!");
            go_Button.GetComponent<ButtonCanvas>().SetKeyCodeToClick(KeyCode.Exclaim);
            go_Button.name = "Button_!";
        }
        else
        {
            go_Button.GetComponent<ButtonCanvas>().SetLetterToClick(s_letter);
            go_Button.GetComponent<ButtonCanvas>().SetKeyCodeToClick(keyCodes[i_indexAlphabet]);
            go_Button.name = "Button_" + s_letter;
        }

        
    }
}
