using System.Collections.Generic;
using UnityEngine;

public class PlatformNode
{
    public GameObject platform;
    public int layer;
}

/// <summary>
/// Выполняет роль процедурного генератора
/// </summary>
public class SceneGenerator : MonoBehaviour
{
    [Header("Necessary objects")]
    [SerializeField] GameObject commonPlatform;
    [SerializeField] GameObject generationTrigger;
    [SerializeField] List<GameObject> uncommonPlatforms;

    [Header("Environment settings")]
    [SerializeField] int maxPlatforms = 5;
    // [SerializeField] int maxEnemies = 2;

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

    /* Старый способ
    public void Generate(float up, bool moveTriggers)
    {
        int availablePlatforms = curPlatforms;
        List<int> levelLayers = new List<int>() { 0, 0, 0, 0, 0 };

        int[] range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };

        for (int i = Random.Range(0, 3); availablePlatforms > 3; availablePlatforms--)
        {
            range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };
            if (levelLayers[range[i]] < 3)
                levelLayers[range[i]]++;
            else
            {
                availablePlatforms++;
                continue;
            }
        }

        range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };

        for (int i = 0; i < availablePlatforms; i++)
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
                if (Random.Range(0, 101) < 30)
                {
                    int num = Random.Range(0, uncommonPlatforms.Count);
                    platforms.Add(Instantiate(uncommonPlatforms[num], newCoords, Quaternion.identity, parentEnvironment.transform));
                }
                else
                    platforms.Add(Instantiate(commonPlatform, newCoords, Quaternion.identity, parentEnvironment.transform));
            }
        }
        if (moveTriggers)
        {
            newCoords = new Vector3(0, 5);
            generationTrigger.transform.Translate(newCoords);
        }

    }
    */



    public void Generate(float up, bool moveTriggers)
    {
        int availablePlatforms = curPlatforms;
        List<List<GameObject>> levelLayers = new List<List<GameObject>>();
        for (int i = 0; i < 5; i++)
        {
            levelLayers.Add(new List<GameObject>());
        }

        int[] range;

        for (int i = Random.Range(0, 3); availablePlatforms > 3; availablePlatforms--)
        {
            range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };
            if (levelLayers[range[i]].Count < 3)
            {
                GameObject platform;
                if (Random.Range(0, 101) < 30)
                {
                    int num = Random.Range(0, uncommonPlatforms.Count);
                    platform = uncommonPlatforms[num];
                }
                else
                    platform = commonPlatform;
                levelLayers[range[i]].Add(platform);
            }
            else
            {
                availablePlatforms++;
                continue;
            }
        }

        range = new[] { Random.Range(0, 2), Random.Range(3, 5), Random.Range(2, 4) };

        for (int i = 0; i < availablePlatforms; i++)
        {
            if (levelLayers[range[i]].Count < 3)
                levelLayers[range[i]].Add(commonPlatform);
            else
            {
                i++;
                continue;
            }
        }

        Vector3 newCoords;

        for (int i = levelLayers.Count - 1; i >= 0; i--)
        {
            for (int j = levelLayers[i].Count-1; j >= 0; j--)
            {
                newCoords = new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(((float)i) + up, ((float)i) + up + 1f));
                Collider2D hits = Physics2D.OverlapBox(newCoords, commonPlatform.transform.localScale, 0f);
                while (hits != null)
                {
                    // Объект пересекается с другими
                    newCoords = new Vector3(Random.Range(-1.9f, 1.9f), Random.Range(((float)i) + up, ((float)i) + up + 1f));
                    hits = Physics2D.OverlapBox(newCoords, commonPlatform.transform.localScale, 0f);
                }
                List<GameObject> platforms = levelLayers[i];
                platforms.Add(Instantiate(platforms[j], newCoords, Quaternion.identity, parentEnvironment.transform));
            }
        }

        if (moveTriggers)
        {
            newCoords = new Vector3(0, 5);
            generationTrigger.transform.Translate(newCoords);
        }


    }

    public void ReduceNumberOfPlatforms()
    {
        if (curPlatforms > 2)
            curPlatforms--;
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
