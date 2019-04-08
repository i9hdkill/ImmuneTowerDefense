using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacement : MonoBehaviour, IPointerDownHandler {

    private bool canBePlaced;
    private bool isPlaced;
    private Tile tile;

    public void OnPointerDown(PointerEventData eventData) {
        if (canBePlaced && eventData.button == PointerEventData.InputButton.Left && transform.position == tile.gameObject.transform.position) {
            Place();
        }
    }

    private void Update() {
        if (Manager.Instance.BuildingPrefabOnMouse == null || isPlaced) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1) || !Manager.Instance.HasEnoughMoney(Manager.Instance.BuildingPrefabOnMouse.GetComponent<Tower>().BuildCost)) {
            Manager.Instance.BuildingPrefabOnMouse.SetActive(false);
            Manager.Instance.BuildingPrefabOnMouse = null;
            Destroy(this);
            return;
        }
        RaycastHit hit;

        int layerMask = 1 << 9;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, layerMask)) {
            tile = hit.collider.gameObject.GetComponent<Tile>();
            if (tile != null && tile.IsEmpty() && Manager.Instance.BuildingPrefabOnMouse != null) {
                Debug.Log("can be placed");
                tile.LocateTowerHere(Manager.Instance.BuildingPrefabOnMouse);
                canBePlaced = true;
            } else {
                canBePlaced = false;
            }
        }
    }

    private void Place() {
        isPlaced = true;
        tile.PlaceTower(Manager.Instance.BuildingPrefabOnMouse);
        Destroy(this);
    }

}

