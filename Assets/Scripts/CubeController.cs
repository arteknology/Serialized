using UnityEngine;
using System;

public class CubeController : MonoBehaviour
{
    public Cube Cube;
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Cube.Position = transform.position;
            DataHandler.Save(UnityDirectory.StreamingAssetPath, Cube, "saved cube");
        }

        else if(Input.GetButton("Fire2"))
        {
            DataHandler.Load<Cube>(UnityDirectory.StreamingAssetPath, "saved cube");
            transform.position = Cube.Position;
        }
    }
}

[Serializable]
public class Cube
{
    public string Name;
    public Vector3 Position;
}
