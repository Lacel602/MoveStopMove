using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField] 
    private Vector3 distance;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        player = GameObject.Find("Player").gameObject;
    }

    private void Start()
    {
        distance = this.transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        this.transform.position = player.transform.position + distance;
    }
}
