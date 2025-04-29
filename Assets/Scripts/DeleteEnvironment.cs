using UnityEngine;

public class DeleteEnvironment : MonoBehaviour
{
    [SerializeField] SceneGenerator sg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Platform")
        {
            sg.GetPlatforms().Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
