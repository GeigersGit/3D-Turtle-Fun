using UnityEngine;
using System.Collections;

public class PlayerMovementWASD : MonoBehaviour 
{
    //Public Global Variables
    public float force = 100;
    public Transform point;
    
	public CapsuleCollider col;
	public PhysicMaterial lowFriction;
	public GameObject model;
	public GameObject blowTorch;
	public GameObject afterBurner;

    //Private Global Variables
	private float maxVelocity = 9f;
    Vector3 playerPos;
    Vector3 newPos;
	Rigidbody rb;
	bool jumping = false;
	bool grounded = false;
	bool sliding = false;
	bool atSpeed = false;
	
	// Called at the start of a scene
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}


	void OnGUI()
	{
		string str = "" + (int)rb.velocity.magnitude;
		Vector2 pos = new Vector2(0,50);
		Vector2 size = new Vector2(50, 50);
		Rect textPos = new Rect (pos, size);
		GUI.TextArea (textPos, str);
	}

	// Called once per frame
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			model.transform.Rotate(-90, 0, 0);
			col.material = lowFriction;
			sliding = true;
		}

		if (Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			model.transform.Rotate ( 90, 0, 0);
			col.material = null;
			sliding = false;
		}

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			jumping = true;
			grounded = false;
			blowTorch.transform.Rotate (90, 0, 0);
			afterBurner.SetActive (true);
		}
		if (Input.GetKeyUp (KeyCode.Space)) 
		{

			jumping = false;
			afterBurner.SetActive (false);
			blowTorch.transform.Rotate (-90, 0, 0);
		}
	}

    //Called at fixed 0.2s intervals
	void FixedUpdate () 
	{

		if (rb.velocity.magnitude > 30)
			atSpeed = true;
		else
			atSpeed = false;

		if(grounded && !sliding)
			rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocity); 

		//JETPACKING



		if(Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce (0, 320, 0);
		}


        
		//MOVEMENT
		////////////////////////////////////////////////////////////
		if (Input.GetKey (KeyCode.W)) 
		{
			//Walking
			if (!sliding && grounded) 
				rb.AddRelativeForce (0, 0, 800);
			else
				
			//Airfalling
			if (!grounded && !jumping && !atSpeed)
				rb.AddRelativeForce (0, 0, 50);
			else
					
			//Airfalling at Speed
				//do nothing
			
			//Jetpacking at Speed
				//do nothing

			//Jetpacking below Speed
			if (jumping && !atSpeed)
				rb.AddRelativeForce (0, 0, 130);
						
			//Sliding on ground at Speed
				//do nothing
				

			//Sliding on ground below Speed
				//do nothing
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			//Walking
			if (!sliding && grounded) 
				rb.AddRelativeForce (0, 0, -800);
			else

			//Airfalling
			if (!grounded && !jumping && !atSpeed)
				rb.AddRelativeForce (0, 0, -50);
			else

			//Airfalling at Speed
			if (!grounded && !jumping && atSpeed)
				rb.AddRelativeForce (0, 0, -50);
			else

			//Jetpacking at Speed
			if (jumping && atSpeed)
				rb.AddRelativeForce (0, 0, -110);
			else

			//Jetpacking below Speed
			if (jumping && !atSpeed)
				rb.AddRelativeForce (0, 0, -110);
			else
			//Sliding on ground at Speed
			if (grounded && sliding && atSpeed)
				rb.AddRelativeForce (0, 0, -50);

			//Sliding on ground below Speed
			//do nothing
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			//Walking
			if (!sliding && grounded)
				rb.AddRelativeForce (-800, 0, 0);
			else

			//Airfalling
			if (!grounded && !jumping && !atSpeed)
				rb.AddRelativeForce (-50, 0, 0);
			else

			//Airfalling at Speed
			if (!grounded && !jumping && atSpeed)
				rb.AddRelativeForce (-50, 0, 0);
			else
				
			//Jetpacking at Speed
			if (jumping && atSpeed)
				rb.AddRelativeForce (-110, 0, 0);
			else

			//Jetpacking below Speed
			if (jumping && !atSpeed)
				rb.AddRelativeForce (-110, 0, 0);
			else
			//Sliding on ground at Speed
			if (grounded && sliding && atSpeed)
				rb.AddRelativeForce (-110, 0, 0);

			//Sliding on ground below Speed
			//do nothing
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			//Walking
			if (!sliding && grounded)
				rb.AddRelativeForce (800, 0, 0);
			else

			//Airfalling
			if (!grounded && !jumping && !atSpeed)
				rb.AddRelativeForce (50, 0, 0);
			else

			//Airfalling at Speed
			if (!grounded && !jumping && atSpeed)
				rb.AddRelativeForce (90, 0, 0);
			else

			//Jetpacking at Speed
			if (jumping && atSpeed)
				rb.AddRelativeForce (110, 0, 0);
			else

			//Jetpacking below Speed
			if (jumping && !atSpeed)
				rb.AddRelativeForce (110, 0, 0);
			else
								
			//Sliding on ground at Speed
			if (grounded && sliding && atSpeed)
				rb.AddRelativeForce (110, 0, 0);

			//Sliding on ground below Speed
			//do nothing
		}
		////////////////////////////////////////////////////////////
	}

    //Called when a collider collides with this object's collider
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Ground")
            	grounded = true;
    }

	void OnCollisionExit(Collision col)
	{
		if (col.collider.tag == "Ground")
			grounded = false;
	}
}
