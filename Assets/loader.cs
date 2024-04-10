using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Globalization;
using TMPro;

public class loader : MonoBehaviour
{

    [Serializable]
    public class IDPrefabPair
    {
        public int ID;
        public GameObject Prefab;
    }
    public TMP_InputField inputField;
    public List<IDPrefabPair> prefabPairs; // Assign in the inspector
    public Button load;
    public GameObject player;
    private Dictionary<int, GameObject> prefabDictionary;
    public Movement control;
    public Vector3 spawnpos;

    void Start()
    {
        InitializePrefabDictionary();
        control = player.GetComponent<Movement>();
        player.gameObject.SetActive(false);
    }

    private void InitializePrefabDictionary()
    {
        prefabDictionary = new Dictionary<int, GameObject>();
        foreach (var pair in prefabPairs)
        {
            prefabDictionary[pair.ID] = pair.Prefab;
        }
    }

    public void InstantiateObjectsFromInput() // Ensure this is public to access it from the inspector
    {
        string inputText = inputField.text;

    var objectDataStrings = inputText.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

    foreach (var dataString in objectDataStrings)
    {
        if (string.IsNullOrWhiteSpace(dataString))
            continue;

        try
        {
            // Extract ID and position from the data string
            var idString = dataString.Split(new[] { "ID: ", ", Position: " }, StringSplitOptions.RemoveEmptyEntries)[0];
            var positionString = dataString.Split(new[] { "ID: ", ", Position: " }, StringSplitOptions.RemoveEmptyEntries)[1];
            positionString = positionString.Trim(new char[] { '(', ')' });

            // Parse the ID and position
            int id = int.Parse(idString);
            var coords = positionString.Split(',');
            float x = float.Parse(coords[0].Trim(), CultureInfo.InvariantCulture);
            float y = float.Parse(coords[1].Trim(), CultureInfo.InvariantCulture);
            float z = float.Parse(coords[2].Trim(), CultureInfo.InvariantCulture);

            // Use the ID to find the corresponding prefab and instantiate it at the given position
            Vector3 position = new Vector3(x, y, z);
                        if(id == 2)
            {
                control.SpawnPoint.position = position;
                spawnpos = position;
            }
            if (prefabDictionary.TryGetValue(id, out GameObject prefab))
            {
                Instantiate(prefab, position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"No prefab assigned for ID {id}");
            }
        }
        catch (FormatException fe)
        {
            Debug.LogError($"FormatException occurred. Data String: '{dataString}' Error: {fe.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while processing data string: '{dataString}'. Error: {ex.Message}");
        }
    }
    
    // Destroy(inputField);
    // Destroy(load);
    load.gameObject.SetActive(false);
    inputField.gameObject.SetActive(false);
    player.transform.position = spawnpos;
    player.gameObject.SetActive(true);
    

    }
}