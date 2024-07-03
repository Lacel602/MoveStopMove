using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    [SerializeField]
    private int score = 0;

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
        //Increased size
        this.transform.localScale *= 1.1f;

        //Increased score
        score++;

        //Change TMP score text
        scroreTMP.text = score.ToString();
    }
}
