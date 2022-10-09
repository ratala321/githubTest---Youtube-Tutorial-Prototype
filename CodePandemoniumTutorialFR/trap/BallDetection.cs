using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : DamageContact
{

    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float chronotimer;
    [SerializeField] private float chronodelay;

    private bool attacking;


    private void OnEnable()
    {

        Stop();
    }


    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        
        else if (chronotimer > chronodelay)
        {
            SearchPlayer();
        }

        chronotimer += Time.deltaTime;
    }

    private void SearchPlayer()
    {
        DirectionSetUp();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                chronotimer = 0;
            }
        }


        

}

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Stop();

    }

    private void DirectionSetUp()
    {
        directions[0] = transform.right*range;
        directions[1] = -transform.right*range;
        directions[2] = transform.up*range;
        directions[3] = -transform.up*range;
    
    
    }
}
