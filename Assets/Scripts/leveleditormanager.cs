using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.UI;

public class leveleditormanager : MonoBehaviour
{
    public class tobexport
{
    public int ID;
    public Vector3 pos;

    public override string ToString()
    {
        return $"ID: {ID}, Position: {pos}";
    }
}
    // Start is called before the first frame update
    public itemcontroller[] Itembuttons;
    public GameObject[] Itemprefabs;
    public GameObject[] Itemimages;
    public int CurrentbuttonPressed;
    private bool GoalCreated = false;
    private bool SpawnCreated = false;
    public GameObject spawnholder;
    // public List< int > exportitems = new List<int>();
    // public List<Vector3> positions = new List<Vector3>();
    public List< tobexport > exportitems = new List<tobexport>();
    public TMP_Text warning;
    public InputField Level;

    private Material originalMaterial; // Store the original material of the hovered item
    private bool isHovering = false;

    private void Start()
    {
        Level.gameObject.SetActive(false);

    }
    private void Update()
    {
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

        if(Input.GetMouseButtonDown(0) && Itembuttons[CurrentbuttonPressed].Clicked)
        {
            Itembuttons[CurrentbuttonPressed].Clicked = false;
            if(CurrentbuttonPressed == 4)
            {
                if (GoalCreated)
                {
                    Destroy(GameObject.FindGameObjectWithTag("goal"));
                }
                else
                {
                    GoalCreated = true;
                }

            }
            else if(CurrentbuttonPressed == 2)
            {
                if (SpawnCreated)
                {
                    Destroy(GameObject.FindGameObjectWithTag("spwan"));
                    Destroy(GameObject.FindGameObjectWithTag("spwan_image"));
                    Instantiate(spawnholder, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
                }
                else
                {
                    SpawnCreated = true;
                    Instantiate(spawnholder, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
                }
            }
            else
            {
                GameObject newObject = Instantiate(Itemprefabs[CurrentbuttonPressed], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
                exportitems.Add(new tobexport { ID = CurrentbuttonPressed, pos = new Vector3(worldPosition.x, worldPosition.y, 0) });

                // Add a tag to the newly instantiated object
                newObject.tag = "PlacedItem";

                // Add collider to the newly instantiated object
                Collider2D collider = newObject.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;

                // Store the original material
                Renderer renderer = newObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    originalMaterial = renderer.material;
                }
            }

            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }

        // Check for right-click to delete objects
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("PlacedItem"))
            {
                Destroy(hit.collider.gameObject);
                // Remove the deleted item from the export list
                exportitems.RemoveAll(item => item.pos == hit.collider.gameObject.transform.position);
            }
        }

        // Check for mouse hover over the instantiated objects
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("PlacedItem"))
            {
                isHovering = true;
                return; // Exit loop early if hovering over any object
            }
        }

        isHovering = false; // Reset if not hovering over any object

        // Hover effect
        if (isHovering && originalMaterial != null)
        {
            // Lower the transparency of the hovered item
            originalMaterial.color = new Color(originalMaterial.color.r, originalMaterial.color.g, originalMaterial.color.b, 0.5f);
        } else {
            // Reset the transparency to its original value
            if (originalMaterial != null)
            {
                originalMaterial.color = new Color(originalMaterial.color.r, originalMaterial.color.g, originalMaterial.color.b, 1f);
            }
        }
    }
    
    public void CopyListToClipboard()
    {
        StartCoroutine(warningtext());
        string listString = ConvertListToString(exportitems);
        GUIUtility.systemCopyBuffer = listString;
        Level.gameObject.SetActive(true);
        Level.text = listString;
    }

    private string ConvertListToString(List<tobexport> items)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in items)
        {
            sb.Append(item.ToString());
            sb.Append(";");
        }
        return sb.ToString();
    }
    public IEnumerator warningtext()
    {
        if (!SpawnCreated)
        {
            warning.text = ("You don't have a spawn point!");
            yield return new WaitForSeconds(2f);
            warning.text = "";
        }
        if(!GoalCreated)
        {
            warning.text = ("You don't have a end point!");
            yield return new WaitForSeconds(2f);
            warning.text = "";

        }
    }
}
