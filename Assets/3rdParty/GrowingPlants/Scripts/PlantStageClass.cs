using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlantStageClass : object
{
    public enum Action
    {
        Grow = 0,
        Decrease = 1
    }


    public enum Rotation
    {
        None = 0,
        Rotate = 1,
        Billboard = 2
    }


    public float GrowTime;
    public GameObject GrowGO;
    public float GrowSpeed;
    public bool X;
    public bool Y;
    public bool Z;
    public PlantStageClass.Action action;
    public PlantStageClass.Rotation rotation;
    public Vector3 RotationSpeed;
    public PlantStageClass()
    {
        this.X = true;
        this.Y = true;
        this.Z = true;
    }

}