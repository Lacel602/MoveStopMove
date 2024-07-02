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
    }


    private void RotateAndThrowWeapon()
    {
        Debug.Log(isThrowing);
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

        //Move weapon
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, projectileSpeed * Time.deltaTime);

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

        //Get distant from startPos to currentPos in 2D
        //float distance = GetDistanceFromStartPos();

        //Reset weapon when out of range
        //if (distance > projectileMaxFlyTime)
        //{
            
        //}
    }

    private float GetDistanceFromStartPos()
    {
        Vector2 originWorldPosV2 = new Vector2(startWeaponWorldPos.x, startWeaponWorldPos.z);
        Vector2 transformPosV2 = new Vector2(this.transform.position.x, this.transform.position.z);
        return Vector2.Distance(originWorldPosV2, transformPosV2);
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
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit object");
            ResetVariable();
            ResetTransform();
        }
    }
}
