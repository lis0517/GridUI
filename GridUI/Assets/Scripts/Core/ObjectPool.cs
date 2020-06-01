using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public enum EStartPoolMode
    {
        Awake, Start, Call
    }

    [System.Serializable]
    public class StartupPool
    {
        public int size;
        public GameObject prefab;
    }

    private static ObjectPool m_Instance;

    //생성된 것을 임시로 저장하는 리스트
    private static List<GameObject> m_TempList = new List<GameObject>();

    //
    private Dictionary<GameObject, List<GameObject>> m_PooledObjects = new Dictionary<GameObject, List<GameObject>>();

    private Dictionary<GameObject, GameObject> m_SpawnedObjects = new Dictionary<GameObject, GameObject>();

    private bool startupPoolsCreated;

    public EStartPoolMode startupPoolMode;
    public StartupPool[] startupPools;

    public static ObjectPool instance
    {
        get
        {
            if (m_Instance != null)
                return m_Instance;

            m_Instance = Object.FindObjectOfType<ObjectPool>();
            if (m_Instance != null)
                return m_Instance;

            var obj = new GameObject("ObjectPool");
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            m_Instance = obj.AddComponent<ObjectPool>();
            return m_Instance;
        }
    }

    private void Awake()
    {
        m_Instance = this;
        if (startupPoolMode == EStartPoolMode.Awake)
            CreateStartupPools();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startupPoolMode == EStartPoolMode.Start)
            CreateStartupPools();
    }

    public static void CreateStartupPools()
    {
        if (!instance.startupPoolsCreated)
        {
            instance.startupPoolsCreated = true;
            var pools = instance.startupPools;
            if (pools != null && pools.Length > 0)
                for (int i = 0; i < pools.Length; ++i)
                    CreatePool(pools[i].prefab, pools[i].size);
        }
    }

    public static void CreatePool<T>(T prefab, int initialPoolSize) where T : Component
    {
        CreatePool(prefab.gameObject, initialPoolSize);
    }
    public static void CreatePool(GameObject prefab, int initialPoolSize)
    {
        if (prefab != null && !instance.m_PooledObjects.ContainsKey(prefab))
        {
            var list = new List<GameObject>();
            instance.m_PooledObjects.Add(prefab, list);

            if (initialPoolSize > 0)
            {
                bool active = prefab.activeSelf;
                prefab.SetActive(false);
                Transform parent = instance.transform;
                while (list.Count < initialPoolSize)
                {
                    var obj = (GameObject)Object.Instantiate(prefab);
                    obj.transform.parent = parent;
                    list.Add(obj);
                }
                prefab.SetActive(active);
            }
        }
    }

    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
    {
        return Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();
    }
    public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();
    }
    public static T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component
    {
        return Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
    }
    public static T Spawn<T>(T prefab, Vector3 position) where T : Component
    {
        return Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
    }
    public static T Spawn<T>(T prefab, Transform parent) where T : Component
    {
        return Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }
    public static T Spawn<T>(T prefab) where T : Component
    {
        return Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static T SpawnUI<T>(T prefab) where T : Component
    {
        return SpawnUI(prefab.gameObject, null, Vector2.zero, Quaternion.identity).GetComponent<T>();
    }

    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position)
    {
        return Spawn(prefab, parent, position, Quaternion.identity);
    }
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Spawn(prefab, null, position, rotation);
    }
    public static GameObject Spawn(GameObject prefab, Transform parent)
    {
        return Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
    }
    public static GameObject Spawn(GameObject prefab, Vector3 position)
    {
        return Spawn(prefab, null, position, Quaternion.identity);
    }
    public static GameObject Spawn(GameObject prefab)
    {
        return Spawn(prefab, null, Vector3.zero, Quaternion.identity);
    }

    public static GameObject SpawnUI(GameObject prefab)
    {
        return SpawnUI(prefab, null, Vector2.zero, Quaternion.identity);
    }

    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        List<GameObject> list;
        GameObject obj;
        Transform tf;

        if(m_Instance.m_PooledObjects.TryGetValue(prefab, out list))
        {
            obj = null;
            if(list.Count > 0)
            {
                while(obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                }

                if(obj != null)
                {
                    tf = obj.transform;
                    tf.parent = parent;
                    tf.localPosition = position;
                    tf.localRotation = rotation;
                    obj.SetActive(true);
                    m_Instance.m_SpawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            obj = (GameObject)Object.Instantiate(prefab);
            tf = obj.transform;
            tf.parent = parent;
            tf.localPosition = position;
            tf.localRotation = rotation;
            m_Instance.m_SpawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            obj = (GameObject)Object.Instantiate(prefab);
            tf = obj.transform;
            tf.parent = parent;
            tf.localPosition = position;
            tf.localRotation = rotation;
            return obj;
        }
    }

    public static GameObject SpawnUI(GameObject prefab, RectTransform parent, Vector2 position, Quaternion rotation)
    {
        List<GameObject> list;
        GameObject obj;
        RectTransform tf;

        if (m_Instance.m_PooledObjects.TryGetValue(prefab, out list))
        {
            obj = null;
            if (list.Count > 0)
            {
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                }

                if (obj != null)
                {
                    tf = obj.gameObject.GetComponent<RectTransform>();
                    tf.parent = parent;
                    tf.localPosition = position;
                    tf.localRotation = rotation;
                    obj.SetActive(true);
                    m_Instance.m_SpawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            obj = (GameObject)Object.Instantiate(prefab);
            tf = obj.gameObject.GetComponent<RectTransform>();
            tf.parent = parent;
            tf.localPosition = position;
            tf.localRotation = rotation;
            m_Instance.m_SpawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            obj = (GameObject)Object.Instantiate(prefab);
            tf = obj.gameObject.GetComponent<RectTransform>();
            tf.parent = parent;
            tf.localPosition = position;
            tf.localRotation = rotation;
            return obj;
        }
    }
    public static void Recycle<T>(T obj) where T : Component
    {
        Recycle(obj.gameObject);
    }
    public static void Recycle(GameObject obj)
    {
        GameObject prefab;
        if (m_Instance.m_SpawnedObjects.TryGetValue(obj, out prefab))
            Recycle(obj, prefab);
        else
        {
            Debug.Log("Object is Destroy" + obj);
            Object.Destroy(obj);
        }
    }
    static void Recycle(GameObject obj, GameObject prefab)
    {
        m_Instance.m_PooledObjects[prefab].Add(obj);
        m_Instance.m_SpawnedObjects.Remove(obj);
        obj.transform.parent = m_Instance.transform;

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.SetActive(false);
    }

    public static void RecycleAll<T>(T prefab) where T : Component
    {
        RecycleAll(prefab.gameObject);
    }
    public static void RecycleAll(GameObject prefab)
    {
        foreach (var item in m_Instance.m_SpawnedObjects)
            if (item.Value == prefab)
                m_TempList.Add(item.Key);
        for (int i = 0; i < m_TempList.Count; ++i)
            Recycle(m_TempList[i]);
        m_TempList.Clear();
    }
    public static void RecycleAll()
    {
        m_TempList.AddRange(m_Instance.m_SpawnedObjects.Keys);
        for (int i = 0; i < m_TempList.Count; ++i)
            Recycle(m_TempList[i]);
        m_TempList.Clear();
    }

    public static bool IsSpawned(GameObject obj)
    {
        return instance.m_SpawnedObjects.ContainsKey(obj);
    }

    public static int CountPooled<T>(T prefab) where T : Component
    {
        return CountPooled(prefab.gameObject);
    }
    public static int CountPooled(GameObject prefab)
    {
        List<GameObject> list;
        if (instance.m_PooledObjects.TryGetValue(prefab, out list))
            return list.Count;
        return 0;
    }

    public static int CountSpawned<T>(T prefab) where T : Component
    {
        return CountSpawned(prefab.gameObject);
    }
    public static int CountSpawned(GameObject prefab)
    {
        int count = 0;
        foreach (var instancePrefab in instance.m_SpawnedObjects.Values)
            if (prefab == instancePrefab)
                ++count;
        return count;
    }

    public static int CountAllPooled()
    {
        int count = 0;
        foreach (var list in instance.m_PooledObjects.Values)
            count += list.Count;
        return count;
    }

    public static List<GameObject> GetPooled(GameObject prefab, List<GameObject> list, bool appendList)
    {
        if (list == null)
            list = new List<GameObject>();
        if (!appendList)
            list.Clear();
        List<GameObject> pooled;
        if (instance.m_PooledObjects.TryGetValue(prefab, out pooled))
            list.AddRange(pooled);
        return list;
    }
    public static List<T> GetPooled<T>(T prefab, List<T> list, bool appendList) where T : Component
    {
        if (list == null)
            list = new List<T>();
        if (!appendList)
            list.Clear();
        List<GameObject> pooled;
        if (instance.m_PooledObjects.TryGetValue(prefab.gameObject, out pooled))
            for (int i = 0; i < pooled.Count; ++i)
                list.Add(pooled[i].GetComponent<T>());
        return list;
    }

    public static List<GameObject> GetSpawned(GameObject prefab, List<GameObject> list, bool appendList)
    {
        if (list == null)
            list = new List<GameObject>();
        if (!appendList)
            list.Clear();
        foreach (var item in instance.m_SpawnedObjects)
            if (item.Value == prefab)
                list.Add(item.Key);
        return list;
    }
    public static List<T> GetSpawned<T>(T prefab, List<T> list, bool appendList) where T : Component
    {
        if (list == null)
            list = new List<T>();
        if (!appendList)
            list.Clear();
        var prefabObj = prefab.gameObject;
        foreach (var item in instance.m_SpawnedObjects)
            if (item.Value == prefabObj)
                list.Add(item.Key.GetComponent<T>());
        return list;
    }

    public static void DestroyPooled(GameObject prefab)
    {
        List<GameObject> pooled;
        if (instance.m_PooledObjects.TryGetValue(prefab, out pooled))
        {
            for (int i = 0; i < pooled.Count; ++i)
                GameObject.Destroy(pooled[i]);
            pooled.Clear();
        }
    }
    public static void DestroyPooled<T>(T prefab) where T : Component
    {
        DestroyPooled(prefab.gameObject);
    }

    public static void DestroyAll(GameObject prefab)
    {
        RecycleAll(prefab);
        DestroyPooled(prefab);
    }
    public static void DestroyAll<T>(T prefab) where T : Component
    {
        DestroyAll(prefab.gameObject);
    }
}

public static class ObjectPoolExtensions
{
    public static void CreatePool<T>(this T prefab) where T : Component
    {
        ObjectPool.CreatePool(prefab, 0);
    }
    public static void CreatePool<T>(this T prefab, int initialPoolSize) where T : Component
    {
        ObjectPool.CreatePool(prefab, initialPoolSize);
    }
    public static void CreatePool(this GameObject prefab)
    {
        ObjectPool.CreatePool(prefab, 0);
    }
    public static void CreatePool(this GameObject prefab, int initialPoolSize)
    {
        ObjectPool.CreatePool(prefab, initialPoolSize);
    }

    public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
    {
        return ObjectPool.Spawn(prefab, parent, position, rotation);
    }
    public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return ObjectPool.Spawn(prefab, null, position, rotation);
    }
    public static T Spawn<T>(this T prefab, Transform parent, Vector3 position) where T : Component
    {
        return ObjectPool.Spawn(prefab, parent, position, Quaternion.identity);
    }
    public static T Spawn<T>(this T prefab, Vector3 position) where T : Component
    {
        return ObjectPool.Spawn(prefab, null, position, Quaternion.identity);
    }
    public static T Spawn<T>(this T prefab, Transform parent) where T : Component
    {
        return ObjectPool.Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
    }
    public static T Spawn<T>(this T prefab) where T : Component
    {
        return ObjectPool.Spawn(prefab, null, Vector3.zero, Quaternion.identity);
    }
    public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        return ObjectPool.Spawn(prefab, parent, position, rotation);
    }
    public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return ObjectPool.Spawn(prefab, null, position, rotation);
    }
    public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position)
    {
        return ObjectPool.Spawn(prefab, parent, position, Quaternion.identity);
    }
    public static GameObject Spawn(this GameObject prefab, Vector3 position)
    {
        return ObjectPool.Spawn(prefab, null, position, Quaternion.identity);
    }
    public static GameObject Spawn(this GameObject prefab, Transform parent)
    {
        return ObjectPool.Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
    }
    public static GameObject Spawn(this GameObject prefab)
    {
        return ObjectPool.Spawn(prefab, null, Vector3.zero, Quaternion.identity);
    }

    public static GameObject SpawnUI(this GameObject prefab)
    {
        return ObjectPool.SpawnUI(prefab, null, Vector2.zero, Quaternion.identity);
    }

    public static GameObject SpawnUI(this GameObject prefab, RectTransform rtf)
    {
        return ObjectPool.SpawnUI(prefab, rtf, Vector2.zero, Quaternion.identity);
    }

    public static void Recycle<T>(this T obj) where T : Component
    {
        ObjectPool.Recycle(obj);
    }
    public static void Recycle(this GameObject obj)
    {
        ObjectPool.Recycle(obj);
    }

    public static void RecycleAll<T>(this T prefab) where T : Component
    {
        ObjectPool.RecycleAll(prefab);
    }
    public static void RecycleAll(this GameObject prefab)
    {
        ObjectPool.RecycleAll(prefab);
    }

    public static int CountPooled<T>(this T prefab) where T : Component
    {
        return ObjectPool.CountPooled(prefab);
    }
    public static int CountPooled(this GameObject prefab)
    {
        return ObjectPool.CountPooled(prefab);
    }

    public static int CountSpawned<T>(this T prefab) where T : Component
    {
        return ObjectPool.CountSpawned(prefab);
    }
    public static int CountSpawned(this GameObject prefab)
    {
        return ObjectPool.CountSpawned(prefab);
    }

    public static List<GameObject> GetSpawned(this GameObject prefab, List<GameObject> list, bool appendList)
    {
        return ObjectPool.GetSpawned(prefab, list, appendList);
    }
    public static List<GameObject> GetSpawned(this GameObject prefab, List<GameObject> list)
    {
        return ObjectPool.GetSpawned(prefab, list, false);
    }
    public static List<GameObject> GetSpawned(this GameObject prefab)
    {
        return ObjectPool.GetSpawned(prefab, null, false);
    }
    public static List<T> GetSpawned<T>(this T prefab, List<T> list, bool appendList) where T : Component
    {
        return ObjectPool.GetSpawned(prefab, list, appendList);
    }
    public static List<T> GetSpawned<T>(this T prefab, List<T> list) where T : Component
    {
        return ObjectPool.GetSpawned(prefab, list, false);
    }
    public static List<T> GetSpawned<T>(this T prefab) where T : Component
    {
        return ObjectPool.GetSpawned(prefab, null, false);
    }

    public static List<GameObject> GetPooled(this GameObject prefab, List<GameObject> list, bool appendList)
    {
        return ObjectPool.GetPooled(prefab, list, appendList);
    }
    public static List<GameObject> GetPooled(this GameObject prefab, List<GameObject> list)
    {
        return ObjectPool.GetPooled(prefab, list, false);
    }
    public static List<GameObject> GetPooled(this GameObject prefab)
    {
        return ObjectPool.GetPooled(prefab, null, false);
    }
    public static List<T> GetPooled<T>(this T prefab, List<T> list, bool appendList) where T : Component
    {
        return ObjectPool.GetPooled(prefab, list, appendList);
    }
    public static List<T> GetPooled<T>(this T prefab, List<T> list) where T : Component
    {
        return ObjectPool.GetPooled(prefab, list, false);
    }
    public static List<T> GetPooled<T>(this T prefab) where T : Component
    {
        return ObjectPool.GetPooled(prefab, null, false);
    }

    public static void DestroyPooled(this GameObject prefab)
    {
        ObjectPool.DestroyPooled(prefab);
    }
    public static void DestroyPooled<T>(this T prefab) where T : Component
    {
        ObjectPool.DestroyPooled(prefab.gameObject);
    }

    public static void DestroyAll(this GameObject prefab)
    {
        ObjectPool.DestroyAll(prefab);
    }
    public static void DestroyAll<T>(this T prefab) where T : Component
    {
        ObjectPool.DestroyAll(prefab.gameObject);
    }
}