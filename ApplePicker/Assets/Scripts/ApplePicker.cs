using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int basketNums = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    [Header("Set Dynamically")]
    static public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Time.timeScale = 1f;

        basketList = new List<GameObject>();
        for (int i = 0; i < basketNums; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleDestroyed()
    {
        // �������� �� ������
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple"); // ������� ����� ��� �������� ������� ��'���� � ����� "Apple". �� ������������� ��������������� FindGameObjectsWithTag() ����� �������� Update() �� FixedUpdate(), ��� ��� �� �� � ���� �������� ����� �������� ������� � ��� ���� ���������� � ���� �����, �� �����
        foreach (GameObject tGo in tAppleArray)
        {
            Destroy(tGo);
        }

        // �������� ���� �������
        // �������� ������ �������� ������� � basketList
        int lastBasketIndex = basketList.Count - 1;
        // �������� ��������� �� ����� ������� ��'��� Basket
        GameObject basketToDelete = basketList[lastBasketIndex];
        // ��������� ������� � ������ basketList �� �������� ��� ������� ��'��� � ����
        basketList.RemoveAt(lastBasketIndex);
        Destroy(basketToDelete);

        // ���� ������ ���� ����� - ������������� ���
        if (basketList.Count == 2)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            SceneManager.LoadScene("_Scene_GameOver");
            //SceneManager.LoadScene("_Scene_0"); // ������� ������ �����
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
