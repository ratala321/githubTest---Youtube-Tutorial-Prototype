using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections; //Ienumerator

public class Vie : MonoBehaviour
{
    [Header ("Vie") ]
    [SerializeField] private float viedepart;
    public float viepresente { get; private set; }
    private Animator anor;
    private bool dead;

    [Header("Iframes")]
    [SerializeField] private float induree;
    [SerializeField] private int nommbreflash;
    private SpriteRenderer sr;


    private void Awake()
    {
        viepresente = viedepart;
        anor = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }


    public void PriseDegat(float _degat)
    {
        viepresente = Mathf.Clamp(viepresente - _degat, 0f, viedepart);
        
        if(viepresente > 0)
        {
            anor.SetTrigger("mal");
            //Méthode Iframes
            StartCoroutine(Invincible()); 
        }
        else
        {
            if (!dead)
            {
                anor.SetTrigger("mort");
               
                //déplacement perso
                if(GetComponent<DeplacementPerso>() != null)
                GetComponent<DeplacementPerso>().enabled = false;

                //déplacement enemi
                if(GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>();

                if(GetComponent<MeleeEnemy>() != null)
                GetComponent<MeleeEnemy>().enabled = false;

                dead = true;
            }
        }

        

    }

    public void Respawn()
    {
        dead = false;

        RetourDeVie(viedepart);
        anor.ResetTrigger("mort");
        anor.Play("idle perso");
        StartCoroutine(Invincible());

        if (GetComponent<DeplacementPerso>() != null)
            GetComponent<DeplacementPerso>().enabled = true;

        //déplacement enemi
        if (GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>();

        if (GetComponent<MeleeEnemy>() != null)
            GetComponent<MeleeEnemy>().enabled = true;


    }

    public void RetourDeVie(float _heal)
    {
        viepresente = Mathf.Clamp(viepresente + _heal, 0f, viedepart);

    }

    /* private void Update()
     {
         if (Input.GetKeyDown(KeyCode.E))
         {
             PriseDegat(1);
         } */
    
    private IEnumerator Invincible()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);

       for (int i = 0; i < nommbreflash; i++)
        {
            sr.color = new Color(1f, 0f, 0f, 0.5f);
            yield return new WaitForSeconds(induree / nommbreflash * 2);
            sr.color = Color.white;
            yield return new WaitForSeconds(induree / nommbreflash * 2);
        }


        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}

