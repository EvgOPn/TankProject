using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraRotationScript : MonoBehaviour
{
	private const string MOUSE_X = "Mouse X";
	private const string MOUSE_Y = "Mouse Y";

	[SerializeField] private Transform _cameraPivotTransform = null;

	[SerializeField] private float _mouseSensitivity = 4f;
	[SerializeField] private float _minXRotateAngle = -50f;
	[SerializeField] private float _maxXRotateAngle = 50f;

	private Vector3 _localRotation;

	private void Start()
	{
		Cursor.visible = false;
	}

	private void Update()
	{
		TakeInput();
		ClampRotation();
		UpdateCameraRotation();
	}

	private void TakeInput()
	{
		_localRotation.x += Input.GetAxis(MOUSE_X) * _mouseSensitivity;
		_localRotation.y += Input.GetAxis(MOUSE_Y) * _mouseSensitivity * -1;
	}

	private void ClampRotation()
	{
		_localRotation.y = Mathf.Clamp(_localRotation.y, _minXRotateAngle, _maxXRotateAngle);
	}

	private void UpdateCameraRotation()
	{
		Quaternion targetRotation = Quaternion.Euler(_localRotation.y, _localRotation.x, 0f);
		_cameraPivotTransform.rotation = targetRotation;
	}
}
