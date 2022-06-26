using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region 코루틴 샘플
    //Coroutine myCoHandle;
    //[Button]
    //void CoStart()
    //{
    //    print("CoStart");
    //    myCoHandle = StartCoroutine(MyCo());
    //    //StartCoroutine("MyCo");
    //}
    //[Button]
    //void CoStop()
    //{
    //    StopCoroutine(myCoHandle);
    //    //StopCoroutine("MyCo");
    //}
    //IEnumerator MyCo()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        print(Time.time);
    //    }
    //}
    #endregion 코루틴 샘플



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

    internal void GameOver()
    {
        if (score > bestScore)
            bestScore = score;
        // GameOver UI표시.
        gameOverUI.ShowScore(score, bestScore);

        // 블럭 소환하는 로직 멈추기.
        StopCo();

        // 시간 스케일 0으로 해서 게임 로직 멈추기
        //Time.timeScale = 0;
        gameState = GameStateType.GameOver;
    }
    public enum GameStateType { BeforePlay, Play, GameOver}
    public GameStateType gameState = GameStateType.BeforePlay;

    Coroutine spawnBlockCoHandle;
    private void Start()
    {
        spawnBlockCoHandle = StartCoroutine(SpawnBlockCo());
    }
    void StopCo() { StopCoroutine(spawnBlockCoHandle); }

    public TextMeshProUGUI countdownText;
    private IEnumerator SpawnBlockCo()
    {
        gameState = GameStateType.BeforePlay;
        // 3,2,1
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);

        scoreText.text = "";
        gameState = GameStateType.Play;
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
