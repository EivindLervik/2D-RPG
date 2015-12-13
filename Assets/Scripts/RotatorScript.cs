using UnityEngine;
using System.Collections;

public class RotatorScript : MonoBehaviour {

    public GameObject point1;
    public GameObject point2;

    private bool hasRotated;

    // Use this for initialization
    void Start () {
        hasRotated = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchRotaded()
    {
        hasRotated = !hasRotated;
    }

    public Vector3 getRotation(Vector3 playerPos, bool getNearest)
    {
        Vector3 rot = new Vector3();

        float p1D = Vector3.Distance(playerPos, point1.transform.position);
        float p2D = Vector3.Distance(playerPos, point2.transform.position);
        if(!getNearest && (p1D > p2D))
        {
            rot = point1.transform.position - playerPos;
        }
        else
        {
            rot = playerPos - point2.transform.position;
        }

        hasRotated = !hasRotated;
        rot.y = 0;
        return Vector3.Cross(rot, Vector3.up);
    }

    public int GetRotIndex()
    {
        int ret = -1;
        if (hasRotated)
        {
            ret = 1;
        }
        return ret;
    }
}
