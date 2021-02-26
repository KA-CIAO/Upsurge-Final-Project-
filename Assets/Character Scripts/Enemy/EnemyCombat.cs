using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    #region Private Variables
    private Animator anim;
    private Rigidbody2D rb;
    private int currentHealth;
    #endregion

    #region Public Variables
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange;
    public int attackDamage;
    public int maxHealth;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentHealth = maxHealth;
    }

    public void EnemyAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerCombat>().PlayerTakeDamage(attackDamage);
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            EnemyDie();
            Destroy(gameObject, 1);
        }
    }

    void EnemyDie()
    {
        anim.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        this.enabled = false;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
