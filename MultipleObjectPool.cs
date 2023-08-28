using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectPool<T> where T : MonoBehaviour
{
    private Transform _parent;
    private List<T> _prefabs;
    private int _initialSize;
    private List<T> _pool = new List<T>();
    
    public MultipleObjectPool(Transform parent, List<T> prefabs, int initialSize)
    {
        _parent = parent;
        _prefabs = new List<T>(prefabs);
        _initialSize = initialSize;
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (T _prefab in _prefabs)
        {
            for (int i = 0; i < _initialSize; i++)
            {
                T t = GameObject.Instantiate(_prefab, _parent);
                t.gameObject.SetActive(false);
                _pool.Add(t);
            }
        }
    }
    
    public T Get(T type)
    {
        var t1 = _pool.Find(x => type.GetType() == x.GetType());
        
        if (_pool.Count == 0 || t1 == null)
        {
            int index = _prefabs.IndexOf(type);
            T t = GameObject.Instantiate(_prefabs[index]);
            return t;
        }
        else
        {
            _pool.Remove(t1);
            t1.gameObject.SetActive(true);
            t1.transform.parent = null;
            return t1;
        }
    }
    
    public void ReturnToPool(T t)
    {
        t.gameObject.SetActive(false);
        t.transform.parent = _parent;
        _pool.Remove(t);
    }
    
    
}