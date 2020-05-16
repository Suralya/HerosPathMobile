using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBillboard : MonoBehaviour
{
    private Quaternion SetRotation;
    // Start is called before the first frame update
    void Start()
    {
        SetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation != SetRotation)
        {
            transform.rotation = SetRotation;
        }
    }
}
