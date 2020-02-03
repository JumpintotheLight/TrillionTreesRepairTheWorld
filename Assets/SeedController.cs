using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{

    [Tooltip("_audioClip defines the audio to be played on button click.")]
    public AudioClip _audioClip;

    public GameObject treePainterGO;

    private TreePainter treePainter;

    public GameObject[] prefabs;


    // Start is called before the first frame update
    void Start()
    {
        treePainter = treePainterGO.GetComponent<TreePainter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectCloud()
    {



    }

    public void selectSun()
    {

    }


    public void selectTree1()
    {
        select3DModel( 0);
    }


    public void selectTree2()
    {
        select3DModel(1);
    }


    public void selectTree3()
    {
        select3DModel(2);
    }



    public void selectTree4()
    {
        select3DModel( 3);
    }


    public void select3DModel( int index)
    {
        //treePainter.treePrefab = incomingModel; 
        treePainter.treePrefabIndex = index; 
    }


}
