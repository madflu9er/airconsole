using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rigidBody;
	public float movementSpeed = 23f;
	public bool isMovingForward;
	public bool isMovingBack;
	public bool isMovingLeft;
	public bool isMovingRight;
	

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	public void ButtonInput(string input)
	{
		switch (input)
		{
			case "right":
				isMovingRight = true;
				break;
			case "left":
				isMovingLeft = true;
				break;
			case "forward":
				isMovingForward = true;
				break;
			case "back":
				isMovingBack = true;
				break;
			case "right-up":
				isMovingRight = false;
				break;
			case "left-up":
				isMovingLeft = false;
				break;
			case "forward-up":
				isMovingForward = false;
				break;
			case "back-up":
				isMovingBack = false;
				break;
		}
	}

	private void MoveLeft()
	{
		rigidBody.AddForce(transform.right * -movementSpeed);
	}
	private void MoveRight()
	{
		rigidBody.AddForce(transform.right * movementSpeed);
	}
	private void MoveForward()
	{
		rigidBody.AddForce(transform.forward * movementSpeed);
	}
	private void MoveBack()
	{
		rigidBody.AddForce(-(transform.forward) * movementSpeed);
	}

	private void FixedUpdate()
	{
		if (isMovingLeft && !isMovingRight)
		{
			MoveLeft();
		}

		if (isMovingRight && !isMovingLeft)
		{
			MoveRight();
		}

		if (isMovingForward && !isMovingBack)
		{
			MoveForward();
		}

		if (isMovingBack && !isMovingBack)
		{
			MoveBack();
		}
	}
}
