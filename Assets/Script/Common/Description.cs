using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;

    private void Awake()
    {
        if (_itemIcon == null || _itemName == null || _itemDescription == null)
        {
            Debug.LogError("Ошибка: Не все ссылки на UI-компоненты или панель установлены в Description.", this);
        }

        _itemIcon.preserveAspect = true;
        Hide();
    }

    /// <summary>
    /// Отображает описание предмета, используя предоставленные данные.
    /// </summary>
    public void Show(Sprite icon, string name, string desc)
    {
        if (_itemIcon == null || _itemName == null || _itemDescription == null) return;

        _itemIcon.sprite = icon;
        _itemIcon.enabled = (icon != null); 
        
        _itemName.text = name;
        _itemDescription.text = desc;   
    }

    public void Hide()
    {
        if (_itemIcon == null || _itemName == null || _itemDescription == null) return;

        _itemIcon.sprite = null;
        _itemIcon.enabled = false;
        _itemName.text = string.Empty;
        _itemDescription.text = string.Empty;
    }
}