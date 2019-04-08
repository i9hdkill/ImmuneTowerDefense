using System.Collections;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour {

    protected bool isEating;
    protected Tower tower;
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected float projectileDelay;
    [SerializeField]
    protected float soundDelay;

    public bool IsEating {
        get {
            return isEating;
        }
    }

    public virtual void Attack(Enemy enemy) {
        Shoot(enemy);
    }

    public virtual void Rotate() {
        if (tower.EnemiesInRange.Count > 0) {
            Vector3 targetPostition = new Vector3(tower.EnemiesInRange[0].transform.position.x, transform.position.y, tower.EnemiesInRange[0].transform.position.z);
            transform.LookAt(targetPostition);
        }
    }

    protected IEnumerator DelaySound(float delay, SoundEnum sound, float volume) {
        yield return new WaitForSecondsRealtime(delay);
        Sound.Instance.PlaySoundClipWithSource(sound, tower.AudioSource, 0, volume);
    }

    protected void Shoot(Enemy enemyToShootAt) {
        if (enemyToShootAt != null) {
            if (enemyToShootAt.IsAlive()) {
                anim.SetTrigger("attack");
                StartCoroutine(DelayProjectile(projectileDelay, enemyToShootAt));
                StartCoroutine(DelaySound(soundDelay, tower.ShootSoundData.SoundName, tower.ShootSoundData.Volume));
            }
        }
    }

    protected IEnumerator DelayProjectile(float delay, Enemy enemy) {
        yield return new WaitForSecondsRealtime(delay);
        if (enemy != null && enemy.IsAlive()) {
            Projectile bullet = Instantiate(tower.Projectile, tower.ProjectileRoot.transform.position, tower.ProjectileRoot.transform.rotation).GetComponent<Projectile>();
            bullet.Target = enemy;
            bullet.Tower = tower;
            bullet.gameObject.SetActive(true);
        }
    }

    private void Start() {
        tower = GetComponent<Tower>();
    }

}
