using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem levelUpEfx;
    [SerializeField]
    public ParticleSystem deathEfx;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        levelUpEfx = this.transform.Find("LevelUp").GetComponent<ParticleSystem>();
        deathEfx = this.transform.Find("Death").GetComponent<ParticleSystem>();
    }
}

