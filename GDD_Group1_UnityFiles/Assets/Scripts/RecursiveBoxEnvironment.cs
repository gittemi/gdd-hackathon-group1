using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RecursiveBoxEnvironment : MonoBehaviour
{
    public int recursionLevel = 2;
    public int mainRecursionLevel = 2;
    public GameObject[] listOfObjects;

    // Relationships between world space and box space
    public Transform worldCentre;
    public Transform boxCentre;
    public float boxScale = 0.25f;

    public Camera camera;

    GameObject[] duplicatedObjects;

    GameObject DuplicateObject2(GameObject obj)
    {
        obj.SetActive(false);
        GameObject newObj = Instantiate(obj);
        obj.SetActive(true);

        // Figure out which components are important. Disable those that aren't
        foreach (Component component in newObj.GetComponents<Component>())
        {
            if (!(component is Transform || component is SpriteRenderer || component is Grid || component is Tilemap || component is TilemapRenderer
                || component is RecursiveBoxEnvironment))
            {
                Destroy(component);
                //component.enabled(false);
            }
        }


        //// For all children of obj, duplicate it ans set it as child of newObj
        //for (int i = 0; i < obj.transform.childCount; i++)
        //{
        //    Transform childTransform = obj.transform.GetChild(i);
        //    GameObject newChild = DuplicateObject2(childTransform.gameObject);
        //    newChild.transform.SetParent(newObj.transform);
        //}

        //newObj.SetActive(true);

        return newObj;
    }

    // Duplicate and return game object & its duplicated children
    GameObject DuplicateObject(GameObject obj)
    {
        // GameObject newObj = Instantiate(obj);
        GameObject newObj = new GameObject();

        // Copy transform
        Transform objTransform = obj.GetComponent<Transform>();

        if (objTransform != null)
        {
            Transform newObjTransform = newObj.GetComponent<Transform>();
            newObjTransform.position = objTransform.position;
            newObjTransform.rotation = objTransform.rotation;
            newObjTransform.localScale = objTransform.localScale;
        }

        // Copy sprite renderer
        SpriteRenderer objSpriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (objSpriteRenderer != null)
        {
            SpriteRenderer newObjSpriteRenderer = newObj.AddComponent<SpriteRenderer>();
            newObjSpriteRenderer.sprite = objSpriteRenderer.sprite;
            newObjSpriteRenderer.color = objSpriteRenderer.color;
        }

        // Copy Grid
        Grid objGrid = obj.GetComponent<Grid>();
        if(objGrid != null)
        {
            Grid newObjGrid = newObj.AddComponent<Grid>();
            newObjGrid.cellSize = objGrid.cellSize;
            newObjGrid.cellGap = objGrid.cellGap;
            newObjGrid.cellLayout = objGrid.cellLayout;
            newObjGrid.cellSwizzle = objGrid.cellSwizzle;
        }

        // Copy Tilemap
        Tilemap objTilemap = obj.GetComponent<Tilemap>();
        if(objTilemap != null)
        {
            Tilemap newObjTilemap = newObj.AddComponent<Tilemap>();
            newObjTilemap.animationFrameRate = objTilemap.animationFrameRate;
            newObjTilemap.color = objTilemap.color;
            newObjTilemap.tileAnchor = objTilemap.tileAnchor;
            newObjTilemap.orientation = objTilemap.orientation;
        }

        // Copy TilemapRenderer
        TilemapRenderer objTilemapRenderer = obj.GetComponent<TilemapRenderer>();
        if(objTilemapRenderer != null)
        {
            TilemapRenderer newObjTilemapRenderer = newObj.AddComponent<TilemapRenderer>();
            newObjTilemapRenderer.sortOrder = objTilemapRenderer.sortOrder;
            newObjTilemapRenderer.mode = objTilemapRenderer.mode;
            newObjTilemapRenderer.detectChunkCullingBounds = objTilemapRenderer.detectChunkCullingBounds;
            newObjTilemapRenderer.maskInteraction = objTilemapRenderer.maskInteraction;
            newObjTilemapRenderer.material = objTilemapRenderer.material;

            Bounds bound = objTilemapRenderer.bounds;
            BoundsInt bounds = new BoundsInt(
                Mathf.FloorToInt(bound.min.x),
                Mathf.FloorToInt(bound.min.y),
                Mathf.FloorToInt(bound.min.z),
                Mathf.FloorToInt(bound.size.x),
                Mathf.FloorToInt(bound.size.y),
                Mathf.FloorToInt(bound.size.z)
            );
            TileBase[] tiles = obj.GetComponent<Tilemap>().GetTilesBlock(bounds);
            Debug.Log("Tiles: " + tiles.Length + " | " + bound.size.x + " | " + bound.size.y);
            newObjTilemapRenderer.GetComponent<Tilemap>().SetTilesBlock(bounds, tiles);
        }

        // For all children of obj, duplicate it ans set it as child of newObj
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            Transform childTransform = obj.transform.GetChild(i);
            GameObject newChild = DuplicateObject(childTransform.gameObject);
            newChild.transform.SetParent(newObj.transform);
        }

        return newObj;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create a set of duplicated objects having just the transform and renderers. 
        duplicatedObjects = new GameObject[listOfObjects.Length];
        Debug.Log(duplicatedObjects.Length);

        // Create duplicates of all objects
        for(int i = 0; i < listOfObjects.Length; i++)
        {
            Debug.Log(listOfObjects[i].name);
            duplicatedObjects[i] = DuplicateObject2(listOfObjects[i]);
        }

        // Find box centre and make changes to it
        GameObject bCentre;
        foreach(GameObject newObj in duplicatedObjects)
        {
            for (int i = 0; i < newObj.transform.childCount; i++)
            {
                Transform childTransform = newObj.transform.GetChild(i);
                GameObject child = childTransform.gameObject;
                
                if (child.tag == "BoxCentre")
                {
                    BoxCentreController bcController = child.GetComponent<BoxCentreController>();
                    bCentre = child;

                    bcController.reference = this.transform;
                    bcController.toAdd
                }
            }
                
        }

        // Find the object that is recursive. Make the required changes to it to make the recursion work
        foreach(GameObject newObj in duplicatedObjects)
        {
            for (int i = 0; i < newObj.transform.childCount; i++)
            {
                Transform childTransform = newObj.transform.GetChild(i);
                GameObject child = childTransform.gameObject;
                if (child.tag == "Recursive")
                {
                    RecursiveBoxEnvironment childEnv = child.GetComponent<RecursiveBoxEnvironment>();
                    if (recursionLevel < 0)
                    {
                        Destroy(child);
                        break;
                    }

                    if (childEnv != null)
                    {
                        Debug.Log("FOUND THE THING ");
                        //Destroy(child);
                        childEnv.recursionLevel = this.recursionLevel - 1;
                        childEnv.listOfObjects = this.duplicatedObjects;

                        childEnv.worldCentre = this.boxCentre;
                        //childEnv.boxCentre = childEnv.transform.GetChild(0);
                    }
                }
            }
            newObj.SetActive(true);
        }

        

        // Centre all new objects around portal
        //Vector3 deltaPos = boxCentre.position - worldCentre.position;

        //for (int i = 0; i < listOfObjects.Length; i++)
        //{
        //    duplicatedObjects[i].transform.position += deltaPos + new Vector3(0.75f, 0f, 0f);
        //    duplicatedObjects[i].transform.localScale *= boxScale;
        //    //duplicatedObjects[i].transform.localScale *= new Vector3(1f, 1f, boxScale);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPos = boxCentre.position - worldCentre.position;

        for (int i = 0; i < listOfObjects.Length; i++)
        {
            duplicatedObjects[i].transform.position = listOfObjects[i].transform.position + deltaPos;
            duplicatedObjects[i].transform.position = duplicatedObjects[i].transform.position + (boxCentre.position - duplicatedObjects[i].transform.position) * (1 - boxScale);
            duplicatedObjects[i].transform.localScale = listOfObjects[i].transform.localScale * boxScale;
            //duplicatedObjects[i].transform.localScale *= new Vector3(1f, 1f, boxScale);
        }

        // If main recursion level, update camera position
        if(camera != null && recursionLevel == mainRecursionLevel)
        {
            camera.transform.position = boxCentre.transform.position + new Vector3(0f, 0f, -10f);
        }
    }

    private void FixedUpdate()
    {
        if(recursionLevel == mainRecursionLevel)
        {
            //GetComponent<Transform>().position += new Vector3(Time.fixedDeltaTime, 0f, 0f);
        }
        
    }
}
