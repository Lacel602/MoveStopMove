using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Attackable : MonoBehaviour
{
    [SerializeField]
    private PlayerStateManager stateManager;

    [SerializeField]
    private GameObject groundCircle;

    [SerializeField]
    private float radius = 5f;
    public float Radius => radius;

    [SerializeField]
    private GameObject enemy;
    public GameObject Enemy => enemy;

    private GameObject oldEnemy;

    [SerializeField]
    private bool hasEnemy = false;

    public bool HasEnemy => hasEnemy;

    [SerializeField]
    private GameObject targetCircle;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        stateManager = this.transform.parent.GetComponent<PlayerStateManager>();
        groundCircle = GameObject.Find("GroundCircle").gameObject;
        targetCircle = GameObject.Find("TargetCircle").gameObject;
    }

    private void FixedUpdate()
    {
        enemy = FindEnemies(this.transform.position, radius);

        if (hasEnemy)
        {
            targetCircle.SetActive(true);
            float mutiply = 1f;
            if (enemy.transform.localScale.x > enemy.transform.localScale.y)
            {
                mutiply = enemy.transform.localScale.x;
            }
            else
            {
                mutiply = enemy.transform.localScale.y;
            }
            targetCircle.transform.localScale = new Vector3(mutiply, mutiply, targetCircle.transform.localScale.z);
            targetCircle.transform.position = new Vector3(enemy.transform.position.x, targetCircle.transform.position.y, enemy.transform.position.z);
        }
        else
        {
            targetCircle.SetActive(false);
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
                if (oldEnemy == null)
                {
                    oldEnemy = hitCollider.gameObject;
                    return hitCollider.gameObject;
                }
                else
                {
                    if (hitCollider.gameObject == oldEnemy)
                    {
                        return oldEnemy;
                    }
                }
            }
        }

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                return hitCollider.gameObject;
            }        
        }

        hasEnemy = false;
        oldEnemy = null;
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
