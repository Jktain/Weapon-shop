using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed = 0.1f;
    [SerializeField] private Vector2 panLimitMin = new Vector2(-10, -10);
    [SerializeField] private Vector2 panLimitMax = new Vector2(10, 10);

    private Vector3 lastMousePosition;
    private bool isDragging = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // початок перетягування
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // відпускання кнопки
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, 0, -delta.y) * panSpeed;

            transform.position += move;

            // Обмеження руху
            float clampedX = Mathf.Clamp(transform.position.x, panLimitMin.x, panLimitMax.x);
            float clampedZ = Mathf.Clamp(transform.position.z, panLimitMin.y, panLimitMax.y);

            transform.position = new Vector3(clampedX, transform.position.y, clampedZ);

            lastMousePosition = Input.mousePosition;
        }
    }
}
