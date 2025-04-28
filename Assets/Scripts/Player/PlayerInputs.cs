using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Управление персонажем
/// </summary>
public class PlayerInputs : MonoBehaviour
{

    InputAction moveAction;
    InputAction attackAction;

    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 3f;       // Ускорение персонажа
    [SerializeField] private float maxSpeed = 8f;            // Максимальная скорость
    [SerializeField] private float deceleration = 2f;       // Замедление при отсутствии ввода

    private Vector2 curVel;                         // Текущая скорость

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");

        curVel = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Считываем и применяем параметры
        Vector2 moveValue = moveAction.ReadValue<Vector2>().normalized;

        if (moveValue.magnitude > 0)
        {
            curVel += moveValue * acceleration;
        }

        // Ограничиваем скорость максимальным значением
        if (curVel.magnitude > maxSpeed)
        {
            curVel = curVel.normalized * maxSpeed;
        }

        // Если ввода нет, применяем замедление
        if (moveValue == Vector2.zero)
        {
            // Постепенно уменьшаем скорость
            curVel = Vector2.Lerp(curVel, Vector2.zero, deceleration * Time.deltaTime);

            // Если скорость очень мала, просто останавливаем
            if (curVel.magnitude < 0.5f)
            {
                curVel = Vector2.zero;
            }
        }

        // Двигаем персонажа
        this.transform.Translate(curVel*Time.deltaTime);
    }
}
