using UnityEngine;
using Assets.Script;
using Assets;
using System;

public class EnemyStateManager : BaseStateManager
{
    [Header("Component")]
    [SerializeField]
    public LayerMask wallLayer;

    [SerializeField]
    private SkinnedMeshRenderer skinMeshRenderer;

    [Header("AttackParameter")]

    [SerializeField]
    public float attackDelayMax = 1.1f;

    [Header("StateParameter")]
    public Vector2 idleTime = new Vector2(2f, 3f);

    public Vector2 wanderTime = new Vector2(2f, 4f);

    public bool isWallAhead = false;

    [SerializeField]
    public float moveSpeed = 2f;

    #region StateMachine
    public BaseEnemyState currentState;
    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyDeathState deathState = new EnemyDeathState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    #endregion

    public void Reset()
    {
        this.LoadComponent();
    }

    private void Start()
    {
        //Random player color
        this.RandomPlayerColor();

        //Enemy start on wander
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
        skinMeshRenderer = this.transform.parent.Find("GFX/initialShadingGroup1").GetComponent<SkinnedMeshRenderer>();
    }

    private void RandomPlayerColor()
    {
        Debug.Log("Random Player Color");
        Color newColor = skinMeshRenderer.sharedMaterial.color;
        //newColor.r = UnityEngine.Random.Range(0, 256);
        //newColor.g = UnityEngine.Random.Range(0, 256);
        //newColor.b = UnityEngine.Random.Range(0, 256);
        //newColor.a = 0.2f;
        skinMeshRenderer.sharedMaterial.color = newColor;
    }

    public void SwitchState(BaseEnemyState newState)
    {
        currentState.OnStageExit(this);
        currentState = newState;

        //Debug.Log("Switch state");
        //Debug.Log("Enemy is in " + currentState);
        newState.OnStageEnter(this);
    }
}
