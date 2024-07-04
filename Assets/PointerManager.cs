using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyPointer> enemyPointers = new List<EnemyPointer>();

    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    private void Reset()
    {
        this.LoadComponent();
    }

    private void Start()
    {
        int i = 0;
        foreach (EnemyPointer pointer in this.enemyPointers)
        {
            pointer.target = enemies[i];
            i++;
        }
    }

    private void LoadComponent()
    {
        GetListPointer();
        GetListEnemyActive();
    }

    private void GetListEnemyActive()
    {
        GameObject enemyContainer = GameObject.Find("ActiveEnemy");

        for (int i = 0; i < enemyContainer.transform.childCount; i++)
        {
            GameObject enemy = enemyContainer.transform.GetChild(i).gameObject;
            enemies.Add(enemy);
        }
    }

    private void GetListPointer()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var pointer = this.transform.GetChild(i).GetComponent<EnemyPointer>();
            enemyPointers.Add(pointer);
        }
    }
}
