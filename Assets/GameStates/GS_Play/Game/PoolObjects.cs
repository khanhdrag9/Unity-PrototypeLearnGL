using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PoolEntry<T> where T : Component
{
    public T prefab;
    public List<T> list = new List<T>();
    public PoolEntry(T prefab)
    {   
        this.prefab = prefab;
    }
}

public class PoolObjects : Singleton<PoolObjects>
{
    ArrayList entries;

    protected override void Awake()
    {
        base.Awake();
        entries = new ArrayList();
    }
    public T GetFreeObject<T>(T prefab, Transform parent = null) where T : Component
    {
        PoolEntry<T> entry = null;
        foreach(var e in entries)
        {
            if(e is PoolEntry<T>)
            {
                var pe = e as PoolEntry<T>;
                if(pe != null && pe.prefab == prefab)
                {
                    entry = pe;
                    break;
                }
            }
        }

        if(entry == null)
        {
            entry = new PoolEntry<T>(prefab);
            entries.Add(entry);
        }
        
        return GetFreeObjectFromEntry<T>(entry, prefab, parent);
    }

    public List<T> GetObjectList<T>(T prefab) where T : Component
    {
        PoolEntry<T> result = null;
        foreach(var entry in entries)
        {
            if(entry is PoolEntry<T>)
            {
                var pe = entry as PoolEntry<T>;
                if(pe != null && pe.prefab == prefab)
                {
                    result = pe;
                    break;
                }
            }
        }

        if(result == null)
        {
            result = new PoolEntry<T>(prefab);
            entries.Add(result);
        }

        return result.list;
    }

    public void Clean()
    {
        entries.Clear();
    }

    T GetFreeObjectFromEntry<T>(PoolEntry<T> entry, T prefab, Transform parent) where T : Component
    {
        T result = null;
        foreach(T o in entry.list)
        {
            if(!o.gameObject.activeSelf)
            {
                result = o;
                break;
            }
        }

        if(result == null)
        {
            if(parent == null)parent = transform;
            result = Instantiate(prefab, parent);
            entry.list.Add(result);
        }

        result.gameObject.SetActive(true);
        return result;
    }
    
}
