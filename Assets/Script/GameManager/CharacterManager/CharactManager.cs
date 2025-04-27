using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactManager : MonoBehaviour
{
    public static CharactManager instance;

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
}
