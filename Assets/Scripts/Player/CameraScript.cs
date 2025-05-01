using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameManager gameManager;

    private PlayerManager pm;

    private void Start()
    {
        pm = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.GetCurrentVelocity().y > 0 && player.transform.position.y > this.transform.position.y)
        {
            this.transform.Translate(pm.GetCurrentVelocity() * Time.deltaTime);
            if (this.transform.position.y > transform.position.y - 1f)
            {
                gameManager.AddToScore(1);
            }
        }
    }
}
