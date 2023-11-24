using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // ������ ��� ��������� ������
    public GameObject applePrefab;
    // �������� ���� �����
    public float speed = 1f; // 1f = 1 ����, 10f = 10 �����
    // ������� �� ��� ������� ������� �������� �����
    public float leftAndRightEdge = 10f;
    // ��������� ��������� ���� �������� ���� �����
    public float chanceToChangeDirections = 0.1f; // 0.1f = 1%, 0.2 = 2% �� ��� ��������
    // ������� ��������� ���������� �����
    public float secondsBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // ��������� ������ ��� � �������    
        Invoke("DropApple", 2f); // ��������� ������� ��������� ����� 2 ������� ���� ��������� �����
    }

    // ��������� ������
    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    // Update is called once per frame
    void Update() // ����������� 400 ��� � �������, ��� 30 ���� ������ �������
    {

    }

    private void FixedUpdate() // ��� 50 ������� � �������, ��� ����� ������ ��� ������� ���� ����
    {
        MoveRandomly();
    }

    void MoveRandomly()
    {
        // ������ ����������
        Vector3 pos = transform.position; // ������� ������� �����
        pos.x += speed * Time.deltaTime; // Time.deltaTime -> ���������� ������, ��������� ����� ����������� ����������� �����
        transform.position = pos; // ���������� ���� ������� ���� �����

        // ���� ��������
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // ������ ��� ������
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // ������ ��� ����
        }

        if (Random.value < chanceToChangeDirections)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        speed *= -1; // ������ �������� ���� (��� �������� ���� ����)
        /*
         * Random.value ���������� ��������� ����� ���� float ����� 0 � 1 (������� 0 � 1 ��� ��������� ��������)
         */
    }
}
