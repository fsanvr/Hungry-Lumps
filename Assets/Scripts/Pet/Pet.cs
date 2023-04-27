using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class Pet : ScriptableObject
{
    [SerializeField] public string petName;
    [SerializeField] public Sprite sprite;
    [SerializeField] public AnimatorController animatorController;
    public SatietyComponent satietyComponent = new SatietyComponent();
    public MoveComponent moveComponent = new MoveComponent();
}