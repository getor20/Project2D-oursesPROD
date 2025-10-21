using UnityEngine;
using UnityEngine.UI;

public class ControllerStatBar : MonoBehaviour
{
    [SerializeField] private Image _imageHP;
    [SerializeField] private Image _imageStamina;

    public void UpdateHealthBar(float fillAmount)
    {
        _imageHP.fillAmount = fillAmount;
    }

    public void UpdateStaminaBar(float fillAmount)
    {
        _imageStamina.fillAmount = fillAmount;
    }
}
