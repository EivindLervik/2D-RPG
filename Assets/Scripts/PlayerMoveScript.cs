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

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
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

        playerModel.transform.eulerAngles = vinkel;
    }

	public void TurnVelocity(Vector3 newDir){
		Vector3 vel = body.velocity;
		float mag = body.velocity.magnitude;

		Vector3 newForward = Quaternion.Euler(newDir) * vel.normalized;

		body.velocity = new Vector3();
		body.velocity = newForward*mag;
	}
}
