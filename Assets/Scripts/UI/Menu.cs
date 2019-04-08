using UnityEngine;

public class Menu : MonoBehaviour {

    public virtual void Show() {
        gameObject.SetActive(true);
    }

    public virtual void Hide() {
        gameObject.SetActive(false);
    }
}
