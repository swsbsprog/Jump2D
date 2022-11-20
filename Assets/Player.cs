using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 jumpForce = new Vector2(0, 7);
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameStateType.Play)
            return;
        if (Time.timeScale == 0)
            return;

        if (Input.anyKeyDown) // 모바일에서 작동하도록 바꿈
            rb.AddForce(jumpForce, ForceMode2D.Impulse);

        //bool isDown = false;
        //foreach (var touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        isDown = true;
        //        break;
        //    }
        //}

        //if (isDown) // 모바일에서 작동하도록 바꿈
        //    rb.AddForce(jumpForce, ForceMode2D.Impulse);
    }
}
