using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float projectileDamage;
    private float baseDamage;
    [SerializeField]
    private float damageIncreasePerTowerLevel;
    [SerializeField]
    private ProjectileTypeEnum projectileType;
    [SerializeField]
    private float areaDamageRadius;
    [SerializeField]
    private float slowAmount;
    [SerializeField]
    private float slowTime;
    [SerializeField]
    private DebuffTypeEnum debuffType;
    public Enemy Target;
    public Tower Tower;
    private bool hasReachedTarget;
    [SerializeField]
    private float debuffTime;

    public float ProjectileDamage {
        get {
            return projectileDamage;
        }
    }

    public float DamageIncreasePerTowerLevel {
        get {
            return damageIncreasePerTowerLevel;
        }
    }

    public void ResetProjectile(float oldDamage) {
        projectileDamage = oldDamage;
    }

    public void Upgrade() {
        projectileDamage += damageIncreasePerTowerLevel;
    }
	
	void Update () {
        if (Manager.Instance.IsPaused || Target == null) {
            return;
        }
        float speed = bulletSpeed * Time.deltaTime;
        if (Target != null && Target.IsAlive()) {
            transform.position = Vector3.MoveTowards(transform.position, Target.gameObject.transform.position, speed);
            if (transform.position == Target.gameObject.transform.position && !hasReachedTarget) {
                hasReachedTarget = true;
                if (projectileType == ProjectileTypeEnum.Single) {
                    DoDamage(Target);
                } else if (projectileType == ProjectileTypeEnum.Area) {
                    Collider[] colliderArray = Physics.OverlapSphere(transform.position, areaDamageRadius, 256);
                    for (int i = 0; i < colliderArray.Length; i++) {
                        if (colliderArray[i].GetComponent<Enemy>() != null) {
                            DoDamage(colliderArray[i].gameObject.GetComponent<Enemy>());
                        }
                    }
                } else if (projectileType == ProjectileTypeEnum.Debuff) {
                    ApplyDebuff(Target);
                    DoDamage(Target);
                }
                Destroy(gameObject);
            }
            return;
        }
        Destroy(gameObject);
    }

    private void ApplyDebuff(Enemy enemy) {
        if (enemy != null) {
            enemy.DamagedFromTower = Tower;
            enemy.ApplyDebuff(PrefabHolder.Instance.GetSprite(debuffType), debuffTime, debuffType, slowAmount, slowTime);
            Tower.RemoveEnemyFromList(enemy);
            return;
        }
    }

    private void DoDamage(Enemy enemy) {
        if (enemy != null && enemy.gameObject.activeInHierarchy) {
            enemy.DamagedFromTower = Tower;
            enemy.TakeDamage(projectileDamage);
            return;
        }
    }
}
