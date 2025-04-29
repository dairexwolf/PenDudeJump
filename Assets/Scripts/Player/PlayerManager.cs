using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// �������� � ���� ����-���� - ����, ������ � ��������� ��������
/// </summary>
public class PlayerManager : MonoBehaviour
{

    [Header("Jump Settings")]
    [SerializeField] private float force = 10f;             // ������ ���������
    [SerializeField] private float gravityMax = 10f;        // ������������ ���� ����

    private Vector2 curVel;                                 // ������� ��������
    private Vector2 down;                                   // ������������ ��������

    private GameManager gm;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curVel = Vector2.zero;
        down = Vector2.down * gravityMax;
    }

    // Update is called once per frame
    void Update()
    {
        // ���������� ������ � �������
        JumpAndFall();
        // ���������� �������� �� ��, ����� ���� ������ ��� � ������ 
        ReplaceBack();
    }

    #region JumpAdnFall
    private void JumpAndFall()
    {
        // ���������� ��������� ��������
        curVel = Vector2.Lerp(curVel, down, 1.3f * Time.deltaTime);

        if (curVel.y <= -gravityMax)
        {
            curVel.y = -gravityMax;
        }

        // ������� ���������
        this.transform.Translate(curVel * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform" && curVel.y < 0)
        {
            curVel.y = force;
            if (this.transform.position.y > 0)
            {
                gm.AddToScore((int)transform.position.y + 5);
            }
        }

        if (col.gameObject.name == "GenerationTrigger")
        {
            gm.GenerateEnviroment(col.transform.position.y+5, true);
        }
    }

    #endregion

    void ReplaceBack()
    {
        if (this.transform.position.x >= 3.7f)
        {
            Vector3 newCoords = new Vector3(-3.6f, transform.position.y, transform.position.z);
            this.transform.position = newCoords;
        }
        else if (this.transform.position.x <= -3.7f)
        {
            Vector3 newCoords = new Vector3(3.6f, transform.position.y, transform.position.z);
            this.transform.position = newCoords;
        }
    }

    public Vector2 GetCurrentVelocity()
    {
        return curVel;
    }

    public float GetForce()
    {
        return force;
    }
}
