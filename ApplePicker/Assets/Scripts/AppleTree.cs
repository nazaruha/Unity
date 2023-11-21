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
    public float chanceToChangeDirections = 0.1f;
    // частота створенн€ екземпл€р≥в €блук
    public float secondsBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // скидувати €блука раз в секунду    
    }

    // Update is called once per frame
    void Update()
    {
        // ѕросте перем≥щенн€
        Vector3 pos = transform.position; // поточна позиц≥€ €блун≥
        pos.x += speed * Time.deltaTime; // Time.deltaTime -> количество секунд, прошедших послеотображени€ предыдущего кадра
        transform.position = pos; // присваюЇмо нову позиц≥ю нашоњ €блон≥
    }
}
