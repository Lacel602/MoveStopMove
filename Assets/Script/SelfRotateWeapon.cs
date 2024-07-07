using Assets;
using Assets.Script;
using System;
using UnityEngine;

public class SelfRotateWeapon : Weapon
{
    private bool isRotate = false;

    private void Reset()
    {
        this.LoadComponent();
    }
    private void Update()
    {
        ThrowWeapon();
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

    private void ThrowWeapon()
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
            this.MoveWeapon(enemyPos);

            //Rotate weapon
            SelfRotation();

            //ResetWeapon when out of range
            CaculateRangeToReset();

            //Reset weapon when out of time
            CountDownToReset();
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

    public override void MoveWeapon(Vector3 enemyPos)
    {
        //Set weapon parent outside player
        this.transform.parent = projectileContainer;

        //Get throwing destination
        Vector3 destination = new Vector3(enemyPos.x, transform.position.y, enemyPos.z);

        //Move by translate
        Vector3 target = destination - startWeaponWorldPos;
        target.y = -7f;
        this .transform.position = new Vector3(transform.position.x, -7f, transform.position.z);
        this.transform.Translate(target.normalized * projectileSpeed * Time.deltaTime, Space.World);
    }

    private void CaculateRangeToReset()
    {
    }

    private void CountDownToReset()
    {
        if (currentFlyingTime < projectileMaxFlyTime)
        {
            currentFlyingTime += Time.deltaTime;
        }
        else
        {
            //Debug.Log("Stop throwing");
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
        this.transform.Rotate(rotationThisFrame, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollide(other);
    }

    private void CheckCollide(Collider other)
    {
        if (isThrowing)
        {
            if (CheckCollideTag(other))
            {
                var enemyStateManager = other.gameObject.GetComponentInChildren<BaseStateManager>();
                var stat = currentHumanoid.gameObject.GetComponent<Statistic>();
                var enemyStat = other.gameObject.GetComponent<Statistic>();

                //Increased size of current game object

                stat.OnKillEnemy(enemyStat);
                //Set alive of hit gameObject to false
                enemyStateManager.isAlive = false;

                //Debug.Log("Hit object");
                ResetVariable();
                ResetTransform();
            }
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
