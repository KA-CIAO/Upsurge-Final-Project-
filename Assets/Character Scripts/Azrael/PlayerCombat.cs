using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    #region Private Variables
    private Animator anim; 
    private Rigidbody2D rb;
    private float lastClickedTime = 0;
    private int currentHealth;
    #endregion

    #region Public Variables
    public LayerMask enemyLayers;
    public Transform attackPoint; 
    public float attackRange;
    public float maxComboDelay;
    public int attackDamage;
    public int clickCount = 0;
    public int maxHealth;
    #endregion
    
   void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentHealth = maxHealth;
    }

    void Update()
    {
        clickCount = Mathf.Clamp(clickCount, 0, 3);
        
        if(Input.GetButtonDown("Fire1"))
        {
            lastClickedTime = Time.time;
            clickCount++;

            if (clickCount == 1)
            {
                anim.SetBool("Attack1", true);
                Attack();
            }
        }
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            clickCount = 0;
        }

        if (transform.position.y < -14)
        {
            Destroy(gameObject, 2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void return1()
    {
        if (clickCount >= 2)
        {
            anim.SetBool("Attack2", true);
        } 
        else 
        {
            anim.SetBool("Attack1", false);
            clickCount = 0;
        }
    }

    public void return2()
    {
        if (clickCount >= 3)
        {
            anim.SetBool("Attack3", true); 
        } 
        else 
        {
            anim.SetBool("Attack2", false);
            clickCount = 0;
        }
    }

    public void return3()
    {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
        clickCount = 0;
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponentInParent<EnemyCombat>().EnemyTakeDamage(attackDamage);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            PlayerDie();
            Destroy(gameObject, 2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void PlayerDie()
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
