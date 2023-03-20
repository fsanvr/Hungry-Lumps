using UnityEngine;
using UnityEngine.Events;

public class SatietyView : MonoBehaviour
{
    [SerializeField] private Transform progressBar;
    private bool _subscribed;
    private void Update()
    {
        TrySubscribe();
    }

    public void Subscribe(UnityEvent<float> @event)
    {
        @event.AddListener(SetValue);
    }

    public void Unsubscribe(UnityEvent<float> @event)
    {
        @event.RemoveListener(SetValue);
    }

    private void TrySubscribe()
    {
        if (_subscribed)
        {
            return;
        }
        
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            var playerComponent = player.GetComponent<Player>();
            Subscribe(playerComponent.SatietyChanged);
            _subscribed = true;
        }
    }

    private void SetValue(float normalizedValue)
    {
        progressBar.localScale = new Vector3(normalizedValue, 1.0f, 1.0f);
    }

    private void OnDestroy()
    {
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            var playerComponent = player.GetComponent<Player>();
            Unsubscribe(playerComponent.SatietyChanged);
        }
    }
}