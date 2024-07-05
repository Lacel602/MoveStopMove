using UnityEngine;

public class test2 : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    private void Reset()
    {
        this.LoadComponent();

        ChangeMaterialTransparent();
    }

    private void ChangeMaterialTransparent()
    {
        meshRenderer.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f, 1f);
    }

    private void LoadComponent()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }
}
