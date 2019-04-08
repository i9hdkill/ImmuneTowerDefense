using UnityEngine;
using UnityEngine.UI;

public class Commands : Menu {

    [SerializeField]
    private Button upgrade;
    [SerializeField]
    private Button destroy;
    [SerializeField]
    private CommandTooltip commandTooltip;
    private Tower selectedTower;

    public void SetSelectedObject(ParentObject parentObject) {
        if (parentObject is Tower) {
            gameObject.SetActive(true);
            selectedTower = (Tower)parentObject;
            if (selectedTower.CurrentTowerLevel == selectedTower.MaxTowerLevel) {
                upgrade.interactable = false;
            } else {
                upgrade.interactable = true;
            }
            return;
        }
        Hide();
    }

    private void Start() {
        Hide();
        EventManager.Instance.OnParentObjectSelectedEvent += SetSelectedObject;
        EventManager.Instance.OnDeselectEvent += Hide;
    }

    public void HoverUpgrade() {
        if (selectedTower.MaxTowerLevel != selectedTower.CurrentTowerLevel) {
            commandTooltip.SetTooltip(new TooltipData("Upgrade Tower", "Beim Upgrade erhöht sich der Schaden der Tower", Manager.Instance.MoneySprite, selectedTower.UpgradeCost, selectedTower.Projectile.GetComponent<Projectile>().ProjectileDamage + selectedTower.Projectile.GetComponent<Projectile>().DamageIncreasePerTowerLevel));
        } else {
            EndHover();
        }
    }

    public void Upgrade() {
        if (selectedTower.Upgrade()) {
            Sound.Instance.PlaySoundClip(SoundEnum.UI_Button_Click, 1f);
            EventManager.Instance.RefreshUI();
            if (selectedTower.MaxTowerLevel == selectedTower.CurrentTowerLevel) {
                upgrade.interactable = false;
                EndHover();
            } else {
                HoverUpgrade();
            }
        } else {
            Sound.Instance.PlaySoundClip(SoundEnum.UI_Error_Click, 1f);
        }
    }

    public void HoverDestroy() {
        commandTooltip.SetTooltip(new TooltipData("Tower zerstören", "Dies entfernt den Tower von der Karte. Du erhältst dabei die Hälfte des Kaufpreises zurück", Manager.Instance.MoneySprite, 0, 0));
    }

    public void Destroy() {
        Sound.Instance.PlaySoundClip(SoundEnum.UI_Button_Click, 1f);
        EventManager.Instance.Deselect();
        selectedTower.Destroy();
        EndHover();
    }

    public void EndHover() {
        commandTooltip.Hide();
    }

}
