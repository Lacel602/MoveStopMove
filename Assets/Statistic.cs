using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    [SerializeField]
    public int score = 0;

    [SerializeField]
    public int level = 0;

    [SerializeField]
    private TMP_Text scroreTMP;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        scroreTMP = this.transform.Find("Canvas").GetComponentInChildren<TMP_Text>();
    }

    public void OnKillEnemy()
    {
        //Increased score and level
        score++;
        level++;

        //Reset size of player
        this.transform.localScale *= ConstantStat.increaseSize;

        //Change TMP score text
        scroreTMP.text = score.ToString();
    }
}
