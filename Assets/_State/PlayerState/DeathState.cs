using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._State
{
    public class DeathState : BaseState
    {
        public override void OnStageEnter(PlayerStateManager stateManager)
        {
            stateManager.DisableAllAnimations();
            stateManager.animator.SetBool(AnimationStrings.isDead, true);

            stateManager.attackable.groundCircle.SetActive(false);

            stateManager.currentCollider.enabled = false;

            stateManager.gameOverUI.SetActive(true);
        }

        public override void OnStageExit(PlayerStateManager stateManager)
        {
 
        }

        public override void OnStageFixedUpdate(PlayerStateManager stateManager)
        {

        }

        public override void OnStageUpdate(PlayerStateManager stateManager)
        {

        }
    }
}
