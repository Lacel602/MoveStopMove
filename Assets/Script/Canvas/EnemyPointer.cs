using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [SerializeField]
    private RectTransform pointerRectTransform;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject arrow;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        target = GameObject.Find("ActiveEnemy").transform.GetChild(0).gameObject;
        arrow = this.transform.Find("Arrow").gameObject;
        pointerRectTransform = arrow.GetComponent<RectTransform>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        MoveAndRotateIndicator();
    }

    private void MoveAndRotateIndicator()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);

        bool isOffScreen = screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

        if (isOffScreen && target.activeSelf)
        {
            arrow.gameObject.SetActive(true);

            //Move arrow
            Vector3 cappedScreenPos = screenPos;
            cappedScreenPos.z = 0;
            cappedScreenPos.x = Mathf.Clamp(cappedScreenPos.x, 20, Screen.width - 20);
            cappedScreenPos.y = Mathf.Clamp(cappedScreenPos.y, 20, Screen.height - 20);

            arrow.transform.position = cappedScreenPos;

            //Rotate arrow
            //Get center point of screen
            Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);

            //Caculate the angle tan(y/x)
            float angle = Mathf.Atan2(cappedScreenPos.y - centerScreen.y, cappedScreenPos.x - centerScreen.x) * Mathf.Rad2Deg;

            //Rotate the arrow 
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
    }
}
