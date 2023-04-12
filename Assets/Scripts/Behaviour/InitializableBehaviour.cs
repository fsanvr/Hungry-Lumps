public abstract class InitializableBehaviour : BlockableBehaviour
{
    public void Init(LevelData data)
    {
        Unblock();
        MyInit(data);
    }
    
    private void Awake()
    {
        Block();
    }

    private void Start() { }

    private void Update()
    {
        if (IsBlocked())
        {
            return;
        }
        MyUpdate();
    }

    private void FixedUpdate()
    {
        if (IsBlocked())
        {
            return;
        }
        MyFixedUpdate();
    }

    protected abstract void MyInit(LevelData data);
    protected virtual void MyUpdate() { }
    protected virtual void MyFixedUpdate() { }
}