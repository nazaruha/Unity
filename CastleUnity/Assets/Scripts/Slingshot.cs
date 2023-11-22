using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    // поля, встановлені в інспекторі Unity (префаби, ще якусь шнягу)
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    // поля, встановлені динамічно (в процесі гри)
    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos; // зберігає 3х мірні координати launchPoint
    public GameObject projectile; // посилання на вже створений Projectile
    public bool aimingMode; // стає true, якщо ми цілимось мячиком в рогатці
    private Rigidbody projectileRigidbody;

    private void Awake() // запускається ще перед запуском сцени
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false); // ігнорує об'єкти. Їх ми візуально не бачимо (але пам'ять займає)
        launchPos = launchPointTrans.position;
    }
    // Start is called before the first frame update
    void OnMouseEnter()
    {
        //print("Мишку навели");
        launchPoint.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        //print("Мишку відвели");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        // Гравець нажав кнопку мишки, коли вказівник знаходиться над рогаткою
        aimingMode = true;
        // Створити знаряд
        projectile = Instantiate(prefabProjectile);
        // Помістити в точку launchPoint
        projectile.transform.position = launchPos;
        // Створити його кінематичним
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true; // щоб на нього не могли впливати фізичні сили двигуна Unity
        /*
         * то есть кинематическое твердое тело не перемещается под
           действием силы тяжести или в результате столкновений, но может вызывать
           автоматическое перемещение других, некинематических твердых тел
         */
    }

    private void Update()
    {
        // Якщо рогатка не в режимі прицілювання, то не виконувати код
        if (!aimingMode) return;

        // Отримати поточні екранні координати вказівника мишки
        Vector3 mousePos2D = Input.mousePosition; // отримання 2D координат положення миші на екрані
        mousePos2D.z = -Camera.main.transform.position.z; // добавляємо третю координату, на яку вказує мишка для роботи з 3D
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // перетворення отриманих 2D координат миші у тривимірні координати у світовому просторі гри

        // Знайти різницю координат між launchPos та mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos; // виходить це відстань мишки від нашого launchPoint
        // Обмежити mouseDelta радіусом коллайдера об'єкта Slingshot
        float maxMagnitude = this.GetComponent<SphereCollider>().radius; // шукає колайдер всередині об'єкта Slingshot
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Передвинути снаряд в нову позицію
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
            // Кнопка мишки відпущена
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null; // не удаляє створений екземпляр, але звільнить дане поле для запису в нього посилання на інший екземпляр, коли користувач рішить зробити другий постріл
        }
    }
}
