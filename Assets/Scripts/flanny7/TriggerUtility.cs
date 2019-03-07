using System.Collections.Generic;
using UnityEngine;

public sealed class TriggerUtility
{
    public class Item
    {
        public GameObject gameObject;
        public float enterTime;

        public Item (GameObject _gameObject, float _enterTime)
        {
            this.gameObject = _gameObject;
            this.enterTime = _enterTime;
        }
    }

    private List<Item> items = null;

    public TriggerUtility()
    {
        this.items = new List<Item>();
    }

    public bool Add(GameObject _gameObject, float _enterTime)
    {
        if (this.Contain(_gameObject)) { return false; }

        var item = new Item(_gameObject, _enterTime);
        this.items.Add(item);

        return true;
    }

    public bool Remove(GameObject _gameObject)
    {
        for (var i = 0; i < this.items.Count; ++i)
        {
            if (this.items[i].gameObject.Equals(_gameObject))
            {
                this.items.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public GameObject FindGameObjectWithTag(string _tag)
    {
        for (var i = 0; i < this.items.Count; ++i)
        {
            if (this.items[i].gameObject.tag == _tag)
            {
                return this.items[i].gameObject;
            }
        }

        return null;
    }

    public GameObject[] FindGameObjectsWithTag(string _tag)
    {
        var results = new List<GameObject>();
        for (var i = 0; i < this.items.Count; ++i)
        {
            if (this.items[i].gameObject.tag == _tag)
            {
                results.Add(this.items[i].gameObject);
            }
        }

        if (results.Count <= 0) { results = null; }

        return results.ToArray();
    }

    public Item FindItemWithTag(string _tag)
    {
        for (var i = 0; i < this.items.Count; ++i)
        {
            if (this.items[i].gameObject.tag == _tag)
            {
                return this.items[i];
            }
        }

        return null;
    }

    public Item[] FindItemsWithTag(string _tag)
    {
        var results = new List<Item>();
        for (var i = 0; i < this.items.Count; ++i)
        {
            if (this.items[i].gameObject.tag == _tag)
            {
                results.Add(this.items[i]);
            }
        }

        if (results.Count <= 0) { results = null; }

        return results.ToArray();
    }

    public bool Contain(GameObject _gameObject)
    {
        for (var i = 0; i < this.items.Count; ++i)
        {
            if (this.items[i].gameObject.Equals(_gameObject))
            {
                return true;
            }
        }

        return false;
    }

    public bool ContainWithTag(string _tag)
    {
        for (var i = 0; i < this.items.Count; ++i)
        {
            try
            {
                if (this.items[i].gameObject.tag.Equals(_tag))
                {
                    return true;
                }
            }
            catch (MissingReferenceException e)
            {
                this.items.RemoveAt(i);
            }
        }

        return false;
    }

}