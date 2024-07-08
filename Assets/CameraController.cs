using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 originalDistance;

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
        GetPlayerDistance();
    }

    private void GetPlayerDistance()
    {
        originalDistance = this.transform.position - player.transform.position;
        distance = originalDistance;
    }

    private void Update()
    {
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        if (Physics.Raycast(this.transform.position, direction, out RaycastHit hit, 100f, layerToHit))
        {
            //Debug.Log("ray cast hit something");
            Obstacle obstacle = hit.collider.gameObject.GetComponent<Obstacle>();
            if (!obstacle.IsOverCastPlayer)
            {
                obstacle.IsOverCastPlayer = true;
                oldObstacle = obstacle;
            }    
        }
        else
        {
            //Debug.Log("ray cast hit nothing");
            if (oldObstacle != null)
            {
                if (oldObstacle.IsOverCastPlayer)
                {
                    oldObstacle.IsOverCastPlayer = false;
                }  
            }
        }
    }
    private void LateUpdate()
    {
        //Make camera follow player
        MoveCamera();
    }

    private void MoveCamera()
    {
        this.transform.position = player.transform.position + distance;
    }

    public void BackwardCamera(int level)
    {
        Debug.Log("BackwardCamera");
        Vector3 newDistance = originalDistance * Mathf.Pow(ConstantStat.increaseSize, level);
        StartCoroutine(ChangeVectorOverTime(distance, newDistance, 0.4f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        Gizmos.DrawRay(this.transform.position, direction * 300f);
    }

    private IEnumerator ChangeVectorOverTime(Vector3 start, Vector3 destination, float t)
    {
        float elapsedTime = 0;

        while (elapsedTime < t)
        {
            // Interpolate between the two vectors
            Vector3 currentVector = Vector3.Lerp(start, destination, elapsedTime / t);

            // Update the value of the vector (this could be a position, a direction, etc.)
            // For example, if you want to change the position of the GameObject:
            distance = currentVector;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Yield execution until the next frame
            yield return null;
        }

        // Ensure the final value is set to the target vector
        distance = destination;
    }
}
