using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    // поля, встановлені в інспекторі Unity (префаби, ще якусь шнягу)
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;

    // поля, встановлені динамічно (в процесі гри)
    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos; // зберігає 3х мірні координати launchPoint
    public GameObject projectile; // посилання на вже створений Projectile
    public bool aimingMode; // стає true, якщо ми цілимось мячиком в рогатці
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
        projectile.GetComponent<Rigidbody>().isKinematic = true; // щоб на нього не могли впливати фізичні сили двигуна Unity
        /*
         * то есть кинематическое твердое тело не перемещается под
           действием силы тяжести или в результате столкновений, но может вызывать
           автоматическое перемещение других, некинематических твердых тел
         */
    }

    private void OnMouseUp()
    {
        
    }
}
