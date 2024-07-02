using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._State
{
    public class WinState : BaseState
    {
        public override void OnStageEnter(PlayerStateManager stateManager)
        {
            stateManager.DisableAllAnimations();
            stateManager.animator.SetBool(AnimationStrings.isWin, true);
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
