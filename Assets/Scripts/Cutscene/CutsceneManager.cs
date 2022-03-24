using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CutsceneState { ANIMATION, DIALOG} 
public class CutsceneManager : MonoBehaviour
{
    
    public Sprite maleSprite, femaleSprite;
    [SerializeField] GameObject partnerSprite, npcFemale, npcMale;
    [SerializeField] GameObject[] player;
    [HideInInspector]
    [SerializeField] GameObject partner;
    [SerializeField] GameObject doctor, nurse;

    [SerializeField] CutsceneDialogs[] dialog;
    [SerializeField] GameObject darkPanel;
    [SerializeField] GameObject dialogBox;

    [SerializeField] GameObject mainCamera,flashbackCamera1,flashbackCamera2,flashbackCamera3;

    [SerializeField] string nextScene="Bedroom";
    
    CutsceneState state;

    public static Action OnDialogEnd;

    Vector2 npcSpawn = new Vector2(-8.5f,2f);
    int currentDialogArray = 0;

    public string currentSound;

    // Start is called before the first frame update
    void Awake()
    {
        //SaveSystem.LoadPlayer();
    }

    private void OnEnable()
    {
        
        DisableCameras();
        mainCamera.SetActive(true);

        OnDialogEnd += incrementDialogArray;
    }

    private void OnDisable()
    {
        OnDialogEnd -= incrementDialogArray;
    }

    private void Start()
    {
        if (PlayerDataManager.Instance.gender == "male")
        {
            foreach(GameObject p in player)
                p.GetComponent<SpriteRenderer>().sprite = maleSprite;

            partnerSprite.GetComponent<SpriteRenderer>().sprite = femaleSprite;
            partner = Instantiate(npcFemale, npcSpawn, Quaternion.identity);
        }
        else if (PlayerDataManager.Instance.gender == "female")
        {
            foreach (GameObject p in player)
                p.GetComponent<SpriteRenderer>().sprite = femaleSprite;

            partnerSprite.GetComponent<SpriteRenderer>().sprite = maleSprite;
            partner = Instantiate(npcMale, npcSpawn, Quaternion.identity);
        }
        PlaySound("IntroStart");
        currentSound = "IntroStart";
        nurse.GetComponent<CharacterController>().setFaceDir(1,0);
        state = CutsceneState.DIALOG;

        StartCoroutine(StartScene());
    }

    public void ButtonPress()
    {
        if (state != CutsceneState.DIALOG)
            return;
        callDialog();    
    }

    void incrementDialogArray()
    {
        state = CutsceneState.ANIMATION;
        switch (currentDialogArray)
        {
            case 0:
                {
                    StartCoroutine(TransScene0to1());
                    currentDialogArray++;
                    break;
                }
            case 1:
                {
                    StartCoroutine(TransScene1to2());
                    currentDialogArray++;
                    break;
                }
            case 2:
                {
                    StartCoroutine(TransScene2to3());
                    currentDialogArray++;
                    break;
                }
            case 3:
                {
                    StartCoroutine(TransScene3to4());
                    currentDialogArray++;
                    break;
                }
            case 4:
                {
                    AudioManager.Instance.Stop("Calm");
                    currentSound = "";
                    currentDialogArray++;
                    state = CutsceneState.DIALOG;
                    callDialog();
                    break;
                }
            case 5:
                {
                    AudioManager.Instance.Play("Flashback");
                    currentSound = "Flashback";
                    currentDialogArray++;
                    state = CutsceneState.DIALOG;
                    callDialog();
                    break;
                }
            case 6:
                {
                    //flashback starts
                    StartCoroutine(TransScene6to7());
                    currentDialogArray++;
                    break;
                }
            case 7:
                {
                    StartCoroutine(TransScene7to8());
                    currentDialogArray++;
                    break;
                }
            case 8:
                {
                    StartCoroutine(TransScene8to9());
                    currentDialogArray++;
                    break;
                }
            case 9:
                {
                    StartCoroutine(TransScene9to10());
                    currentDialogArray++;
                    break;
                }
            case 10:
                {
                    StartCoroutine(TransScene10to11());
                    currentDialogArray++;
                    break;
                }
            case 11:
                {
                    StartCoroutine(TransScene11to12());
                    currentDialogArray++;
                    break;
                }
            case 12:
                {
                    StartCoroutine(TransScene12to13());
                    currentDialogArray++;
                    break;
                }
            case 13:
                {
                    PlaySound("Motivational");
                    currentDialogArray++;
                    state = CutsceneState.DIALOG;
                    callDialog();
                    break;
                }
            case 14:
                {
                    AudioManager.Instance.Stop("Motivational");
                    SceneLoaderManager.OnSceneLoad(nextScene);
                    break;
                }
        }
    }

