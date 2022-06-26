using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dicrection { Right, Left}

public class GameManager : MonoBehaviour
{
    // �� ���ʿ��� ����
    // �����ʿ��� ����
    // �ݺ�

    // x ���������� ������ ���� 
    // x ��ġ -
    // �������� �����϶��� x ��ġ ���

    // y�� �ݺ��ɶ����� 1�� �ø�(1 ���� ����)
    public Block block;
    public Dicrection moveDirection = Dicrection.Right; // ���������� �����δ�.(���ʿ� �����ؼ� ���������� �̵�)
    public bool waitNextBlock = true; // true�� ���� ��ٸ���.
    private float initY = -3;
    private float blockHeight = 1;
    private float level = 0;
    public float offsetX = 3; // ���� �����Ǵ� ��ġ.
    public float blockSpeed = 3;
    private IEnumerator Start()
    {
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
                yield return null; // 1�纸�ϰڴ�.
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
