using UnityEditor;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMerger1 : MonoBehaviour
{
    public Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(MergeMeshs());
    }

    // Update is called once per frame
    IEnumerator MergeMeshs()
    {
        yield return new WaitForSeconds(3f);
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
//        print(meshFilters.Length);

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            print("yep " + meshFilters[i].sharedMesh);
            i++;
        }

        mesh = new Mesh();
        mesh.CombineMeshes(combine);

        mesh.RecalculateNormals();
        
        transform.GetComponent<MeshFilter>().mesh = mesh;
        //transform.GetComponent<MeshCollider>().sharedMesh = mesh;
        //transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        

        transform.gameObject.SetActive(true);
        transform.position = new Vector3(0f,0f,0f);
    }
}
