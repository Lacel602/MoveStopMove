using Assets.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class BaseStateManager : MonoBehaviour
    {
        [SerializeField]
        public Transform currentHumanoidTransform;

        [SerializeField]
        public Attackable attackable;

        [SerializeField]
        public Animator animator;

        public bool isAlive = true;

        public bool hasAttacked = false;

        public bool hasUlti = false;

        public Weapon currentWeaponScript;

        public Collider currentCollider;

        public Statistic currentStatistic;

        public HumanoidManager humanoidManager;

        protected virtual void LoadComponent()
        {
            currentHumanoidTransform = this.transform.parent;
            animator = this.transform.parent.Find("GFX").GetComponent<Animator>();
            attackable = this.transform.Find("AttackRange").GetComponent<Attackable>();
            currentCollider = currentHumanoidTransform.GetComponent<Collider>();
            currentStatistic = currentHumanoidTransform.GetComponent<Statistic>();
        }

        protected virtual Transform FindChildByName(Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name)
                {
                    return child;
                }

                Transform result = FindChildByName(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public virtual void DisableAllAnimations()
        {
            foreach (var anim in AnimationStrings.listAnimations)
            {
                animator.SetBool(anim, false);
            }
        }

        public virtual void ResetVariable()
        {
            isAlive = true;
            hasAttacked = false;
            hasUlti = false;
        }
    }
}
