using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	[SerializeField] float wheelSensitivity = 10f;
	[SerializeField] float moveSpeed = 500f;

	private float scrollDeltaY;
	private Vector2 moveInput;

	private void OnScrollY(InputValue value)
	{
		scrollDeltaY = value.Get<float>();
	}

	private void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
	}

	private void Update()
	{
		transform.Translate(new Vector3(0f, 0f, scrollDeltaY * Time.deltaTime * wheelSensitivity));

		Vector3 vec = new Vector3(moveInput.x, 0f, moveInput.y);
		vec *= Time.deltaTime * moveSpeed;
		transform.Translate(vec, Space.World);
	}
}