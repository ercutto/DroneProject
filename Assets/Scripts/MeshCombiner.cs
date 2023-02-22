using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PinBall
{
    public class MeshCombiner : MonoBehaviour
    {
      
        [SerializeField] private MeshFilter[] sourceMeshFilters;
        [SerializeField] private MeshFilter targetMeshFilter;
        [ContextMenu(itemName:"CombineMeshes")]
        private void CombineMeshes()
        {
            
            if (GetComponentsInChildren<MeshFilter>() != null)
            { sourceMeshFilters = GetComponentsInChildren<MeshFilter>(); }
            
            var combine = new CombineInstance[sourceMeshFilters.Length];
            for (int i = 0; i < sourceMeshFilters.Length; i++)
            {
                combine[i].mesh = sourceMeshFilters[i].sharedMesh;
                combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
            }
            var mesh = new Mesh();
            mesh.CombineMeshes(combine);
            targetMeshFilter.mesh = mesh;
        }
    }
}

