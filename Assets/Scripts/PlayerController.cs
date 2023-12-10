using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using MouseButton = UnityEngine.UIElements.MouseButton;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float walkSpeed = 5;
	[SerializeField] private float walkAcceleration = 5;
	
	[SerializeField] private float jumpForce = 100;
	[SerializeField] private float jumpAgainDelay = 0.1f;
	
	
	[SerializeField] private float gravity = 9.8f;

	private Rigidbody rb;

	[SerializeField] private DebugUIController debug;
	
	
	private Transform tf;
	private bool isGrounded;
	private float jumpTimer;
	private bool didLetGoOfSpace = true;
	
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.maxLinearVelocity = 300;
		tf = transform;
	}


	void Update()
	{

		jumpTimer -= Time.deltaTime;
		if (Input.GetKeyUp(KeyCode.Space)) didLetGoOfSpace = true;
		var canJump = didLetGoOfSpace && isGrounded && jumpTimer < 0;
		if (Input.GetKey(KeyCode.Space) && canJump)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			jumpTimer = jumpAgainDelay;
			didLetGoOfSpace = false;
		}
		

		var input = Vector3.zero;
		if (Input.GetKey(KeyCode.W)) input.z += 1;
		if (Input.GetKey(KeyCode.S)) input.z -= 1;
		if (Input.GetKey(KeyCode.A)) input.x -= 1;
		if (Input.GetKey(KeyCode.D)) input.x += 1;
		input.Normalize();

		var tempVelocity = rb.velocity;
		tempVelocity.y = 0;
		if (tempVelocity.magnitude < walkAcceleration)
		{
			rb.AddForce(input * walkSpeed, ForceMode.Acceleration);
		}

		if (Input.GetMouseButtonDown(0))
		{
			debug.Print("Shoot");
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				debug.Print(hit.collider.gameObject.name);
				EnemyController enemy = hit.collider.GetComponent<EnemyController>();
				if (enemy != null)
				{
					var direction = transform.position - hit.point;
					enemy.TakeDamage(hit, direction);
				}

			}
		}
		
		
	}
	
	// private void OnCollisionEnter(Collision other)
	// {
	// 	if (other.gameObject.CompareTag("Ground"))
	// 	{
	// 		isGrounded = true;
	// 	}
	// }
	//
	// private void OnCollisionExit(Collision other)
	// {
	// 	if (other.gameObject.CompareTag("Ground"))
	// 	{
	// 		isGrounded = false;
	// 	}
	// }
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			isGrounded = false;
		}
	}
}
