using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        public Stack<GameObject> _poolStack = new Stack<GameObject>();


        public void Init(GameObject original, int count = 50)
        {
            Original = original;

            Root = new GameObject { name = $"{Original.name}_Root" }.transform;

            for(int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        GameObject Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            return go;
        }

        public void Push(GameObject obj)
        {
            if (obj == null)
                return;

            obj.transform.parent = Root;
            obj.gameObject.SetActive(false);

            _poolStack.Push(obj);
        }

        public GameObject Pop(Transform parent)
        {
            GameObject obj = null;
            if (_poolStack.Count > 0)
                obj = _poolStack.Pop();
            else
                obj = Create();


            obj.SetActive(true);

            obj.transform.parent = parent;
            return obj;
        }
    }

    public static PoolManager Instance;

    Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Clear()
    {
        poolDict.Clear();
    }

    public void CreatePool(GameObject original, int count = 50)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = transform;

        poolDict.Add(original.name, pool);
    }

    public void Push(GameObject obj)
    {
        string name = obj.gameObject.name;

        if(poolDict.ContainsKey(name) == false)
        {
            GameObject.Destroy(obj.gameObject);
            return;
        }

        poolDict[name].Push(obj);
    }

    public GameObject Pop(GameObject original, Transform parent = null)
    {
        if(poolDict.ContainsKey(original.name) == false)
        {
            CreatePool(original);
        }
        return poolDict[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (poolDict.ContainsKey(name) == false)
            return null;

        return poolDict[name].Original;
    }
}
