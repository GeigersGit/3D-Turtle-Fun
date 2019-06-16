using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	
	public float speed = 0.5F;
	private float startTime;
	private float journeyLength;
	private Vector3 location;

	void Start () 
	{
		location = transform.position;
	}
		
	void Update () 
	{
		//Find where the player should go on mouseclick/tap.
		//if(Input.GetMouseButton(0))
			findLocation();

		moveToLocation();

	}

	void findLocation ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out hit, 1000))
		{
			location = new Vector3(hit.point.x, hit.point.y, hit.point.z);
		}
	}

	void moveToLocation ()
	{
		//if(Vector3.Distance(transform.position, location) > 2f)
		//{
			Quaternion newRotation = Quaternion.LookRotation(location - transform.position);

			newRotation.x = 0f;
			newRotation.z = 0f;

			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

			

			//transform.Translate(Vector3.forward * Time.deltaTime * speed);
		//}
	}		
}
