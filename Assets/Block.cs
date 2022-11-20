using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public float speed = 3;
    public float minX = -10;
    public float maxX = 10;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if(transform.position.x < minX || transform.position.x > maxX)
            GameManager.instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled == false)
            return;
        if (collision.collider.CompareTag("Player") == false)
            return;


        // 블럭 이동 멈추기.
        enabled = false;

        // 다음 블럭 나오게 하기. -> GameManager
        //FindObjectOfType<GameManager>().waitNextBlock = false;
        GameManager.instance.SetNextLevel();

        // 점수 추가 하기 -> GameManager
    }
}
