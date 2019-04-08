using UnityEngine;
using UnityEngine.UI;

public class ParentObjectDescription : MonoBehaviour {

    [SerializeField]
    private Text description;
    [SerializeField]
    private Image icon;
    private ParentObject selectedObject;

    void Start() {
        EventManager.Instance.OnParentObjectSelectedEvent += SetObject;
        EventManager.Instance.OnDeselectEvent += Deselect;
        EventManager.Instance.OnEnemyDeathEvent += Disable;
        gameObject.SetActive(false);
    }

    private void Disable(ParentObject deadObject, bool killedByPlayer) {
        if (deadObject == selectedObject) {
            selectedObject = null;
            Deselect();
        }
    }

    private void SetObject(ParentObject selectedObject) {
        this.selectedObject = selectedObject;
        Deselect();
        Show();
    }

    private void Deselect() {
        Clear();
        Hide();
    }

    private void Show() {
        Clear();
        description.text = selectedObject.Description.DescriptionText;
        if (selectedObject is Enemy) {
            Enemy enemy = (Enemy)selectedObject;
            icon.sprite = enemy.TypeIcon;
            icon.enabled = true;
        }
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Clear() {
        description.text = "";
        icon.enabled = false;
    }
}
