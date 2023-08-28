using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiplePoolBase<T> : MonoBehaviour where T : MonoBehaviour 
{
    [SerializeField] private List<T> prefabs;
    [SerializeField] private int initialCount = 5;
    [SerializeField] private Transform poolParent;
    
    private MultipleObjectPool<T> pool;
    
    public UnityAction<T> OnReturnToPool { get; set; }
    public UnityAction<T> OnGetFromPool { get; set; }
    
    protected virtual void Awake()
    {
        pool = new MultipleObjectPool<T>(poolParent, prefabs, initialCount);
    }
    
    public virtual T Get(T type)
    {
        T t = pool.Get(type);
        OnGetFromPool?.Invoke(t);
        return t;
    }
    
    public virtual void ReturnToPool(T t)
    {
        OnReturnToPool?.Invoke(t);
        pool.ReturnToPool(t);
    }
}