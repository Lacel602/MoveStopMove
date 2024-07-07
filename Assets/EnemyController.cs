using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public Statistic stat;
    [SerializeField]   
    public EnemyStateManager stateManager;
    [SerializeField]
    public EnemyAttackable attackable;
    [SerializeField]
    public TMP_Text scoreText;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        stat = this.GetComponent<Statistic>();
        stateManager = this.GetComponentInChildren<EnemyStateManager>();
        attackable = this.GetComponentInChildren<EnemyAttackable>();
        scoreText = this.transform.Find("Canvas/Score").GetComponent<TMP_Text>();
    }
}
