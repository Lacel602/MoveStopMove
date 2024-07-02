using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._State
{
    public class MoveState : BaseState
    {
        public override void OnStageEnter(PlayerStateManager stateManager)
        {
            stateManager.DisableAllAnimations();
        }

        public override void OnStageExit(PlayerStateManager stateManager)
        {
            
        }

        public override void OnStageFixedUpdate(PlayerStateManager stateManager)
        {

        }

        public override void OnStageUpdate(PlayerStateManager stateManager)
        {
            //Reset player attack
            if (stateManager.hasAttacked)
            {
                stateManager.hasAttacked = ! stateManager.hasAttacked;
            }

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

            if (stateManager.isJoystickEnable)
            {
                Vector3 moveDirection = new Vector3(stateManager.variableJoystick.Direction.x, 0f, stateManager.variableJoystick.Direction.y);

                //Check value of square Vector3 of joystick
                if (moveDirection.sqrMagnitude <= 0f && !stateManager.hasAttacked)
                {
                    stateManager.SwitchState(stateManager.idleState);
                    return;
                }

                stateManager.characterController.SimpleMove(moveDirection * stateManager.speed);

                stateManager.animator.SetBool(AnimationStrings.isIdle, false);

                Vector3 targetDirection = Vector3.RotateTowards(stateManager.characterController.transform.forward, moveDirection, stateManager.rotationSpeed * Time.deltaTime, 0f);

                stateManager.characterController.transform.rotation = Quaternion.LookRotation(targetDirection);
            }
        }
    }
}
