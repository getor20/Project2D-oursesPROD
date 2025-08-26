using UnityEngine;

public class TestObject : MonoBehaviour
{
    // Awake() вызывается когда объект скрипта загружается
    void Awake()
    {
        Debug.Log("Вызван: Awake()");
    }

    // OnEnable() вызывается сразу после Awake() и когда объект становится активным
    void OnEnable()
    {
        Debug.Log("Вызван: OnEnable()");
    }

    // Start() вызывается после Awake()
    void Start()
    {
        Debug.Log("Вызван: Start()");
    }

    // Update() вызывается каждый кадр, частота вызова зависит от частоты кадров
    void Update()
    {
        Debug.Log("Вызван: Update()");
    }

    // FixedUpdate() вызывается с фиксированным интервалом по умолчанию 0.02 секунды
    void FixedUpdate()
    {
        Debug.Log("Вызван: FixedUpdate()");
    }

    // LateUpdate() вызывается каждый кадр, но после выполнения всех Update()
    void LateUpdate()
    {
        Debug.Log("Вызван: LateUpdate()");
    }

    // OnDisable() вызывается при деактивации объекта
    void OnDisable()
    {
        Debug.Log("Вызван: OnDisable()");
    }

    // OnDestroy() вызывается, когда объект уничтожается
    void OnDestroy()
    {
        Debug.Log("Вызван: OnDestroy()");
    }
}