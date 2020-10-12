using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TurretRotationScript : MonoBehaviour
{
	[SerializeField] private Transform _tankPosition = null;
	[SerializeField] private Transform _cameraPivotTransform = null;
	[SerializeField] private Transform _turretPivotTransform = null;

	[SerializeField] private float _rotationDampening = 50f;

	private Quaternion _targetRotation;

	private bool _isRotating = true;

	private void Update()
	{
		CheckRotatingState();
		UpdateTurretPosition();

		if (_isRotating)
		{
			FindRotationAngle();
			RotateTurret();
		}
	}

	private void CheckRotatingState()
	{
		if (Input.GetMouseButton(1))
		{
			_isRotating = false;
		}
		else if (Input.GetMouseButtonUp(1))
		{
			_isRotating = true;
		}
		else
		{
			_isRotating = true;
		}
	}

	private void UpdateTurretPosition()
	{
		transform.position = _tankPosition.position;
	}

	private void FindRotationAngle()
	{
		_targetRotation = _cameraPivotTransform.rotation;
		_targetRotation.x = 0f;
		_targetRotation.z = 0f;
	}

	private void RotateTurret()
	{
		_turretPivotTransform.rotation = Quaternion.RotateTowards(_turretPivotTransform.rotation, _targetRotation, Time.deltaTime * _rotationDampening);
	}
}
