
using UnityEngine;

public class EnemyChaseState : BaseEnemyState
{
    private float chaseTime = 0f;
    private float chaseTimeMax = 0f;

    private float rotationTime;
    public float rotationTimeMax = 1f;

    private bool hasRotate = false;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private GameObject nearestEnemy;
    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        stateManager.DisableAllAnimations();
        Debug.Log("Enemy start chase state");
        //Get a nearest enemy
        nearestEnemy = EnemySpawnManager.Instance.GetNearestEnemyActive(stateManager.currentHumanoidTransform.gameObject);
        //Lock enemy and chase for an amount of time
        chaseTimeMax = Random.Range(2f ,4f);

        initialRotation = stateManager.currentHumanoidTransform.rotation;

        targetRotation = GetTargetRotateDirection(stateManager, nearestEnemy);
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

        //Check for wall ahead of this enemy
        if (CheckForWallAhead(stateManager))
        {
            //Set wall ahead to true and return to idle
            stateManager.isWallAhead = true;
            this.ResetVariable();
            stateManager.SwitchState(stateManager.idleState);
            return;
        }

        //Rotate this enemy to facing the nearest enemy
        if (!hasRotate)
        {
            RotateOverTime(stateManager.currentHumanoidTransform);
        }
        //Run to the target
        stateManager.currentHumanoidTransform.Translate(Vector3.forward * stateManager.moveSpeed * Time.deltaTime);

        //Return idle when time over
        if (chaseTime < chaseTimeMax)
        {
            chaseTime += Time.deltaTime;
        } 
        else
        {
            this.ResetVariable();
            stateManager.SwitchState(stateManager.idleState);
            return;
        }
    }

    private bool CheckForWallAhead(EnemyStateManager stateManager)
    {
        Vector3 origin = stateManager.currentHumanoidTransform.position;
        Vector3 direction = stateManager.currentHumanoidTransform.forward.normalized;
        //Cast ray
        if (Physics.Raycast(origin, direction, out RaycastHit hit, 5f, stateManager.wallLayer))
        {
            return true;
        }
        return false;
    }

    private void RotateOverTime(Transform currentEnemy)
    {
        if (rotationTime < rotationTimeMax)
        {
            currentEnemy.rotation = Quaternion.Slerp(initialRotation, targetRotation, rotationTime / rotationTimeMax);
            rotationTime += Time.deltaTime;

            //Debug.Log("Enemy rotation: " + currentEnemy.rotation);
        }
        else
        {
            //Ensure the object reaches the exact target rotation at the end
            currentEnemy.rotation = targetRotation;

            //Prevent next rotation
            hasRotate = true;
        }
    }

    private void ResetVariable()
    {
        chaseTime = 0f;
        rotationTime = 0f;
        hasRotate = false;
    }

    private Quaternion GetTargetRotateDirection(EnemyStateManager stateManager, GameObject nearestEnemy)
    {
        // Get the direction vector from this gameobject to the nearest enemy
        Vector3 directionToEnemy = nearestEnemy.transform.position - stateManager.currentHumanoidTransform.position;
        Debug.Log("Direction to enemy" + directionToEnemy);
        // Caculate the target rotation
        Quaternion target = Quaternion.LookRotation(directionToEnemy);

        return target;
    }
}

