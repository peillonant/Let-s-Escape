using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_LineController : MonoBehaviour
{
    [SerializeField] private Transform[] tr_Points;
    [SerializeField] private GameObject go_CharactLinked;

    public void ActiveLine(GameObject go_ButtonBlock)
    {
        GetComponent<LineRenderer>().enabled = true;
        tr_Points[0] = go_CharactLinked.transform.GetChild(1);
        tr_Points[1] = go_ButtonBlock.transform;
    }

    public void DeactivateLine()
    {
        GetComponent<LineRenderer>().enabled = false;
        tr_Points[0] = go_CharactLinked.transform.GetChild(1);
        tr_Points[1] = go_CharactLinked.transform.GetChild(1);
    }

    private void Update()
    {
        if (GetComponent<LineRenderer>().enabled)
        {
            for (int i = 0; i < tr_Points.Length; i++)
            {
                GetComponent<LineRenderer>().SetPosition(i, tr_Points[i].position);
            }
        }
    }
}
