using UnityEngine;

public class Accelerator : MonoBehaviour {

    public static Accelerator Instance { get; private set; }

    [SerializeField]
    private GameObject speedUp;
    [SerializeField]
    private GameObject slowDown;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        EventManager.Instance.OnStartGamePostEvent += SlowDown;
    }

    public void SpeedUp() {
        speedUp.SetActive(false);
        slowDown.SetActive(true);
        Manager.Instance.IncreaseSpeed();
    }

    public void SlowDown() {
        speedUp.SetActive(true);
        slowDown.SetActive(false);
        Manager.Instance.ReduceSpeed();
    }

}
