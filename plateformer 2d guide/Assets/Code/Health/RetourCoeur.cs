using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetourCoeur : MonoBehaviour
{
    [SerializeField] private Vie vie;
    private float retour = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Vie>().RetourDeVie(retour);
            gameObject.SetActive(false);
        }
    }
}
