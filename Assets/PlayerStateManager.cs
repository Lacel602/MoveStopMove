using Assets._State;
using Assets._State.PlayerState;
using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [Header("Component")]
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

    [Header("MovementParameter")]
    [SerializeField]
    public float speed = 5f;

    [SerializeField]
    public float rotationSpeed = 10f;

    [Header("AttackParameter")]
    [SerializeField]
    internal Transform playerTransform;

    [SerializeField]
    internal float attackDelayMax = 1.1f;

    private GameObject weaponList;

    public GameObject[] weapons;

    public GameObject currentWeapon;

    public Weapon currentWeaponScript;

    public float projectileSpeed = 8f;

    public Transform projectileContainer;

    [Header("StateParameter")]
    public bool isJoystickEnable = false;

    public bool isAlive = true;

    public bool isWin = false;

    public bool hasAttacked = false;

    public bool isDance = false;

    #region StateMachine
    public BasePlayerState currentState;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public DeathState deathState = new DeathState();
    public DanceState danceState = new DanceState();
    public AttackState attackState = new AttackState();
    public WinState winStage = new WinState();
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
                    currentWeapon = weapons[i];
                    currentWeaponScript = currentWeapon.GetComponent<Weapon>();
                }
            }
        }
    }
    
    private void LoadComponent()
    {
        inputCanvas = GameObject.Find("InputCanvas").GetComponent<Canvas>();
        variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        characterController = this.transform.parent.GetComponent<CharacterController>();
        animator = this.transform.parent.Find("Character_Optimieze2").GetComponent<Animator>();
        playerAttack = this.transform.Find("AttackRange").GetComponent<PlayerAttack>();
        playerTransform = GameObject.Find("Player").transform;
        weaponList = GameObject.Find("PlayerWeapons");
        projectileContainer = GameObject.Find("ProjectileContainer").transform;
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
