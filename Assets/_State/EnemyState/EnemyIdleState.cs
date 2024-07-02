using UnityEngine;

public class EnemyIdleState : BaseEnemyState
{
    private float idleTimeMax;
    private float idleTime = 0;
    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        //Disable all running animations
        stateManager.DisableAllAnimations();
        //Turn on idle anim
        stateManager.animator.SetBool(AnimationStrings.isIdle, true);

        idleTimeMax = Random.Range(stateManager.idleTime.x, stateManager.idleTime.y);
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

    private void ResetVariable()
    {
        idleTime = 0;
    }
}