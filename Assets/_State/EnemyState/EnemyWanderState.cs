using System;
using UnityEngine;

public class EnemyWanderState : BaseEnemyState
{
    private float wanderTimeMax;
    private float wanderTime = 0;

    public float duration = 1f; // Duration of the rotation in seconds
    public float rotationRange = 120f; // Range of rotation in degrees

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float elapsedTime;
    private bool isRotating = false;


    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        stateManager.DisableAllAnimations();

        //Set max wanderTime
        wanderTimeMax = UnityEngine.Random.Range(stateManager.wanderTime.x, stateManager.wanderTime.y);
    }

    public override void OnStageExit(EnemyStateManager stateManager)
    {
    }

    public override void OnStageUpdate(EnemyStateManager stateManager)
    {
        //Reset attack
        if (stateManager.hasAttacked)
        {
            stateManager.hasAttacked = !stateManager.hasAttacked;
        }

        if (stateManager.attackable.HasEnemy)
        {
            this.ResetVariable();
            stateManager.SwitchState(stateManager.attackState);
            return;
        }

        if (!stateManager.isAlive)
        {
            this.ResetVariable();
            stateManager.SwitchState(stateManager.deathState);
            return;
        }

        //Enemy movement here

        //Randomfacing
        RotateOverTime(stateManager.currentEnemy);

        //Run infinite until time over (check for wall)
        stateManager.currentEnemy.transform.Translate(Vector3.forward * stateManager.moveSpeed * Time.deltaTime);

        //Use physic to check for wall 

        //Return to idle when time over
        if (wanderTime < wanderTimeMax)
        {
            wanderTime += Time.deltaTime;
        }
        else
        {
            this.ResetVariable();
            stateManager.SwitchState(stateManager.idleState);
            return;
        }
    }

    private void RotateOverTime(Transform currentEnemy)
    {
        if (!isRotating)
        {
            isRotating = true;
            elapsedTime = 0f;
        }

        if (elapsedTime < duration)
        {
            currentEnemy.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            // Ensure the object reaches the exact target rotation at the end
            currentEnemy.rotation = targetRotation;

            // Reset for potential future rotations
            isRotating = false; 
        }
    }

    private void ResetVariable()
    {
        wanderTime = 0;
        isRotating = false;
    }

    
}