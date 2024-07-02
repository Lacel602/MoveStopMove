using Assets._State.PlayerState;
using Assets._State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Script;

public class EnemyStateManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField]
    public Attackable attackable;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    public Transform currentEnemy;

    [Header("AttackParameter")]

    [SerializeField]
    public float attackDelayMax = 1.1f;

    public Weapon currentWeaponScript;

    public Transform projectileContainer;

    [Header("StateParameter")]
    public Vector2 idleTime = new Vector2(2f, 3f);

    public Vector2 wanderTime = new Vector2(2f, 4f);

    public bool isAlive = true;

    public bool hasAttacked = false;


    #region StateMachine
    public BaseEnemyState currentState;
    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyDeathState deathState = new EnemyDeathState();
    public EnemyAttackState attackState = new EnemyAttackState();
    #endregion

    private void Reset()
    {
        this.LoadComponent();
    }

    private void Start()
    {
        //Enemy start on idle
        currentState = idleState;
        currentState.OnStageEnter(this);
        Debug.Log("Enemy is in " + currentState);
    }
    private void Update()
    {
        //Update based on stage
        currentState.OnStageUpdate(this);
    }
    private void LoadComponent()
    {
        currentEnemy = this.transform.parent;
        animator = this.transform.parent.Find("GFX").GetComponent<Animator>();
        attackable = this.transform.Find("AttackRange").GetComponent<Attackable>();
        projectileContainer = GameObject.Find("ProjectileContainer").transform;
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
    private Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }

            Transform result = FindChildByName(child, name);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public void DisableAllAnimations()
    {
        foreach (var anim in AnimationStrings.listAnimations)
        {
            animator.SetBool(anim, false);
        }
    }
}
