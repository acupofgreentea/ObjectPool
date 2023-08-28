using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Transform _parent;
    private T _prefab;
    private int _initialSize;
    private Stack<T> _pool = new Stack<T>();
    
    public ObjectPool(Transform parent, T prefab, int initialSize)
    {
        _parent = parent;
        _prefab = prefab;
        _initialSize = initialSize;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            T t = GameObject.Instantiate(_prefab, _parent);
            t.gameObject.SetActive(false);
            _pool.Push(t);
        }
    }
    
    public T Get()
    {
        if (_pool.Count == 0)
        {
            T t = GameObject.Instantiate(_prefab);
            return t;
        }
        else
        {
            T t = _pool.Pop();
            t.gameObject.SetActive(true);
            t.transform.SetParent(null, false);
            return t;
        }
    }
    
    public void ReturnToPool(T t)
    {
        t.gameObject.SetActive(false);
        t.transform.SetParent(_parent, false);
        _pool.Push(t);
    }
    
    
}