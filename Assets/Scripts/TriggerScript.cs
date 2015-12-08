using UnityEngine;
using System.Collections;

public enum TriggerType
{
    Door, CaveEntrance, CameraPositionerRight, CameraPositionerLeft, Rotator
}

public class TriggerScript : MonoBehaviour {

    public TriggerType type;
    public string message;
	public Vector3 rotation;

    private bool interactable;
	private bool hasRotated;
    //private 

	// Use this for initialization
	void Start () {
		hasRotated = false;
        switch (type)
        {
            case TriggerType.Door:
                interactable = true;
                break;
            case TriggerType.CaveEntrance:
                interactable = false;
                break;
            case TriggerType.CameraPositionerRight:
                interactable = false;
                break;
            case TriggerType.CameraPositionerLeft:
                interactable = false;
                break;
            default:
                print("Woops!?");
                break;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool Interact()
    {
        if (interactable)
        {

        }

        return interactable;
    }

    public bool isInteractable()
    {
        return interactable;
    }

    public string getMessage()
    {
        return message;
    }

	public Vector3 getRotation(){
		Vector3 rot = rotation;
		if(hasRotated){
			rot *= -1;
		}
		hasRotated = !hasRotated;
		return rot;
	}
}
