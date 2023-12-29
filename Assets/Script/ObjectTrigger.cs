using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public Transform player;
    public Transform objectToApproach;
    public Camera mainCamera;
    public SpriteRenderer objectImageRenderer;

    [SerializeField] float distanceThreshold = 2f;

    bool objectInSight = false;

    public bool IsObjectInSight() // Check if the object is in sight
    {
        return objectInSight;
    }

    void Update()
    {
        // Check if the object is within the camera's view
        Vector3 objectViewportPos = mainCamera.WorldToViewportPoint(objectToApproach.position);
        if (objectViewportPos.x > 0 && objectViewportPos.x < 1 && objectViewportPos.y > 0 && objectViewportPos.y < 1 && objectViewportPos.z > 0)
        {
            objectInSight = true;
        }
        else
        {
            objectInSight = false;
        }

        // Check the proximity of the player to the object
        if (Vector3.Distance(player.position, objectToApproach.position) < distanceThreshold)
        {
            // Player is close enough to the object
            //
            //
        }
    }
}
