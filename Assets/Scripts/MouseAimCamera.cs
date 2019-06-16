using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour 
{
	
	public GameObject player;
	Rigidbody RB;
	public GameObject orbitPoint;
	public GameObject cameraRotation;
	public GameObject playerAnchor;
	public float rotateSpeed = 5;
	public float playerRotateSpeed = 20;
	Vector3 offset;

	float anchorAngle;
	float playerAngle;

	public Vector3 playerPos;
	public Vector3 anchorPos;
	public Vector3 targetPos;

	float desiredAngle;

	void Start()
	{
		RB = player.GetComponent<Rigidbody>();
	}

	void OnGUI(){
		//GUI.Box(new Rect((Screen.width/2)-5,(Screen.height/2)-5, 10, 10), ".");
	}

	void Update()
	{

		if(Input.GetKey(KeyCode.Escape))
		{
			//Screen.lockCursor = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			//Screen.lockCursor = true;
			Cursor.lockState = CursorLockMode.Locked;
		}

		anchorPos = playerAnchor.transform.position;
		playerPos = player.transform.position;
		anchorAngle = playerAnchor.transform.rotation.eulerAngles.y;
		playerAngle = player.transform.rotation.eulerAngles.y;
	}

	void LateUpdate()
	{

		//set location of camera anchor to the player's position
		playerAnchor.transform.position = Vector3.Lerp(anchorPos, playerPos, Time.deltaTime * 4);

		//Get x-axis mouse input and assign it to the anchor
		//Since the camera is a child of the anchor, it will rotate with it
		float horizontal = Input.GetAxisRaw("Mouse X") * rotateSpeed;
		playerAnchor.transform.Rotate(0, horizontal, 0);

		//Get y-axis mouse input and assign it to the orbitPoint
		//Since the camera is also a child of the orbitPoint, rotating this object will allow the camera to orbit around it.
		float vertical = Input.GetAxisRaw("Mouse Y") * rotateSpeed;
		orbitPoint.transform.Rotate(-vertical, 0, 0);





		//Compute where to rotate the body to match the anchor
        //Apply torque in the direction the body needs to face

		float deltaAngle = Mathf.Abs(anchorAngle - playerAngle);
		if((deltaAngle) > 2)
		{
			if(anchorAngle > playerAngle)
			{
				if(anchorAngle > (playerAngle + 180))
					RB.AddRelativeTorque(0, -deltaAngle, 0);
				else
					RB.AddRelativeTorque(0, deltaAngle, 0);
			}

			if(anchorAngle < playerAngle)
			{
				if(anchorAngle < (playerAngle - 180))
					RB.AddRelativeTorque(0, deltaAngle, 0);
				else
					RB.AddRelativeTorque(0, -deltaAngle, 0);
			}
		}
	}
}