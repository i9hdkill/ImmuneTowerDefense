
public class Lymphozyt : TowerBehaviour {

    public override void Attack(Enemy enemy) {
        if (enemy.CurrentDebuff != DebuffTypeEnum.Lymphozyt) {
            base.Attack(enemy);
        } else {
            tower.RemoveEnemyFromList(enemy);
        }
    }

}
