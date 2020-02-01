﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum tree_types
{
    Spruce = 0,
    Fur = 1,
    Grass = 2,
}


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int number = 5;

    public static GameManager Instance { get { return _instance; } }

    //public List<tree_types> treeCounter;
    public Dictionary<tree_types, int> treeDictionary; 

    public delegate void OnButtonClick();
    public static event OnButtonClick onButtonClick;


    public void AddTreeOfType(tree_types treetype)
    {
        treeDictionary[treetype] += 1;
        Debug.Log("Number of trees of type : " + treetype + " is " + treeDictionary[treetype]);
    }


    public void BroadcastOnButtonClick()
    {
        if (onButtonClick != null)
        {
            onButtonClick();
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //treeCounter.Add(tree_types.Fur);
        //treeCounter.Add(tree_types.Spruce);
        //treeCounter.Add(tree_types.Grass);
        treeDictionary = new Dictionary<tree_types, int>(); 
        treeDictionary.Add(tree_types.Fur, 0);
        treeDictionary.Add(tree_types.Spruce, 0);
        treeDictionary.Add(tree_types.Grass, 0);


    }



    public void PrintMessage()
    {
        Debug.Log("Hello. My number is " + number);
    }



}