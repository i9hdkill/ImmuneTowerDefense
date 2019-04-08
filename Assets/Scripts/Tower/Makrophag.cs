using System.Collections;
using UnityEngine;

public class Makrophag : TowerBehaviour {

    [SerializeField]
    private GameObject grabJoint;
    [SerializeField]
    private float eatTime;
    [SerializeField]
    private float waitBeforeParent;
    [SerializeField]
    private float eatSoundDelay;
    [SerializeField]
    private int eatAfterNormalAttacks;
    private int normalAttacks = 6;
    [SerializeField]
    private SoundDataHolder eatSoundData;

    public override void Rotate() {
        if (!IsEating) {
            base.Rotate();
        }
    }

    public override void Attack(Enemy enemy) {
        if (enemy.CurrentDebuff == DebuffTypeEnum.Lymphozyt && normalAttacks > eatAfterNormalAttacks && enemy.ParentName != ParentObjectNameEnum.Malaria) {
            StartCoroutine(WaitForEatStart(enemy));
            normalAttacks = 0;
            anim.SetTrigger("eat");
            StartCoroutine(DelaySound(eatSoundDelay, eatSoundData.SoundName, eatSoundData.Volume));
        } else {
            base.Attack(enemy);
            normalAttacks++;
        }
    }

    private IEnumerator WaitForEatStart(Enemy enemyScript) {
        yield return new WaitForSecondsRealtime(waitBeforeParent);
        isEating = true;
        enemyScript.SetPath(null);
        StartCoroutine(WaitForEatFinished(enemyScript));
        StartCoroutine(Grab(enemyScript.gameObject));
        StartCoroutine(ResetEat());
    }

    private IEnumerator WaitForEatFinished(Enemy enemyScript) {
        yield return new WaitForSecondsRealtime(eatTime);
        enemyScript.DamagedFromTower = tower;
        enemyScript.TakeDamage(9000);
    }

    private IEnumerator ResetEat() {
        yield return new WaitForSecondsRealtime(eatTime + 1);
        isEating = false;
    }

    private IEnumerator Grab(GameObject enemy) {
        float waitTime = eatTime;
        while (waitTime > 0) {
            if (enemy != null) {
                enemy.transform.position = grabJoint.transform.position;
            }
            waitTime -= Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

}
