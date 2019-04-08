using cakeslice;
using UnityEngine;
using UnityEngine.EventSystems;

public class ParentObject : MonoBehaviour, IPointerDownHandler {

    [SerializeField]
    private GameObject closeUp;
    [SerializeField]
    protected bool isInShowMode;
    [SerializeField]
    private ParentObjectNameEnum parentName;
    [SerializeField]
    private Description description;
    [SerializeField]
    protected AudioSource audioSource;
    private Outline outline;

    public ParentObjectNameEnum ParentName {
        get {
            return parentName;
        }
    }

    public Description Description {
        get {
            return description;
        }
    }

    public GameObject CloseUp {
        get {
            return closeUp;
        }
    }

    public AudioSource AudioSource {
        get {
            return audioSource;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData) {
        EventManager.Instance.Deselect();
        EventManager.Instance.ParentObjectSelected(this);
        outline.enabled = true;
    }

    protected virtual void OnEnable() {
        outline = GetComponentInChildren<Outline>();
    }

    protected virtual void OnDisable() {
        enabled = false;
    }

    protected virtual void Start() {
        EventManager.Instance.OnDeselectEvent += DisableOutline;
    }

    private void DisableOutline() {
        outline.enabled = false;
    }

}
