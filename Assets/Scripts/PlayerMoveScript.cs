using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

    public GameObject playerModel;

    // Movement
    public float maxSpeed;
    public float maxRunSpeed;
    public float acceleration;
    private bool running;

    // Jumping
    public float jumpForce;
    public bool canDoublejump;
    private bool grounded;
    private bool doublejumping;

    private Rigidbody body;
    private InteractHandeler iH;
    private Vector3 modelTargetAngle;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        iH = GetComponent<InteractHandeler>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    public void ApplyForce(Vector3 direction)
    {
        direction.Normalize();
        Vector3 force = new Vector3();

        force += direction * Time.fixedDeltaTime * 100 * acceleration;

        float bodySpeed = body.velocity.magnitude;

        if (bodySpeed < maxSpeed || (running && bodySpeed < maxRunSpeed))
        {
            body.AddRelativeForce(force);
        }
    }

    public void Jump()
    {
        if (!grounded)
        {
            if (canDoublejump && !doublejumping)
            {

            }
        }
    }

    public void setRunning(bool r)
    {
        running = r;
    }

    public void Turn()
    {
        Vector3 vinkel = playerModel.transform.eulerAngles;
        vinkel.y += 180;

        if(vinkel.y >= 360.0f)
        {
            vinkel.y -= 360.0f;
        }
        else if (vinkel.y <= 0.0f)
        {
            vinkel.y += 360.0f;
        }

        modelTargetAngle = vinkel;
        playerModel.transform.eulerAngles = vinkel;
        iH.DidTurn();
    }

	public void TurnVelocity(Vector3 newDir){
		Vector3 vel = body.velocity;
		float mag = body.velocity.magnitude;

		Vector3 newForward = Quaternion.Euler(newDir) * vel.normalized;

		body.velocity = new Vector3();
		body.velocity = newForward*mag;
	}

	public bool IsAlligned(){
		return transform.right == Vector3.right;
	}

    public void Face(Vector3 retning)
    {
        float mag = body.velocity.magnitude;
        Vector3 lastDirNorm = body.velocity.normalized;
        lastDirNorm.y = 0;
        body.velocity = new Vector3();
        body.velocity = retning * mag;
    }
}
