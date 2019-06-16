using UnityEngine;
using System.Collections;

public class LegMovement : MonoBehaviour {

	public bool leftLeg;
	public bool rightLeg;
	public Rigidbody rb;
	public float sign = -1;
	public float speed;
	public float speedFactor = 5;
	public float limit = 30;
	float angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		angle = transform.rotation.eulerAngles.x;
		speed = rb.velocity.magnitude / speedFactor;

		if(leftLeg)
		{
			transform.Rotate(speed * sign, 0, 0);

			if(angle > 1 + limit)
				if(angle < 180)
					sign = -1;

			if(angle < 359 - limit)
				if(angle > 180)
					sign = 1;
		}

		if(rightLeg)
		{
			transform.Rotate(speed * -sign, 0, 0);

			if(angle > 1 + limit)
				if(angle < 180)
					sign = -1;

			if(angle < 359 - limit)
				if(angle > 180)
					sign = 1;
		}

	}
}
