using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReset : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private Vector3[] initialposition;

    private void Awake()
    {

        initialposition = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {

         
            {
                if(enemies[i]!= null)
                initialposition[i] = enemies[i].transform.position;
            }


        }
    }
    public void BackToRoom(bool _activation)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            {
                enemies[i].transform.position = initialposition[i];
                enemies[i].SetActive(_activation);
            }
        }
    }

}
