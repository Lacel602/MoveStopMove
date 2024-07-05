using JetBrains.Annotations;
using UnityEngine;

public class EnemyIdleState : BaseEnemyState
{
    private float idleTimeMax;
    private float idleTime = 0;

    private float turnAroundTimeMax = 1;
    private float turnAroundTime = 0;

    private bool hasTurnAround = false;

    Transform currentEnemy;
    Quaternion initialRotation;
    Quaternion targetRotation;

    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        //Disable all running animations
        stateManager.DisableAllAnimations();
        //Turn on idle anim
        stateManager.animator.SetBool(AnimationStrings.isIdle, true);

        idleTimeMax = Random.Range(stateManager.idleTime.x, stateManager.idleTime.y);

        CaculateRotationTarget(stateManager);
    }

    private void CaculateRotationTarget(EnemyStateManager stateManager)
    {
        currentEnemy = stateManager.currentHumanoidTransform;
        initialRotation = currentEnemy.rotation;
        // Generate a random angle within the range
        float randomAngle = Random.Range(-120f, -240f);
        // Calculate target rotation
        targetRotation = initialRotation * Quaternion.Euler(0, randomAngle, 0);
    }

    public override void OnStageExit(EnemyStateManager stateManager)
    {
    }

    public override void OnStageUpdate(EnemyStateManager stateManager)
    {
        if (!stateManager.isAlive)
        {
            this.ResetVariable();
            stateManager.SwitchState(stateManager.deathState);
            return;
        }

        if (stateManager.isWallAhead)
        {
            if (!hasTurnAround)
            {
                TurnEnemyAround(stateManager);
            }
            //Turn around
        }

        if (stateManager.attackable.HasEnemy)
        {
            if (!stateManager.hasAttacked)
            {
                this.ResetVariable();
                stateManager.SwitchState(stateManager.attackState);
                return;
            }
        }

        if (idleTime < idleTimeMax)
        {
            idleTime += Time.deltaTime;
        }
        else
        {
            this.ResetVariable();
            stateManager.SwitchState(stateManager.wanderState);
            return;
        }
    }

    private void TurnEnemyAround(EnemyStateManager stateManager)
    {
        if (turnAroundTime < turnAroundTimeMax)
        {
            currentEnemy.rotation = Quaternion.Slerp(initialRotation, targetRotation, turnAroundTime / turnAroundTimeMax);

            turnAroundTime += Time.deltaTime;
        }
        else
        {
            //Ensure the object reaches the exact target rotation at the end
            currentEnemy.rotation = targetRotation;
            hasTurnAround = true;
            stateManager.isWallAhead = false;
        }
    }

    private void ResetVariable()
    {
        hasTurnAround = false;
        idleTime = 0;
        turnAroundTime = 0;
    }
}