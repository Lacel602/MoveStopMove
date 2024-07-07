using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField]
    private Transform Cam;
    private void Update()
    {
        RotateBillBoard();  
    }

    private void RotateBillBoard()
    {
        transform.LookAt(transform.position + Cam.forward);
    }
}
