using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset;

    bool isFinal;
    void LateUpdate()
    {
       if (isFinal)
        {
            return;
        }
        else
        {
        transform.position = new Vector3(0f, 0f,playerTransform.position.z) + offset;
        }
       
        
    }
}
