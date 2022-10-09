using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private GameObject objet;
    public int vitesse;
    public int movement;
    public Vector2 pdep;
   
   
    // Start is called before the first frame update
    void Start()
    {
         //objet = GetComponent<GameObject>();
     // objet =   gameObject.transform.position;
        gameObject.transform.position = pdep;
        /*objet.transform.position = pdep;*/
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector2(1f, 1f) * vitesse * (Time.deltaTime));
    }
}
