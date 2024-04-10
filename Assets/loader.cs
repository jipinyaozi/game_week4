using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Globalization;

public class loader : MonoBehaviour
{

    [Serializable]
    public class IDPrefabPair
    {
        public int ID;
        public GameObject Prefab;
    }
    public InputField inputField;
    public List<IDPrefabPair> prefabPairs; // Assign in the inspector
    public Button load;
    public GameObject player;
    private Dictionary<int, GameObject> prefabDictionary;
    public Movement control;

    void Start()
    {
        InitializePrefabDictionary();
        control = player.GetComponent<Movement>();
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
        var lines = inputText.Split('\n');

    foreach (var line in lines)
    {
        if (string.IsNullOrWhiteSpace(line))
            continue;

        try
        {
            var idPart = line.Split(new[] { "ID: " }, StringSplitOptions.None)[1];
            var idString = idPart.Split(',')[0];
            int id = int.Parse(idString.Trim());


            var positionPart = line.Split(new[] { "Position: " }, StringSplitOptions.None)[1];
            var positionString = positionPart.Trim(new[] { '(', ')' });
            var coords = positionString.Split(',');

            if (coords.Length == 3)
            {
                float x = float.Parse(coords[0].Trim(), CultureInfo.InvariantCulture);
                float y = float.Parse(coords[1].Trim(), CultureInfo.InvariantCulture);
                float z = 0;

                Vector3 position = new Vector3(x, y, z);
                if(id == 2)
                {
                    control.SpawnPoint.position = position;
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
        }
        catch (FormatException fe)
        {
            Debug.LogError($"FormatException occurred. Line: '{line}' Error: {fe.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while processing line: '{line}'. Error: {ex.Message}");
        }
    }
    // Destroy(inputField);
    // Destroy(load);
    load.gameObject.SetActive(false);
    inputField.gameObject.SetActive(false);
    control.kill();
    }
}
