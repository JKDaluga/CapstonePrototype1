using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Pipe exitPipe;
    public Transform exitSpot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePipe(Pipe exit)
    {
        exitPipe = exit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Gas")
        {
            other.gameObject.transform.position = exitSpot.position;
        }
    }
}
