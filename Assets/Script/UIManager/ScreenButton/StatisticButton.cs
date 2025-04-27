using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticButton : MonoBehaviour
{
    public void OnClick()
    {
        UIManager.instance.UpdateStatisticScreen();
    }

    public void OnClick_Reset()
    {
        DataPersistence.instance.ResetData();
        UIManager.instance.UpdateStatisticScreen();
    }

    public void OnClick_Back()
    {
        UIManager.instance.BackToMenuFromStat();
    }
}

