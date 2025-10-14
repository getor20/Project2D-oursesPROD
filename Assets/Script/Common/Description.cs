using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;

    [Tooltip("Объект, который нужно активировать/деактивировать (обычно это родительская панель)")]
    [SerializeField] private GameObject _panel;

    private void Awake()
    {
        if (_itemIcon == null || _itemName == null || _itemDescription == null || _panel == null)
        {
            Debug.LogError("Ошибка: Не все ссылки на UI-компоненты или панель установлены в Description.", this);
        }

        _itemIcon.preserveAspect = true;

        _itemIcon.sprite = null;
        _itemName.text = "";
        _itemDescription.text = "";

        _itemIcon.enabled = false;
    }

    /// <summary>
    /// Отображает описание предмета, используя предоставленные данные.
    /// </summary>
    public void Show(Sprite icon, string name, string desc)
    {
        if (_itemIcon == null || _itemName == null || _itemDescription == null) return;
        _panel.SetActive(true);

        _itemIcon.sprite = icon;
        _itemIcon.enabled = (icon != null); 
        
        _itemName.text = name;
        _itemDescription.text = desc;
    }
}