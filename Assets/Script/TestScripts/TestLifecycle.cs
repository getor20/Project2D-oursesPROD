using UnityEngine;

public class TestLifecycle : MonoBehaviour
{
    //Awake вызывается первой 1 раз при запуске программы
    private void Awake()
    {
        Debug.Log("Вызов метода: Awake");
    }

    //OnEnable вызывается при запуске или деактивации объекта на котором закреплена погрома
    private void OnEnable()
    {
        Debug.Log("Вызов метода: OnEnable");
    }

    //Start вызывается 1 раз после Awake при запуске программы
    private void Start()
    {
        Debug.Log("Вызов метода: Start");
    }

    //Update вызывается при каждом кадре
    private void Update()
    {
        Debug.Log("Вызов метода: Update");
    }

    //FixedUpdate вызывается фиксированное количество в раз
    private void FixedUpdate()
    {
        Debug.Log("Вызов метода: FixedUpdate");
    }

    //LateUpdate вызывается после Update    
    private void LateUpdate()
    {
        Debug.Log("Вызов метода: LateUpdate");
    }

    //OnDisable вызывается при отключение объекта на котором закреплена погрома
    private void OnDisable()
    {
        Debug.Log("Вызов метода: OnDisable");
    }

    //OnDestroy вызывается при удаление объекта на котором закреплена погрома
    private void OnDestroy()
    {
        Debug.Log("Вызов метода: OnDestroy");
    }
}

