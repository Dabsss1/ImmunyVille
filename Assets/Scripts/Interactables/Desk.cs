using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour,Interactable
{
    public Vector3 interactPosition;

    public void Interact()
    {
        Vector3 interactPos = transform.position + interactPosition;
        //interactPos.y -= .5f;

        Collider2D collider = Physics2D.OverlapCircle(interactPos, 0.2f, GameLayers.Instance.Interactable);
        if (collider != null)
        {
            //call the interact function on the interactable
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
}
