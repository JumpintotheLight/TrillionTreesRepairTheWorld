using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Plant : MonoBehaviour
{
    public Color GizmoColor;
    public float GizmoSize;
    public Vector3 GrowSizeMin;
    public System.Collections.Generic.List<PlantStageClass> Stages;
    public bool Loop;
    public int LoopFrom;
    private int StageId;
    private GameObject _plant;
    private System.Collections.Generic.List<Transform> Mass;
    private float currentTime;
    public virtual void Start()
    {
        this.transform.localScale = new Vector3(this.GrowSizeMin.x, this.GrowSizeMin.y, this.GrowSizeMin.z);
        this.StageId = -1;
        this.StartCoroutine(this.NextStage());
    }

    public virtual void LateUpdate()
    {
        if (this.StageId >= 0)
        {
            _plant.transform.localScale = this.transform.localScale;
            if (this.currentTime > 0f)
            {
                this.currentTime = this.currentTime - Time.deltaTime;
                if (this.Stages[this.StageId].action == PlantStageClass.Action.Grow)
                {
                    if (this.Stages[this.StageId].X)
                    {

                        {
                            float _1 = this.transform.localScale.x + (this.Stages[this.StageId].GrowSpeed * Time.deltaTime);
                            Vector3 _2 = this.transform.localScale;
                            _2.x = _1;
                            this.transform.localScale = _2;
                        }
                    }
                    if (this.Stages[this.StageId].Y)
                    {

                        {
                            float _3 = this.transform.localScale.y + (this.Stages[this.StageId].GrowSpeed * Time.deltaTime);
                            Vector3 _4 = this.transform.localScale;
                            _4.y = _3;
                            this.transform.localScale = _4;
                        }
                    }
                    if (this.Stages[this.StageId].Z)
                    {

                        {
                            float _5 = this.transform.localScale.z + (this.Stages[this.StageId].GrowSpeed * Time.deltaTime);
                            Vector3 _6 = this.transform.localScale;
                            _6.z = _5;
                            this.transform.localScale = _6;
                        }
                    }
                }
                if (this.Stages[this.StageId].action == PlantStageClass.Action.Decrease)
                {
                    if (this.Stages[this.StageId].X && (this.transform.localScale.x > 0f))
                    {

                        {
                            float _7 = this.transform.localScale.x - (this.Stages[this.StageId].GrowSpeed * Time.deltaTime);
                            Vector3 _8 = this.transform.localScale;
                            _8.x = _7;
                            this.transform.localScale = _8;
                        }
                    }
                    if (this.Stages[this.StageId].Y && (this.transform.localScale.y > 0f))
                    {

                        {
                            float _9 = this.transform.localScale.y - (this.Stages[this.StageId].GrowSpeed * Time.deltaTime);
                            Vector3 _10 = this.transform.localScale;
                            _10.y = _9;
                            this.transform.localScale = _10;
                        }
                    }
                    if (this.Stages[this.StageId].Z && (this.transform.localScale.z > 0f))
                    {

                        {
                            float _11 = this.transform.localScale.z - (this.Stages[this.StageId].GrowSpeed * Time.deltaTime);
                            Vector3 _12 = this.transform.localScale;
                            _12.z = _11;
                            this.transform.localScale = _12;
                        }
                    }
                }
                if (this.Stages[this.StageId].rotation == PlantStageClass.Rotation.Rotate)
                {
                    this.transform.Rotate(this.Stages[this.StageId].RotationSpeed.x * Time.deltaTime, this.Stages[this.StageId].RotationSpeed.y * Time.deltaTime, this.Stages[this.StageId].RotationSpeed.z * Time.deltaTime);
                }
            }
            else
            {
                if ((this.StageId + 1) < this.Stages.Count)
                {
                    this.StartCoroutine(this.NextStage());
                }
                else
                {
                    if (this.Loop)
                    {
                        this.StageId = this.LoopFrom - 1;
                        this.StartCoroutine(this.NextStage());
                    }
                }
            }
        }
        if (this.Stages[this.StageId].rotation == PlantStageClass.Rotation.Billboard)
        {
            this.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public virtual IEnumerator NextStage()
    {
        this.StageId++;
        this.currentTime = this.Stages[this.StageId].GrowTime;
        _plant = UnityEngine.Object.Instantiate(this.Stages[this.StageId].GrowGO, this.transform.position, this.transform.rotation);
        _plant.transform.parent = this.transform;
        _plant.transform.localScale = this.transform.localScale;
        yield return new WaitForSeconds(0.001f);
        foreach (Transform child in this.transform)
        {
            this.Mass.Add(child);
            if (this.Mass.Count > 1)
            {
                UnityEngine.Object.Destroy(this.Mass[0].gameObject);
                this.Mass.RemoveAt(0);
            }
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = this.GizmoColor;
        Gizmos.DrawCube(this.transform.position, new Vector3(this.GizmoSize, this.GizmoSize, this.GizmoSize));
    }

    public Plant()
    {
        this.GizmoColor = new Color(255, 0, 0, 255);
        this.GizmoSize = 0.5f;
        this.Stages = new List<PlantStageClass>();
        this.Mass = new List<Transform>();
    }

}