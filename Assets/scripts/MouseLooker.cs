using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooker : MonoBehaviour
{

    public float Xsensitivity = 2f;
    
    public float xMax = 90f;
    public float xMin = -90f;

   // private bool isLock =  true;
    private float xRot;
    
    void Update()
    {
        SeeAround();
        
    }

    public void SeeAround()
    {
        xRot += Input.GetAxis("Mouse X") * Xsensitivity;
        transform.eulerAngles = new Vector3(0, xRot, 0);

       /* if (xRot<=xMax && xRot>=xMin)
          {
             
          }*/

    }
    
}
