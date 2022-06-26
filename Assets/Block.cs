using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public float speed = 3;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled == false)
            return;
        if (collision.collider.CompareTag("Player") == false)
            return;


        // �� �̵� ���߱�.
        enabled = false;

        // ���� �� ������ �ϱ�. -> GameManager
        //FindObjectOfType<GameManager>().waitNextBlock = false;
        GameManager.instance.SetNextLevel();

        // ���� �߰� �ϱ� -> GameManager
    }
}
