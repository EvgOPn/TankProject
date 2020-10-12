using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class TankMoveController : MonoBehaviour
{
	[SerializeField] private UnityEvent OnTankForwardMovement = null;
	[SerializeField] private UnityEvent OnTankBackMovement = null;
	[SerializeField] private UnityEvent OnTankStoped = null;

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
			CallInputBasedMethods();
			HandleMovement();
			HandleRotation();
		}
	}

	private void GetInput()
	{
		_horizontalInput = Input.GetAxis(HORIZONTAL);
		_verticalInput = Input.GetAxis(VERTICAL);
	}

	private void CallInputBasedMethods()
	{
		if (_verticalInput > 0)
		{
			OnTankForwardMovement?.Invoke();
		}
		else if (_verticalInput < 0)
		{
			OnTankBackMovement?.Invoke();
		}
		else if (_verticalInput == 0)
		{
			OnTankStoped?.Invoke();
		}
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
