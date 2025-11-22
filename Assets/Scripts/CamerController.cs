using UnityEngine;

public class CameraController : MonoBehaviour
{    
    [Header("Pan Settings")]
    [SerializeField] float panSpeed = 10f;
    [SerializeField] float panBorder = 10f;
    [SerializeField] float panSmoothing = 0.1f;
    
    [Header("Zoom Settings")]
    [SerializeField] float zoomSpeed = 500f;
    [SerializeField] float minZoom = 5f;
    [SerializeField] float maxZoom = 100f;
    [SerializeField] float zoomSmoothing = 0.1f;

    Camera cam;
    Vector2 boundaries;
    
    float currentZoom;
    float targetZoom;
    float zoomVelocity;
    
    Vector3 targetPosition;
    Vector3 panVelocity;

    private void Start()
    {
        cam = Camera.main;
        boundaries = new Vector2(WorldGrid.Instance.Width / 2f, WorldGrid.Instance.Height / 2f);
        currentZoom = cam.orthographicSize;
        targetZoom = currentZoom;
        targetPosition = transform.position;
    }
    
    private void LateUpdate()
    {
        HandlePanning();
        HandleZoom();
    }
    
    void HandlePanning()
    {
        Vector3 inputDirection = Vector3.zero;
        if(Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorder)
        {
            inputDirection.y += 1f;
        }
        if(Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorder)
        {
            inputDirection.y -= 1f;
        }
        if(Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorder)
        {
            inputDirection.x += 1f;
        }
        if(Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorder)
        {
            inputDirection.x -= 1f;
        }
        
        float zoomFactor = currentZoom / minZoom;
        targetPosition += inputDirection * panSpeed * zoomFactor * Time.deltaTime;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -boundaries.x, boundaries.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -boundaries.y, boundaries.y);
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref panVelocity, panSmoothing);
    }
    
    void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;
        if(scroll != 0)
        {
            targetZoom -= scroll * zoomSpeed * Time.deltaTime;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
        
        currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, zoomSmoothing);
        cam.orthographicSize = currentZoom;
    }
}
