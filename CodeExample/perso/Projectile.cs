using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float vitesseBoule;
    
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anor;

    private float direction;

    private float esperance;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anor = GetComponent<Animator>();
    }

    //déplacement + temps de vie en Update
    private void Update()
    {
        if (hit) return;
        float vitesseBouleTime = vitesseBoule * Time.deltaTime * direction;
        transform.Translate(vitesseBouleTime, 0f, 0f);

        esperance += Time.deltaTime;
        if (esperance > 5) gameObject.SetActive(false);
    }


    //frappe de l'explosion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anor.SetTrigger("explosion");

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Vie>().PriseDegat(1);
        }
    }


    //Direction + allignement
    public void SetDirection(float _direction)
    {
        esperance = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(_direction != Mathf.Sign(localScaleX) )
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3 (localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //fin d'animation explosion event
    private void Desactivation()
    {
        gameObject.SetActive(false);
    }
}
