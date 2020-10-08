using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BarrelCrosshairController : MonoBehaviour
{
	[SerializeField] private GameObject _barrelImageCrosshair = null;
	[SerializeField] private Transform _barrelShootPointTransform = null;

	[SerializeField] private float _radiusOfRayFromBarrel = 0.2f;

	private RaycastHit _rayHitInfo;
	private Ray _barrelRay;
	private Vector3 _rayHitPoint;

	private const float MAX_RAY_DISTANCE = 150f;

	private void Update()
	{
		FindCrosshairPoint();
		UpdateCrosshairPositionOnCanvas();
		DrawDebugRay();
	}

	private void FindCrosshairPoint()
	{
		_barrelRay = new Ray(_barrelShootPointTransform.position, _barrelShootPointTransform.forward);

		if (Physics.SphereCast(_barrelRay, _radiusOfRayFromBarrel, out _rayHitInfo))
		{
			_rayHitPoint = _rayHitInfo.point;
		}
		else
		{
			_rayHitPoint = _barrelRay.GetPoint(MAX_RAY_DISTANCE);
		}
	}

	private void UpdateCrosshairPositionOnCanvas()
	{
		_barrelImageCrosshair.transform.position = Camera.main.WorldToScreenPoint(_rayHitPoint);
	}

	private void DrawDebugRay()
	{
		Debug.DrawRay(_barrelRay.origin, _barrelRay.direction * 200, Color.red);
	}
}
