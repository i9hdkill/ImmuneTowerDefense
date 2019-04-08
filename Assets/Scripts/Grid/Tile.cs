using UnityEngine;

public class Tile : MonoBehaviour {

    public Coord Position;

    [SerializeField]
    private bool allowTower;
    [SerializeField]
    private bool isWalkable;

    public Tower Tower { get; private set; }

    public Vector3 PositionInWorld {
        get {
            return transform.position;
        }
    }

    public bool IsEmpty() {
        return Tower == null && allowTower;
    }
    public bool IsWalkable {
        get {
            return isWalkable;
        }
    }

    public void PlaceTower(GameObject towerG) {
        Tower = towerG.GetComponent<Tower>();
        Manager.Instance.SubtractMoney(Tower.BuildCost);
        Manager.Instance.BuildingPrefabOnMouse = null;
        Tower.enabled = true;
    }

    public void LocateTowerHere(GameObject towerG) {
        towerG.transform.position = transform.position;
    }

    public void RemoveTower(Tower tower) {
        if (Tower == tower) {
            Tower = null;
        }
    }

    public void Setup(Coord pos, Vector3 position) {
        Position = pos;
        transform.position = position;
    }

    private void Start() {
        EventManager.Instance.OnBuildingRemovedEvent += RemoveTower;
        EventManager.Instance.OnGameResetPostEvent += ResetGame;
    }

    private void ResetGame() {
        Tower = null;
    }

}
