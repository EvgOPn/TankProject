using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HelicopterRotationController : MonoBehaviour
{
	[SerializeField] private GameObject _helicopter = null;

	[SerializeField] private float _yawRotationSpeed = 50f;
	[SerializeField] private float _pitchRotationSpeed = 10f;
	[SerializeField] private float _rollRotationSpeed = 10f;

	private const string HORIZONTAL = "Horizontal";
	private const string VERTICAL = "Vertical";

	private float _horizontalInput;
	private float _verticalInput;

	private Vector3 _yawRotateDirection;
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
		HandleYawRotation();
		HandlePitchRotation();
		HandleRollRotation();
	}

	private void GetInput()
	{
		_horizontalInput = Input.GetAxis(HORIZONTAL);
		_verticalInput = Input.GetAxis(VERTICAL);

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			_yawRotateDirection = Vector3.down;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			_yawRotateDirection = Vector3.up;
		}
		else
		{
			_yawRotateDirection = Vector3.zero;
		}
	}

	private void HandleYawRotation()
	{
		if (_helicopterRigidbody != null)
		{
			Quaternion rotationDifference = Quaternion.Euler(0f, _yawRotateDirection.y * _yawRotationSpeed * Time.deltaTime, 0f);
			Quaternion targetRotation = _helicopter.transform.rotation;
			targetRotation *= rotationDifference;
			_helicopter.transform.rotation = Quaternion.Lerp(_helicopter.transform.rotation, targetRotation, Time.deltaTime * 20f);
		}
	}

	private void HandlePitchRotation()
	{
		Quaternion targetRotation = Quaternion.Euler(_verticalInput * _pitchRotationSpeed * Time.deltaTime, 0f, 0f);
		_helicopter.transform.rotation *= targetRotation;
		_helicopter.transform.rotation = Quaternion.Euler(AngleClamp.Clamp(_helicopter.transform.localEulerAngles.x, -20f, 20f), _helicopter.transform.localEulerAngles.y, _helicopter.transform.localEulerAngles.z);
	}

	private void HandleRollRotation()
	{
		Quaternion targetRotation = Quaternion.Euler(0f, 0f, _horizontalInput * _rollRotationSpeed * Time.deltaTime * -1f);
		_helicopter.transform.rotation *= targetRotation;
		_helicopter.transform.rotation = Quaternion.Euler(_helicopter.transform.localEulerAngles.x, _helicopter.transform.localEulerAngles.y, AngleClamp.Clamp(_helicopter.transform.localEulerAngles.z, -25f, 25f));
	}
}
