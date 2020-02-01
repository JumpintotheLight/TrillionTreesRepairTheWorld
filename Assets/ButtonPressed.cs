using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onButtonClick += buttonPressed;
    }
    

    public void buttonPressed()
    {
        Debug.Log("ButtonPressed.");
        GameManager.Instance.PrintMessage();

        GameManager.Instance.AddTreeOfType(tree_types.Fur);


        //GameManager.PrintMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
