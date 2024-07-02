using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public abstract class Weapon: MonoBehaviour
    {
        public Transform originParent;

        public Vector3 originPosition;
        public Vector3 originScale;
        public Quaternion originRotation;

        public bool isThrowing;

        public Vector3 enemyPos;
        public abstract void ThrowWeapon(PlayerStateManager stateManager, Vector3 enemyPos);
        public abstract void ResetTransform();
    }
}
