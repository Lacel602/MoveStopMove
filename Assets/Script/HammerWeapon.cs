using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HammerWeapon : Weapon
{
    [SerializeField]
    private PlayerStateManager stateManager;
    private void Reset()
    {
        stateManager = GameObject.Find("Logic").GetComponent<PlayerStateManager>();
        originParent = this.transform.parent;
        originPosition = this.transform.localPosition;
        originRotation = this.transform.localRotation;
        originScale = this.transform.localScale;

    }

    private void FixedUpdate()
    {
        if (isThrowing)
        {
            this.ThrowWeapon(stateManager, enemyPos);
        }
    }
    public override void ThrowWeapon(PlayerStateManager stateManager, Vector3 enemyPos)
    {
        stateManager.currentWeapon.transform.parent = stateManager.projectileContainer;
        Vector3 destination = new Vector3(enemyPos.x, transform.position.y, enemyPos.z);
        stateManager.currentWeapon.transform.position = Vector3.MoveTowards(stateManager.currentWeapon.transform.position, destination, stateManager.projectileSpeed);

        if (Vector3.Distance(stateManager.playerTransform.position, transform.position) > stateManager.playerAttack.Radius + 2f)
        {
            isThrowing = false;
            this.ResetTransform();
        }
    }

    public override void ResetTransform()
    {
        Debug.Log("ResetTransform");
        this.transform.parent = originParent;
        this.transform.localPosition = originPosition;
        this.transform.localRotation = originRotation;
        this.transform.localScale = originScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isThrowing = false;
            ResetTransform();
        }
    }
}
