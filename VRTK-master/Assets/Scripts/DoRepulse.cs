using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoRepulse : MonoBehaviour {

	private Rigidbody thisRigidbody;

	public float sphRadius = 5;
    private float sphRadiusSqr = 25;
	public float repulse = 0.5f; //was 5
    GameObject[] nodelist;


    // Use this for initialization

    void Start () {
		thisRigidbody = this.GetComponent<Rigidbody>();
        sphRadiusSqr = sphRadius * sphRadius;
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        sphRadius = GameObject.Find("GameController").GetComponent<GraphController>().sphRadius;
        repulse = GameObject.Find("GameController").GetComponent<GraphController>().repulse;
        sphRadiusSqr = sphRadius * sphRadius;


    }

    // Update is called once per frame
    void FixedUpdate () {
        //sphRadiusSqr = repulseStrength;

        // Doesn't make a noticable difference withouth the script.frozen for fps
        InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();

        if (Script.frozen==false)
        {
            //////////  AGAIN, NEED TO ADD THIS BACK SOMEDAY
            //sphRadius = GameObject.Find("GlobalController").GetComponent<GraphController>().sphRadius;
            //repulse = GameObject.Find("GlobalController").GetComponent<GraphController>().repulse;
            //sphRadiusSqr = sphRadius * sphRadius;
            doRepulse();
        }

    }

	void doRepulse()
	{
		// test which node in within forceSphere.
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, sphRadius);
        //print(this.name + "below is nodes within repulsion:");
        //foreach (GameObject node in nodelist)

        // only apply force to nodes within forceSphere, with Falloff towards the boundary of the Sphere and no force if outside Sphere.
        foreach (Collider hitCollider in hitColliders)
		{
			Rigidbody hitRb = hitCollider.attachedRigidbody;
            //print(hitCollider.name);
			if (hitRb != null && hitRb != thisRigidbody)
			{
				Vector3 direction = hitCollider.transform.position - this.transform.position;
				float distSqr = direction.sqrMagnitude;

				// Normalize the distance from forceSphere Center to node into 0..1
				float impulseExpoFalloffByDist = Mathf.Clamp( 1 - (distSqr / sphRadiusSqr), 0, 1);

				// apply normalized distance
				hitRb.AddForce(direction.normalized * repulse * impulseExpoFalloffByDist);
			}
		}



	}
}
