using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerStateManager stateManager;

    [SerializeField]
    private float radius = 5f;
    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        stateManager = this.transform.parent.GetComponent<PlayerStateManager>();
    }

    private void FixedUpdate()
    {
        FindEnemies(this.transform.position, radius);
    }

    private void FindEnemies(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Debug.Log("FoundEnemy");
            }
        }
    }
}
