using UnityEngine;
using System.Collections;

public class InteractHandeler : MonoBehaviour {

    public GUI_Handeler handeler;

    // Kamera
    public GameObject kamera;
    public float lerpSpeed;
    public Vector3 cL;
    public Vector3 cR;
    private Vector3 originalPos;
    private Vector3 targetPos;

    private TriggerScript tempTS;
    private string messageToDisplay;

	// Use this for initialization
	void Start () {
        originalPos = kamera.transform.localPosition;
        targetPos = originalPos;
	}
	
	// Update is called once per frame
	void Update () {
        kamera.transform.localPosition= Vector3.Lerp(kamera.transform.localPosition, targetPos, lerpSpeed);
	}

    void OnTriggerEnter(Collider collider)
    {
        print("Traff");
        tempTS = collider.transform.gameObject.GetComponent<TriggerScript>();
        if (tempTS.isInteractable())
        {
            messageToDisplay = tempTS.getMessage();
        }
        else
        {
            TriggerType type = tempTS.type;
            switch (type)
            {
                case TriggerType.CameraPositionerLeft:
                    targetPos = cL;
                    break;
                case TriggerType.CameraPositionerRight:
                    targetPos = cR;
                    break;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        tempTS = collider.transform.gameObject.GetComponent<TriggerScript>();
        if (tempTS.isInteractable())
        {
            messageToDisplay = tempTS.getMessage();
        }
        else
        {
            TriggerType type = tempTS.type;
            if (type == TriggerType.CameraPositionerLeft || type == TriggerType.CameraPositionerRight)
            {
                targetPos = originalPos;
            }
        }
        tempTS = null;
        messageToDisplay = "";
        HideOnInt();
    }

    private void Interact()
    {
        tempTS.Interact();
    }

    private void DisplayOnInt()
    {
        handeler.DisplayInteracter(messageToDisplay);
    }

    private void HideOnInt()
    {
        handeler.HideInteracter();
    }
}
