using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeDayNight : MonoBehaviour
{
    [SerializeField]
    private Light2D _light2D;
    [SerializeField]
    private Gradient _lightGradient;
    [SerializeField]
    private AnimationCurve _lightIntensity;

    [SerializeField]
    private float _timeNightDay = 120f;

    private float _currentTime = 0;

    private void Awake()
    {
        if (_light2D is null)
        {
            Debug.LogError("component not initialized.");
            enabled = false;
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        _currentTime %= _timeNightDay;

        // 0 - начало дня
        // 25 - рассвет
        // 5 - полдень
        // 75 - закат
        // 1 - ночь

        float cyclePercentage = _currentTime / _timeNightDay;

        UpdateLight(cyclePercentage);
    }

    private void UpdateLight(float precent)
    {
        _light2D.color = _lightGradient.Evaluate(precent); 
        _light2D.intensity = _lightIntensity.Evaluate(precent);
    }
}
