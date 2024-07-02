using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HammerWeapon : Weapon
{
    private bool isRotate = false;

    private void Reset()
    {
        projectileContainer = GameObject.Find("ProjectileContainer").transform;
        originParent = this.transform.parent;
        originPosition = this.transform.localPosition;
        originRotation = this.transform.localRotation;
        originScale = this.transform.localScale;
        projectileSpeed = 6f;
        projectileRange = 6.5f;
    }

    private void Update()
    {
        if (isThrowing)
        {
            if (!isRotate)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(-90, 10, 0));
                isRotate = true;
            }
            this.ThrowWeapon(enemyPos);
        }
    }
    public override void ThrowWeapon(Vector3 enemyPos)
    {
        this.transform.parent = projectileContainer;
        Vector3 destination = new Vector3(enemyPos.x, transform.position.y, enemyPos.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, projectileSpeed * Time.deltaTime);

        SelfRotation();

        if (Vector3.Distance(originPosition, transform.position) > projectileRange)
        {
            isThrowing = false;
            isRotate = false;
            ResetTransform();
        }
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
            isRotate = false;
            isThrowing = false;
            ResetTransform();
        }
    }

    //public override void ResetTransform()
    //{
    //    this.transform.parent = originParent;
    //    this.transform.localPosition = originPosition;
    //    this.transform.localRotation = originRotation;
    //    this.transform.localScale = originScale;
    //}

}
