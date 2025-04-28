using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// ���������� ����������
/// </summary>
public class PlayerInputs : MonoBehaviour
{

    InputAction moveAction;
    InputAction attackAction;

    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 3f;       // ��������� ���������
    [SerializeField] private float maxSpeed = 8f;            // ������������ ��������
    [SerializeField] private float deceleration = 2f;       // ���������� ��� ���������� �����

    private Vector2 curVel;                         // ������� ��������

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
        // ��������� � ��������� ���������
        Vector2 moveValue = moveAction.ReadValue<Vector2>().normalized;

        if (moveValue.magnitude > 0)
        {
            curVel += moveValue * acceleration;
        }

        // ������������ �������� ������������ ���������
        if (curVel.magnitude > maxSpeed)
        {
            curVel = curVel.normalized * maxSpeed;
        }

        // ���� ����� ���, ��������� ����������
        if (moveValue == Vector2.zero)
        {
            // ���������� ��������� ��������
            curVel = Vector2.Lerp(curVel, Vector2.zero, deceleration * Time.deltaTime);

            // ���� �������� ����� ����, ������ �������������
            if (curVel.magnitude < 0.5f)
            {
                curVel = Vector2.zero;
            }
        }

        // ������� ���������
        this.transform.Translate(curVel*Time.deltaTime);
    }
}
