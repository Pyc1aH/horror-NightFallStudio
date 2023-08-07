using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{

    private bool opf;


    public void Interact()
    {
        if(PlayerInteraction.buttonDown)
        {
            Fonarik.Energy += 25;
            Destroy (gameObject);
        }
    }
}
