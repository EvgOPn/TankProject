using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotationScript : MonoBehaviour
{
	[SerializeField] private Transform _barrelTransform = null;
	[SerializeField] private GameObject _crosshairImageGO = null;

	[SerializeField] private float _upBarrelAngle = -20f;
	[SerializeField] private float _downBarrelAngle = 15f;
	[SerializeField] private float _barrelRotateAngleFixer = 10f;

	private Quaternion _lookRotation;
	private Vector3 _direction;
	private Vector3 _crosshairScreenPoint;
	private Ray _rayToCrosshair;

	private const float MAX_RAY_DISTANCE = 1000f;

	private void Update()
	{
		CastRayToCrosshair();
		FindBarrelRotationDirection();
		HandleBarrelRotation();
	}

	private void CastRayToCrosshair()
	{
		_crosshairScreenPoint = _crosshairImageGO.transform.position;
		_rayToCrosshair = Camera.main.ScreenPointToRay(_crosshairScreenPoint);
	}

	private void FindBarrelRotationDirection()
	{
		_direction = (_rayToCrosshair.GetPoint(MAX_RAY_DISTANCE) - _barrelTransform.position).normalized;
		_lookRotation = Quaternion.LookRotation(_direction);		
	}

	private void HandleBarrelRotation()
	{
		float barrelRotAngle = _lookRotation.eulerAngles.x - _barrelRotateAngleFixer;
		_barrelTransform.localEulerAngles = new Vector3(AngleClamp.Clamp(barrelRotAngle, _upBarrelAngle, _downBarrelAngle), 0f, 0f);
	}
}
