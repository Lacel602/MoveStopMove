using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    [SerializeField]
    private Color color;
    [SerializeField]
    private Image scoreBackground;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        scoreBackground = this.transform.Find("Image").GetComponent<Image>();
        color = this.transform.parent.Find("GFX/initialShadingGroup1").GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;
        scoreBackground.color = color;
    }
    
}
