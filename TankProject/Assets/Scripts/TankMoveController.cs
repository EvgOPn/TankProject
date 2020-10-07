using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TankMoveController : MonoBehaviour
{
	private const string HORIZONTAL = "Horizontal";
	private const string VERTICAL = "Vertical";

	private float _horizontalInput;
	private float _verticalInput;

	[SerializeField] private Transform _tankTransform = null;

	[SerializeField] private float _movementSpeed = 10f;
	[SerializeField] private float _rotationSpeed = 10f;

	private Rigidbody _tankRigidbody;

	private void Start()
	{
		_tankRigidbody = _tankTransform.gameObject.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (_tankRigidbody)
		{
			GetInput();
			HandleMovement();
			HandleRotation();
		}
	}

	private void GetInput()
	{
		_horizontalInput = Input.GetAxis(HORIZONTAL);
		_verticalInput = Input.GetAxis(VERTICAL);
	}

	private void HandleMovement()
	{
		Vector3 targetPosition = _tankTransform.position + (_tankTransform.forward * _verticalInput * _movementSpeed * Time.deltaTime);
		_tankRigidbody.MovePosition(targetPosition);
	}

	private void HandleRotation()
	{
		Quaternion targetRotation = _tankTransform.rotation * Quaternion.Euler(Vector3.up * (_rotationSpeed * _horizontalInput * Time.deltaTime));
		_tankRigidbody.MoveRotation(targetRotation);
	}
}
