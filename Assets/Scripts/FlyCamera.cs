using UnityEngine;

public class FlyCamera : MonoBehaviour {

    private float YVelocity = 0.0f;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private float scrollBorder;
    [SerializeField]
    private Vector2 scrollLimitEnd;
    [SerializeField]
    private Vector2 scrollLimitBegin;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float minFov = 40f;
    [SerializeField]
    private float maxFov = 70f;
    [SerializeField]
    private float sensitivity = 10f;

    private void SetCameraY(double smoothTime) {
        float newPosition = Mathf.SmoothDamp(transform.position.y, 8, ref YVelocity, (float)smoothTime);
        transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
    }

    private void HandleZooming() {
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    void Update() {
        HandleZooming();

        double smoothTime = 0.1 / (mainCamera.velocity.magnitude / 100);
        float finalSpeed = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - scrollBorder) {
            transform.Translate(new Vector3(finalSpeed, 0, 0));
            SetCameraY(smoothTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= scrollBorder) {
            transform.Translate(new Vector3(-finalSpeed, 0, 0));
            SetCameraY(smoothTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= scrollBorder) {
            transform.Translate(new Vector3(0, 0, -finalSpeed));
            SetCameraY(smoothTime);
        }
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - scrollBorder) {
            transform.Translate(new Vector3(0, 0, finalSpeed));
            SetCameraY(smoothTime);
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, scrollLimitBegin.x, scrollLimitEnd.x);
        pos.z = Mathf.Clamp(pos.z, scrollLimitBegin.y, scrollLimitEnd.y);

        transform.position = pos;

    }

}
