using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : BaseState
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


        if (stateManager.isWin)
        {
            stateManager.SwitchState(stateManager.winState);
            return;
        }

        if (stateManager.attackable.HasEnemy)
        {
            //Check if player has attacked or not
            if (!stateManager.hasAttacked)
            {
                //Check if player has ulti or not
                if (!stateManager.hasUlti)
                {
                    stateManager.SwitchState(stateManager.attackState);
                    return;
                } 
                else
                {
                    stateManager.SwitchState(stateManager.ultiState);
                    return;
                }              
            }
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
