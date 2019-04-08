using UnityEngine;
using UnityEngine.UI;

public class ButtonScaler : MonoBehaviour {

    [SerializeField]
    private GameObject button;
    [SerializeField]
    private BuildingTooltip tooltip;
    [SerializeField]
    private ParentObjectNameEnum type;
    [SerializeField]
    private Image image;
    [SerializeField]
    private RawImage rawImage;

    public void ScaleUp() {
        button.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        RenderView.Instance.SetImage(type);
        image.enabled = false;
        rawImage.enabled = true;
        tooltip.Show();
    }

    public void ScaleDown() {
        button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        rawImage.enabled = false;
        image.enabled = true;
        tooltip.Hide();
    }
}
