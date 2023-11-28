using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        // Коли в область тригера попадає щось, то перевіряєм чи є цей об'єкт Projectile
        if (other.gameObject.tag == "Projectile")
        {
            // Якщо це все таки знаряд, присваюємо полю goalMet значення true
            goalMet = true;
            // Також змінити альфа-канал коліру, щоб збільшити непрозорість
            Material mat = GetComponent<Renderer>().material; 
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
