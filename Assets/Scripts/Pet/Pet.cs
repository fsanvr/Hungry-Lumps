using UnityEngine;

[CreateAssetMenu]
public class Pet : ScriptableObject
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public Animator animator;
    public SatietyComponent satietyComponent = new SatietyComponent();
    public MoveComponent moveComponent = new MoveComponent();
}