    void callDialog()
    {
        CutsceneDialogManager.Instance.showDialog(dialog[currentDialogArray]);
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1f);
        callDialog();
    }
    IEnumerator TransScene0to1()
    {
        FadeOut();
        yield return new WaitForSeconds(1f);

        PlaySound("Calm");

        yield return new WaitForSeconds(1f);
        dialogBox.SetActive(true);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene1to2()
    {
        StartCoroutine(nurse.GetComponent<CharacterController>().Move(new Vector2(-8,0)));
        yield return new WaitForSeconds(3f);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene2to3()
    {
        StartCoroutine(partner.GetComponent<CharacterController>().Move(new Vector2(9, 0)));
        yield return new WaitForSeconds(2f);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene3to4()
    {
        StartCoroutine(MovePartner());
        
        StartCoroutine(doctor.GetComponent<CharacterController>().Move(new Vector2(8, 0)));

        yield return new WaitForSeconds(2f);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene6to7()
    {
        FadeIn();
        yield return new WaitForSeconds(1f);
        DisableCameras();
        flashbackCamera1.SetActive(true);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene7to8()
    {
        FadeOut();
        yield return new WaitForSeconds(2f);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene8to9()
    {
        FadeIn();
        yield return new WaitForSeconds(1f);
        DisableCameras();
        flashbackCamera2.SetActive(true);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene9to10()
    {
        FadeOut();
        yield return new WaitForSeconds(1f);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene10to11()
    {
        FadeIn();
        yield return new WaitForSeconds(2f);
        DisableCameras();
        flashbackCamera3.SetActive(true);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene11to12()
    {
        FadeOut();
        yield return new WaitForSeconds(1f);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator TransScene12to13()
    {
        FadeIn();
        yield return new WaitForSeconds(1f);
        DisableCameras();
        mainCamera.SetActive(true);
        FadeOut();
        yield return new WaitForSeconds(1f);
        state = CutsceneState.DIALOG;
        callDialog();
    }

    IEnumerator MovePartner()
    {
        StartCoroutine(partner.GetComponent<CharacterController>().Move(new Vector2(0, -1)));
        yield return new WaitForSeconds(.2f);
        StartCoroutine(partner.GetComponent<CharacterController>().Move(new Vector2(2, 0)));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(partner.GetComponent<CharacterController>().Move(new Vector2(0, 1)));
        yield return new WaitForSeconds(.2f);
        partner.GetComponent<CharacterController>().setFaceDir(-1, 0);
    }

    void DisableCameras()
    {
        mainCamera.SetActive(false);
        flashbackCamera1.SetActive(false);
        flashbackCamera2.SetActive(false);
        flashbackCamera3.SetActive(false);
    }

    void FadeIn()
    {
        darkPanel.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    void FadeOut()
    {
        darkPanel.GetComponent<Animator>().SetTrigger("FadeOut");
    }
    
    void PlaySound(string sound)
    {
        if (currentSound != sound)
        {
            AudioManager.Instance.Stop(currentSound);
            currentSound = sound;
            AudioManager.Instance.Play(sound);
        }
    }

    public void SkipCutscene()
    {
        AudioManager.Instance.Stop(currentSound);
        SceneLoaderManager.OnSceneLoad(nextScene);
    }
}

