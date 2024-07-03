using Assets;
using Assets.Script;
using System;
using UnityEngine;

public class HammerWeapon : Weapon
{
    private bool isRotate = false;

    private void Reset()
    {
        this.LoadComponent();

    }
    private void Update()
    {
        RotateAndThrowWeapon();
    }

    private void LoadComponent()
    {
        projectileContainer = GameObject.Find("ProjectileContainer").transform;
        originParent = this.transform.parent;
        originPosition = this.transform.localPosition;
        originRotation = this.transform.localRotation;
        originScale = this.transform.localScale;
        projectileSpeed = 6f;
        projectileMaxFlyTime = 1.5f;
        currentHumanoid = GetParent(this.transform, 12);
    }

    private GameObject GetParent(Transform transform, int count)
    {
        Transform result = transform;
        while (count > 0)
        {
            result = result.parent;
            count--;
        }

        return result.gameObject;
    }

    private void RotateAndThrowWeapon()
    {
        //Debug.Log(isThrowing);

        if (isThrowing)
        {
            GetStartPosition();

            if (!isRotate)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(-90, 10, 0));
                isRotate = true;
            }
            this.ThrowWeapon(enemyPos);
        }
    }

    private void GetStartPosition()
    {
        if (!getStartPos)
        {
            getStartPos = !getStartPos;
            startWeaponWorldPos = this.transform.position;
        }
    }

    public override void ThrowWeapon(Vector3 enemyPos)
    {
        //Set weapon parent outside player
        this.transform.parent = projectileContainer;

        //Get throwing destination
        Vector3 destination = new Vector3(enemyPos.x, transform.position.y, enemyPos.z);

        //Move by move toward
        //this.transform.position = Vector3.MoveTowards(this.transform.position, destination, projectileSpeed * Time.deltaTime);

        //Move by translate
        Vector3 target = this.transform.position - destination;
        this.transform.Translate(-target.normalized * projectileSpeed * Time.deltaTime, Space.World);

        //Rotate weapon
        SelfRotation();

        //Debug.Log("origin" + startWorldPos);
        //Debug.Log("current pos" + this.transform.position);

        //Reset weapon when out of range
        CountDownToReset();
    }

    private void CountDownToReset()
    {
        if (currentFlyingTime < projectileMaxFlyTime)
        {
            currentFlyingTime += Time.deltaTime;
        }
        else
        {
            Debug.Log("Stop throwing");
            ResetVariable();
            ResetTransform();
        }
    }

    protected override void ResetVariable()
    {
        base.ResetVariable();
        isRotate = false;
    }

    private void SelfRotation()
    {
        Vector3 rotationThisFrame = new Vector3(0, 0, -1);
        transform.Rotate(rotationThisFrame, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckCollideTag(other))
        {
            var stateManager = other.gameObject.GetComponentInChildren<BaseStateManager>();
            var statistic = currentHumanoid.gameObject.GetComponent<Statistic>();

            stateManager.currentCollider.enabled = false;
            //Turn off weapon (need assign to death state)
            stateManager.currentWeaponScript.gameObject.SetActive(false);

            //Increased size of current game object
            stateManager.attackable.IncreaseRange();
            statistic.OnKillEnemy();

            //Turn off enemy collider
            //Set alive of hit gameObject to false
            stateManager.isAlive = false;

            Debug.Log("Hit object");
            ResetVariable();
            ResetTransform();
        }
    }

    private bool CheckCollideTag(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("Player")) && other.gameObject != currentHumanoid)
        {
            return true;
        }
        return false;
    }
}
