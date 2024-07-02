using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackable: Attackable
{
    protected override void TransformTargetCircle()
    {
        
    }
    protected override bool CheckValidObject(Collider hitCollider)
    {
        if ((hitCollider.CompareTag("Enemy") ||  hitCollider.CompareTag("Player")) && hitCollider.gameObject != currentHumanoid)
        {
            return true;
        }

        return false;
    }
}