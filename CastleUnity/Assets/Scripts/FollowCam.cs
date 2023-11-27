using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; // POI (Point Of Interest) - Силка на зацікавлений об'єкт (за яким камера буде слідувати)
    static public GameObject Castle; // Об'єкт нашого замку

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; // Бажана координата Z камери
    public float castlePositionX; // позиція замку по віссі Х

    private void Awake()
    {
        camZ = this.transform.position.z;    
    }

    // Start is called before the first frame update
    void Start()
    {
        Castle = GameObject.FindGameObjectWithTag("Castle");
        if (Castle != null)
        {
            castlePositionX = Castle.transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // якщо гравець сам захоче повернутись назад до рогатки, то просто натиснути мишку
        if (Input.GetMouseButtonDown(0))
        {
            POI = null;
        }
    }

    private void FixedUpdate()
    {
        //if (POI == null) return; // Вийти якщо об'єкт, за яким мусить слідкувати камера, пустий

        // отримути позицію зацікавленого об'єкту
        //Vector3 destination = POI.transform.position;

        Vector3 destination;
        if (POI == null)
            destination = Vector3.zero;
        else
        {
            // Отримати позицію зацікавленого об'єкту
            destination = POI.transform.position;
            // Якщо зацікавлений об'єкт є знаряддям, то впевнетись, він зупинився
            if (POI.tag == "Projectile")
            {
                // Якщо він не рухається (Edit -> Project Settings -> Physics -> Sleep Threshold = 0.02 (2 см в кадр, якщо проходить то спрацьовує функція IsSleeping -> true))
                if (POI.GetComponent<Rigidbody>().IsSleeping()) // якщо шарік вже зупинився
                {
                    // Повернути початкові налаштування розташування камери
                    POI = null;
                    // у наступному кадрі
                    return;
                }
            }
        }
        // Обмежує Х та Y мінімальними значеннями
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // Оприділяє точку між поточним місцеположенням камери і destination 
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Примусово встановити значення destination.z рівним camZ, щоб відвинути камеру подалі
        destination.z = camZ;
        if (destination.x >= castlePositionX)
        {
            // щоб камера не уходила далі за замок, якщо знаряд полетить далі
            destination.x = castlePositionX;
        }
        // Помістити камеру в позицію destination
        transform.position = destination;
        // Змінює розмір orthographicSize камери, щоб земля залишалась в полі зору
        Camera.main.orthographicSize = destination.y + 10;
    }
}
