using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRotations : MonoBehaviour
{
    public Quaternion[] rotations;
    int count;

    void Start()
    {
        count = 0;
        rotations = new Quaternion[24];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotations[count] = transform.rotation;
            count++;
        }
    }
}
