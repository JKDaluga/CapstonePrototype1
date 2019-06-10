using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public float RiseSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.velocity = new Vector3(0, RiseSpeed, 0);
            print("TEST");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
