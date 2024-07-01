using Assets._State;
using Assets._State.PlayerState;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField]
    internal PlayerAttack playerAttack;

    [SerializeField]
    internal VariableJoystick variableJoystick;

    [SerializeField]
    internal Canvas inputCanvas;

    [SerializeField]
    internal CharacterController characterController;

    [SerializeField]
    internal Animator animator;

    [SerializeField]
    public float speed = 3f;

    [SerializeField]
    public float rotationSpeed = 10f;

    internal bool isJoystickEnable = false;

    public bool isAlive = true;

    public bool isWin = false;

    public bool isAttack = false;

    public bool isDance = false;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        inputCanvas = GameObject.Find("InputCanvas").GetComponent<Canvas>();
        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        characterController = this.transform.parent.GetComponent<CharacterController>();
        animator = this.transform.parent.Find("Character_Optimieze2").GetComponent<Animator>();
        playerAttack = this.transform.Find("AttackRange").GetComponent<PlayerAttack>();
    }

    public BasePlayerState currentState;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public DeathState deathState = new DeathState();
    public DanceState danceState = new DanceState();
    public AttackState attackState = new AttackState();
    public WinState winStage = new WinState();
    public UltiState ultiStage = new UltiState();

    private void Start()
    {
        EnableJoyStick();

        //Player start on idle
        currentState = idleState;
        currentState.OnStageEnter(this);
    }

    private void Update()
    {
        //Update based on stage
        currentState.OnStageUpdate(this);
    }

    public void SwitchState(BasePlayerState newState)
    {
        currentState.OnStageExit(this);
        currentState = newState;
        newState.OnStageEnter(this);
    }

    private void EnableJoyStick()
    {
        isJoystickEnable = true;
        inputCanvas.gameObject.SetActive(true);
    }

    public void DisableAllAnimations()
    {
        foreach (var anim in AnimationStrings.listAnimations)
        {
            animator.SetBool(anim, false);
        }
    }
}
