using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Содержит в себе мета-инфу - счет, прыжок и обработка коллизий
/// </summary>
public class PlayerManager : MonoBehaviour
{

    [Header("Jump Settings")]
    [SerializeField] public float force = 10f;             // Прыжок персонажа
    [SerializeField] private float gravityMax = 10f;        // Максимальная тяга вниз

    [SerializeField] Transform cameraTransform;

    public Vector2 curVel;                                 // Текущая скорость
    private Vector2 down;                                   // Максимальная скорость

    private GameManager gm;

    bool gameOver;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameOver = false;
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
        if (!gameOver)
        {
            // Активируем прыжок и падение
            JumpAndFall();
            // Активируем проверку на то, чтобы перс всегда был в экране 
            ReplaceBack();
        }
    }

    #region JumpAdnFall
    private void JumpAndFall()
    {
        // Постепенно уменьшаем скорость
        curVel = Vector2.Lerp(curVel, down, 1.3f * Time.deltaTime);

        if (curVel.y <= -gravityMax)
        {
            curVel.y = -gravityMax;
        }

        // Двигаем персонажа
        this.transform.Translate(curVel * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform" && curVel.y < 0)
        {
            // curVel.y = force;
            IPlatform platform = col.gameObject.GetComponent<IPlatform>();
            // Если платформа реализует интерфейс — вызываем метод
            if (platform != null)
            {
                platform.AtTouch(this);
                this.GetComponent<Animator>().ResetTrigger("Jumping");
                this.GetComponent<Animator>().SetTrigger("Jumping");
                
            }
        }

        if (col.gameObject.name == "GenerationTrigger")
        {
            gm.GenerateEnvironment(col.transform.position.y + 5, true);
        }
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DeleteTrigger")
        {
            gm.GameOver();
            gameOver = true;
            this.transform.Translate(Vector2.down * 10);
        }
    }

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
