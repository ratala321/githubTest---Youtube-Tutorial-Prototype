using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPerso : MonoBehaviour
{
    //raccourci rigidbody
    private Rigidbody2D rbd;
    //raccourci spriterenderer
    private SpriteRenderer sr;
    //vitesse
    public int vitesse;
    //public vitesse
    [SerializeField] private int vitessesaut;
    //animation raccourci
    private Animator anor;
    //raccourci boxcollider2D
    BoxCollider2D boxcollider;
    //lien entre layer sol et code
    [SerializeField] private LayerMask solLayer;
    //lien entre layer mur et code
    [SerializeField] private LayerMask murLayer;
    //intervalle saut valeur
    private float intervallesaut;
   
    /*surleSOL
    private bool ausol; */


    float igah;
    
    
    private void Awake()
    {
        //set up du raccourci
        rbd = GetComponent<Rigidbody2D>();
        //activation du raccourci spriterenderer
        sr = GetComponent<SpriteRenderer>();
        //activation du raccourci
        anor = GetComponent<Animator>();
        //activation du raccourci boxcollider2d
        boxcollider = GetComponent<BoxCollider2D>();

    }


    private void Update()

    {
        //set up raccourci Input.GetAxis("Horizontal") pourquoi séquence fonctionne pas sur private void awake?
        igah = Input.GetAxis("Horizontal");

        /*intervallesaut = Time.time + 0.2f;*/

        // en appuyant sur space, le perso saute + cooldown
        if (intervallesaut > 0.2f /*Time.time*/)
        {
            //déplacement joueur selon horizontal * par vitesse
            rbd.velocity = new Vector2(Input.GetAxis("Horizontal") * vitesse, rbd.velocity.y);

            if ( !estausol() && surMur())
            {
                rbd.gravityScale = 0f;
                rbd.velocity = Vector2.zero;
            } else
            { 
                rbd.gravityScale = 5f;
            }
           
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                Jump();
            }
            
            
        }
        else
        {
            intervallesaut += Time.deltaTime;
        }
        //flip du personnage si il va vers la droite ou la gauche
        if (rbd.velocity.x > 0.001f)
        {
            /*sr.flipX = false;*/
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (igah < -0.001f)
        {
            //inversion effective si vitesse négative aka gauche
            gameObject.transform.localScale = new Vector3 (-1f,1f,1f);

            /*
            Cette technique ne fonctionne pas, car une fois le local scale changé, le local scale demeure le même tout le temps et, du coup, le personnage ne se retourne jamais.
            Il aurait fallu que le if plus haut soit aussi en new Vector3 (1f,1f,1f);
            gameObject.transform.localScale = new Vector3 (-1f,1f,1f); */
        }

        //activation de l'animation 
        anor.SetBool("grounded", estausol());
        anor.SetBool("run perso", igah != 0);
       
    }
        private void Jump()
        {
        if (estausol())
        {
            rbd.velocity = new Vector2(rbd.velocity.x, vitessesaut);
            anor.SetTrigger("jump");
        }
        else if (!estausol() && surMur())
            {
            if (igah == 0 )
            {
                rbd.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            { rbd.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6); 
            }

            intervallesaut = 0f;
        }
        
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Sol")
        {
            
        }
    }

    private bool estausol()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0f, Vector2.down, 0.1f, solLayer);

        return raycastHit.collider != null;
    }
    private bool surMur()
    {
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0f, new Vector2(transform.localScale.x,0f), 0.2f, murLayer);
        return raycastHit2.collider !=null;
    }

    public bool canAttaque()
    {
        return igah == 0 && estausol() && !surMur();
    }

}

