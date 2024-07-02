using UnityEngine;

public class EnemyAttackState : BaseEnemyState
{
    private float attackAnimationDelayTime = 0f;

    private bool isAttackked = false;

    private float attackDelayTime = 0f;

    private Vector3 enemyPos = Vector3.zero;
    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        Debug.Log("Enemy Attack!");
        stateManager.DisableAllAnimations();

        if (!stateManager.hasAttacked)
        {
            //Enable attack
            stateManager.animator.SetBool(AnimationStrings.isAttack, true);

            //Need fixed for enemy
            //Set has attacked to true
            stateManager.hasAttacked = !stateManager.hasAttacked;
        }

        //Get enemy position at present
        if (stateManager.attackable.Enemy != null)
        {
            enemyPos = stateManager.attackable.Enemy.transform.position;
        }
    }

    public override void OnStageExit(EnemyStateManager stateManager)
    {
    }

    public override void OnStageUpdate(EnemyStateManager stateManager)
    {
        if (!stateManager.isAlive)
        {
            //this.ResetVariable();
            stateManager.SwitchState(stateManager.deathState);
            return;
        }

        //Change rotation to look at enemy
        if (stateManager.attackable.HasEnemy)
        {
            Transform player = stateManager.transform;
            Transform enemy = stateManager.attackable.Enemy.transform;
            Vector3 direction = new Vector3(enemy.position.x, player.position.y, enemy.position.z);
            stateManager.currentEnemy.LookAt(direction);
        }

        if (stateManager.hasAttacked)
        {
            //Check if attack animation has played for a amount of time before throw weapon
            if (attackDelayTime < stateManager.attackDelayMax / 4)
            {
                attackDelayTime += Time.deltaTime;
            }
            else
            {
                if (enemyPos != Vector3.zero && !isAttackked)
                {
                    stateManager.currentWeaponScript.isThrowing = true;
                    stateManager.currentWeaponScript.enemyPos = this.enemyPos;
                    isAttackked = true;
                }
            }

            //Ensure attack animation done before return to idle state
            if (attackAnimationDelayTime >= stateManager.attackDelayMax)
            {
                ResetVariable();
                stateManager.SwitchState(stateManager.idleState);
                return;
            }
            else
            {
                attackAnimationDelayTime += Time.deltaTime;
            }
        }
    }

    private void ResetVariable()
    {
        attackDelayTime = 0;
        attackAnimationDelayTime = 0;
        isAttackked = false;
    }
}