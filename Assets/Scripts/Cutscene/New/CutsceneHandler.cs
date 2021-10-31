using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneHandler : MonoBehaviour
{
    public List<CutscenePart> cutsceneParts;

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        foreach (CutscenePart part in cutsceneParts)
        {
            StartCoroutine(part.character.Move(new Vector2(part.movement[0].MoveX, part.movement[0].MoveY)));

            DialogManager.Instance.showDialog(part.dialog, part.character.GetComponent<NpcController>().characterName);
        }
        yield return null;
    }
}
