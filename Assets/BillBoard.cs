using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField]
    private Transform Cam;
    private void Update()
    {
        transform.LookAt(transform.position + Cam.forward);
    }
}
