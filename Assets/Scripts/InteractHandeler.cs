using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private PlayerMoveScript moveScript;
    private string messageToDisplay;
    private int lastTriggerWay;
    private RotatorScript rs;
    private List<RotatorScript> rotatorStack;

    // Use this for initialization
    void Start () {
        lastTriggerWay = 1;
		rotatorStack = new List<RotatorScript>();
		moveScript = GetComponent<PlayerMoveScript>();
        originalPos = kamera.transform.localPosition;
        targetPos = originalPos;
	}
	
	// Update is called once per frame
	void Update () {
        kamera.transform.localPosition= Vector3.Lerp(kamera.transform.localPosition, targetPos, lerpSpeed);

		if(ErFri()){
			if(!moveScript.IsAlligned()){
                NyForward(Vector3.forward, true);
			}
		}
	}

    void OnTriggerEnter(Collider collider)
    {
        string tagg = collider.transform.tag;
        if (tagg.Equals("Trigger"))
        {
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

        if (tagg.Equals("Rotator"))
        {
            rs = collider.transform.gameObject.GetComponent<RotatorScript>();
            rotatorStack.Add(rs);
            Vector3 nyFram = rs.getRotation(transform.position, false);
            NyForward(nyFram, true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        string tagg = collider.transform.tag;
        if (tagg.Equals("Trigger"))
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

        if (tagg.Equals("Rotator"))
        {
            rotatorStack.Remove(collider.transform.gameObject.GetComponent<RotatorScript>());
            if (ErFri())
            {
                rs = null;
            }
            else
            {
                rs = rotatorStack[rotatorStack.Count - 1];
                Vector3 nyFram = rs.getRotation(transform.position, false);
                NyForward(nyFram, false);
            }
        }
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

	private bool ErFri(){
		return rotatorStack.Count == 0;
	}

    private void NyForward(Vector3 r, bool snuVel)
    {
        kamera.transform.SetParent(null);
        transform.forward = r;
        kamera.transform.SetParent(transform);
        if (snuVel)
        {
            moveScript.Face(transform.right * lastTriggerWay);
        }
    }

    public void DidTurn()
    {
        lastTriggerWay *= (-1);
        if (!ErFri())
        {
            foreach(RotatorScript r in rotatorStack)
            {
                if(r == rs)
                {
                    Vector3 nyFram = rs.getRotation(transform.position, true);
                    NyForward(nyFram, false);
                }
                else
                {
                    r.SwitchRotaded();
                }
            }
        }
    }
}
