using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void OnStageEnter(PlayerStateManager stateManager);

    public abstract void OnStageUpdate(PlayerStateManager stateManager);

    public abstract void OnStageFixedUpdate(PlayerStateManager stateManager);

    public abstract void OnStageExit(PlayerStateManager stateManager);
}
