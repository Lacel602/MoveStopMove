using UnityEngine;
using Assets.Script;
using Assets;

public class EnemyStateManager : BaseStateManager
{
    [Header("Component")]
    public LayerMask wallLayer;

    [Header("AttackParameter")]

    [SerializeField]
    public float attackDelayMax = 1.1f;

    [Header("StateParameter")]
    public Vector2 idleTime = new Vector2(2f, 3f);

    public Vector2 wanderTime = new Vector2(2f, 4f);

    public bool isWallAhead;

    [SerializeField]
    public float moveSpeed = 2f;

    #region StateMachine
    public BaseEnemyState currentState;
    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyDeathState deathState = new EnemyDeathState();
    public EnemyAttackState attackState = new EnemyAttackState();
    #endregion

    public void Reset()
    {
        this.LoadComponent();
    }

    private void Start()
    {
        //Enemy start on idle
        currentState = wanderState;
        currentState.OnStageEnter(this);

        //Debug.Log("Enemy is in " + currentState);
    }
    private void Update()
    {
        //Update based on stage
        currentState.OnStageUpdate(this);
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        animator = this.transform.parent.Find("GFX").GetComponent<Animator>();
        attackable = this.transform.Find("AttackRange").GetComponent<Attackable>();
        currentWeaponScript = FindChildByName(this.transform.parent, "PlayerWeapons").GetChild(0).GetComponent<Weapon>();
    }

    public void SwitchState(BaseEnemyState newState)
    {
        currentState.OnStageExit(this);
        currentState = newState;

        //Debug.Log("Switch state");
        //Debug.Log("Enemy is in " + currentState);
        newState.OnStageEnter(this);
    }

    private void OnDrawGizmos()
    {
        
    }
}
