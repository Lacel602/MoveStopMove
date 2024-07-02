public abstract class BaseEnemyState
{
    public abstract void OnStageEnter(EnemyStateManager stateManager);

    public abstract void OnStageUpdate(EnemyStateManager stateManager);

    public abstract void OnStageExit(EnemyStateManager stateManager);
}