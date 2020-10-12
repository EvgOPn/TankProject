using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BarrelRotationScript : MonoBehaviour
{
	[SerializeField] private Transform _barrelTransform = null;
	[SerializeField] private GameObject _crosshairImageGO = null;

	[SerializeField] private float _upBarrelAngle = -15f;
	[SerializeField] private float _downBarrelAngle = 15f;
	[SerializeField] private float _barrelRotateAngleFixer = 10f;
	[SerializeField] private float _barrelDampingTime = 4f;

	private Quaternion _lookRotation;
	private Vector3 _direction;
	private Vector3 _crosshairScreenPoint;
	private Ray _rayToCrosshair;
	private RaycastHit _rayHitInfo;

	private const float MAX_RAY_DISTANCE = 250f;

	private bool _isRotating = true;

	private void Update()
	{
		CheckRotatingState();

		if (_isRotating)
		{
			CastRayToCrosshair();
			FindBarrelRotationDirection();
			HandleBarrelRotation();
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

	private void CastRayToCrosshair()
	{
		_crosshairScreenPoint = _crosshairImageGO.transform.position;
		_rayToCrosshair = Camera.main.ScreenPointToRay(_crosshairScreenPoint);
	}

	private void FindBarrelRotationDirection()
	{
		if (Physics.Raycast(_rayToCrosshair, out _rayHitInfo))
		{
			_direction = (_rayHitInfo.point - _barrelTransform.position).normalized;
		}
		else
		{
			_direction = (_rayToCrosshair.GetPoint(MAX_RAY_DISTANCE) - _barrelTransform.position).normalized;
		}

		_lookRotation = Quaternion.LookRotation(_direction);
	}

	private void HandleBarrelRotation()
	{
		float barrelRotAngle = _lookRotation.eulerAngles.x - _barrelRotateAngleFixer;
		float newAngle = Mathf.LerpAngle(_barrelTransform.localEulerAngles.x, AngleClamp.Clamp(barrelRotAngle, _upBarrelAngle, _downBarrelAngle), _barrelDampingTime * Time.deltaTime);
		_barrelTransform.localEulerAngles = new Vector3(newAngle, 0f, 0f);
	}
}
