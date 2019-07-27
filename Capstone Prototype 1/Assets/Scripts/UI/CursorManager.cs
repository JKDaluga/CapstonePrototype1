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
        if (obj == null || obj.GetComponent<IInteractable>() == null)
        {
            crossHairs.color = Color.white;
        }
        else
        {
            crossHairs.color = Color.yellow;
        }
    }
}
