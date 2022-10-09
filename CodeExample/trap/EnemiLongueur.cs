using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiLongueur : MonoBehaviour
{
    [SerializeField] private float dommage;
    private float frontieredroite;
    private float frontieregauche;
    [SerializeField] private float distancescie;
    private bool deplacementgauche;
    [SerializeField] private float vitessescie;

    private void Awake()
    {
        frontieredroite = transform.position.x + distancescie;
        frontieregauche = transform.position.x - distancescie;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Vie>().PriseDegat(dommage);
        }
    }

    private void Update()
    {
        if (deplacementgauche)
        {
            if (transform.position.x > frontieregauche) 
            
            {
                transform.Translate(new Vector3(-vitessescie * Time.deltaTime, 0f, 0f));
                //transform.position = new Vector3(transform.position.x - vitessescie * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                deplacementgauche = false;
            }
        }
        else
        {
            if (transform.position.x < frontieredroite) 
            {
                transform.Translate(new Vector3(vitessescie * Time.deltaTime, 0f, 0f));
                //transform.position = new Vector3(transform.position.x + vitessescie * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                deplacementgauche = true;
            }
        }
        
    }
}
