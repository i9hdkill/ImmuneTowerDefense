using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    private List<GameObject> pool = new List<GameObject>();
    public ParentObjectNameEnum type;
    public int size = 15;

    private void PopulatePool() {
        for (int i = 0; i < size; i++) {
            CreateEntity(type);
        }
    }

    public void Reset() {
        for (int i = 0; i < pool.Count; i++) {
            pool[i].gameObject.SetActive(false);
        }
    }

    private GameObject CreateEntity(ParentObjectNameEnum type) {
        Transform parent;
        if (type == ParentObjectNameEnum.Tomato || type == ParentObjectNameEnum.Malaria || type == ParentObjectNameEnum.Schnupfen) {
            parent = PoolHolder.Instance.Entities.transform;
        } else {
            parent = PoolHolder.Instance.Buildings.transform;
        }
        GameObject gObject = Instantiate(PrefabHolder.Instance.Get(type), parent);
        gObject.SetActive(false);
        pool.Add(gObject);
        return gObject;
    }

    public GameObject GetPoolObject() {
        if (pool.Count == 0) {
            PopulatePool();
        }
        foreach (GameObject item in pool) {
            if (!item.activeInHierarchy) {
                return item;
            }
        }
        return CreateEntity(type);
    }
}
