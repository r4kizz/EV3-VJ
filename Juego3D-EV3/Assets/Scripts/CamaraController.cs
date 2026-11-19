using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    
    [Header("Configuración del Mouse")]
    public float mouseSensitivity = 3f;
    public float distanceFromPlayer = 7f;
    public float height = 4f;
    
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (player == null) return;

        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY = Mathf.Clamp(rotationY, -10f, 60f);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 targetPosition = player.position - (rotation * Vector3.forward * distanceFromPlayer) + (Vector3.up * height);

        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up * 0.5f);
    }
}