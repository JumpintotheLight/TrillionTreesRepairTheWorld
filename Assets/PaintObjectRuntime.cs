using UnityEngine;
//using UnityEditor;

namespace PrefabPainter
{
    [System.Serializable]
    public class PaintObjectRuntime
    {
        public GameObject prefab;
        //private GameObject go; 
        public Vector2 size = Vector2.one;
        public bool randomRotationX = false;
        public bool randomRotationY = false;
        public bool randomRotationZ = false;
        public string prefabName;

        public bool settingsToggled;

        //[HideInInspector] public Editor gameObjectEditor;

        public PaintObjectRuntime(GameObject go)
        {
            this.prefab = go;
        }

        public void displaySettings()
        {

            // FIXME
            //EditorGUILayout.BeginVertical(PrefabPainter.BoxStyle);
            //GUILayout.Space(3);

            //if (prefabName == "") GUILayout.Label("Prefab Settings", EditorStyles.boldLabel);
            //else GUILayout.Label("Prefab Settings - " + prefabName, EditorStyles.boldLabel);

            //size.x = EditorGUILayout.FloatField("Min Size", size.x);
            //size.y = EditorGUILayout.FloatField("Max Size", size.y);
            //GUILayout.Label("Random Rotation :");
            //EditorGUILayout.BeginHorizontal();
            //randomRotationX = GUILayout.Toggle(randomRotationX, "X");
            //randomRotationY = GUILayout.Toggle(randomRotationY, "Y");
            //randomRotationZ = GUILayout.Toggle(randomRotationZ, "Z");
            //EditorGUILayout.EndHorizontal();

            //GUILayout.Space(3);
            //EditorGUILayout.EndVertical();
            //GUILayout.Space(0);
        }

        public bool getRandomRotationX()
        {
            return randomRotationX;
        }

        public bool getRandomRotationY()
        {
            return randomRotationY;
        }

        public bool getRandomRotationZ()
        {
            return randomRotationZ;
        }

        public Vector2 getSize()
        {
            return size;
        }

        public GameObject GetGameObject()
        {
            return prefab;
        }

        public void setName(string name)
        {
            prefabName = name;
        }

        public string getName()
        {
            return prefabName;
        }
    }
}