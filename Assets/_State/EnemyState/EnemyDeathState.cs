using UnityEngine;

public class EnemyDeathState : BaseEnemyState
{
    private float disableDelayTime = 2f;
    private float eslapsedTime = 0f;

    public override void OnStageEnter(EnemyStateManager stateManager)
    {
        stateManager.DisableAllAnimations();
        stateManager.animator.SetBool(AnimationStrings.isDead, true);
    }

    public override void OnStageExit(EnemyStateManager stateManager)
    {
    }

    public override void OnStageUpdate(EnemyStateManager stateManager)
    {
        if (eslapsedTime < disableDelayTime)
        {
            eslapsedTime += Time.deltaTime;
        } else {
            eslapsedTime = 0f;
            stateManager.currentHumanoidTransform.gameObject.SetActive(false);
        }
    }
}