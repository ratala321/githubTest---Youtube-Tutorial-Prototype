using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //*important pour que Image plus bas soit en vert!!

public class BarreDeVieCode : MonoBehaviour
{
    [SerializeField] private Vie vie;
    [SerializeField] private Image barredevietotale;
    [SerializeField] private Image barredeviecourante;

    private void Start()
    {
        barredevietotale.fillAmount = vie.viepresente / 10;
    }

    private void Update()
    {
        barredeviecourante.fillAmount = vie.viepresente / 10;
    }
}
