using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurRespawn : MonoBehaviour
{

    private Transform currentCheckpoint; //latest checkpoint
    private Vie vieJoueur;
    private UIManager uimanager;

    private void Awake()
    {
        vieJoueur = GetComponent<Vie>();
        uimanager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            //Game Over Screen
            uimanager.GameOver();
            
            return; // arrêt de l'exécution
            
        }

        transform.position = currentCheckpoint.position; //joueur retourne au checkpoint
        //Vie du joueur restaurée et relancement des animations
        vieJoueur.Respawn();

        Camera.main.GetComponent<MouvementCamera>().MouvementNewRoom(currentCheckpoint.parent);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {

            currentCheckpoint = collision.transform;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Apparition");

        }
    }
}
