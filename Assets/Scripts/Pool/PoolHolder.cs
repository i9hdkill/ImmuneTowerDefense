using System.Collections.Generic;
using UnityEngine;

public class PoolHolder : MonoBehaviour {

    public static PoolHolder Instance { get; private set; }
    [SerializeField]
    private GameObject entities;
    [SerializeField]
    private GameObject buildings;
    private List<Pool> pools = new List<Pool>();

    public GameObject Entities {
        get {
            return entities;
        }
    }

    public GameObject Buildings {
        get {
            return buildings;
        }
    }

    void Awake () {
        Instance = this;
	}

    private void Start() {
        EventManager.Instance.OnGameResetEvent += ResetGame;
    }

    private void ResetGame() {
        for (int i = 0; i < pools.Count; i++) {
            pools[i].Reset();
        }
    }

    public GameObject GetObject(ParentObjectNameEnum type) {
        return GetRightPool(type).GetPoolObject();
    }

    private Pool GetRightPool(ParentObjectNameEnum type) {
        if (pools.Count > 0) {
            foreach (Pool item in pools) {
                if (item.type == type) {
                    return item;
                }
            }
        }
        CreatePool(type);
        return GetRightPool(type);
    }

    private void CreatePool(ParentObjectNameEnum type) {
        Pool poolToAdd = gameObject.AddComponent<Pool>();
        poolToAdd.type = type;
        poolToAdd.size = 10;
        pools.Add(poolToAdd);
    }
	
}
