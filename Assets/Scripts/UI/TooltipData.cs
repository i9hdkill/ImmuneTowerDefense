using UnityEngine;

public class TooltipData {

    public string Title { get; }
    public string Description { get; }
    public Sprite Sprite { get; }
    public float Amount { get; }
    public float PlusDamage { get; }

    public TooltipData(string title, string description, Sprite sprite, float amount, float plusDamage) {
        Title = title;
        Description = description;
        Sprite = sprite;
        Amount = amount;
        PlusDamage = plusDamage;
    }

}
