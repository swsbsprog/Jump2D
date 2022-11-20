using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI myScoreText;
    public TextMeshProUGUI bestScoreText;

    public void ShowScore(int currentScore, int bestScore)
    {
        gameObject.SetActive(true);
        myScoreText.text = currentScore.ToString();
        bestScoreText.text = bestScore.ToString();
    }

    public void Retry()
    {
        // 처음부터 다시.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        //이어서 하기
        GameManager.instance.Continue();
    }
}
