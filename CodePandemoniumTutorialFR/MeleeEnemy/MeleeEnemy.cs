using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    private float cooldownTimer = Mathf.Infinity;
    [Header("Paramètres Attaque")]
    [SerializeField] private int damage;
    [SerializeField] private float cooldownDelay;
    
    [Header("Paramètres BoxCast")]
    [SerializeField] private float distanceRay;
    [SerializeField] private float rangeRay;
    [SerializeField] private float sizeRay;

    [Header("Paramètres Joueurs")]
    [SerializeField] private LayerMask playerLayer;
    // Pour que OnDrawGizmos fonctionne, il faut intégrer le BoxCollider2D en Serialize Field
    [SerializeField] private BoxCollider2D boxCollider;

    private EnemyPatrol enemyPatrol;

    //références
    private Animator anor;
    private Vie playerHealth;
    

    private void Awake()
    {
        anor = GetComponent<Animator>();
        //  boxCollider = GetComponent<BoxCollider2D>(); voir plus haut

        enemyPatrol = GetComponentInParent<EnemyPatrol>(); 
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(PlayerInSight())
        {
            if(cooldownTimer > cooldownDelay)
            {
                cooldownTimer = 0;
                anor.SetTrigger("meleeAttack");
                
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        // + transform.right nécessaire, pourquoi? Parce que c'est un Vecteur3. distanceRay est inutile!
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangeRay * transform.localScale.x, new Vector3(boxCollider.bounds.size.x * sizeRay, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0f, Vector2.left, distanceRay, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Vie>();


        return hit.collider != null;

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeRay * transform.localScale.x, new Vector3(boxCollider.bounds.size.x * sizeRay, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {

        if(PlayerInSight())
        {
            playerHealth.PriseDegat(damage);

        }

    }
}
