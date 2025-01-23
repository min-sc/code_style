using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.15f; 
    private Vector2 offset;
    private Vector3 camPos;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        if (target == null) return;

        camPos = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10f);
        transform.position = Vector3.Lerp(transform.position, camPos, 3f * Time.deltaTime);
    }
}
