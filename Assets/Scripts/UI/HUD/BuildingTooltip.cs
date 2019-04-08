using UnityEngine;
using UnityEngine.UI;

public class BuildingTooltip : Menu {

    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Text ingameDescription;
    [SerializeField]
    private Image sprite;
    [SerializeField]
    private Text priceAmount;
    [SerializeField]
    private Text damageAmount;

    public void SetTooltip(string title, string description, string ingameDescription, int amount, float damageAmount) {
        this.title.text = title;
        this.description.text = description;
        this.ingameDescription.text = ingameDescription;
        sprite.sprite = Manager.Instance.MoneySprite;
        sprite.preserveAspect = true;
        priceAmount.text = amount.ToString();
        this.damageAmount.text = damageAmount.ToString();
    }

}
