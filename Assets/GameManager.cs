using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    public GameOverUI gameOverUI;
    internal void GameOver()
    {
        if (score > bestScore)
            bestScore = score;


        // GameOver UI표시.
        gameOverUI.ShowScore(score, bestScore);


        // 블럭 소환하는 로직 멈추기.
    }

    public enum Dicrection { Right, Left }

    public Block block;
    public Dicrection moveDirection = Dicrection.Right; // 오른쪽으로 움직인다.(왼쪽에 생성해서 오른쪽으로 이동)

    public int score;
    public int bestScore;
    public TextMeshProUGUI scoreText;
    public void SetNextLevel()
    {
        waitNextBlock = false;
        score += 100;
        // ui에 score표시하자.
        scoreText.text = score.ToString();
    }

    public bool waitNextBlock = true; // true인 동안 기다리자.
    private float initY = -3;
    private float blockHeight = 1;
    private float level = 0;
    public float offsetX = 3; // 블럭이 생성되는 위치.
    public float blockSpeed = 3;
    private IEnumerator Start()
    {
        scoreText.text = "";
        while (true)
        {
            print(level);
            waitNextBlock = true;

            var newBlock = Instantiate(block);
            float regenX;
            if (moveDirection == Dicrection.Right) // 왼쪽에 생성되어서 오른쪽으로 이동한다
            {
                regenX = -offsetX;
                newBlock.speed = blockSpeed;
            }
            else
            {
                regenX = offsetX;
                newBlock.speed = -blockSpeed;
            }
            //float regenX = moveDirection == Dicrection.Right ? -offsetX : offsetX;
            float regenY = initY + level * blockHeight;
            newBlock.transform.position = new Vector2(regenX, regenY);

            while (waitNextBlock)
            {
                yield return null; // 1 프레임 양보하겠다.
            }
            level++;
            moveDirection = moveDirection == Dicrection.Right ? Dicrection.Left : Dicrection.Right;
        }

        // 레벨 증가할때마다 카메라 위로올리기.

        // 플레이어 죽음(GameOver UI 표시)
        // 기초 단계


        // 평균
        // 타이틀씬 Touch!
        // 3,2,1게임시작

        // 최고 점수 기록 (앱 재시작해도 유지되어야함)
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) waitNextBlock = false;

    }
}
