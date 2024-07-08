using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanoidManager : MonoBehaviour
{
    [SerializeField]
    public Statistic stat;
    [SerializeField]
    public BaseStateManager stateManager;
    [SerializeField]
    public Attackable attackable;
    [SerializeField]
    public Color mainColor;
    [SerializeField]
    public EffectManager effectManager;
    [SerializeField]
    public ColorManager colorManager;
    protected virtual void LoadComponent()
    {
        stat = this.GetComponent<Statistic>();
        stateManager = this.GetComponentInChildren<BaseStateManager>();
        attackable = this.GetComponentInChildren<Attackable>();
        mainColor = this.transform.Find("GFX/initialShadingGroup1").GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;
        effectManager = this.GetComponentInChildren<EffectManager>();
        colorManager = this.GetComponentInChildren<ColorManager>();
    }
    private void Start()
    {
        this.SetColorToEfx();
    }

    private void SetColorToEfx()
    {
        if (effectManager != null)
        {
            effectManager.levelUpEfx.startColor = mainColor;
            effectManager.deathEfx.startColor = mainColor;
        }
    }
}
