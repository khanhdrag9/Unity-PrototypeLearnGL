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
    List<PoolEntry<Component>> entries;

    protected override void Awake()
    {
        base.Awake();
        entries = new List<PoolEntry<Component>>();
    }
    public T GetFreeObject<T>(T prefab, Transform parent = null) where T : Component
    {
        PoolEntry<Component> entry = null;
        foreach(PoolEntry<Component> e in entries)
        {
            if(e.prefab == prefab)
            {
                entry = e;
                break;
            }
        }
        
        if(entry == null)
        {
            entry = new PoolEntry<Component>(prefab);
            entries.Add(entry);
        }
        
        return GetFreeObjectFromEntry<T>(entry, prefab, parent);
    }

    public List<T> GetObjectList<T>(T prefab) where T : Component
    {
        PoolEntry<Component> result = null;
        foreach(PoolEntry<Component> entry in entries)
        {
            if(entry.prefab == prefab)
            {
                result = entry;
                break;
            }
        }

        if(result == null)
        {
            result = new PoolEntry<Component>(prefab);
            entries.Add(result);
        }

        return result.list as List<T>;
    }

    public void Clean()
    {
        entries.Clear();
    }

    T GetFreeObjectFromEntry<T>(PoolEntry<Component> entry, T prefab, Transform parent) where T : Component
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
