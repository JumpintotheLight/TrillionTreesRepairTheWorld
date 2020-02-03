/// <summary>
/// CodeArtist.mx 2015
/// This is the main class of the project, its in charge of raycasting to a model and place brush prefabs infront of the canvas camera.
/// If you are interested in saving the painted texture you can use the method at the end and should save it to a file.
/// </summary>


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Valve.VR;
using System;


//public enum Painter_BrushMode { PAINT, DECAL };
public class TreePainter : MonoBehaviour
{
    public GameObject brushCursor, brushContainer; //The cursor that overlaps the model and our container for the brushes painted
    //public Camera sceneCamera, canvasCam;  //The camera that looks at the model, and the camera that looks at the canvas.
    //public Camera canvasCam;
    public Sprite cursorPaint; // cursorDecal; // Cursor for the differen functions 
                               //public RenderTexture canvasTexture; // Render Texture that looks at our Base Texture and the painted brushes
                               // public Material baseMaterial; // The material of our base texture (Were we will save the painted texture)

    Renderer renderer;
    MeshCollider meshCollider;

    public GameObject waterPrefab; 

    //lenght of ray
    public float rayLength = 300.0f; //0.3f;


    public GameObject[] treePrefabs;

    public int treePrefabIndex; 
    //Painter_BrushMode mode; //Our painter mode (Paint brushes or decals)
    float brushSize = 1.0f; //The size of our brush
    Color brushColor; //The selected color
    int brushCounter = 0, MAX_BRUSH_COUNT = 1000; //To avoid having millions of brushes
    bool saving = false; //Flag to check if we are saving the texture

    //public SteamVR_Action_Boolean TexturePaint;
    //// a reference to the hand
    //public SteamVR_Input_Sources handType;


    public delegate void OnAddSeed1();
    public static event OnAddSeed1 onAddSeed1;

    public void BroadcastOnAddSeed1()
    {
        if (onAddSeed1 != null)
        {
            onAddSeed1();

        }
    }


    public delegate void OnAddSeed2();
    public static event OnAddSeed2 onAddSeed2;

    public void BroadcastOnAddSeed2()
    {
        if (onAddSeed2 != null)
        {
            onAddSeed2();

        }
    }


    public delegate void OnAddSeed3();
    public static event OnAddSeed3 onAddSeed3;

    public void BroadcastOnAddSeed3()
    {
        if (onAddSeed3 != null)
        {
            onAddSeed3();

        }
    }


    public delegate void OnAddSeed4();
    public static event OnAddSeed4 onAddSeed4;

    public void BroadcastOnAddSeed4()
    {
        if (onAddSeed4 != null)
        {
            onAddSeed4();

        }
    }


    [Tooltip("the controller the [Paint Brush] object is attached to")]
    public GameObject VRController;
    SteamVR_TrackedObject trackedObj;
    [HideInInspector]
    public SteamVR_Controller.Device controller;

    //GameObject controller;

    bool buttonpressed = false;

    //public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    Debug.Log("Trigger is up");
    //    //Sphere.GetComponent<MeshRenderer>().enabled = false;
    //    buttonpressed = false;
    //}
    //public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    Debug.Log("Trigger is down");
    //    //Sphere.GetComponent<MeshRenderer>().enabled = true;
    //    buttonpressed = true;

    //}


