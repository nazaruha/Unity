using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    // ����, ���������� � ��������� Unity (�������, �� ����� �����)
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;

    // ����, ���������� �������� (� ������ ���)
    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos; // ������ 3� ��� ���������� launchPoint
    public GameObject projectile; // ��������� �� ��� ��������� Projectile
    public bool aimingMode; // ��� true, ���� �� �������� ������� � �������
    private void Awake() // ����������� �� ����� �������� �����
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false); // ������ ��'����. �� �� �������� �� ������ (��� ���'��� �����)
        launchPos = launchPointTrans.position;
    }
    // Start is called before the first frame update
    void OnMouseEnter()
    {
        //print("����� ������");
        launchPoint.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        //print("����� ������");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        // ������� ����� ������ �����, ���� �������� ����������� ��� ��������
        aimingMode = true;
        // �������� ������
        projectile = Instantiate(prefabProjectile);
        // �������� � ����� launchPoint
        projectile.transform.position = launchPos;
        // �������� ���� �����������
        projectile.GetComponent<Rigidbody>().isKinematic = true; // ��� �� ����� �� ����� �������� ������ ���� ������� Unity
        /*
         * �� ���� �������������� ������� ���� �� ������������ ���
           ��������� ���� ������� ��� � ���������� ������������, �� ����� ��������
           �������������� ����������� ������, ���������������� ������� ���
         */
    }

    private void OnMouseUp()
    {
        
    }
}
