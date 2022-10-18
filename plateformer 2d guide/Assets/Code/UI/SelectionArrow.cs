using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options; 
    private RectTransform rect;
    private int currentpositiony;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //arrow
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        //interaction option
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        options[currentpositiony].GetComponent<Button>().onClick.Invoke();
    }

    private void ChangePosition(int _change)
    {
        currentpositiony += _change;

        //Keep it on the menu, lenght-1 parce que lenght = 3, mais le array s'arrête à 2 (0,1,2)
        if (currentpositiony < 0)
            currentpositiony = options.Length - 1;
        else if (currentpositiony > options.Length - 1)
            currentpositiony = 0;

        rect.position = new Vector3(rect.transform.position.x, options[currentpositiony].position.y, rect.transform.position.z);
    }

}
