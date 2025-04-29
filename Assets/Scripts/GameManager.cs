using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int score = 0;
    private int genScore = 0;

    [SerializeField] private SceneGenerator sceneGen;
    [SerializeField] private PlayerManager playerMg;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateEnviroment(float up, bool moveTriggers)
    {
        sceneGen.Generate(up, moveTriggers);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int i)
    {
        score += i;
        genScore += i;
        if (genScore > 5)
        {
            genScore -= 5;
        }
    }
}
