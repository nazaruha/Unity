using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int basketNums = 3;
    public float basketBottomY = -17f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public GameObject CanvasGameOver;

    [Header("Set Dynamically")]
    static public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        
        //приховуємо курсор мишки
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        basketList = new List<GameObject>();
        for (int i = 0; i < basketNums; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleDestroyed()
    {
        // Видалити всі яблука
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple"); // Поверне масив всіх існуючих ігрових об'єктів з тегом "Apple". Не рекомендується використовувати FindGameObjectsWithTag() метод всередині Update() чи FixedUpdate(), але так як ми в даній ситуації будем видаляти корзину і гра буде зупинятись в даній точці, то можна
        foreach (GameObject tGo in tAppleArray)
        {
            Destroy(tGo);
        }

        // Видалити одну корзину
        // Отримати індекс останньої корзини в basketList
        int lastBasketIndex = basketList.Count - 1;
        // Отримати посилання на даний ігровий об'єкт Basket
        GameObject basketToDelete = basketList[lastBasketIndex];
        // Виключити корзину зі списку basketList та видалити сам ігровий об'єкт з ігри
        basketList.RemoveAt(lastBasketIndex);
        Destroy(basketToDelete);

        // Якщо корзин немає більше - перезапустити гру
        if (basketList.Count == 2)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            print("SetActive GameOverPopup");
            CanvasGameOver.SetActive(true);
            print("SetActivated GameOverPopup");
            //GameObject canvasGameOver = Instantiate(CanvasGameOver);
            //canvasGameOver.SetActive(true);
            //SceneManager.LoadScene("_Scene_GameOver");
            //SceneManager.LoadScene("_Scene_0"); // Загружає наново сцену
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
