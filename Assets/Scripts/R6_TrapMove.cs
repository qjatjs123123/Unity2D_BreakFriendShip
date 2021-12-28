using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R6_TrapMove : MonoBehaviour
{
    Vector3 pos; //������ġ

    [SerializeField]
    private float delta = 2.0f; // �̵������� �ִ밪
    [SerializeField]
    private float speed = 3.0f; // �̵��ӵ�
    [SerializeField]
    private bool isXmove = false;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if (isXmove) { moveX(); }
        else moveY();
    }

    private void moveX() {
        Vector3 vectorPos = pos;
        vectorPos.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = vectorPos;
    }
    private void moveY() {
        Vector3 vectorPos = pos;
        vectorPos.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = vectorPos;
    }
}
