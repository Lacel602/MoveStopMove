using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._State
{
    public class AttackState : BaseState
    {
        private float attackAnimationDelayTime = 0f;

        private bool isAttackked = false;

        private float attackDelayTime = 0f;

        private Vector3 enemyPos = Vector3.zero;
        public override void OnStageEnter(PlayerStateManager stateManager)
        {
            //Debug.Log("Attackk!");
            stateManager.DisableAllAnimations();
            if (!stateManager.hasAttacked)
            {
                //Enable attack
                stateManager.animator.SetBool(AnimationStrings.isAttack, true);

                //Set has attacked to true
                stateManager.hasAttacked = !stateManager.hasAttacked;
            }

            //Get original parent and transform of weapon
            

            //Get current enemy position
            if (stateManager.attackable.Enemy != null)
            {
                enemyPos = stateManager.attackable.Enemy.transform.position;
            }
        }

        public override void OnStageExit(PlayerStateManager stateManager)
        {
        }

        public override void OnStageFixedUpdate(PlayerStateManager stateManager)
        {
        }

        public override void OnStageUpdate(PlayerStateManager stateManager)
        {
            if (!stateManager.isAlive)
            {
                this.ResetVariable();
                stateManager.SwitchState(stateManager.deathState);
                return;
            }

            if (stateManager.isWin)
            {
                this.ResetVariable();
                stateManager.SwitchState(stateManager.winState);
                return;
            }

            //Change player rotation to look at enemy
            if (stateManager.attackable.HasEnemy)
            {
                Transform player = stateManager.transform;
                Transform enemy = stateManager.attackable.Enemy.transform;
                Vector3 direction = new Vector3(enemy.position.x, player.position.y, enemy.position.z);
                stateManager.currentHumanoidTransform.LookAt(direction);
            }

            //Check move state
            if (stateManager.isJoystickEnable)
            {
                Vector3 moveDirection = new Vector3(stateManager.variableJoystick.Direction.x, 0f, stateManager.variableJoystick.Direction.y);

                //Check value square Vector3 of joystick
                if (moveDirection.sqrMagnitude > 0f)
                {
                    this.ResetVariable();                
                    stateManager.SwitchState(stateManager.moveState);
                    return;
                }
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
}
