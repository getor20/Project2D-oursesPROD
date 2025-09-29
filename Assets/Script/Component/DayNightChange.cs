using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightChange : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    [SerializeField] private Gradient _lightGradient;
    [SerializeField] private AnimationCurve _lightIntensity;
    [SerializeField] private float _timeNightDay;
    [SerializeField] private float _currentTime;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        _currentTime %= _timeNightDay;

        float cyclePercentage = _currentTime / _timeNightDay;

        UpdateLight(cyclePercentage);
    }

    private void UpdateLight(float precent)
    {
        _light2D.color = _lightGradient.Evaluate(precent); 
        _light2D.intensity = _lightIntensity.Evaluate(precent);
    }
}
