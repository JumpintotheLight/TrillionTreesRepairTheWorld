//using UnityEngine;
//using System.Collections;

//public class GetMaterialFromRaycast : MonoBehaviour
//{
//    Ray ray;
//    RaycastHit hit;
//    Renderer renderer;
//    MeshCollider meshCollider;
//    public void Update()
//    {
//        RaycastHit hit;

//        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        if (!Physics.Raycast(ray.origin, ray.direction, hit, 100))
//        {
//            return;
//        }

//         renderer = hit.transform.gameObject.GetComponent<Renderer>();
//         meshCollider = hit.collider as MeshCollider;

//        if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
//        {
//            return;
//        }

//        int materialIdx = -1;

//        Mesh mesh = meshCollider.sharedMesh;
//        int triangleIdx = hit.triangleIndex;
//        int lookupIdx1 = mesh.triangles[triangleIdx * 3];
//        int lookupIdx2 = mesh.triangles[triangleIdx * 3 + 1];
//        int lookupIdx3 = mesh.triangles[triangleIdx * 3 + 2];

//        int subMeshesNr = mesh.subMeshCount;
//        for (int i = 0; i< subMeshesNr; i++)
//        {
//            int[] tr = mesh.GetTriangles(i);
//            for (int j = 0; j < tr.Length; j += 3)
//            {
//                if (tr[j] == lookupIdx1 && tr[j + 1] == lookupIdx2 && tr[j + 2] == lookupIdx3)
//                {
//                    materialIdx = i;
//                    break;
//                }
//            }
//            if (materialIdx != -1) break;
//        }
//        if (materialIdx != -1)
//        {
//            Debug.Log("-------------------- I'm using " + renderer.materials[materialIdx].name + " material(s)");
//        }
//    }


//}
