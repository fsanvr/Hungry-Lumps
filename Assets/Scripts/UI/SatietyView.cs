using UnityEngine;

public class SatietyView : MonoBehaviour
{
    [SerializeField] private SatietySystem satietySystem;
    [SerializeField] private Transform progressBar;
    private float _targetValue;
    [SerializeField] private float updateSpeed = 0.8f;
    private void Awake()
    {
        if (satietySystem)
        {
            satietySystem.SatietyChanged.AddListener(SetTargetValue);
        }
    }

    private void OnDestroy()
    {
        if (satietySystem)
        {
            satietySystem.SatietyChanged.RemoveListener(SetTargetValue);
        }
    }
    
    private void SetTargetValue(float max, float before, float after)
    {
        _targetValue = after / max;
    }

    private void Update()
    {
        UpdateCurrentValue();
    }

    private void UpdateCurrentValue()
    {
        var scale = progressBar.localScale;
        scale.x = Mathf.Lerp(scale.x, _targetValue, updateSpeed * Time.deltaTime);
        progressBar.localScale = scale;
    }
}