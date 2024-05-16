using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;  
    public float distance = 10.0f;  
    public float speed = 50.0f; 

    private float currentAngle;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for CameraOrbit script.");
            return;
        }

        Vector3 offset = transform.position - target.position;
        currentAngle = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;

        UpdateCameraPosition();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentAngle -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentAngle += speed * Time.deltaTime;
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        float radians = currentAngle * Mathf.Deg2Rad;
        float x = Mathf.Sin(radians) * distance;
        float z = Mathf.Cos(radians) * distance;
        Vector3 newPos = new Vector3(x, 0, z);

        transform.position = new Vector3(target.position.x + newPos.x, target.position.y, target.position.z + newPos.z);

        transform.LookAt(target);
    }
}
