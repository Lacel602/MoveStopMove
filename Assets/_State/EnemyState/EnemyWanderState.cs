using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyWanderState : BaseEnemyState
{
    private float wanderTimeMax;
    private float wanderTime = 0;

    public float rotationTimeMax = 1f; // Duration of the rotation in seconds
    public float rotationRange = 180f; // Range of rotation in degrees

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float elapsedTime;
    private bool hasRotate = false;


    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        stateManager.DisableAllAnimations();

        //Set max wanderTime
        wanderTimeMax = UnityEngine.Random.Range(stateManager.wanderTime.x, stateManager.wanderTime.y);

        GetTargetRotateDirection(stateManager);
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
            this.ResetVariable();
            stateManager.SwitchState(stateManager.attackState);
            return;
        }

        //Randomfacing
        if (!hasRotate)
        {
            RotateOverTime(stateManager.currentHumanoidTransform);
        }

        //Run infinite until time over (check for wall)
        stateManager.currentHumanoidTransform.Translate(Vector3.forward * stateManager.moveSpeed * Time.deltaTime);

        //Use physic.Raycast to check for wall 
        //if (CheckForWallAhead(stateManager))
        //{
        //    //Turn around
        //    TurnEnenmyAround(stateManager);
        //}

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

    private void TurnEnenmyAround(EnemyStateManager stateManager)
    {
        Debug.Log("TurnAround");
    }

    Vector3 origin;
    Vector3 direction;
    int layerIndex;

    private bool CheckForWallAhead(EnemyStateManager stateManager)
    {
        origin = stateManager.currentHumanoidTransform.position;
        direction = stateManager.currentHumanoidTransform.forward;
        //Get wall layer
        layerIndex = LayerMask.NameToLayer("Ground&Wall");
        //Cast ray
        if (Physics.Raycast(origin, direction, out RaycastHit hit, 10f, layerIndex))
        {
            return true;
        }
        return false;
    }

    private void GetTargetRotateDirection(EnemyStateManager stateManager)
    {
        initialRotation = stateManager.currentHumanoidTransform.rotation;
        float randomAngle = Random.Range(-rotationRange / 2f, rotationRange / 2f); // Generate a random angle within the range
        targetRotation = initialRotation * Quaternion.Euler(0, randomAngle, 0); // Calculate target rotation

    }

    private void RotateOverTime(Transform currentEnemy)
    {
        //if (!isRotating)
        //{
        //    isRotating = true;
        //    elapsedTime = 0f;
        //}
        //else
        if (elapsedTime < rotationTimeMax)
        {
            currentEnemy.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationTimeMax);

            elapsedTime += Time.deltaTime;

            //Debug.Log("Enemy rotation: " + currentEnemy.rotation);
        }
        else
        {
            //Ensure the object reaches the exact target rotation at the end
            currentEnemy.rotation = targetRotation;

            //Reset for potential future rotations
            hasRotate = true;
        }
    }

    private void ResetVariable()
    {
        wanderTime = 0;
        //isRotating = false;
        hasRotate = false;
        elapsedTime = 0;
    }
}