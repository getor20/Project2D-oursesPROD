using UnityEngine;
using UnityEngine.UI;

public class ControllerStatBar : MonoBehaviour
{
    [SerializeField] private Image _imageHP;
    [SerializeField] private Image _imageStamina;

    public void UpdateHealthBar(float fillAmount)
    {
        Debug.Log($"Updating Health Bar: {fillAmount}");
        _imageHP.fillAmount = fillAmount;
        Debug.Log($"Health Bar fill amount set to: {_imageHP.fillAmount}");
    }

    public void UpdateStaminaBar(float fillAmount)
    {
        Debug.Log($"Updating Stamina Bar: {fillAmount}");
        _imageStamina.fillAmount = fillAmount;
        Debug.Log($"Stamina Bar fill amount set to: {_imageStamina.fillAmount}");
    }
}
