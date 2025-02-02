using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private CameraManager cameraManager;

    [SerializeField]
    private HumanoidManager humanoidManager;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        scroreTMP = this.transform.Find("Canvas").GetComponentInChildren<TMP_Text>();
        cameraManager = GameObject.Find("MainCamera").GetComponent<CameraManager>();
        humanoidManager = this.GetComponent<HumanoidManager>();
    }

    public void OnKillEnemy(Statistic enemyStat)
    {
        //Increased score and level
        score++;

        //Play death efx
        enemyStat.humanoidManager.effectManager.deathEfx.Play();

        if (enemyStat.level >= level)
        {
            level++;
            //Play level up Efx
            humanoidManager.effectManager.levelUpEfx.Play();

            //Increase size of player
            Vector3 newScale = this.transform.localScale * ConstantStat.increaseSize;
            StartCoroutine(ChangeVectorOverTime(this.transform.localScale, newScale, 0.2f));


            //Backward camera if player level up
            if (this.CompareTag("Player"))
            {
                cameraManager.BackwardCamera(level);
            }
        }

        //Change TMP score text
        scroreTMP.text = level.ToString();
    }

    private IEnumerator ChangeVectorOverTime(Vector3 start, Vector3 destination, float t)
    {
        float elapsedTime = 0;

        while (elapsedTime < t)
        {
            // Interpolate between the two vectors
            Vector3 currentVector = Vector3.Lerp(start, destination, elapsedTime / t);

            // Update the value of the localScale
            this.transform.localScale = currentVector;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Yield execution until the next frame
            yield return null;
        }

        // Ensure the final value is set to the target vector
        this.transform.localScale = destination;
    }
}
