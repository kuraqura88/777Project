using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    class Pool<T> where T : UnityEngine.Component
    {
        // Ǯ�� ��� ������Ʈ
        public GameObject Original { get; private set; }

        // ������Ʈ�� ��Ƶδ� �θ� ������Ʈ
        public Transform Root { get; set; }

        // ������ ������Ʈ�� �����ϴ� ������
        public Stack<T> _poolStack = new Stack<T>();


        // �������� ������Ʈ ������
        public GameObject GetOriginal() { return Original; }


        /// <summary>
        /// 
        /// [���]
        /// 
        /// Pool�� �ʱ�ȭ�ϴ� �޼���
        /// 1. Original ������Ʈ�� ����
        /// 2. ������Ʈ�� ��Ƶ� Parent ������Ʈ ����
        /// 3. Count��ŭ ������Ʈ�� ���� ��, Push
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="count"></param>
        public void Init(GameObject original, int count = 300)
        {
            Original = original;

            Root = new GameObject { name = $"{Original.name}_Root" }.transform;

            for(int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }


        /// <summary>
        /// 
        /// [���]
        /// ������Ʈ�� �����ϴ� �޼���
        /// 
        /// </summary>
        /// <returns></returns>
        T Create()
        {
            GameObject go = Instantiate(Original);
            go.name = Original.name;

            // �ش� Ÿ������ ������Ʈ�� ����
            // ���� �ش� ������Ʈ�� ���� �� ������Ʈ�� ����
            T component = go.GetComponent<T>();
            if (component == null)
            {
                component = go.AddComponent<T>();
            }

            return component;
        }

        public void Push(T obj)
        {
            if (obj == null)
                return;

            obj.transform.parent = Root;
            obj.gameObject.SetActive(false);

            _poolStack.Push(obj);
        }

        public T Pop(Transform parent)
        {
            T obj = null;
            if (_poolStack.Count > 0)
                obj = _poolStack.Pop();
            else
                obj = Create();


            obj.gameObject.SetActive(true);

            obj.transform.parent = parent;
            return obj;
        }
    }

    public static PoolManager Instance;

    // �ֻ��� �θ�
    private Dictionary<(string, Type), object> poolDict = new Dictionary<(string, Type), object>();

    private Transform root = null;
    public Transform Root
    {
        get
        {
            if(root == null)
            {
                root = Instantiate(new GameObject("Root")).transform;
            }
            return root;
        }
    }
    private void Awake()
    {
        Init();
    }

    // �ʱ�ȭ
    public void Init()
    {
        Instance = FindObjectOfType<PoolManager>();
        if (Instance == null)
        {
            GameObject go = new GameObject { name = "PoolManager" };
            Instance = go.AddComponent<PoolManager>();
        }

        DontDestroyOnLoad(Instance.gameObject);

        GameManager.Instance.Data.Init();
    }


    // ������ ���� ����
    public void Clear()
    {
        poolDict.Clear();
    }


    /// <summary>
    /// 
    /// [���]
    /// Pool�� �����ϴ� �޼���
    /// 
    /// ���׸����� �پ��� Ÿ���� Ǯ�� �ڵ����� ���� ����
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"> ���� ��� ������Ʈ </param>
    /// <param name="count" : �ʱ� Ǯ������Ʈ ���� ></param>
    public void CreatePool<T>(GameObject original, int count = 50) where T : Component
    {
        var key = (original.name, typeof(T));
        if (poolDict.ContainsKey(key))
        {
            return;
        }

        Pool<T> pool = new Pool<T>();
        pool.Init(original, count);

        pool.Root.SetParent(transform);
        poolDict.Add(key, pool);
    }

    /// <summary>
    /// 
    /// [���]
    /// ���Ϸ��� Ǯ ������Ʈ�� �ݳ��ϴ� �޼���
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public void Push<T>(T obj) where T : Component
    {
        var key = (obj.gameObject.name, typeof(T));
        if (!poolDict.ContainsKey(key))
        {
            Destroy(obj.gameObject);
            return;
        }

        ((Pool<T>)poolDict[key]).Push(obj);
    }



    /// <summary>
    /// 
    /// [���]
    /// ����� ������Ʈ�� ������ �޼���
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"></param>
    /// <param name="parent"> �����鼭 ������ �θ� ������Ʈ </param>
    /// <returns></returns>
    public T Pop<T>(GameObject original, Transform parent = null) where T : Component
    {
        var key = (original.name, typeof(T));
        if (!poolDict.ContainsKey(key))
        {
            CreatePool<T>(original);
        }
        Transform pr = parent;
        if(pr == null)
        {
            pr = Root;
            
        }
        return ((Pool<T>)poolDict[key]).Pop(pr);
    }

    public void AllReset()
    {

    }
}