/// <summary>
/// CodeArtist.mx 2015
/// This is the main class of the project, its in charge of raycasting to a model and place brush prefabs infront of the canvas camera.
/// If you are interested in saving the painted texture you can use the method at the end and should save it to a file.
/// </summary>

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// FIXME - this should not be global!!!
public enum TexturePainter_BrushMode { PAINT, DECAL };


public class TexturePainterVIVE : MonoBehaviour
{

    [Tooltip("the controller the [Paint Brush] object is attached to")]
    public GameObject VRController;
    SteamVR_TrackedObject trackedObj;
    [HideInInspector]
    public SteamVR_Controller.Device controller;

    //raycasting pointer    
    public RaycastHit hitInfo;

    //lenght of ray
    public float rayLength = 30.0f; //0.3f;

    //// For displaying linerenderer
     Color c1 = Color.yellow;
     Color c2 = Color.red;
     int lengthOfLineRenderer = 20;




    public GameObject brushCursor, brushContainer; //The cursor that overlaps the model and our container for the brushes painted
    //public Camera sceneCamera, canvasCam;  //The camera that looks at the model, and the camera that looks at the canvas.
    public Camera canvasCam;  //The camera that looks at the model, and the camera that looks at the canvas.

    public Sprite cursorPaint, cursorDecal; // Cursor for the differen functions 
    public RenderTexture canvasTexture; // Render Texture that looks at our Base Texture and the painted brushes
    public Material baseMaterial; // The material of our base texture (Were we will save the painted texture)

    TexturePainter_BrushMode mode; //Our painter mode (Paint brushes or decals)
    float brushSize = 1.0f; //The size of our brush
    Color brushColor; //The selected color
    int brushCounter = 0, MAX_BRUSH_COUNT = 1000; //To avoid having millions of brushes
    bool saving = false; //Flag to check if we are saving the texture

    private void Awake()
    {

        //controller
        try { trackedObj = VRController.GetComponent<SteamVR_TrackedObject>(); }
        catch (Exception e) { print("VRController object doesn't seem to exist or has been moved?"); };


        if (false)
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.2f;
            lineRenderer.positionCount = lengthOfLineRenderer;

            // A simple 2 color gradient with a fixed alpha of 1.0f.
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            lineRenderer.colorGradient = gradient;

        }



    }


    void Update()
    {

        if(false)
        {
            Vector3 fwd = VRController.transform.TransformDirection(Vector3.forward);

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, VRController.transform.position);
            lineRenderer.SetPosition(1, VRController.transform.position + 30.0f * fwd);

        }



        brushColor = ColorSelector.GetColor();  //Updates our painted color with the selected color
        //if (Input.GetMouseButton(0))
        //{
        //    DoAction();
        //}


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
        UpdateBrushCursor();
    }

    //The main action, instantiates a brush or decal entity at the clicked position on the UV map
    void DoAction()
    {
        if (saving)
            return;
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ref uvWorldPosition))
        {
            GameObject brushObj;
            if (mode == TexturePainter_BrushMode.PAINT)
            {

                brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/BrushEntity")); //Paint a brush
                brushObj.GetComponent<SpriteRenderer>().color = brushColor; //Set the brush color
            }
            else
            {
                brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/DecalEntity")); //Paint a decal
            }
            brushColor.a = brushSize * 2.0f; // Brushes have alpha to have a merging effect when painted over.
            brushObj.transform.parent = brushContainer.transform; //Add the brush to our container to be wiped later
            brushObj.transform.localPosition = uvWorldPosition; //The position of the brush (in the UVMap)
            brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush
        }
        brushCounter++; //Add to the max brushes
        if (brushCounter >= MAX_BRUSH_COUNT)
        { //If we reach the max brushes available, flatten the texture and clear the brushes
            brushCursor.SetActive(false);
            saving = true;
            Invoke("SaveTexture", 0.1f);

        }
    }
    //To update at realtime the painting cursor on the mesh
    void UpdateBrushCursor()
    {
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ref uvWorldPosition) && !saving)
        {

            Debug.Log("UpdateBrushCursor");
            brushCursor.SetActive(true);
            brushCursor.transform.position = uvWorldPosition + brushContainer.transform.position;
        }
        else
        {
            brushCursor.SetActive(false);
        }
    }
    //Returns the position on the texuremap according to a hit in the mesh collider
    bool HitTestUVPosition(ref Vector3 uvWorldPosition)
    {
        RaycastHit hit;
        //Vector3 cursorPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        //Ray cursorRay=sceneCamera.ScreenPointToRay (cursorPos);

        // Replace Ray from sceneCamera with Ray from Controller. 
        // Use controller instead of transform
        //Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 fwd = VRController.transform.TransformDirection(Vector3.forward);

        //if (Physics.Raycast(transform.position, fwd, out hitInfo, rayLength))
        //{
        //    //menu items
        //    //if (hit.collider.gameObject.name == "RayCollider")

        //}

        //if (Physics.Raycast(cursorRay, out hit, 200))
        //if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        if (Physics.Raycast(VRController.transform.position, fwd, out hit, rayLength))

        {

            Debug.Log("Physics.Raycast hit target object. ");
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            uvWorldPosition.x = pixelUV.x - canvasCam.orthographicSize;//To center the UV on X
            uvWorldPosition.y = pixelUV.y - canvasCam.orthographicSize;//To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }
        else
        {
            return false;
        }

    }
    //Sets the base material with a our canvas texture, then removes all our brushes
    void SaveTexture()
    {
        brushCounter = 0;
        System.DateTime date = System.DateTime.Now;
        RenderTexture.active = canvasTexture;
        Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;
        baseMaterial.mainTexture = tex; //Put the painted texture as the base
        foreach (Transform child in brushContainer.transform)
        {//Clear brushes
            Destroy(child.gameObject);
        }
        //StartCoroutine ("SaveTextureToFile"); //Do you want to save the texture? This is your method!
        Invoke("ShowCursor", 0.1f);
    }
    //Show again the user cursor (To avoid saving it to the texture)
    void ShowCursor()
    {
        saving = false;
    }

    ////////////////// PUBLIC METHODS //////////////////

    public void SetBrushMode(TexturePainter_BrushMode brushMode)
    { //Sets if we are painting or placing decals
        mode = brushMode;
        brushCursor.GetComponent<SpriteRenderer>().sprite = brushMode == TexturePainter_BrushMode.PAINT ? cursorPaint : cursorDecal;
    }
    public void SetBrushSize(float newBrushSize)
    { //Sets the size of the cursor brush or decal
        brushSize = newBrushSize;
        brushCursor.transform.localScale = Vector3.one * brushSize;
    }

    ////////////////// OPTIONAL METHODS //////////////////

#if !UNITY_WEBPLAYER
    IEnumerator SaveTextureToFile(Texture2D savedTexture)
    {
        brushCounter = 0;
        string fullPath = System.IO.Directory.GetCurrentDirectory() + "\\UserCanvas\\";
        System.DateTime date = System.DateTime.Now;
        string fileName = "CanvasTexture.png";
        if (!System.IO.Directory.Exists(fullPath))
            System.IO.Directory.CreateDirectory(fullPath);
        var bytes = savedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath + fileName, bytes);
        Debug.Log("<color=orange>Saved Successfully!</color>" + fullPath + fileName);
        yield return null;
    }
#endif
}
