using UnityEngine;
using UnityEngine.UI;

public class Avatar : Menu {

    private ParentObject currentObject;

    [SerializeField]
    private RawImage avatarImage;

    void Start() {
        EventManager.Instance.OnParentObjectSelectedEvent += SetObject;
        EventManager.Instance.OnEnemyDeathEvent += Disable;
        EventManager.Instance.OnDeselectEvent += Deselect;
        Hide();
    }

    void SetObject(ParentObject parentObject) {
        if (currentObject != null) {
            currentObject.CloseUp.SetActive(false);
        }
        currentObject = parentObject;
        currentObject.CloseUp.SetActive(true);
        Show();

    }

    private void Deselect() {
        if (currentObject != null) {
            currentObject.CloseUp.SetActive(false);
        }
        currentObject = null;
        Hide();
    }

    void Disable(ParentObject parentObject, bool killedByPlayer) {
        if (currentObject == parentObject) {
            currentObject.CloseUp.SetActive(false);
            currentObject = null;
            Hide();
        }
    }
}
