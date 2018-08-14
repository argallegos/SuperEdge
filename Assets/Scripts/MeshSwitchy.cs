using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSwitchy : MonoBehaviour {


    public Mesh fall, idle, run, runJump, sprint, standJump, climb, wallDrop, wallHang, wallJump;

    void Start () {
        GetComponent<MeshFilter>().mesh = idle;

    }
	

    public void SwitchMesh(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
