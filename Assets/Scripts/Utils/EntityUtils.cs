using UnityEngine;

public class EntityUtils {

    /// <summary>
    /// returns the closest gameobject with a collider on it that is an instance of ParentObject
    /// ignores dead entities
    /// </summary>
    public static GameObject GetNearestEnemy(Collider[] collider, Enemy enemy) {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = enemy.gameObject.transform.position;
        foreach (Collider potentialTarget in collider) {
            if (potentialTarget.GetComponent<ParentObject>() != null) {
                if (potentialTarget.gameObject != enemy.gameObject.transform.gameObject && potentialTarget.gameObject.GetComponent<Enemy>().IsAlive()) {
                    Debug.Log(enemy.name + " now sees " + potentialTarget.gameObject.name);
                    Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistanceSqr) {
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget.gameObject;
                    }
                }
            }
        }
        return bestTarget;
    }
}
