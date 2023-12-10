using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private DebugUIController debug;
	private Rigidbody rb;
	private bool isAlive = true;
	
	private void Start()
	{
		debug = FindObjectOfType<DebugUIController>();
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (isAlive)
		{
			transform.position += Vector3.left * Time.deltaTime;
		}
	}

	public void TakeDamage(RaycastHit hit, Vector3 hitAngle)
	{
		debug.Print("I'm hit");
		isAlive = false;
		rb.freezeRotation = false;
		
		var screenToPlayerRotatedHitpoint = Quaternion.AngleAxis(-90, Vector3.up) * hit.point;
		// screenToPlayerRotatedHitpoint.x = -hit.point.z;
		// screenToPlayerRotatedHitpoint.y = hit.point.y;
		// screenToPlayerRotatedHitpoint.z = hit.point.x;

		
		// var pointJustBeforeHitPoint = screenToPlayerRotatedHitpoint - hitAngle.normalized * 0.05f;
		// rb.AddExplosionForce(10000, pointJustBeforeHitPoint, 0.2f);
		rb.AddForceAtPosition(screenToPlayerRotatedHitpoint - hitAngle.normalized * 1000, screenToPlayerRotatedHitpoint, ForceMode.Impulse);
	}
	
	
}
