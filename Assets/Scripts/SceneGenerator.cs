using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Выполняет роль процедурного генератора
/// </summary>
public class SceneGenerator : MonoBehaviour
{

    private int score;

    [SerializeField] GameObject commonPlatform;
    [SerializeField] GameObject generationTrigger;

    [SerializeField] int maxPlatforms = 5;
    [SerializeField] int maxEnemies = 2;

    int curPlatforms;

    GameObject parentEnvironment;
    private List<GameObject> platforms;
    private List<GameObject> enemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentEnvironment = this.gameObject;
        platforms = new List<GameObject>();
        platforms.Add(GameObject.Find("Platform"));
        curPlatforms = maxPlatforms;
        Generate(-3, false);
        Generate(3f, false);
    }




    public void Generate(float up, bool moveTriggers)
    {
        int avaiblePlatforms = curPlatforms;
        List<int> levelLayers = new List<int>() { 0, 0, 0, 0, 0 };

        int[] range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };

        for (int i = Random.Range(0, 3); avaiblePlatforms > 3; avaiblePlatforms--)
        {
            range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };
            if (levelLayers[range[i]] < 3)
                levelLayers[range[i]]++;
            else
            {
                avaiblePlatforms++;
                continue;
            }
        }

        range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };

        for (int i = 0; i < avaiblePlatforms; i++)
        {
            if (levelLayers[range[i]] < 3)
                levelLayers[range[i]]++;
            else
            {
                i++;
                continue;
            }
        }

        Vector3 newCoords;

        for (int i = levelLayers.Count - 1; i >= 0; i--)
        {
            for (int j = levelLayers[i]; j > 0; j--)
            {
                newCoords = new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(((float)i) + up, ((float)i) + up + 1f));
                Collider2D hits = Physics2D.OverlapBox(newCoords, commonPlatform.transform.localScale, 0f);
                while (hits != null)
                {
                    // Объект пересекается с другими
                    newCoords = new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(((float)i) + up, ((float)i) + up + 1f));
                    hits = Physics2D.OverlapBox(newCoords, commonPlatform.transform.localScale, 0f);
                }
                platforms.Add(Instantiate(commonPlatform, newCoords, Quaternion.identity, parentEnvironment.transform));
            }
        }
        if (moveTriggers)
        {
            newCoords = new Vector3(0, 5);
            generationTrigger.transform.Translate(newCoords);
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
