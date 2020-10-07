using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCrosshairController : MonoBehaviour
{
	[SerializeField] private GameObject _barrelImageCrosshair = null;
	[SerializeField] private Transform _barrelShootPointTransform = null;

	private Ray _barrelRay;
	private Vector3 _rayHitPoint;

	private const float MAX_RAY_DISTANCE = 900f;

	private void Update()
	{
		FindCrosshairPoint();
		UpdateCrosshairPositionOnCanvas();
		DrawDebugRay();
	}

	private void FindCrosshairPoint()
	{
		_barrelRay = new Ray(_barrelShootPointTransform.position, _barrelShootPointTransform.rotation * Vector3.forward);
		_rayHitPoint = _barrelRay.GetPoint(MAX_RAY_DISTANCE);
	}

	private void UpdateCrosshairPositionOnCanvas()
	{
		_barrelImageCrosshair.transform.position = Camera.main.WorldToScreenPoint(_rayHitPoint);
	}

	private void DrawDebugRay()
	{
		Debug.DrawRay(_barrelRay.origin, _barrelRay.direction);
	}
}
