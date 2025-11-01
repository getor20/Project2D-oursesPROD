using UnityEngine;

public class UserUiController : MonoBehaviour
{
    [SerializeField] private GameObject _death;
    [SerializeField] private GameObject _exit;

    void Start()
    {
        _death.SetActive(false);
        _exit.SetActive(true);
    }

    public void OnDeath()
    {
        _death.SetActive(true);
        _exit.SetActive(false);
    }
}
