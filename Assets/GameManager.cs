using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dicrection { Right, Left}

public class GameManager : MonoBehaviour
{
    // 블럭 왼쪽에서 스폰
    // 오른쪽에서 스폰
    // 반복

    // x 오른쪽으로 움직일 때는 
    // x 위치 -
    // 왼쪽으로 움직일때는 x 위치 양수

    // y는 반복될때마다 1씩 올림(1 블럭의 높이)
    public Block block;
    public Dicrection moveDirection = Dicrection.Right; // 오른쪽으로 움직인다.(왼쪽에 생성해서 오른쪽으로 이동)
    public bool waitNextBlock = true; // true인 동안 기다리자.
    private float initY = -3;
    private float blockHeight = 1;
    private float level = 0;
    public float offsetX = 3; // 블럭이 생성되는 위치.
    public float blockSpeed = 3;
    private IEnumerator Start()
    {
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
                yield return null; // 1양보하겠다.
            }
            level++;
            moveDirection = moveDirection == Dicrection.Right ? Dicrection.Left : Dicrection.Right;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) waitNextBlock = false;

    }
}
