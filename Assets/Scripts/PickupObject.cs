using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
    bool forcable = false;
	GameObject carriedObject;
	Rigidbody rb;
	Collider col;
	public GameObject upright;
    float distance;
	public float smooth = 4;
    float oMass;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
	}

    void FixedUpdate()
    {
        if (carrying)
        {
            //o.transform.position = Vector3.Lerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
            Vector3 initialPoint = carriedObject.transform.position;
            Vector3 targetPoint = mainCamera.transform.position + mainCamera.transform.forward * distance;
            // if (targetPoint.y < 0)
            //  targetPoint.y = 0.3f;
            Vector3 newVector = targetPoint - initialPoint;
            float oDistance = newVector.magnitude;
            
            rb.AddForce(newVector * oDistance * 100);
            rb.drag = (rb.velocity.magnitude / oDistance);
            //rb.angularDrag = 50;

            if (oDistance > 100)
            {
                rb.isKinematic = true;
                carriedObject.transform.position = targetPoint;
                rb.isKinematic = false;
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
		if(carrying)
		{
			checkDrop();
			checkUpright();
		} 
		else
		{
			pickup();
		}
	}
	

	void pickup()
	{
		if(Input.GetKeyDown (KeyCode.E))
		{
			int x = Screen.width/2;
			int y = Screen.height/2;
			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null)
				{
					carrying = true;
					carriedObject = p.gameObject;
					rb = p.gameObject.GetComponent<Rigidbody>();
                    col = p.gameObject.GetComponent<Collider>();
                    forcable = p.forcePlace;
                
                    rb.useGravity = false;
                    if (forcable)
                        col.enabled = false;
                    else
                        col.enabled = true;
                    distance = Vector3.Distance(mainCamera.transform.position, carriedObject.transform.position);
                    rb.isKinematic = false;
                }
			}
		}
	}

	void checkDrop()
	{
		if(Input.GetKeyDown (KeyCode.E))
		{
            if (forcable)
            {
                rb.isKinematic = true;
                col.enabled = true;
                rb.drag = 0;
                

                carrying = false;
                carriedObject = null;
            }
            else
            {
                rb.isKinematic = false;
                col.enabled = true;
                rb.useGravity = true;
                rb.drag = 0;

                carrying = false;
                carriedObject = null;
            }
		}
	}


	void checkUpright()
	{
        if (Input.GetKey(KeyCode.R))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            carriedObject.transform.rotation = Quaternion.Slerp(carriedObject.transform.rotation, upright.transform.rotation, 0.3f);
        }
	}
}
