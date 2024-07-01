using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : BasePlayerState
{
    public override void OnStageEnter(PlayerStateManager stateManager)
    {
        //Disable all running animations
        stateManager.DisableAllAnimations();
        //Turn on idle anim
        stateManager.animator.SetBool(AnimationStrings.isIdle, true);
    }

    public override void OnStageUpdate(PlayerStateManager stateManager)
    {
        //Check player alive
        if (!stateManager.isAlive)
        {
            stateManager.SwitchState(stateManager.deathState);
            return;
        }

        if (stateManager.isAttack)
        {
            stateManager.SwitchState(stateManager.attackState);
            return;
        }

        if (stateManager.isWin)
        {
            stateManager.SwitchState(stateManager.winStage);
            return;
        }

        if (stateManager.isJoystickEnable)
        {
            Vector3 moveDirection = new Vector3(stateManager.variableJoystick.Direction.x, 0f, stateManager.variableJoystick.Direction.y);

            //Check value of square Vector3 of joystick
            if (moveDirection.sqrMagnitude > 0f)
            {
                stateManager.SwitchState(stateManager.moveState);
                return;
            }
        }
    }

    public override void OnStageFixedUpdate(PlayerStateManager stateManager)
    {
    }

    public override void OnStageExit(PlayerStateManager stateManager)
    {
    }


}
