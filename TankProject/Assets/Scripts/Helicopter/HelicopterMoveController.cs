using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HelicopterMoveController : MonoBehaviour
{
	[SerializeField] private GameObject _helicopter = null;

	[SerializeField] private float _movementSpeed = 10f;

	private Vector3 _moveDirection;
	private Rigidbody _helicopterRigidbody;

	private void Start()
	{
		_helicopterRigidbody = _helicopter.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		GetInput();
	}

	private void FixedUpdate()
	{
		HandleMovement();
	}

	private void GetInput()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			_moveDirection = Vector3.up;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			_moveDirection = Vector3.down;
		}
		else
		{
			_moveDirection = Vector3.zero;
		}
	}

	private void HandleMovement()
	{
		if (_helicopterRigidbody != null)
		{
			Vector3 targetPosition = _helicopter.transform.position + (_helicopter.transform.up * _moveDirection.y * _movementSpeed * Time.deltaTime);
			_helicopterRigidbody.MovePosition(targetPosition);
		}
	}
}
