using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private int count;

    [SerializeField]
    private GameObject spherePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{transform.position} | {transform.localPosition}");
        HingeJoint joint = GetComponent<HingeJoint>();

        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        var center = (meshes[0].bounds.center + meshes[1].bounds.center + meshes[2].bounds.center + meshes[3].bounds.center) / 4;
        joint.anchor = center;

        for (int i = 0; i < count; i++)
        {
            GameObject sphere = Instantiate(spherePrefab);
            sphere.transform.position = center;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
