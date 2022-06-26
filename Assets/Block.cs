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
}
