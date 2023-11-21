using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // шаблон дл€ створенн€ €блука
    public GameObject appleTree;
    // швидк≥сть руху €блун≥
    public float speed = 1f; // 1f = 1 метр, 10f = 10 метр≥в
    // в≥дстань на €к≥й повинен м≥н€тись напр€мок €блун≥
    public float leftAndRightEdge = 10f;
    // в≥рог≥дн≥сть випадковоњ зм≥ни напр€мку руху €блун≥
    public float chanceToChangeDirections = 0.1f; // 0.1f = 1%, 0.2 = 2% що рух зм≥нитьс€
    // частота створенн€ екземпл€р≥в €блук
    public float secondsBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // скидувати €блука раз в секунду    
    }

    // Update is called once per frame
    void Update() // викликаЇтьс€ 400 раз в секунду, або 30 €кщо слабий пристр≥й
    {
        // ѕросте перем≥щенн€
        Vector3 pos = transform.position; // поточна позиц≥€ €блун≥
        pos.x += speed * Time.deltaTime; // Time.deltaTime -> количество секунд, прошедших послеотображени€ предыдущего кадра
        transform.position = pos; // присваюЇмо нову позиц≥ю нашоњ €блон≥

        // «м≥на напр€мку
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // почати рух вправо
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // почати рух вл≥во
        }
    }

    private void FixedUpdate() // тут 50 виклик≥в в секунду, тут краще робити так≥ рандомн≥ €к≥сь зм≥ни
    {
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1; // м≥н€Їмо напр€мок руху (дл€ рандомноњ зм≥ни руху)
            /*
             * Random.value возвращает случайное число типа float между 0 и 1 (включа€ 0 и 1 как возможные значени€)
             */
        }
    }
}
