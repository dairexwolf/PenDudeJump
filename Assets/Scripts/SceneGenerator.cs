using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Выполняет роль процедурного генератора
/// </summary>
public class SceneGenerator : MonoBehaviour
{

    private int score;
    
    [SerializeField] GameObject commonPlatform;
    [SerializeField] int maxPlatforms = 5;
    [SerializeField] int maxEnemies = 2;

    GameObject parentEnvironment;
    private List<GameObject> platforms;
    private List<GameObject> enemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentEnvironment = this.gameObject;
        platforms = new List<GameObject>();
        platforms.Add(GameObject.Find("Platform"));
        Generate(0);
        Generate(10f);
    }

    public void Generate(float up)
    {
        Vector3 newCoords;
        for(int i=0;i<maxPlatforms;i++)
        {
            newCoords = new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(-5f+up, 5f+up));
            Collider2D hits = Physics2D.OverlapBox(newCoords, commonPlatform.transform.localScale, 0f);
            while (hits!=null)
            {
                // Объект пересекается с другими
                newCoords = new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(-5f+up, 5f+up));
                hits = Physics2D.OverlapBox(newCoords, commonPlatform.transform.localScale, 0f);
            }

            platforms.Add(Instantiate(commonPlatform, newCoords, Quaternion.identity, parentEnvironment.transform));
        }
        
    }

    public List<GameObject> GetPlatforms()
    {
        return platforms;
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

}
