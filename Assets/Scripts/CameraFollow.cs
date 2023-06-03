using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public bool isCustomOffset;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;

  //  [SerializeField] private int speed = 5;
    [SerializeField] private int sensitivity = 5;

    private float currentX = 0f;
    private float currentY = 0f;

    private void Start()
    {
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;
        currentY = Mathf.Clamp(currentY, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 targetPosition = target.position;

        // Ensure that the camera's Y position never goes below ground level
        if (targetPosition.y < 0f)
        {
            targetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
        }

        Vector3 desiredPosition = targetPosition + (rotation * offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target);
    }
}

