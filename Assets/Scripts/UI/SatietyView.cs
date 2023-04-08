using UnityEngine;

public class SatietyView : MonoBehaviour
{
    [SerializeField] private Transform progressBar;
    private float _targetValue;
    [SerializeField] private float updateSpeed = 0.8f;
    private void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            player.GetComponent<Player>().SatietyChanged.AddListener(SetTargetValue);
        }
    }

    private void OnDestroy()
    {
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            player.GetComponent<Player>().SatietyChanged.RemoveListener(SetTargetValue);
        }
    }
    
    private void SetTargetValue(float normalizedValue)
    {
        _targetValue = normalizedValue;
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