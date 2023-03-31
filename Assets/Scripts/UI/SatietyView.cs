using UnityEngine;
using UnityEngine.Events;

public class SatietyView : MonoBehaviour
{
    [SerializeField] private Transform progressBar;
    private void Start()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Subscribe(player.SatietyChanged);
    }

    public void Subscribe(UnityEvent<float> @event)
    {
        @event.AddListener(SetValue);
    }

    public void Unsubscribe(UnityEvent<float> @event)
    {
        @event.RemoveListener(SetValue);
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