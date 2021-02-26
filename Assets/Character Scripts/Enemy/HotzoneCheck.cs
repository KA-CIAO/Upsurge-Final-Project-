using UnityEngine;

public class HotzoneCheck : MonoBehaviour
{
    private EnemyBehavior enemyParent;
    private bool inRange;
    private Animator anim;

    void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehavior>();
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            enemyParent.Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.TriggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }
} 
