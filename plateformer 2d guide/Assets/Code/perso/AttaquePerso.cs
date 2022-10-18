using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaquePerso : MonoBehaviour
{
    [SerializeField] private float intervalleAttaque;
    private Animator anor;
    private DeplacementPerso deplacementPerso;
    private float chronometre = Mathf.Infinity;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bouleDeFeu;

    private void Awake()
    {
        anor = GetComponent<Animator>();
        deplacementPerso = GetComponent<DeplacementPerso>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && chronometre > intervalleAttaque && deplacementPerso.canAttaque() )
            Attaque();

        chronometre += Time.deltaTime;
    }

    private void Attaque()
    {
        anor.SetTrigger("attaque");

        chronometre = 0;

        //object pooling
        bouleDeFeu[PlacementBouleDeFeu()].transform.position = firePoint.position;
        bouleDeFeu[PlacementBouleDeFeu()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int PlacementBouleDeFeu()
    {
        for (int i = 0; i < bouleDeFeu.Length; i++)
        {
            if (!bouleDeFeu[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
