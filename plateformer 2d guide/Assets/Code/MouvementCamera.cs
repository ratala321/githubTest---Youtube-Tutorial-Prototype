using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementCamera : MonoBehaviour
{
    [SerializeField] private float vitesse;
    private float courantepositionx;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform persotrans;
    [SerializeField] private float vitesseLerp;
    [SerializeField] private float ajoutlerp;
    float avancement;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(courantepositionx, transform.position.y, transform.position.z), ref velocity, vitesse /*changement direct sans transitionavec Time.deltaTime*/);
        //transform.position = new Vector3(persotrans.position.x + avancement, transform.position.y, transform.position.z);
        //avancement = Mathf.Lerp(avancement, ajoutlerp * persotrans.localScale.x, vitesseLerp*Time.deltaTime);
    }

    public void MouvementNewRoom(Transform _newRoom)
    {
        courantepositionx = _newRoom.position.x;
    }

}
