using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 tempVector3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempVector3 = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        tempVector3.z = 0;
        transform.position = tempVector3;
    }
}
