using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveleditormanager : MonoBehaviour
{
    // Start is called before the first frame update
    public itemcontroller[] Itembuttons;
    public GameObject[] Itemprefabs;
    public int CurrentbuttonPressed;

    private void Update()
    {
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

        if(Input.GetMouseButtonDown(0) && Itembuttons[CurrentbuttonPressed].Clicked)
        {
            Itembuttons[CurrentbuttonPressed].Clicked = false;
            Instantiate(Itemprefabs[CurrentbuttonPressed], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
        }
    }
}
