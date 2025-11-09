using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    // Массив всех объектов (врагов)
    [SerializeField] GameObject[] gameObjects;

    // ОБЩЕЕ КОЛИЧЕСТВО объектов в массиве. Не должно меняться после Start.
    [SerializeField] private int totalObjectCount;
    // НОВАЯ ПЕРЕМЕННАЯ: Количество объектов, которые АКТИВНЫ в данный момент.
    [field: SerializeField] public int ActiveEnemyCount { get; private set; }

    private void Start()
    {
        // 1. Устанавливаем общее количество объектов.
        totalObjectCount = gameObjects.Length;
        // 2. Изначально активное количество равно общему количеству, если все активны.
        ActiveEnemyCount = totalObjectCount;

        Debug.Log($"Общее количество объектов: {totalObjectCount}");
    }

    private void Update()
    {
        // Выполняем проверку и обновляем счетчик активных объектов
        CountActiveEnemies();
    }

    private void CountActiveEnemies()
    {
        int currentActiveCount = 0;

        foreach (GameObject go in gameObjects)
        {
            // Проверяем, если объект АКТИВЕН в иерархии
            if (go != null && go.activeInHierarchy)
            {
                currentActiveCount++;
            }
            // Опционально: Если вы хотите узнать, какой объект стал неактивным
            else if (go != null && !go.activeInHierarchy)
            {
                // Это просто Debug, чтобы видеть, когда враг деактивируется
                // Debug.Log($"Объект {go.name} стал неактивным.");
            }
        }

        // Обновляем публичное поле активного количества
        ActiveEnemyCount = currentActiveCount;

        // Это будет выводиться каждый кадр, пока вы не удалите Debug.Log
        // Debug.Log($"Активных объектов осталось: {activeEnemyCount}");
    }
}