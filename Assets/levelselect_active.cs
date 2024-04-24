using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelselect_active : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    public void activepanel()
    {
        panel.SetActive(true);
    }
    public void deactivepanel()
    {
        panel.SetActive(false);
    }
}
