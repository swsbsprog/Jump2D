using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region �ڷ�ƾ ����
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
    #endregion �ڷ�ƾ ����



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
    public Dicrection moveDirection = Dicrection.Right; // ���������� �����δ�.(���ʿ� �����ؼ� ���������� �̵�)

    public int score;
    public int bestScore;
    public TextMeshProUGUI scoreText;
    public void SetNextLevel()
    {
        waitNextBlock = false;
        score += 100;
        // ui�� scoreǥ������.
        scoreText.text = score.ToString();
    }

    public bool waitNextBlock = true; // true�� ���� ��ٸ���.
    private float initY = -3;
    private float blockHeight = 1;
    private float level = 0;
    public float offsetX = 3; // ���� �����Ǵ� ��ġ.
    public float blockSpeed = 3;

    internal void GameOver()
    {
        if (score > bestScore)
            bestScore = score;
        // GameOver UIǥ��.
        gameOverUI.ShowScore(score, bestScore);

        // �� ��ȯ�ϴ� ���� ���߱�.
        StopCo();

        // �ð� ������ 0���� �ؼ� ���� ���� ���߱�
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
            if (moveDirection == Dicrection.Right) // ���ʿ� �����Ǿ ���������� �̵��Ѵ�
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
                yield return null; // 1 ������ �纸�ϰڴ�.
            }
            level++;
            moveDirection = moveDirection == Dicrection.Right ? Dicrection.Left : Dicrection.Right;
        }

        // ���� �����Ҷ����� ī�޶� ���οø���.

        // �÷��̾� ����(GameOver UI ǥ��)
        // ���� �ܰ�


        // ���
        // Ÿ��Ʋ�� Touch!
        // 3,2,1���ӽ���

        // �ְ� ���� ��� (�� ������ص� �����Ǿ����)
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) waitNextBlock = false;

    }
}
