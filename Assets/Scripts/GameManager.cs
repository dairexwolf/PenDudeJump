using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int score = 0;
    private int genScore = 0;

    [SerializeField] private SceneGenerator sceneGen;
    [SerializeField] private PlayerManager playerMg;
    [SerializeField] private GameObject gameOverMessage;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void GenerateEnvironment(float up, bool moveTriggers)
    {
        sceneGen.Generate(up, moveTriggers);
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        gameOverMessage.SetActive(true);
    }

    public void AddToScore(int i)
    {
        score += i;
        genScore += i;
        if (genScore > 500)
        {
            genScore -= 500;
            sceneGen.ReduceNumberOfPlatforms();
        }

        scoreText.text = score.ToString();
    }
}
