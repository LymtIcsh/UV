using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPlaneFade : MonoBehaviour
{
    public MeshRenderer r;

    public Transform c;

    void Update()
    {
        r.material.SetVector("_Character_Position", new Vector4(c.position.x, c.position.y, c.position.z, 0.0f));
    }
}
