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
        projectileSpeed = 6f;
    }

    private void Update()
    {
        Debug.Log("IsThrowing " + isThrowing);
        if (isThrowing)
        {
            this.ThrowWeapon(stateManager, enemyPos);
        }
    }
    public override void ThrowWeapon(PlayerStateManager stateManager, Vector3 enemyPos)
    {
        this.transform.parent = stateManager.projectileContainer;
        Vector3 destination = new Vector3(enemyPos.x, transform.position.y, enemyPos.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, projectileSpeed * Time.deltaTime);

        if (Vector3.Distance(stateManager.playerTransform.position, transform.position) > stateManager.playerAttack.Radius + 0.5f)
        {
            isThrowing = false;
            this.ResetTransform();
            Debug.Log("IsThrowing " + isThrowing);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collide with enemy");
            isThrowing = false;
            ResetTransform();
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

}
