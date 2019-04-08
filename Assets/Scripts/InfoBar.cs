using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : Menu {

    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private Text entityName;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Image entityTypeIcon;
    [SerializeField]
    private new Camera camera;
    private Enemy entity;

    private void OnEnable() {
        camera = Camera.main;
        entity = GetComponentInParent<Enemy>();
        entityName.text = entity.Description.Name;
        entityTypeIcon.sprite = entity.TypeIcon;
    }

    public override void Hide() {
        if (entity != null && entity.CurrentHealthpoints < entity.Healthpoints) {
            return;
        }
        base.Hide();
    }

    private void Update() {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }

    public bool IsEnabled() {
        return gameObject.activeSelf;
    }

    public void UpdateBar() {
        if (entity == null) {
            entity = GetComponentInParent<Enemy>();
        }
        healthImage.fillAmount = entity.CurrentHealthpoints / entity.Healthpoints;
        healthText.text = entity.CurrentHealthpoints.ToString() + Path.AltDirectorySeparatorChar + entity.Healthpoints;
    }

}
