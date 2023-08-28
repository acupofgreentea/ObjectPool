using UnityEngine;
using UnityEngine.Events;

public class SinglePoolBase<T> : MonoBehaviour where T : MonoBehaviour 
{
    [SerializeField] private T prefab;
    [SerializeField] private int initialCount = 5;
    [SerializeField] private Transform poolParent;
    
    private ObjectPool<T> pool;
    
    public UnityAction<T> OnReturnToPool { get; set; }
    public UnityAction<T> OnGetFromPool { get; set; }
    
    protected virtual void Awake()
    {
        pool = new ObjectPool<T>(poolParent, prefab, initialCount);
    }
    
    public virtual T Get()
    {
        T t = pool.Get();
        OnGetFromPool?.Invoke(t);
        return t;
    }
    
    public virtual void ReturnToPool(T t)
    {
        OnReturnToPool?.Invoke(t);
        pool.ReturnToPool(t);
    }
}