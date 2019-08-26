using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotHorizontal = 10;
    public float rotVertical = 10;

    private float vertical = 0;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X")*rotHorizontal + transform.parent.localEulerAngles.y;
        transform.parent.localEulerAngles = new Vector3(0, horizontal, 0);

        vertical -= Input.GetAxis("Mouse Y")*rotVertical;
        vertical = Mathf.Clamp(vertical, -60, 60);
        transform.localEulerAngles = new Vector3(vertical, 0, 0);
    }
}
