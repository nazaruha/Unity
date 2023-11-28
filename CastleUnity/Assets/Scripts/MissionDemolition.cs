using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle, // гра ще не почалась
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // Скритий об'єкт одинак

    [Header("Set in Inspector")]
    public Text uitLevel; // посилання на об'єкт UIText_Level
    public Text uitShots; // посилання на об'єкт UIText_Shots
    public Text uitButton; // посилання на дочірній об'єкт Text в UIButton_View
    public Vector3 castlePos; // місцеположення замку
    public GameObject[] castles; // масив замків

    [Header("Set Dynamically")]
    public int level; // Поточний рівень
    public int levelMax; // Кількість рівнів
    public int shotsTaken; // Скільки було вистрілів
    public GameObject castle; // Поточний замок
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; // режив FollowCam

    // Start is called before the first frame update
    void Start()
    {
        S = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // Знищити попередній замок, якщо він існує
        if (castle != null)
        {
            Destroy(castle);
        }

        // Знищити попередні знаряди, якщо вони існують
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        if (gos.Length > 0)
        {
            foreach (var projectile in gos)
            {
                Destroy(projectile);
            }
        }

        // Створити новий замок
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // Перезавантажити камеру в початкову позицію
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        // Скинути ціль
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        // Показати данні в елементах UI
        uitLevel.text = $"Level: {level + 1} of {levelMax}";
        uitShots.text = $"Shots Taken: {shotsTaken}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        // Перевірити завершення рівня
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            // Змінити режим, щоб припинити перевірку завершення рівня
            mode = GameMode.levelEnd;
            // Зменшити масштаб
            SwitchView("Show Both");
            // Почати новий рівень через 2 секунди
            Invoke("NextLevel", 2f);
        }
    }
    
    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    // статичний метод, який дозволяє з любого коду збільшити shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
