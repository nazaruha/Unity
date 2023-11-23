using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

public class Basket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �������� ������ ���������� ��������� ����� �� ����� �� ��������� Input
        Vector3 mousePos2D = Input.mousePosition; // ���������� Z � Input.mousePositon ������ ����� 0, ������ ��� �����, �� ����, ��� ��������� ���������.

        // ���������� Z ������ ��������, �� ������ � ����������� ����������� ����������� �������� �����
        mousePos2D.z = -Camera.main.transform.position.z; // ��� ������ ����������� ���������� Z � mousePos2D �������� ���������� Z ������� ������ � �������� ������. � ���� ���������� Z ������� ������ ����� -10, �������������� mousePos2D. z ������� �������� 10. ��� ����� �� �������� ������������ ������ ������� ScreenToWorldPoint(), ��� ������ �� ������ ������ ���������� ����� mousePos3D � ���������� ������������, ���������� ������� �� �� ��������� Z=0.

        // ��������� ����� � 2�-������ ������ ������ � 3�-����� ���������� ��� 
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // ScreenToWorldPoint() ����������� �������� ���������� mousePoint2D � ���������� � ���������� ������� ������������. ���� �������� mousePos2D. z �������� ������ 0, ����� mousePos3D ������� ���������� Z, ������ - 10 (���������� Z ������� ������).�������� mousePos2D.z �������� 10, �� ��������� ����� mousePos3D � ���������� ������������ �� �������� 10 ������ �� ������� ������, ��������� ���� ���� mousePos3D.z �������� �������� 0.� ���� Apple Picker ��� �� ����� �������� ��������, �� � ������� ����� ���������� Z ��������� ���� ����� ������ ����� ������ ����. ���� ��� - �� ��� ��� �������� �������, � ������� ��������� � �������� Camera. ScreenToWorldPoint() ����������� �� ���������� ��������� ��� Unity1

        // ������� ������� ������ �� � � ���������� � ��������� �����
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
}
