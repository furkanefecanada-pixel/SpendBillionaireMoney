using UnityEngine;

public class WorldMoveManager : MonoBehaviour
{
    public Transform targetSphere;     // Dünya (Sphere objesi)
    public float rotationSpeed = 0.2f; // Parmağın sürükleme hassasiyeti
    public float distance = 10f;       // Kameranın dünyaya uzaklığı
    public float zoomSpeed = 0.05f;    // Zoom hassasiyeti
    public float minDistance = 3f;     // Minimum zoom mesafesi
    public float maxDistance = 20f;    // Maksimum zoom mesafesi

    private Vector2 lastTouchPos;
    private bool isDragging = false;

    private float yaw = 0f;   // Yatay dönüş
    private float pitch = 0f; // Dikey dönüş

    private float lastPinchDistance;   // İki parmak arasındaki önceki mesafe

    void Start()
    {
        if (targetSphere == null)
        {
            Debug.LogError("Sphere atanmadı!");
            return;
        }

        // Kamerayı başlangıçta hedefin arkasına konumlandır
        transform.position = targetSphere.position - transform.forward * distance;
    }

    void Update()
    {
        if (Input.touchCount == 1) // Tek parmak: döndürme
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastTouchPos = t.position;
            }
            else if (t.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = t.position - lastTouchPos;
                lastTouchPos = t.position;

                // Kamera rotasyonu
                yaw += delta.x * rotationSpeed;
                pitch -= delta.y * rotationSpeed;
                pitch = Mathf.Clamp(pitch, -80f, 80f); // dikey limitleme
            }
            else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
        else if (Input.touchCount == 2) // İki parmak: pinch zoom
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float currentDist = Vector2.Distance(t0.position, t1.position);

            if (t0.phase == TouchPhase.Began || t1.phase == TouchPhase.Began)
            {
                lastPinchDistance = currentDist;
            }
            else
            {
                float delta = currentDist - lastPinchDistance;
                lastPinchDistance = currentDist;

                distance -= delta * zoomSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
            }
        }

        // Kamerayı döndür ve konumlandır
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = targetSphere.position + offset;
        transform.LookAt(targetSphere);

        // Dünya da kamera hareketine göre dönsün (gerçekçi kayma efekti)
        if (isDragging && Input.touchCount == 1)
        {
            targetSphere.Rotate(Vector3.up, -Input.GetTouch(0).deltaPosition.x * rotationSpeed, Space.World);
            targetSphere.Rotate(transform.right, Input.GetTouch(0).deltaPosition.y * rotationSpeed, Space.World);
        }
    }
}
