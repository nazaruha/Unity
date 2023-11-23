using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int basketNums = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < basketNums; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
        }
    }

    public void AppleDestroyed()
    {
        // Видалити всі яблука
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple"); // Поверне масив всіх існуючих ігрових об'єктів з тегом "Apple"
        foreach (GameObject tGo in tAppleArray)
        {
            Destroy(tGo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
