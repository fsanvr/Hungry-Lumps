using UnityEngine;

public class SatietyView : MonoBehaviour
{
    [SerializeField] private Transform progressBar;
    private float _incrementRemainder;
    [SerializeField] private float incrementRate = 0.24f;
    private void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            player.GetComponent<Player>().SatietyChanged.AddListener(AccumulateChanges);
        }
    }

    private void OnDestroy()
    {
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            player.GetComponent<Player>().SatietyChanged.RemoveListener(AccumulateChanges);
        }
    }
    
    private void AccumulateChanges(float normalizedValue)
    {
        _incrementRemainder += normalizedValue - progressBar.localScale.x;
    }

    private void Update()
    {
        ProcessIncrement();
    }

    private void ProcessIncrement()
    {
        if (Mathf.Abs(_incrementRemainder) == 0.0f)
        {
            return;
        }
        
        var currentScale = progressBar.localScale;
        var newScale = new Vector3(currentScale.x + GetIncrement(), currentScale.y, currentScale.z);
        progressBar.localScale = newScale;

        _incrementRemainder -= GetIncrement();
    }

    private float GetIncrement()
    {
        return Mathf.Min(
            Mathf.Abs(incrementRate) * Time.deltaTime, 
            Mathf.Abs(_incrementRemainder))
            * Mathf.Sign(_incrementRemainder);
    }
}