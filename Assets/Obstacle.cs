using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Obstacle : MonoBehaviour
{
    private bool isOverCastPlayer = false;
    [SerializeField]
    private MeshRenderer obstacleRender;
    [SerializeField]
    private Material originalMaterial;
    [SerializeField]
    private Material transparentMaterial;

    public bool IsOverCastPlayer
    {
        get
        {
            return isOverCastPlayer;
        }
        set
        {
            isOverCastPlayer = value;
            ChangeMaterial(isOverCastPlayer);
        }
    }
    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        obstacleRender = this.GetComponent<MeshRenderer>();
        originalMaterial = obstacleRender.sharedMaterial;
        //Get value of transparent material
        transparentMaterial = GetTransparentMaterial();

        //Debug.Log("Blend Mode: " + GetBlendMode(transparentMaterial));
        //Debug.Log("Color a = " + transparentMaterial.color.a);
    }

    private Material GetTransparentMaterial()
    {
        //Create new material
        Material newMaterial = new Material(originalMaterial);

        //Create trasnparent color
        Color newColor = newMaterial.color;
        newColor.a = 0.1f;
        newMaterial.color = newColor;

        //Change render mode of material
        SetRenderMode(BlendMode.Transparent, newMaterial);

        //Debug.Log("Blend Mode: " + GetBlendMode(newMaterial));

        return newMaterial;
    }

    private void ChangeMaterial(bool isOvercasting)
    {
        if (isOvercasting)
        {
            //To transparent
            Debug.Log("Set material to transparent");
            
            obstacleRender.material = transparentMaterial;
            SetRenderMode(BlendMode.Transparent, obstacleRender.material);

            //Debug.Log("Blend Mode: " + GetBlendMode(obstacleRender.material));
            //Debug.Log("Color a = " + obstacleRender.material.color.a);
        }
        else
        {
            //To original
            obstacleRender.material = originalMaterial;
            SetRenderMode(BlendMode.Opaque, obstacleRender.material);
        }
    }

    public void SetRenderMode(BlendMode blendMode, Material material)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                break;
            case BlendMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                break;
            case BlendMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
            case BlendMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
        }
    }

    public BlendMode GetBlendMode(Material material)
    {
        int srcBlend = material.GetInt("_SrcBlend");
        int dstBlend = material.GetInt("_DstBlend");
        int zWrite = material.GetInt("_ZWrite");

        if (srcBlend == (int)UnityEngine.Rendering.BlendMode.One && dstBlend == (int)UnityEngine.Rendering.BlendMode.Zero)
        {
            if (zWrite == 1)
                return BlendMode.Opaque;
        }
        else if (srcBlend == (int)UnityEngine.Rendering.BlendMode.One && dstBlend == (int)UnityEngine.Rendering.BlendMode.Zero)
        {
            if (zWrite == 1)
                return BlendMode.Cutout;
        }
        else if (srcBlend == (int)UnityEngine.Rendering.BlendMode.SrcAlpha && dstBlend == (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha)
        {
            if (zWrite == 0)
                return BlendMode.Fade;
        }
        else if (srcBlend == (int)UnityEngine.Rendering.BlendMode.One && dstBlend == (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha)
        {
            if (zWrite == 0)
                return BlendMode.Transparent;
        }

        return BlendMode.Fade;
    }
}
