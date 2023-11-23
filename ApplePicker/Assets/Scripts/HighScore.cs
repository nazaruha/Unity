using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static public int score = 1000;

    private void Awake() // вызываетс€ при создании экземпл€ра класса HighScore(то есть Awake() всегда вызываетс€ перед Start()).
    {
        // якщо значенн€ HighScore вже ≥снуЇ в PlayerPrefs - прочитати його. PlayerPrefs Ч это словарь значений, на которые можно ссылатьс€ по ключам(то есть уникальным строкам)
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        // «берегети найкращий результат HighScore в хранилищ≥
        PlayerPrefs.SetInt("HighScore", score);
    }

    // Update is called once per frame
    void Update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = $"High Score: ${score}";
        // ќбновити HighScore в PlayerPrefs, €кщо необх≥дно
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
