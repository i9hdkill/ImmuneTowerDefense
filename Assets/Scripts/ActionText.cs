using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionText : MonoBehaviour {

    public static ActionText Instance { get; private set; }

    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Image actionImage;

    public void SetActionText(Sprite sprite, float duration) {
        actionImage.sprite = sprite;
        actionImage.enabled = true;
        StartCoroutine(FadeAction(duration));
    }

    public void SetActionText(string text, float duration) {
        actionText.text = text;
        StartCoroutine(FadeAction(duration));
    }

    private void Awake() {
        Instance = this;
        actionText.text = string.Empty;
        actionImage.preserveAspect = true;
    }

    private IEnumerator FadeAction(float duration) {
        yield return new WaitForSecondsRealtime(duration);
        actionText.text = string.Empty;
        actionImage.enabled = false;
    }

}
