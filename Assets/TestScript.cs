using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    public List<EnemyStateManager> enemyStates = new List<EnemyStateManager>();
    private void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var script = transform.GetChild(i).GetComponentInChildren<EnemyStateManager>();
            if (script != null)
            {
                Debug.Log("Found");
            } else
            {
                Debug.Log("NotFound");
            }

            script.Reset();
            //enemyStates.Add(script);
        }

        //ChangeValue();
    }

    private void ChangeValue()
    {
        foreach (var enemyState in enemyStates)
        {
            Debug.Log("Change");
            enemyState.wanderTime = new Vector2(2f, 5f);
            enemyState.idleTime = new Vector2(0.5f, 3);
        }
    }
}
