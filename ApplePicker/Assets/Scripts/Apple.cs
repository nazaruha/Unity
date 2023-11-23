using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f; // ������� ���� �� ������������� � ���������
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            // ���� ����� ��������� �������� �� ��� �� �� Y, �� ��������� ����, ��� ����� �������� ���'���
            Destroy(this.gameObject);

            // �������� ��������� �� ��������� ApplePicker (Script) �������� ������ Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            // ��������� �������� ����� AppleDestoyed() � apScript
            apScript.AppleDestroyed();
        }
    }
}
