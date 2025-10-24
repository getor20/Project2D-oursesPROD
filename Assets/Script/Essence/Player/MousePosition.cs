using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private WeaponRotation _weaponRotation;

    private void Update()
    {
        GetMousePosition();
    }

    public void GetMousePosition()
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _weaponRotation.SetRotationDirection(mousePos);
    }
}
