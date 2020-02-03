using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class DetachMyBaby : MonoBehaviour
{
    public virtual void FixedUpdate()
    {
        foreach (Transform baby in this.transform)
        {
            baby.parent = null;
        }
    }

}