using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; // вовк одинак

    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this; // встановити посилання на об'єкт одинак
        // Отримати посилання на LineRenderer
        line = GetComponent<LineRenderer>();
        // Вимкнути LineRenderer поки він не знадобиться
        line.enabled = false;
        // Ініціалізувати список точок
        points = new List<Vector3>();
    }

    // Це властивість (тобто метод, маскуючись під поле)
    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                // Якщо поле _poi містить дійсне посилання, скине всі інші параметри в початкове положення
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    // Цей метод можна використати, щоб стерти лінію
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();   
    }

    public void AddPoint()
    {
        // Викликається для добавлення точки в лінії
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            // Якщо точка недостатньо далеко від попередньої, то просто вийти
            return;
        }
        if (points.Count == 0) // Якщо це точка запуску...
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS; // для визначення
            // ... добавити додатковий фрагмент лінії  щоб допомогти краще прицілитись в майбутньому
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            // Встановити перші дві точки
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            // Включити LineRenderer
            line.enabled = true;
        }
        else
        {
            // Звичайна послідовність добавлення точки
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    // Повернення місцеположення останньої добавленої точки
    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                // Якщо точок немає, то повернути Vector3.zero
                return Vector3.zero;
            }
            return points[points.Count - 1];
        }
    }

    private void FixedUpdate()
    {
        if (poi == null)
        {
            // якщо властивість poi має пусте значення, найти об'єкт який цікавить
            if (FollowCam.POI != null && FollowCam.POI.tag == "Projectile")
            {
                poi = FollowCam.POI;
            } else
            {
                return; // вийти, якщо зацікавлений об'єкт не знайдений
            }
        }
        // якщо зацікавлений об'єкт знайдений, то спробувати додати точку з його координатами в кожному FixedUpdate
        AddPoint();
        if (FollowCam.POI == null)
        {
            // Якщо FollowCam.POI == null, записати в poi значення null
            poi = null;
        }
    }
}
