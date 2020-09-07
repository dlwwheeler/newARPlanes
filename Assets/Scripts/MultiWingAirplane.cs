//
// Copyright (c) Brian Hernandez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
//

using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;


public class MultiWingAirplane : MonoBehaviour
{
	public Collider propeller;
	public Slider UIthrottle;
	private List<SimpleWing> wings = new List<SimpleWing>();
	public Joystick variableJoystick;
	public ControlSurface[] elevator;
	public ControlSurface[] aileronLeft;
	public ControlSurface[] aileronRight;
	public ControlSurface[] rudder;
	public Engine engine;

	public WeaponDropper[] weapons;

	public Rigidbody Rigidbody { get; internal set; }

	private float throttle = 1.0f;
	private bool yawDefined = false;

	private void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
		Rigidbody.maxDepenetrationVelocity = 3;
		foreach(Transform child in transform){
			if(child.gameObject.GetComponent<SimpleWing>())
			wings.Add(child.gameObject.GetComponent<SimpleWing>());
		}
	}

	private void Start()
	{
		if (elevator == null)
			Debug.LogWarning(name + ": Airplane missing elevator!");
		if (aileronLeft == null)
			Debug.LogWarning(name + ": Airplane missing left aileron!");
		if (aileronRight == null)
			Debug.LogWarning(name + ": Airplane missing right aileron!");
		if (rudder == null)
			Debug.LogWarning(name + ": Airplane missing rudder!");
		if (engine == null)
			Debug.LogWarning(name + ": Airplane missing engine!");
		
	}

	// Update is called once per frame
	void Update()
	{
		if (elevator != null)
		{
			elevator.ToList().ForEach((surface)=>{surface.targetDeflection = -Input.GetAxis("Vertical")-variableJoystick.Vertical;});
		}
		if (aileronLeft != null)
		{
			aileronLeft.ToList().ForEach((surface)=>{surface.targetDeflection = -Input.GetAxis("Horizontal")-variableJoystick.Horizontal;});
		}
		if (aileronRight != null)
		{
			aileronRight.ToList().ForEach((surface)=>{surface.targetDeflection = Input.GetAxis("Horizontal")+variableJoystick.Horizontal;});
		}
		if (rudder != null && yawDefined)
		{
			// YOU MUST DEFINE A YAW AXIS FOR THIS TO WORK CORRECTLY.
			// Imported packages do not carry over changes to the Input Manager, so
			// to restore yaw functionality, you will need to add a "Yaw" axis.
			rudder.ToList().ForEach((surface)=>{surface.targetDeflection = Input.GetAxis("Yaw");});
		}

		if (engine != null)
		{
			// Fire 1 to speed up, Fire 2 to slow down. Make sure throttle only goes 0-1.
			/*
			throttle += InputSystem.GetAxis("Fire1") * Time.deltaTime;
			throttle -= InputSystem.GetAxis("Fire2") * Time.deltaTime;
			throttle = Mathf.Clamp01(throttle);
			*/
			

			engine.throttle = UIthrottle.value;
		}

		if (weapons.Length > 0)
		{
			
		}
		//this should be an event and not in the update function, however etheir way is functionally identical as long as framerate is consistant and this is not an operation I would conisder to have affect on performance so it will stay for now
		if(Rigidbody.velocity.magnitude<0.4){
			foreach(var wing in wings){
				wing.enabled=false;
			}
		}
		else{
			foreach(var wing in wings){
				wing.enabled=true;
			}
		}
	}
	public void OnCollisionEnter(Collision collision){
		
		if(Rigidbody.velocity.magnitude >0.2){
			Rigidbody.velocity= new Vector3(0,0,0);
			collision.contacts.ToList().ForEach((Contact)=>{if(Contact.thisCollider == propeller)Destroy(gameObject);});
		}
	}
	private float CalculatePitchG()
	{
		// Angular velocity is in radians per second.
		Vector3 localVelocity = transform.InverseTransformDirection(Rigidbody.velocity);
		Vector3 localAngularVel = transform.InverseTransformDirection(Rigidbody.angularVelocity);

		// Local pitch velocity (X) is positive when pitching down.

		// Radius of turn = velocity / angular velocity
		float radius = (Mathf.Approximately(localAngularVel.x, 0.0f)) ? float.MaxValue : localVelocity.z / localAngularVel.x;

		// The radius of the turn will be negative when in a pitching down turn.

		// Force is mass * radius * angular velocity^2
		float verticalForce = (Mathf.Approximately(radius, 0.0f)) ? 0.0f : (localVelocity.z * localVelocity.z) / radius;

		// Express in G (Always relative to Earth G)
		float verticalG = verticalForce / -9.81f;

		// Add the planet's gravity in. When the up is facing directly up, then the full
		// force of gravity will be felt in the vertical.
		verticalG += transform.up.y * (Physics.gravity.y / -9.81f);

		return verticalG;
	}

	private void OnGUI()
	{
		const float msToKnots = 1.94384f;
		GUI.Label(new Rect(10, 40, 300, 20), string.Format("Speed: {0:0.0} knots", Rigidbody.velocity.magnitude * msToKnots));
		GUI.Label(new Rect(10, 60, 300, 20), string.Format("Throttle: {0:0.0}%", throttle * 100.0f));
		GUI.Label(new Rect(10, 80, 300, 20), string.Format("G Load: {0:0.0} G", CalculatePitchG()));
	}
}
