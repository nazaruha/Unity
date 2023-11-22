using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f; // статичні поля не відображаються в Інспекторі
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            // якщо даний екземпляр виходить за межі по осі Y, то видаляємо його, тим самим очищаючи пам'ять
            Destroy(this.gameObject);
        }
    }
}
