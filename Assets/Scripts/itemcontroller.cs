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
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        Instantiate(editor.Itemimages[ID], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);

        Clicked = true;
        editor.CurrentbuttonPressed = ID;
    }



}
