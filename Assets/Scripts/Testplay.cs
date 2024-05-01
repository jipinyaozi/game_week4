using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testplay : MonoBehaviour
{
    public class IDPrefabPair
    {
        public int ID;
        public GameObject Prefab;
    }

    public Canvas canvas;
    public leveleditormanager level;
    public List<IDPrefabPair> prefabPairs;
    public GameObject player;
    private Dictionary<int, GameObject> prefabDictionary;
    public Movement control;
    public Vector3 spawnpos;
    public Camera editorCam;
    public Camera testCam;

    // Start is called before the first frame update
    void Start()
    {
        control = player.GetComponent<Movement>();
        player.gameObject.SetActive(false);
        testCam.gameObject.SetActive(false);

    }

    // Update is called once per frame
    public void playtest()
    {
        canvas.gameObject.SetActive(false);
        player.transform.position = level.SpawnPoint;
        editorCam.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        testCam.gameObject.SetActive(true);
        Debug.Log(level.SpawnPoint);

    }
}
