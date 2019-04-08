using UnityEngine;

public class RenderView : MonoBehaviour {

    public static RenderView Instance { get; private set; }

    public RenderTexture RenderTexture {
        get {
            return renderTexture;
        }
    }

    [SerializeField]
    private GameObject monozytCameraParent;
    [SerializeField]
    private GameObject B_LymphozytCameraParent;
    [SerializeField]
    private GameObject MakrophagCameraParent;

    [SerializeField]
    private RenderTexture renderTexture;

    [SerializeField]
    private GameObject renderCamera;

    private void Awake() {
        Instance = this;
    }

    public void SetImage(ParentObjectNameEnum parentName) {
        switch (parentName) {
            case ParentObjectNameEnum.Monozyt:
                renderCamera.transform.position = monozytCameraParent.transform.position;
                break;
            case ParentObjectNameEnum.B_Lymphozyt:
                renderCamera.transform.position = B_LymphozytCameraParent.transform.position;
                break;
            case ParentObjectNameEnum.Makrophag:
                renderCamera.transform.position = MakrophagCameraParent.transform.position;
                break;
            case ParentObjectNameEnum.Tomato:
                break;
            default:
                break;
        }
    }
	
}
