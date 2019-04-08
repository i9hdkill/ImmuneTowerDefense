using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour {

    [SerializeField]
    private ParentObjectNameEnum towerType;
    [SerializeField]
    private BuildingTooltip buildingTooltip;
    [SerializeField]
    private Image icon;
    private Tower building;

    private void Start() {
        building = (Tower)PrefabHolder.Instance.GetInfo(towerType);
        icon.sprite = building.Description.UiSprite;
    }

    public void SetToolTip() {
        buildingTooltip.SetTooltip(building.Description.Name, building.Description.DescriptionText, building.Description.IngameDescriptionText, building.BuildCost, building.Projectile.GetComponent<Projectile>().ProjectileDamage);
    }

    public void GetBuilding() {
        if (!Manager.Instance.HasEnoughMoney(building.BuildCost)) {
            Sound.Instance.PlaySoundClip(SoundEnum.UI_Error_Click, 1f);
            return;
        }
        if (Manager.Instance.BuildingPrefabOnMouse != null) {
            Manager.Instance.BuildingPrefabOnMouse.SetActive(false);
            Manager.Instance.BuildingPrefabOnMouse = null;
        }
        Manager.Instance.BuildingPrefabOnMouse = PoolHolder.Instance.GetObject(towerType);
        if (Manager.Instance.BuildingPrefabOnMouse.GetComponent<BuildingPlacement>() == null) {
            Manager.Instance.BuildingPrefabOnMouse.AddComponent<BuildingPlacement>();
        }
        Manager.Instance.BuildingPrefabOnMouse.SetActive(true);
    }

}
