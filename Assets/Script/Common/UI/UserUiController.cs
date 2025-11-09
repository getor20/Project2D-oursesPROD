using UnityEngine;

public class UserUiController : MonoBehaviour
{
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _death;
    [SerializeField] private GameObject _menuButton;
    [SerializeField] private GameObject _resetButton;
    [SerializeField] private GameObject _exit;

    void Start()
    {
        _death.SetActive(false);
        _win.SetActive(false);
        _menuButton.SetActive(false);
        _exit.SetActive(true);
        _resetButton.SetActive(false);
    }

    public void OnDeath()
    {
        _death.SetActive(true);
        _win.SetActive(false);
        _menuButton.SetActive(true);
        _exit.SetActive(false);
        _resetButton.SetActive(true);
    }

    public void OnWin()
    {   
        _win.SetActive(true);
        _menuButton.SetActive(true);
        _death.SetActive(false);
        _exit.SetActive(false);
        _resetButton.SetActive(false);
    }
}
