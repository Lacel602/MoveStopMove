using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BasePlayerState
{
    public override void OnStageEnter(PlayerStateManager stateManager)
    {
        stateManager.DisableAllAnimations();
        stateManager.animator.SetBool(AnimationStrings.isIdle, true);
    }

    public override void OnStageExit(PlayerStateManager stateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void OnStageFixedUpdate(PlayerStateManager stateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void OnStageUpdate(PlayerStateManager stateManager)
    {
        throw new System.NotImplementedException();
    }
}
