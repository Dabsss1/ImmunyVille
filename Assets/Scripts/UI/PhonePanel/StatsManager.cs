using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Sprite maleSprite, femaleSprite;
    [SerializeField] GameObject AvatarDisplay;

    // Start is called before the first frame update
    void Start()
    {
        name.text = PlayerData.playerName;

        if(PlayerData.gender == "male")
        {
            AvatarDisplay.GetComponent<Image>().sprite = maleSprite;
            //AvatarDisplay.GetComponent<SpriteRenderer>().sprite = maleSprite;
        }
        else if (PlayerData.gender == "female")
        {
            AvatarDisplay.GetComponent<Image>().sprite = femaleSprite;
            //AvatarDisplay.GetComponent<SpriteRenderer>().sprite = femaleSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
