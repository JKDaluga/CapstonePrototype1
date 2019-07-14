using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public Pool(string tag, GameObject prefab, int size)
    {
        this.tag = tag;
        this.prefab = prefab;
        this.size = size;
    }

    public string tag = null;
    public GameObject prefab = null;
    public int size = 0;
}