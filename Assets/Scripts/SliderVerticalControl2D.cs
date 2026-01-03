using UnityEngine;
using UnityEngine.InputSystem;

public class AutoVerticalSlider2D : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float amplitude = 3f; // половина высоты движения

    private bool isMoving;
    private float startY;
    private float direction = 1f;
    public Transform tr;
    void OnEnable()
    {
        startY = transform.position.y;
        isMoving = false;
        direction = 1f;
       
    }

    void Update()
    {
        HandleInput();

        if (!isMoving)
            return;

        Move();
    }

    void HandleInput()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            isMoving = true;
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isMoving = false;
        }
    }

    void Move()
    {
        Vector3 pos = transform.position;
        pos.y += direction * speed * Time.deltaTime;

        if (pos.y >= startY + amplitude)
        {
            pos.y = startY + amplitude;
            direction = -1f;
        }
        else if (pos.y <= startY - amplitude)
        {
            pos.y = startY - amplitude;
            direction = 1f;
        }

        transform.position = pos;
    }
}
