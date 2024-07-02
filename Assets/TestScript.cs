using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 2f);
    }

}
