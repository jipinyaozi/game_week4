using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour
{
    public GameObject itemInfo;

    void Start()
    {
        itemInfo.SetActive(false);
    }

    public void ShowInfo()
    {
        itemInfo.SetActive(true);
    }

    public void HideInfo()
    {
        itemInfo.SetActive(false);
    }
}