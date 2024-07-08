using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : HumanoidManager
{
    [SerializeField]
    public TMP_Text scoreText;

    private void Reset()
    {
        this.LoadComponent();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        scoreText = this.transform.Find("Canvas/Score").GetComponent<TMP_Text>();
    }
}
