using Assets;
using Assets._State;
using Assets._State.PlayerState;
using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerStateManager : BaseStateManager
{
    [Header("Component")]

    [SerializeField]
    internal VariableJoystick variableJoystick;

    [SerializeField]
    internal Canvas inputCanvas;

    [SerializeField]
    internal CharacterController characterController;

    

    [Header("MovementParameter")]
    [SerializeField]
    public float moveSpeed = 5f;

    [SerializeField]
    public float rotationSpeed = 10f;

    [Header("AttackParameter")]
    protected GameObject weaponList;

    [SerializeField]
    public float attackDelayMax = 0.8f;

    public GameObject[] weapons;

    [Header("StateParameter")]
    public bool isJoystickEnable = false;

    public bool isWin = false;

    public bool isDance = false;



    #region StateMachine
    public BaseState currentState;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public DeathState deathState = new DeathState();
    public DanceState danceState = new DanceState();
    public AttackState attackState = new AttackState();
    public WinState winState = new WinState();
    public UltiState ultiStage = new UltiState();
    #endregion

    #region Reset
    private void Reset()
    {
        this.LoadComponent();
        this.LoadWeaponArray();
    }

    private void LoadWeaponArray()
    {
        int numberOfWeapon = weaponList.transform.childCount;
        if (numberOfWeapon > 0)
        {
            weapons = new GameObject[numberOfWeapon];

            for (int i = 0; i < numberOfWeapon; i++)
            {
                weapons[i] = weaponList.transform.GetChild(i).gameObject;

                if (weapons[i].activeSelf)
                {
                    currentWeaponScript = weapons[i].GetComponent<Weapon>();
                }
            }
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        inputCanvas = GameObject.Find("InputCanvas").GetComponent<Canvas>();
        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        characterController = this.transform.parent.GetComponent<CharacterController>();
        weaponList = FindChildByName(this.transform.parent, "PlayerWeapons").gameObject;
    }
    #endregion

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

    public void SwitchState(BaseState newState)
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
}
