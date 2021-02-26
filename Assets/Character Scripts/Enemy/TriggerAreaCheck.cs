using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private EnemyBehavior enemyParent;

    void Start()
    {
        enemyParent = GetComponentInParent<EnemyBehavior>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
