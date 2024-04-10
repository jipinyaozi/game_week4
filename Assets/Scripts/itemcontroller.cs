using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemcontroller : MonoBehaviour
{
    public int ID;
    public leveleditormanager editor;
    public bool Clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("leveleditormanager").GetComponent<leveleditormanager>();

        
    }
    public void ButtonClicked()
    {
        Clicked = true;
        editor.CurrentbuttonPressed = ID;
    }



}
