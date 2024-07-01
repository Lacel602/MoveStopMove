using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerStateManager stateManager;

    [SerializeField]
    private GameObject groundCircle;

    [SerializeField]
    private float radius = 5f;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    public bool hasEnemy = false;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        stateManager = this.transform.parent.GetComponent<PlayerStateManager>();
        groundCircle = GameObject.Find("GroundCircle").gameObject;
    }

    private void FixedUpdate()
    {
        enemy = FindEnemies(this.transform.position, radius);

        if (hasEnemy )
        {
            Debug.Log("enemy: " + enemy.name);
        }

    }

    private GameObject FindEnemies(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hasEnemy = true;
                return hitCollider.gameObject;
            }
        }

        hasEnemy = false;
        return null;
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void IncreaseRange()
    {
        radius *= 1.1f;
        groundCircle.transform.localScale = new Vector3(groundCircle.transform.localScale.x * 1.1f, groundCircle.transform.localScale.y * 1.1f, groundCircle.transform.localScale.z);
    }
}
