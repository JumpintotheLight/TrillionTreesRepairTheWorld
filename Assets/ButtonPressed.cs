using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameManager_v2.onButtonClick += buttonPressed;
    }
    

    public void buttonPressed()
    {
        Debug.Log("ButtonPressed.");
        //GameManager_v2.Instance.PrintMessage();

        //GameManager_v2.Instance.AddTreeOfType(tree_types_v2.Fur);


        //GameManager.PrintMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
