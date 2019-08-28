using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPoolTag
{
    Steam,
    Ice,
    Ice3,
}

public class ObjectPooler: MonoBehaviour
{
    private static ObjectPooler m_instance;

    [SerializeField]
    private Pool[] pools;

    [Range(0, 100)]
    [SerializeField]
    private int maxWaterObjects = 20;

    private static Dictionary<string, Queue<(GameObject, System.Action)>> dictionary;
    public static string ICE_KEY = "Ice";
    public static string STEAM_KEY = "Steam";

    private void Awake()
    {
        if (m_instance) Destroy(this);
        else
        {
            m_instance = this;
            dictionary = new Dictionary<string, Queue<(GameObject, System.Action)>>();
            foreach (Pool pool in pools)
            {
                if (pool.tag != null && pool.prefab != null)
                {
                    CreateObjectPool(pool);
                }
            }
            CreateObjectPool(STEAM_KEY, Resources.Load<GameObject>("Prefabs/Steam"), maxWaterObjects); 
            CreateObjectPool(ICE_KEY, Resources.Load<GameObject>("Prefabs/Ice"), maxWaterObjects);
        }
    }

    #region CreateObjectPool
    void CreateObjectPool(string poolTag, GameObject sampleObj, int numberOfObjs)
    {
        if (dictionary.ContainsKey(poolTag))
        {
            Debug.LogWarning("Object Pool with tag, " + poolTag + ", already exists!");
            return;
        }

        Queue<(GameObject, System.Action)> pool = new Queue<(GameObject, System.Action)>();
        for (int i = 0; i < numberOfObjs; i++)
        {
            GameObject temp = GameObject.Instantiate(sampleObj);
            temp.SetActive(false);
            System.Action action = null;
            IPooledObject poolInterface = temp.GetComponent<IPooledObject>();
            if (poolInterface != null)
            {
                action = poolInterface.OnObjectSpawn;
            }
            pool.Enqueue((temp, action));
        }
        dictionary.Add(poolTag, pool);
    }

    void CreateObjectPool(Pool pool)
    {
        if (dictionary.ContainsKey(pool.tag))
        {
            Debug.LogWarning("Object Pool with tag, " + pool.tag + ", already exists!");
            return;
        }

        Queue<(GameObject, System.Action)> queue = new Queue<(GameObject, System.Action)>();
        for (int i = 0; i < pool.size; i++)
        {
            GameObject temp = GameObject.Instantiate(pool.prefab);
            temp.SetActive(false);
            System.Action action = null;
            IPooledObject poolInterface = temp.GetComponent<IPooledObject>();
            if (poolInterface != null)
            {
                action = poolInterface.OnObjectSpawn;
            }
            queue.Enqueue((temp, action));
        }
        dictionary.Add(pool.tag, queue);
    }
    #endregion

    #region SpawningFromPool

    private GameObject SpawnFromPool(string poolTag, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (!dictionary.ContainsKey(poolTag))
        {
            Debug.LogWarning("WARNING: Pool with tag, " + poolTag + ", does not exist in the current context!");
            return null;
        }

        (GameObject obj, System.Action action) = dictionary[poolTag].Dequeue();

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = scale;

        obj.SetActive(true);
        action.Invoke();

        dictionary[poolTag].Enqueue((obj, action));

        return obj;
    }

    GameObject SpawnFromPool(string poolTag, Vector3? position, Vector3? rotation, Vector3? scale = null)
    {
        return SpawnFromPool(poolTag, position ?? Vector3.zero, Quaternion.Euler(rotation ?? Vector3.zero), scale ?? Vector3.one);
    }

    public GameObject SpawnFromPool(string poolTag, Transform objTransform)
    {
        return SpawnFromPool(poolTag, objTransform.position, objTransform.rotation, objTransform.localScale);
    }

    public GameObject SpawnFromPool(ObjectPoolTag tag, Transform objTransform)
    {
        string poolTag = GetTag(tag);
        return SpawnFromPool(poolTag, objTransform.position, objTransform.rotation, objTransform.localScale);
    }

    public GameObject SpawnFromPool(ObjectPoolTag tag, Vector3? position, Vector3? rotation, Vector3? scale)
    {
        string poolTag = GetTag(tag);
        return SpawnFromPool(poolTag, position ?? Vector3.zero, Quaternion.Euler(rotation ?? Vector3.zero), scale ?? Vector3.one);
    }
    #endregion

    private string GetTag(ObjectPoolTag tag)
    {
        switch (tag)
        {
            case ObjectPoolTag.Ice3:
            {
                return "Ice 3";
            }
            case ObjectPoolTag.Ice:
            {
                return ICE_KEY;
            }
            case ObjectPoolTag.Steam:
            {
                return STEAM_KEY;
            }
            default:
            {
                return null;
            }
        }
    }
}
