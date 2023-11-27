using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; // POI (Point Of Interest) - Силка на зацікавлений об'єкт (за яким камера буде слідувати)

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; // Бажана координата Z камери

    private void Awake()
    {
        camZ = this.transform.position.z;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            destination = POI.transform.position;
            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping()) // якщо шарік вже зупинився
                {
                    POI = null;
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
        // Помістити камеру в позицію destination
        transform.position = destination;
        // Змінює розмір orthographicSize камери, щоб земля залишалась в полі зору
        Camera.main.orthographicSize = destination.y + 10;
    }
}
