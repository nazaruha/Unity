using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    // ����, ���������� � ��������� Unity (�������, �� ����� �����)
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    // ����, ���������� �������� (� ������ ���)
    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos; // ������ 3� ��� ���������� launchPoint
    public GameObject projectile; // ��������� �� ��� ��������� Projectile
    public bool aimingMode; // ��� true, ���� �� �������� ������� � �������
    private Rigidbody projectileRigidbody;

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
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true; // ��� �� ����� �� ����� �������� ������ ���� ������� Unity
        /*
         * �� ���� �������������� ������� ���� �� ������������ ���
           ��������� ���� ������� ��� � ���������� ������������, �� ����� ��������
           �������������� ����������� ������, ���������������� ������� ���
         */
    }

    private void Update()
    {
        // ���� ������� �� � ����� ������������, �� �� ���������� ���
        if (!aimingMode) return;

        // �������� ������ ������ ���������� ��������� �����
        Vector3 mousePos2D = Input.mousePosition; // ��������� 2D ��������� ��������� ���� �� �����
        mousePos2D.z = -Camera.main.transform.position.z; // ���������� ����� ����������, �� ��� ����� ����� ��� ������ � 3D
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // ������������ ��������� 2D ��������� ���� � �������� ���������� � �������� ������� ���

        // ������ ������ ��������� �� launchPos �� mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos; // �������� �� ������� ����� �� ������ launchPoint
        // �������� mouseDelta ������� ���������� ��'���� Slingshot
        float maxMagnitude = this.GetComponent<SphereCollider>().radius; // ���� �������� �������� ��'���� Slingshot
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // ����������� ������ � ���� �������
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
            // ������ ����� ��������
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null; // �� ������ ��������� ���������, ��� �������� ���� ���� ��� ������ � ����� ��������� �� ����� ���������, ���� ���������� ����� ������� ������ ������
        }
    }
}
