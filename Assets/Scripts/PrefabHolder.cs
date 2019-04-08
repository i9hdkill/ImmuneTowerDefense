using UnityEngine;

public class PrefabHolder : MonoBehaviour {

    public static PrefabHolder Instance { get; private set; }
    [SerializeField]
    private GameObject monozyt;
    [SerializeField]
    private GameObject b_lymphozyt;
    [SerializeField]
    private GameObject makrophag;
    [SerializeField]
    private GameObject tomato;
    [SerializeField]
    private GameObject schnupfen;
    [SerializeField]
    private GameObject malaria;
    [SerializeField]
    private GameObject fuchsbandwurm;
    [SerializeField]
    private Sprite debuffLymphozyt;
    [SerializeField]
    private GameObject deathobject;

    void Awake () {
        Instance = this;
	}

    public ParentObject GetInfo(ParentObjectNameEnum objectNameEnum) {
        return Get(objectNameEnum).GetComponent<ParentObject>();
    }

    public Sprite GetSprite(DebuffTypeEnum type) {
        switch (type) {
            case DebuffTypeEnum.Lymphozyt:
                return debuffLymphozyt;
            default:
                return null;
        }
    }

    public GameObject Get(ParentObjectNameEnum objectNameEnum) {
        switch (objectNameEnum) {
            case ParentObjectNameEnum.Monozyt:
                return monozyt;
            case ParentObjectNameEnum.B_Lymphozyt:
                return b_lymphozyt;
            case ParentObjectNameEnum.Makrophag:
                return makrophag;
            case ParentObjectNameEnum.Tomato:
                return tomato;
            case ParentObjectNameEnum.Schnupfen:
                return schnupfen;
            case ParentObjectNameEnum.Malaria:
                return malaria;
            case ParentObjectNameEnum.Fuchsbandwurm:
                return fuchsbandwurm;
            case ParentObjectNameEnum.DeathObject:
                return deathobject;
            default:
                return null;
        }
    }
	
}
