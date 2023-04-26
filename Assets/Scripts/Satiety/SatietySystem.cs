using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SatietySystem : InitializableBehaviour, Buffable
{
    public readonly SatietyUpdateEvent SatietyChanged = new SatietyUpdateEvent();
    public float CurrentSatiety { get; private set; }
    
    [SerializeField] private LevelEndSystem levelEndSystem;
    [SerializeField] private float decreaseSpeed = 0.5f;
    private readonly List<Buff> _buffs = new List<Buff>();
    private float _maxSatiety;
    
    protected override void MyInit(LevelData data)
    {
        _maxSatiety = data.pet.satietyComponent.GetSatiety();
        CurrentSatiety = _maxSatiety;
    }

    public void AddBuff(Buff buff)
    {
        _buffs.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        _buffs.Remove(buff);
    }

    public void FillSatiety()
    {
        var before = CurrentSatiety;
        CurrentSatiety = _buffs.Aggregate(
            _maxSatiety,
            (current, buff) => buff.ApplyByName(current, "SatietySystem.MaxSatiety")
            );
        
        Changed(before, CurrentSatiety);
    }

    public void DecreaseSatiety(float value)
    {
        var before = CurrentSatiety;
        CurrentSatiety -= value;
        
        Changed(before, CurrentSatiety);
    }

    protected override void MyUpdate()
    {
        base.MyUpdate();
        DecreaseSatietyByTime();
        CheckSatiety();
    }

    private void DecreaseSatietyByTime()
    {
        var before = CurrentSatiety;
        var speed = _buffs.Aggregate(
            decreaseSpeed,
            (current, buff) => buff.ApplyByName(current, "SatietySystem.DecreaseSpeed")
            );
        CurrentSatiety -= speed * Time.deltaTime;
        
        Changed(before, CurrentSatiety);
    }

    private void CheckSatiety()
    {
        if (SatietyIsOver())
        {
            LevelLose();
        }
    }

    private bool SatietyIsOver()
    {
        return CurrentSatiety <= 0.0f;
    }

    private void LevelLose()
    {
        if (levelEndSystem.IsLevelEnded())
        {
            return;
        }
        
        levelEndSystem.LevelLose();
    }
    
    private void Changed(float before, float after)
    {
        SatietyChanged.Invoke(_maxSatiety, before, after);
    }
}