    private void Awake()
    {
        //controller = GameObject.Find("Controller (right)");
        //TexturePaint.AddOnStateDownListener(TriggerDown, handType);
        //TexturePaint.AddOnStateUpListener(TriggerUp, handType);

        //controller
        try { trackedObj = VRController.GetComponent<SteamVR_TrackedObject>(); }
        catch (Exception e) { print("VRController object doesn't seem to exist or has been moved?"); };
    }
    void Update()
    {
        //brushColor = ColorSelector.GetColor();  //Updates our painted color with the selected color
        // FIXME
        //if (Input.GetMouseButton(0)) {
        //	DoAction();
        //}
        //if (buttonpressed)
        //{
        //    DoAction();

        //}


        UpdateBrushCursor();

        //get controller buttons
        controller = SteamVR_Controller.Input((int)trackedObj.index);

        //painting
        if (controller != null) // && !ray.busy)
        {
            if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x > 0.8f)
            {
                Debug.Log("TexturePainter: DoAction()");
                DoAction();
            }

        }
        //UpdateBrushCursor();
    }

    //The main action, instantiates a brush or decal entity at the clicked position on the UV map
    void DoAction()
    {
        //if (saving)
        //    return;
        Vector3 worldPosition = Vector3.zero;
        Vector3 normal = Vector3.one;
        bool iswater = false; 
        if (HitTestWorldPosition(ref worldPosition, ref normal, ref iswater))
        {


            // Display Brush Cursor at world postioin and normal
            Debug.Log("TexturePainter: HitTestWorldPosition()");

            if (!iswater)

            {

                GameObject brushObj;
                // FIXME - can cause null reference if not checked!!!
                brushObj = (GameObject)Instantiate(treePrefabs[treePrefabIndex]);

                brushObj.transform.parent = brushContainer.transform;
                brushObj.transform.position = worldPosition; //The position of the brush (in the UVMap)
                                                             //brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush

                brushObj.transform.rotation = brushContainer.transform.rotation;
                brushObj.transform.up = normal;

                switch (treePrefabIndex)
                {
                    case 0:
                        print("BroadcastOnAddSeed1");
                        BroadcastOnAddSeed1();
                        break;
                    case 1:
                        print("BroadcastOnAddSeed2");
                        BroadcastOnAddSeed2();
                        break;
                    case 2:
                        print("BroadcastOnAddSeed3");
                        BroadcastOnAddSeed3();
                        break;
                    case 3:
                        print("BroadcastOnAddSeed4");
                        BroadcastOnAddSeed4();
                        break;

                }
            }


            if(iswater)
            {
                // instantiate water prefab and make sound

                GameObject waterObj;
                waterObj = (GameObject)Instantiate(waterPrefab);

                waterObj.transform.parent = brushContainer.transform;
                waterObj.transform.position = worldPosition; //The position of the brush (in the UVMap)
                                                             //brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush

                //waterObj.transform.rotation = brushContainer.transform.rotation;
                waterObj.transform.up = normal;
            }



            //if (mode == Painter_BrushMode.PAINT)
            //{

            //    brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/BrushEntity")); //Paint a brush
            //    brushObj.GetComponent<SpriteRenderer>().color = brushColor; //Set the brush color
            //}
            //else
            //{
            //    brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/DecalEntity")); //Paint a decal
            //}
            //brushColor.a = brushSize * 2.0f; // Brushes have alpha to have a merging effect when painted over.
            //brushObj.transform.parent = brushContainer.transform; //Add the brush to our container to be wiped later
            //brushObj.transform.localPosition = uvWorldPosition; //The position of the brush (in the UVMap)
            //brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush
        }
        brushCounter++; //Add to the max brushes
        if (brushCounter >= MAX_BRUSH_COUNT)
        { //If we reach the max brushes available, flatten the texture and clear the brushes
            //brushCursor.SetActive(false);
            //saving = true;
            //Invoke("SaveTexture", 0.1f);

        }
    }
    //To update at realtime the painting cursor on the mesh
    void UpdateBrushCursor()
    {
        Vector3 worldPosition = Vector3.zero;
        Vector3 normal = Vector3.one;
        bool iswater = false;
        if (HitTestWorldPosition(ref worldPosition, ref normal, ref iswater) )
        {
            brushCursor.SetActive(true);
            brushCursor.transform.position = worldPosition;
            //brushCursor.transform.position += brushCursor.transform
            //brushCursor.transform.rotation = brushContainer.transform.rotation;
            brushCursor.transform.up = normal;
            brushCursor.transform.rotation = brushCursor.transform.rotation * (new Quaternion(90,0,0,1));
        }
        else
        {
            brushCursor.SetActive(false);
        }
    }

    bool HitTestWorldPosition(ref Vector3 worldPosition, ref Vector3 normal, ref bool iswater)
    {
        RaycastHit hit;
        //Ray cursorRay = new Ray(controller.transform.position, controller.transform.forward);

        //if (Physics.Raycast(cursorRay, out hit, 200))
        Vector3 fwd = VRController.transform.TransformDirection(Vector3.forward);


        //if (Physics.Raycast(cursorRay, out hit, 200))
        //if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        if (Physics.Raycast(VRController.transform.position, fwd, out hit, rayLength))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;

            worldPosition = hit.point;
            normal = hit.normal;

            renderer = hit.transform.gameObject.GetComponent<Renderer>();
            meshCollider = hit.collider as MeshCollider;

            //if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
            //{
            //    return false;
            //}

            int materialIdx = -1;

            Mesh mesh = meshCollider.sharedMesh;
            int triangleIdx = hit.triangleIndex;
            int lookupIdx1 = mesh.triangles[triangleIdx * 3];
            int lookupIdx2 = mesh.triangles[triangleIdx * 3 + 1];
            int lookupIdx3 = mesh.triangles[triangleIdx * 3 + 2];

            int subMeshesNr = mesh.subMeshCount;
            for (int i = 0; i < subMeshesNr; i++)
            {
                int[] tr = mesh.GetTriangles(i);
                for (int j = 0; j < tr.Length; j += 3)
                {
                    if (tr[j] == lookupIdx1 && tr[j + 1] == lookupIdx2 && tr[j + 2] == lookupIdx3)
                    {
                        materialIdx = i;
                        break;
                    }
                }
                if (materialIdx != -1) break;
            }
            if (materialIdx != -1)
            {
                Debug.Log("-------------------- I'm using " + renderer.materials[materialIdx].name + " material(s)");

                if(renderer.materials[materialIdx].name == "Material_Water (Instance)" ) iswater = true; 
            }

            return true;
        }
        else
        {
            return false;
        }
    }
    //Returns the position on the texuremap according to a hit in the mesh collider
    //bool HitTestUVPosition(ref Vector3 uvWorldPosition)
    //{
    //    RaycastHit hit;

    //    //FIXME - Use Vive Controller instead
    //    //Vector3 cursorPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
    //    //Ray cursorRay=sceneCamera.ScreenPointToRay (cursorPos);


    //    //Debug.Log("Found controller : " + go.name);
    //    //Ray cursorRay = new Ray(controller.transform.position, controller.transform.forward);

    //    //if (Physics.Raycast(cursorRay, out hit, 200))
    //    Vector3 fwd = VRController.transform.TransformDirection(Vector3.forward);


    //    //if (Physics.Raycast(cursorRay, out hit, 200))
    //    //if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
    //    if (Physics.Raycast(VRController.transform.position, fwd, out hit, rayLength))
    //    {
    //        MeshCollider meshCollider = hit.collider as MeshCollider;
    //        if (meshCollider == null || meshCollider.sharedMesh == null)
    //            return false;
    //        Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
    //        uvWorldPosition.x = pixelUV.x - canvasCam.orthographicSize;//To center the UV on X
    //        uvWorldPosition.y = pixelUV.y - canvasCam.orthographicSize;//To center the UV on Y
    //        uvWorldPosition.z = 0.0f;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}
    //Sets the base material with a our canvas texture, then removes all our brushes
    void SaveTexture()
    {
        brushCounter = 0;
        System.DateTime date = System.DateTime.Now;
        //RenderTexture.active = canvasTexture;
        //Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGB24, false);
        //tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
        //tex.Apply();
        //RenderTexture.active = null;
        //baseMaterial.mainTexture = tex; //Put the painted texture as the base
        foreach (Transform child in brushContainer.transform)
        {//Clear brushes
            Destroy(child.gameObject);
        }
        //StartCoroutine ("SaveTextureToFile"); //Do you want to save the texture? This is your method!
        //Invoke("ShowCursor", 0.1f);
    }
    //Show again the user cursor (To avoid saving it to the texture)
    //void ShowCursor()
    //{
    //    saving = false;
    //}

    ////////////////// PUBLIC METHODS //////////////////

    //public void SetBrushMode(Painter_BrushMode brushMode)
    //{ //Sets if we are painting or placing decals
    //    mode = brushMode;
    //    brushCursor.GetComponent<SpriteRenderer>().sprite = brushMode == Painter_BrushMode.PAINT ? cursorPaint : cursorDecal;
    //}
    public void SetBrushSize(float newBrushSize)
    { //Sets the size of the cursor brush or decal
        brushSize = newBrushSize;
        brushCursor.transform.localScale = Vector3.one * brushSize;
    }

    ////////////////// OPTIONAL METHODS //////////////////

//#if !UNITY_WEBPLAYER
//    IEnumerator SaveTextureToFile(Texture2D savedTexture)
//    {
//        brushCounter = 0;
//        string fullPath = System.IO.Directory.GetCurrentDirectory() + "\\UserCanvas\\";
//        System.DateTime date = System.DateTime.Now;
//        string fileName = "CanvasTexture.png";
//        if (!System.IO.Directory.Exists(fullPath))
//            System.IO.Directory.CreateDirectory(fullPath);
//        var bytes = savedTexture.EncodeToPNG();
//        System.IO.File.WriteAllBytes(fullPath + fileName, bytes);
//        Debug.Log("<color=orange>Saved Successfully!</color>" + fullPath + fileName);
//        yield return null;
//    }
//#endif
}
