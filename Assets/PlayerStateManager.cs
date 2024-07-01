using Assets._State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

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

    private bool isJoystick = false;

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
    }

    public BasePlayerState currentState;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public DeathState deathState = new DeathState();
    public DanceState danceState = new DanceState();
    public AttackState attackState = new AttackState();

    private void Start()
    {
        //Player start on idle
        currentState = idleState;
        currentState.OnStageEnter(this);

        EnableJoyStick();
    }
    private void Update()
    {
        //Update based on stage
        currentState.OnStageUpdate(this);

        if (isJoystick)
        {
            Vector3 moveDirection = new Vector3(variableJoystick.Direction.x, 0f, variableJoystick.Direction.y);

            //Check value vector3 in square
            if (moveDirection.sqrMagnitude <= 0f)
            {
                animator.SetBool(AnimationStrings.isIdle, true);
                return;
            }

            characterController.SimpleMove(moveDirection * speed);

            animator.SetBool(AnimationStrings.isIdle, false);

            Vector3 targetDirection = Vector3.RotateTowards(characterController.transform.forward, moveDirection, rotationSpeed * Time.deltaTime, 0f);

            characterController.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }

    public void SwitchState(BasePlayerState newState)
    {
        currentState.OnStageExit(this);
        currentState = newState;
        currentState.OnStageEnter(this);
    }

    private void EnableJoyStick()
    {
        isJoystick = true;
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
