using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    // Точка, из которой будет "вылетать" предмет
    [SerializeField] private Transform _dropPoint;

    /// <summary>
    /// Создает экземпляры предмета в игровом мире.
    /// </summary>
    public void Drop(int itemID, int count)
    {
        // 1. Получаем полные данные о предмете, включая префаб, используя ItemDataRegistry
        ItemsStatBlock itemData = Inventory.ItemDataRegistry.GetItemData(itemID);

        if (itemData == null || itemData.PrefabObject == null)
        {
            Debug.LogError($"ItemDropper: Не удалось найти данные или PrefabObject для ID: {itemID}");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            // 2. Создаем экземпляр префаба
            GameObject droppedItem = Instantiate(itemData.PrefabObject, _dropPoint.position, Quaternion.identity);

            // 3. Добавляем физический эффект (для реалистичного "вылета")
            if (droppedItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Vector3 randomDropForce = new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(1f, 2f),
                    Random.Range(-0.5f, 0.5f));

                // Используем небольшую силу, чтобы предмет отскочил от игрока
                rb.AddForce(randomDropForce * 5f, ForceMode.Impulse);
            }

            Debug.Log($"Dropped: {itemData.Name} x1");
        }
    }
}