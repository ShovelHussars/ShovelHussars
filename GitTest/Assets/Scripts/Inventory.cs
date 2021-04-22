using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour {
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<Item> items = new List<Item>();

    public int space = 6;

    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            //ToDo Inventory full message here
            return false;
        }
        items.Add(item);
        OnItemChangedCallback.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        OnItemChangedCallback.Invoke();
    }
}
