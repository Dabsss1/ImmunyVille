using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerThirstUI : MonoBehaviour
{
    public float hungerCount;
    public float thirstCount;

    public GameObject hungerContentGO;
    public GameObject thirstContentGO;

    public HungerThirstItem htItem;

    public GameObject htIcon;

    private void Update()
    {
        hungerCount = HungerThirst.Instance.hungerStat % 10;                                                                                                                           
        thirstCount = HungerThirst.Instance.thirstStat % 10;

        UpdateHungerThirstUI();
    }

    void UpdateHungerThirstUI()
    {
        foreach (Transform child in hungerContentGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in thirstContentGO.transform)
        {
            Destroy(child.gameObject);
        }

        //hunger
        for(int i=0; i < 10; i++)
        {
            if(hungerCount > 0)
            {
                GameObject hungerIcon = Instantiate(htIcon, hungerContentGO.transform);
                hungerIcon.GetComponent<Image>().sprite = htItem.hungerFilledIcon;
                hungerCount--;
            }
            else
            {
                GameObject hungerIcon = Instantiate(htIcon, hungerContentGO.transform);
                hungerIcon.GetComponent<Image>().sprite = htItem.hungerEmptyIcon;
            }
        }

        //thirst
        for (int i = 0; i < 10; i++)
        {
            if (thirstCount > 0)
            {
                GameObject thristicon = Instantiate(htIcon, thirstContentGO.transform);
                thristicon.GetComponent<Image>().sprite = htItem.thirstFilledIcon;
                thirstCount--;
            }
            else
            {
                GameObject thristicon = Instantiate(htIcon, thirstContentGO.transform);
                thristicon.GetComponent<Image>().sprite = htItem.thirstEmptyIcon;
            }
        }
    }
}
