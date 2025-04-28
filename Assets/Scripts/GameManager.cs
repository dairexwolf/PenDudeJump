using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int score = 0;
    private int genScore = 0;

    public SceneGenerator sceneGen;
    public PlayerManager playerMg;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateEnviroment()
    {
        sceneGen.Generate(10f);
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
