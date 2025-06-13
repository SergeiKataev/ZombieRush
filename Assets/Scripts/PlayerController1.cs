using UnityEngine;
using UnityEngine.InputSystem; // Добавьте эту строку

public class MainPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera gameCamera;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePosition;
    
    // Добавьте эти поля для новой системы ввода
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Инициализация новой системы ввода
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
    }

    void Update()
    {
        // Получаем ввод из новой системы
        movement = moveAction.ReadValue<Vector2>();
        
        // Для мыши можно оставить старый способ или адаптировать
        mousePosition = gameCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    void FixedUpdate()
    {
        // Движение
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        
        // Поворот
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}