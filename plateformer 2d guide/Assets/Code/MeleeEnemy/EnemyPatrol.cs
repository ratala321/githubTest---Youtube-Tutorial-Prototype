using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Point de patrouille")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Paramètres de mouvement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Paramètres Idle")]
    [SerializeField] private float idleDuration;
    private float idleTimer;


    [Header("Enemy Animation")]
    [SerializeField] private Animator anor;

    private void Awake()
    {
        initScale = enemy.localScale;
        
    }


    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MouvementDirection(-1);
            }
            else 
            { 
                ChangementDirection(); 
            }
        }
        else
        {
           if (enemy.position.x <= rightEdge.position.x)
            { 
                MouvementDirection(1);

            }
            else
            {
                ChangementDirection();
            }
        }
    }

    private void OnDisable()
    {
        anor.SetBool("moving", false);
    }

    private void ChangementDirection()
    {
        anor.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if (idleDuration < idleTimer)
        { movingLeft = !movingLeft; }
    }

    private void MouvementDirection(int _direction)
    {
        idleTimer = 0;
        anor.SetBool("moving", true);
        //face
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);


        //mouvement
         enemy.position = new Vector3(enemy.position.x + speed * Time.deltaTime * _direction, enemy.position.y, enemy.position.y);
        /*enemy.Translate(speed * Time.deltaTime * _direction, 0f, 0f);
         Alternative*/
    }
}
