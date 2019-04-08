using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ParentObjectInfo : MonoBehaviour {

    [SerializeField]
    private Text entityInfoName;
    [SerializeField]
    private Text entityInfoLife;
    [SerializeField]
    private Image entityInfoLifeImage;
    [SerializeField]
    private Text entityInfoLevel;
    [SerializeField]
    private Text entityInfoDamage;
    [SerializeField]
    private Image entityInfoDamageImage;
    private ParentObject selectedObject;

    private void Start () {
        EventManager.Instance.OnParentObjectSelectedEvent += SetObject;
        EventManager.Instance.OnEnemyDeathEvent += Disable;
        EventManager.Instance.OnEntityDamageEvent += Refresh;
        EventManager.Instance.OnRefreshUIEvent += Refresh;
        EventManager.Instance.OnDeselectEvent += Deselect;
        gameObject.SetActive(false);
    }

    private void SetObject(ParentObject selectedObject) {
            gameObject.SetActive(true);
            this.selectedObject = selectedObject;
            Show();
    }

    private void Deselect() {
        selectedObject = null;
        Clear();
        gameObject.SetActive(false);
    }

    private void Disable(ParentObject deadObject, bool killedByPlayer) {
        if (deadObject == selectedObject) {
            selectedObject = null;
            Clear();
            gameObject.SetActive(false);
        }
    }

    private void Clear() {
        entityInfoLife.text = string.Empty;
        entityInfoLifeImage.enabled = false;
        entityInfoDamage.text = string.Empty;
        entityInfoDamageImage.enabled = false;
    }

    private void Show() {
        if (selectedObject == null) {
            return;
        }
        Clear();
        entityInfoName.text = selectedObject.Description.Name;
        if (selectedObject is Tower) {
            Tower temp = (Tower)selectedObject;
            entityInfoDamage.text = temp.Projectile.GetComponent<Projectile>().ProjectileDamage.ToString();
            entityInfoDamageImage.enabled = true;
            entityInfoLevel.text = temp.CurrentTowerLevel.ToString() + Path.AltDirectorySeparatorChar + temp.MaxTowerLevel;
        }
        if (selectedObject is Enemy) {
            Enemy temp = (Enemy)selectedObject;
            entityInfoLife.text = temp.CurrentHealthpoints.ToString() + Path.AltDirectorySeparatorChar + temp.Healthpoints;
            entityInfoLifeImage.enabled = true;
            entityInfoLevel.text = temp.CurrentLevel.ToString();
        }
    }

    private void Refresh() {
        Show();
    }

    private void Refresh(ParentObject damagedObject, bool damagedByPlayer) {
        if (damagedObject == selectedObject) {
            Show();
        }
    }

}
