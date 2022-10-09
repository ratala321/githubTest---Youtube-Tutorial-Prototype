using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPorte : MonoBehaviour
{
    [SerializeField] GameObject room1;
    [SerializeField] GameObject room2;

    [Header ("Camera")]
    [SerializeField] private Transform salleprecedente;
    [SerializeField] private Transform salleSuivante;
     [SerializeField] private MouvementCamera mouvementCamera;
    // private MouvementCamera camessai; ne fonctionne pas, pourquoi?

    private void Awake()
    {
        //camessai = GetComponent<MouvementCamera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                mouvementCamera.MouvementNewRoom(salleSuivante);
                
                salleSuivante.GetComponent<RoomReset>().BackToRoom(true);
                salleprecedente.GetComponent<RoomReset>().BackToRoom(false);
            }
            else 
            {
                mouvementCamera.MouvementNewRoom(salleprecedente);
                
                room2.GetComponent<RoomReset>().BackToRoom(false);
                room1.GetComponent<RoomReset>().BackToRoom(true);
            }
        }
    }
}
