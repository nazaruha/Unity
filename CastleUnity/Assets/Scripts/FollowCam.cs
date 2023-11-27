using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; // POI (Point Of Interest) - ����� �� ������������ ��'��� (�� ���� ������ ���� ��������)
    static public GameObject Castle; // ��'��� ������ �����

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; // ������ ���������� Z ������
    public float castlePositionX; // ������� ����� �� ��� �

    private void Awake()
    {
        camZ = this.transform.position.z;    
    }

    // Start is called before the first frame update
    void Start()
    {
        Castle = GameObject.FindGameObjectWithTag("Castle");
        if (Castle != null)
        {
            castlePositionX = Castle.transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ������� ��� ������ ����������� ����� �� �������, �� ������ ��������� �����
        if (Input.GetMouseButtonDown(0))
        {
            POI = null;
        }
    }

    private void FixedUpdate()
    {
        //if (POI == null) return; // ����� ���� ��'���, �� ���� ������ ��������� ������, ������

        // �������� ������� ������������� ��'����
        //Vector3 destination = POI.transform.position;

        Vector3 destination;
        if (POI == null)
            destination = Vector3.zero;
        else
        {
            // �������� ������� ������������� ��'����
            destination = POI.transform.position;
            // ���� ������������ ��'��� � ���������, �� ����������, �� ���������
            if (POI.tag == "Projectile")
            {
                // ���� �� �� �������� (Edit -> Project Settings -> Physics -> Sleep Threshold = 0.02 (2 �� � ����, ���� ��������� �� ��������� ������� IsSleeping -> true))
                if (POI.GetComponent<Rigidbody>().IsSleeping()) // ���� ���� ��� ���������
                {
                    // ��������� �������� ������������ ������������ ������
                    POI = null;
                    // � ���������� ����
                    return;
                }
            }
        }
        // ������ � �� Y ���������� ����������
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // �������� ����� �� �������� �������������� ������ � destination 
        destination = Vector3.Lerp(transform.position, destination, easing);
        // ��������� ���������� �������� destination.z ����� camZ, ��� �������� ������ �����
        destination.z = camZ;
        if (destination.x >= castlePositionX)
        {
            // ��� ������ �� ������� ��� �� �����, ���� ������ �������� ���
            destination.x = castlePositionX;
        }
        // �������� ������ � ������� destination
        transform.position = destination;
        // ����� ����� orthographicSize ������, ��� ����� ���������� � ��� ����
        Camera.main.orthographicSize = destination.y + 10;
    }
}
