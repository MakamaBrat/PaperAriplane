using UnityEngine;

public class AutoVerticalSlider2D : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float amplitude = 3f; // половина высоты движения

    private bool isMoving;
    private float startY;
    private float direction = 1f;

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
        // Нажали кнопку
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
        }

        // Отпустили кнопку
        if (Input.GetMouseButtonUp(0))
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
