using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("WeaponStats")]
        [SerializeField]
        protected float projectileSpeed;

        [SerializeField]
        protected float projectileMaxFlyTime;

        protected float currentFlyingTime = 0f;

        [Header("Component")]
        [SerializeField]
        protected Transform projectileContainer;
        [SerializeField]
        protected GameObject currentHumanoid;

        [Header("OriginTransform")]
        [SerializeField]
        protected Transform originParent;
        [SerializeField]
        protected Vector3 startWeaponWorldPos;
        [SerializeField]
        protected Vector3 originPosition;
        [SerializeField]
        protected Vector3 originScale;
        [SerializeField]
        protected Quaternion originRotation;

        [Header("LogicVariable")]
        [SerializeField]
        protected bool getStartPos = false;
        [SerializeField]
        public bool isThrowing;
        [SerializeField]
        public Vector3 enemyPos;

        public abstract void MoveWeapon(Vector3 enemyPos);

        protected virtual void ResetTransform()
        {
            this.transform.parent = originParent;
            this.transform.localPosition = originPosition;
            this.transform.localRotation = originRotation;
            this.transform.localScale = originScale;
        }

        protected virtual void ResetVariable()
        {
            isThrowing = false;
            getStartPos = !getStartPos;
            currentFlyingTime = 0f;
        }
    }
}
