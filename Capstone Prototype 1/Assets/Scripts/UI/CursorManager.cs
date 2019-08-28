using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    Dictionary<string, Sprite>  cursorImages;
    private Image crossHairs;

    public GameObject IceIcon;
    public GameObject WaterIcon;
    public GameObject WaterIcon1;
    public GameObject SteamIcon;
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
            IceIcon.SetActive(false);
            WaterIcon.SetActive(false);
            WaterIcon1.SetActive(false);
            SteamIcon.SetActive(false);
        }
        else
        {
            crossHairs.color = Color.yellow;
            if (obj.tag == "Ice")
            {
                IceIcon.SetActive(false);
                WaterIcon.SetActive(false);
                WaterIcon1.SetActive(true);
                SteamIcon.SetActive(false);
            }
            else if(obj.tag == "water")
            {
                IceIcon.SetActive(true);
                WaterIcon.SetActive(false);
                WaterIcon1.SetActive(false);
                SteamIcon.SetActive(true);
            }
            else if(obj.tag == "Steam")
            {
                IceIcon.SetActive(false);
                WaterIcon.SetActive(true);
                WaterIcon1.SetActive(false);
                SteamIcon.SetActive(false);
            }
            else
            {
                IceIcon.SetActive(false);
                WaterIcon.SetActive(false);
                WaterIcon1.SetActive(false);
                SteamIcon.SetActive(false);
            }
        }
    }
}
