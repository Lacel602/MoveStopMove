using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isOverCastPlayer = false;
    [SerializeField]
    private MeshRenderer obstacleRender;
    [SerializeField]
    private Color originColor;

    private Color transparentColor;

    [SerializeField]
    public bool TransparentOn = false;

    public bool IsOverCastPlayer
    {
        get
        {
            return isOverCastPlayer;
        }
        set
        {
            isOverCastPlayer = value;
            ChangeMaterialTransParent(isOverCastPlayer);
        }
    }

    private void Update()
    {
        //IsOverCastPlayer = TransparentOn;
    }
    private void Reset()
    {
        this.LoadComponent();
    }

    private void Start()
    {
        transparentColor = GetTransparentColor(originColor);
    }

    private void LoadComponent()
    {
        obstacleRender = this.GetComponent<MeshRenderer>();
        originColor = obstacleRender.sharedMaterial.color;
    }

    private Color GetTransparentColor(Color color)
    {
        Color newColor = color;
        newColor.a = 0.2f;
        return newColor;
    }

    private void ChangeMaterialTransParent(bool overcast)
    {
        if (overcast)
        {
            SetRenderMode(BlendMode.Transparent);
            obstacleRender.sharedMaterial.color = transparentColor;
        }
        else
        {
            SetRenderMode(BlendMode.Opaque);
            obstacleRender.sharedMaterial.color = originColor;
        }
    }

    public void SetRenderMode(BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                obstacleRender.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                obstacleRender.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                obstacleRender.sharedMaterial.SetInt("_ZWrite", 1);
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHATEST_ON");
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHABLEND_ON");
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                obstacleRender.sharedMaterial.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                obstacleRender.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                obstacleRender.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                obstacleRender.sharedMaterial.SetInt("_ZWrite", 1);
                obstacleRender.sharedMaterial.EnableKeyword("_ALPHATEST_ON");
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHABLEND_ON");
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                obstacleRender.sharedMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                break;
            case BlendMode.Fade:
                obstacleRender.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                obstacleRender.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                obstacleRender.sharedMaterial.SetInt("_ZWrite", 0);
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHATEST_ON");
                obstacleRender.sharedMaterial.EnableKeyword("_ALPHABLEND_ON");
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                obstacleRender.sharedMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
            case BlendMode.Transparent:
                obstacleRender.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                obstacleRender.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                obstacleRender.sharedMaterial.SetInt("_ZWrite", 0);
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHATEST_ON");
                obstacleRender.sharedMaterial.DisableKeyword("_ALPHABLEND_ON");
                obstacleRender.sharedMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                obstacleRender.sharedMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
        }
    }
}
