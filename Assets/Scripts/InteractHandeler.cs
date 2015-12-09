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
	private PlayerMoveScript moveScript;
    private string messageToDisplay;
	private int rotatorStack;

	// Use this for initialization
	void Start () {
		rotatorStack = 0;
		moveScript = GetComponent<PlayerMoveScript>();
        originalPos = kamera.transform.localPosition;
        targetPos = originalPos;
	}
	
	// Update is called once per frame
	void Update () {
        kamera.transform.localPosition= Vector3.Lerp(kamera.transform.localPosition, targetPos, lerpSpeed);

		if(ErFri()){
			//print("ErFri");
			if(!moveScript.IsAlligned()){
				//print("IsAlligned");
				//moveScript.AllignPlayer();
				transform.forward = Vector3.forward;
				moveScript.TurnVelocity(transform.eulerAngles*(-1));
			}
		}
	}

    void OnTriggerEnter(Collider collider)
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
				case TriggerType.Rotator:
					rotatorStack++;
					Vector3 nyRot = transform.eulerAngles;
					Vector3 appRot = tempTS.getRotation();
					nyRot += appRot;
					kamera.transform.SetParent(null);
					transform.eulerAngles = nyRot;
					moveScript.TurnVelocity(appRot);
					kamera.transform.SetParent(transform);
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
			if(type == TriggerType.Rotator){
				rotatorStack--;
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

	private bool ErFri(){
		return rotatorStack == 0;
	}
}
