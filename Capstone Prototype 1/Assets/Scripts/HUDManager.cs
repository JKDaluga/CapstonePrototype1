using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private Image crossHairs;
    private Water target;
    // Start is called before the first frame update

    void Start()
    {
        target = null;
        crossHairs = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hit) && hit.collider.gameObject.CompareTag("water"))
        {
            target = hit.collider.GetComponent<Water>();
            crossHairs.color = Color.yellow;
        }
        else if (target)
        {
            target = null;
            crossHairs.color = Color.white;
        }

        if (target)
        {
            if (Input.GetMouseButtonDown(0)) target.ChangeWaterState(true);
            else if (Input.GetMouseButtonDown(1)) target.ChangeWaterState(false);
        }

    }
}
