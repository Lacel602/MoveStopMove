﻿using System;
using UnityEngine;

public class EnemyWanderState : BaseEnemyState
{
    private float wanderTimeMax;
    private float wanderTime = 0;
    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        stateManager.DisableAllAnimations();

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

        //Run infinite until time over (check for wall)

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

    private void ResetVariable()
    {
        wanderTime = 0;
    }
}