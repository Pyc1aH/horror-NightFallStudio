using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCam;
    public float interactionDistance = 10f;
    public static bool buttonDown = false;

    public GameObject interactionUI;

    void Update()
    {
        InteractionRay();
    }

    public void onClick()
    {
        buttonDown = true;
    }


    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        
        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;

                if (buttonDown)
                {
                    interactable.Interact();
                }

            }
        }
        buttonDown = false;
        interactionUI.SetActive(hitSomething);
    }
}
