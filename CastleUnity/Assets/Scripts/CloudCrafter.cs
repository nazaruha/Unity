using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; // кількість хмар
    public GameObject cloudPrefab; // шаблон для хмар
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; // Мін. масштаб кожної хмари
    public float cloudScaleMax = 3; // Макс. масштаб кожної хмари
    public float cloudSpeedMult = 0.5f; // Коефіцієнт швидкості хмар

    private GameObject[] cloudInstances;

    private void Awake()
    {
        // Створити масив для збережння всіх екземплярів хмар
        cloudInstances = new GameObject[numClouds];
        // Знайти батьківський ігровий об'єкт CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");
        // Створити в циклі задану кількість хмар
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // Створити екземпляр CloudPrefab
            cloud = Instantiate(cloudPrefab);
            // Вибрати місцеположення для хмар
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            // Масштабувати хмару
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // Менші хмари (з меншим значенням scaleU) мають бути ближче до землі
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            // Менші хмари мають бути далі
            cPos.z = 100 - 90 * scaleU;
            // Применити отриманні значення координат та масштаба до хмари
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            // Зробити хмару дочірнім по відношенню до anchor 
            cloud.transform.SetParent(anchor.transform);
            // Додати хмару в масив cloudInstances
            cloudInstances[i] = cloud;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     * смещает каждое облако чуть влево в каждом кадре. Когда облако оказывается левее точки cloudPosMin. х, оно переносится вправо, в позицию cloudPosMax.x.
     */
    void Update() 
    {
        // Обійти в циклі всі створенні хмари
        foreach (GameObject cloud in cloudInstances)
        {
            // Отримати масштаб та координати хмари
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            // Збільшити швидкість для ближчих хмар
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            // Якщо хмара змістилась дуже далеко вліво ...
            if (cPos.x <= cloudPosMin.x)
            {
                // Перемістити її далеко вправо
                cPos.x = cloudPosMax.x;
            }
            // Примінити нові координати до хмари
            cloud.transform.position = cPos;
        }
    }
}
