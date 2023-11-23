using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

public class Basket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Отримати поточні координати вказівника мишки на екрані за допомогою Input
        Vector3 mousePos2D = Input.mousePosition; // Координата Z в Input.mousePositon всегда равна 0, потому что экран, по сути, это двумерная плоскость.

        // Координата Z камери оприділяє, як далеко в трьохмірному пространстві знаходиться вказівник мишки
        mousePos2D.z = -Camera.main.transform.position.z; // Эта строка присваивает координате Z в mousePos2D значение координаты Z главной камеры с обратным знаком. В игре координата Z главной камеры равна -10, соответственно mousePos2D. z получит значение 10. Тем самым мы сообщаем последующему вызову функции ScreenToWorldPoint(), как далеко от камеры должна находиться точка mousePos3D в трехмерном пространстве, фактически помещая ее на плоскость Z=0.

        // Перетворю точку в 2х-вимірній площині екрану в 3х-вимірні координати гри 
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // ScreenToWorldPoint() преобразует экранные координаты mousePoint2D в координаты в трехмерном игровом пространстве. Если значение mousePos2D. z оставить равным 0, точка mousePos3D получит координату Z, равную - 10 (координата Z главной камеры).Присвоив mousePos2D.z значение 10, мы поместили точку mousePos3D в трехмерном пространстве на удалении 10 метров от главной камеры, благодаря чему поле mousePos3D.z получило значение 0.В игре Apple Picker это не имеет большого значения, но в будущих играх координата Z указателя мыши будет играть более важную роль. Если что - то для вас осталось неясным, я советую заглянуть в описание Camera. ScreenToWorldPoint() руководства по разработке сценариев для Unity1

        // Переміщає корзину вздовж осі Х в координату Х вказівника мишки
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision) // Даний метод викликається, коли з корзиною стикається якийсь інший об'єкт. У Collision передається об'єкт який стикнувся з корзиною
    {
        // Найти яблуко, яке попало на корзину
        GameObject collidedWith = collision.gameObject; // Эта строка присваивает локальной переменной collidedWith ссылку на игровой объект, столкнувшийся с корзиной.
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
        }
    }
}
