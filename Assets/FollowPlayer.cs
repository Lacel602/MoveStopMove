using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField] 
    private Vector3 distance;

    [SerializeField]
    private LayerMask layerToHit;

    private Obstacle oldObstacle;

    private void Reset()
    {
        this.LoadComponent();
    }

    private void LoadComponent()
    {
        player = GameObject.Find("Player").gameObject;
    }

    private void Start()
    {
        distance = this.transform.position - player.transform.position;
    }

    private void Update()
    {
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        if (Physics.Raycast(this.transform.position, direction, out RaycastHit hit, 100f, layerToHit))
        {
            Debug.Log("ray cast hit something");
            Obstacle obstacle = hit.collider.gameObject.GetComponent<Obstacle>();
            obstacle.IsOverCastPlayer = true;
            oldObstacle = obstacle;
        } 
        else
        {
            Debug.Log("ray cast hit nothing");
            if (oldObstacle != null)
            {
                oldObstacle.IsOverCastPlayer = false;
            }
        }
    }
    private void LateUpdate()
    {
        this.transform.position = player.transform.position + distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        Gizmos.DrawRay(this.transform.position, direction * 300f);
    }
}
