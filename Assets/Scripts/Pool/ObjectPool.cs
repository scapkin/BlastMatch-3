using System.Collections.Generic;
using System.Linq;
using Singleton;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Pool
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        [SerializeField] private List<ObjectPoolItem> objectPoolItems;
        private Dictionary<GameObject, Queue<GameObject>> _objectPoolDictionary;

        private void Awake()
        {
            InitializeObjectPools();
            //DontDestroyOnLoad(this.gameObject);
            //DOVirtual.DelayedCall(2f, () => { SceneManager.LoadScene(1); });
        }

        private void InitializeObjectPools()
        {
            _objectPoolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

            foreach (var item in objectPoolItems)
            {
                GameObject prefab = item.prefab;
                int initialPoolSize = item.initialPoolSize;

                if (!_objectPoolDictionary.ContainsKey(prefab))
                {
                    Queue<GameObject> objectPool = new Queue<GameObject>();

                    for (int i = 0; i < initialPoolSize; i++)
                    {
                        GameObject obj = Instantiate(prefab);
                        obj.name = prefab.name;
                        obj.transform.parent = this.transform;
                        obj.SetActive(false);
                        objectPool.Enqueue(obj);
                    }

                    _objectPoolDictionary.Add(prefab, objectPool);
                }
            }
        }

        private Queue<GameObject> _objectPool;
        private GameObject _obj;

        public GameObject GetObjectFromPool(ObjectPoolItem.GemType gemType, Vector3 pos)
        {
            _obj = objectPoolItems.FirstOrDefault(x => x.type == gemType)!.prefab;

            if (_objectPoolDictionary.ContainsKey(_obj))
            {
                _objectPool = _objectPoolDictionary[_obj];

                if (_objectPool.Count > 0)
                {
                    _obj = _objectPool.Dequeue();
                }
                else
                {
                    _obj = Instantiate(_obj);
                }

                _obj.transform.DOScale(Vector3.one, 0.2f);
                _obj.transform.position = pos;
                _obj.SetActive(true);
                return _obj;
            }
            else
            {
                Debug.LogWarning($"Object pool for object {_obj.name} does not exist!");
                return null;
            }
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => obj.SetActive(false));

            foreach (var kvp in _objectPoolDictionary)
            {
                if (kvp.Key.name == obj.name)
                {
                    //Debug.Log(obj);
                    kvp.Value.Enqueue(obj);
                    return;
                }
            }

            Debug.LogWarning($"Object pool for object {obj.name} does not exist!");
        }
    }
}