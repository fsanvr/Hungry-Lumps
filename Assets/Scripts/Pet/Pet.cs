using UnityEngine;

[CreateAssetMenu]
public class Pet : ScriptableObject
{
    [SerializeField] public string petName;
    [SerializeField] public Sprite sprite;
    [SerializeField] public RuntimeAnimatorController animatorController;
    public SatietyComponent satietyComponent = new SatietyComponent();
    public MoveComponent moveComponent = new MoveComponent();
}