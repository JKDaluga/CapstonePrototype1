using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    Dictionary<string, Sprite>  cursorImages;
    private Image crossHairs;
    // Start is called before the first frame update

    void Start()
    {
        crossHairs = GetComponent<Image>();
        cursorImages = new Dictionary<string, Sprite>();
        cursorImages.Add("default", crossHairs.sprite);
    }

    public void SetCurser(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Steam":
            case "Ice":
                crossHairs.color = Color.yellow;
                break;
            default:
                crossHairs.color = Color.white;
                break;
        }
    }
}
