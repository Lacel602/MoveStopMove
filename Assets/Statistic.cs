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

    [SerializeField]
    private CameraController cameraCtl;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        scroreTMP = this.transform.Find("Canvas").GetComponentInChildren<TMP_Text>();
        cameraCtl = GameObject.Find("MainCamera").GetComponent<CameraController>();
    }

    public void OnKillEnemy(Statistic enemyStat)
    {
        //Increased score and level
        score++;

        if (enemyStat.level >= level)
        {
            level++;

            //Increase size of player
            this.transform.localScale *= ConstantStat.increaseSize;

            if (this.CompareTag("Player"))
            {
                cameraCtl.BackwardCamera(level);
            }
        } 

        //Change TMP score text
        scroreTMP.text = level.ToString();
    }
}
