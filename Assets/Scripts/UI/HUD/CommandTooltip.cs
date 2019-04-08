using UnityEngine;
using UnityEngine.UI;

public class CommandTooltip : Menu {

    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Image sprite;
    [SerializeField]
    private Image plusDamageSprite;
    [SerializeField]
    private Text moneyAmount;
    [SerializeField]
    private Text plusDamage;

    public void SetTooltip(TooltipData tooltipData) {
        plusDamageSprite.enabled = false;
        gameObject.SetActive(true);
        title.text = tooltipData.Title;
        description.text = tooltipData.Description;
        sprite.enabled = true;
        if (tooltipData.Sprite == null) {
            sprite.enabled = false;
        } else {
            sprite.sprite = tooltipData.Sprite;
            sprite.preserveAspect = true;
        }
        if (tooltipData.Amount == 0) {
            moneyAmount.text = string.Empty;
        } else {
            moneyAmount.text = tooltipData.Amount.ToString();
        }
        if (tooltipData.PlusDamage == 0) {
            plusDamage.text = string.Empty;
        } else {
            plusDamage.text = tooltipData.PlusDamage.ToString();
            plusDamageSprite.enabled = true;
        }
    }

}
