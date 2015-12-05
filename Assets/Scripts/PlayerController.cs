using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private PlayerMoveScript moveScript;

    private bool peikerVenstre;

	// Use this for initialization
	void Start () {
        moveScript = GetComponent<PlayerMoveScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            moveScript.Jump();
        }
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            if (peikerVenstre)
            {
                moveScript.Turn();
                peikerVenstre = false;
            }

            moveScript.ApplyForce(Vector3.right);
        }
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            if (!peikerVenstre)
            {
                moveScript.Turn();
                peikerVenstre = true;
            }

            moveScript.ApplyForce(Vector3.left);
        }
        if (Input.GetButtonDown("Run"))
        {
            moveScript.setRunning(true);
        }
        if (Input.GetButtonUp("Run"))
        {
            moveScript.setRunning(false);
        }
    }
}
