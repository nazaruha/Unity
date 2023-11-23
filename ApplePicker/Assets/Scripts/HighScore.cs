using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static public int score = 1000;

    private void Awake() // ���������� ��� �������� ���������� ������ HighScore(�� ���� Awake() ������ ���������� ����� Start()).
    {
        // ���� �������� HighScore ��� ���� � PlayerPrefs - ��������� ����. PlayerPrefs � ��� ������� ��������, �� ������� ����� ��������� �� ������(�� ���� ���������� �������)
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        // ��������� ��������� ��������� HighScore � ���������
        PlayerPrefs.SetInt("HighScore", score);
    }

    // Update is called once per frame
    void Update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = $"High Score: ${score}";
        // �������� HighScore � PlayerPrefs, ���� ���������
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
