using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Inventory: MonoBehaviour {
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    private Player player;
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<Item> items = new List<Item>();

    public int space = 6;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            //ToDo Inventory full message here
            return false;
        }
        items.Add(item);
        try
        {
            StartCoroutine(Coroutine());
            
            OnItemChangedCallback.Invoke();
            Debug.Log("Success!");
        }
        catch (Exception)
        {
            Debug.Log("Failed");
        }

        return true;
    }

    private IEnumerator Coroutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
    }

    public void Remove(Item item)
    {
        GameObject pref = ItemPrefHandler.instance.FindPref(item);
        Instantiate(pref, player.transform.position,player.transform.rotation);
        items.Remove(item);
        OnItemChangedCallback.Invoke();
    }

    public void Consume(Item item)
    {
        items.Remove(item);
        OnItemChangedCallback.Invoke();
    }
}
