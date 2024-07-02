﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._State
{
    public class AttackState : BasePlayerState
    {
        private float attackAnimationDelayCrr = 0f;


        private float attackDelayCrr = 0f;
        private Vector3 enemyPos = Vector3.zero;
        public override void OnStageEnter(PlayerStateManager stateManager)
        {
            //Debug.Log("Attackk!");
            stateManager.DisableAllAnimations();
            if (!stateManager.hasAttacked)
            {
                Debug.Log("Attackk!");
                //Enable attack
                stateManager.animator.SetBool(AnimationStrings.isAttack, true);

                //Set has attacked to true
                stateManager.hasAttacked = !stateManager.hasAttacked;
            }

            //Get original parent and transform of weapon
            

            //Get current enemy position
            if (stateManager.playerAttack.Enemy != null)
            {
                enemyPos = stateManager.playerAttack.Enemy.transform.position;
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
            //Change player rotation to look at enemy
            if (stateManager.playerAttack.HasEnemy)
            {
                Transform player = stateManager.transform;
                Transform enemy = stateManager.playerAttack.Enemy.transform;
                Vector3 direction = new Vector3(enemy.position.x, player.position.y, enemy.position.z);
                stateManager.playerTransform.LookAt(direction);
            }

            if (stateManager.isJoystickEnable)
            {
                Vector3 moveDirection = new Vector3(stateManager.variableJoystick.Direction.x, 0f, stateManager.variableJoystick.Direction.y);

                //Check value of square Vector3 of joystick
                if (moveDirection.sqrMagnitude > 0f)
                {
                    attackAnimationDelayCrr = 0;
                    stateManager.SwitchState(stateManager.moveState);
                    return;
                }
            }

            if (stateManager.hasAttacked)
            {
                if (attackDelayCrr < stateManager.attackDelayMax / 3)
                {
                    attackDelayCrr += Time.deltaTime;
                }
                else
                {
                    if (enemyPos != Vector3.zero)
                    {
                        stateManager.currentWeaponScript.isThrowing = true;
                        stateManager.currentWeaponScript.enemyPos = this.enemyPos;
                    }
                }

                if (attackAnimationDelayCrr >= stateManager.attackDelayMax)
                {
                    attackDelayCrr = 0;
                    attackAnimationDelayCrr = 0;
                    stateManager.SwitchState(stateManager.idleState);
                    return;
                }
                else
                {
                    attackAnimationDelayCrr += Time.deltaTime;
                }
            }
        }

        private void ThrowWeapons(PlayerStateManager stateManager, Vector3 enemyPos)
        {
            //stateManager.currentWeapon.transform.parent = stateManager.projectileContainer;
            //Vector3 targetDirection = new Vector3(enemyPos.x, stateManager.currentWeapon.transform.position.y, enemyPos.z);
            //stateManager.currentWeapon.transform.position = Vector3.MoveTowards(stateManager.currentWeapon.transform.position, enemyPos, stateManager.projectileSpeed);

            //if (Vector3.Distance(stateManager.currentWeapon.transform.position, targetDirection) < 0.1f)
            //{
            //    stateManager.currentWeapon.transform.parent = originParent;
            //    stateManager.currentWeapon.transform.position = originTransform.position;
            //}
        }
    }
}
