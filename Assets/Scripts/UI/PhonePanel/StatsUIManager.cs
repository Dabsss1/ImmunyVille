using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] Sprite maleSprite, femaleSprite;
    [SerializeField] GameObject AvatarDisplay;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PlayerDataManager.Instance.playerName;

        if(PlayerDataManager.Instance.gender == "male")
        {
            AvatarDisplay.GetComponent<Image>().sprite = maleSprite;
            //AvatarDisplay.GetComponent<SpriteRenderer>().sprite = maleSprite;
        }
        else if (PlayerDataManager.Instance.gender == "female")
        {
            AvatarDisplay.GetComponent<Image>().sprite = femaleSprite;
            //AvatarDisplay.GetComponent<SpriteRenderer>().sprite = femaleSprite;
        }
    }
}
