using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraOrbitScript : MonoBehaviour
{
	private const string MOUSE_X = "Mouse X";
	private const string MOUSE_Y = "Mouse Y";
	private const string MOUSE_SCROLL_WHEEL = "Mouse ScrollWheel";

	private Vector3 _localRotation;

	[SerializeField] private Transform _cameraTransform = null;
	[SerializeField] private Transform _cameraPivot = null;
	[SerializeField] private float _cameraDistance = 10f;
	[SerializeField] private float _mouseSensitivity = 4f;
	[SerializeField] private float _scrollSensitvity = 2f;
	[SerializeField] private float _orbitDampening = 10f;
	[SerializeField] private float _scrollDampening = 6f;


	private void Start()
	{
		Cursor.visible = false;
	}

	private void Update()
	{
		GetInput();
		HandleZooming();
		UpdateCameraPosition();
	}

	private void GetInput()
	{
		_localRotation.x += Input.GetAxis(MOUSE_X) * _mouseSensitivity;
		_localRotation.y += Input.GetAxis(MOUSE_Y) * _mouseSensitivity * -1;

		ClampYCameraRotation();
	}

	private void ClampYCameraRotation()
	{
		_localRotation.y = Mathf.Clamp(_localRotation.y, -10f, 90f);
	}

	private void HandleZooming()
	{
		float ScrollAmount = Input.GetAxis(MOUSE_SCROLL_WHEEL) * _scrollSensitvity;
		ScrollAmount *= (_cameraDistance * 0.3f);
		_cameraDistance += ScrollAmount * -1f;

		ClampZooming();
	}

	private void ClampZooming()
	{
		_cameraDistance = Mathf.Clamp(_cameraDistance, 5f, 15f);
	}

	private void UpdateCameraPosition()
	{
		Quaternion targetRotation = Quaternion.Euler(_localRotation.y, _localRotation.x, 0);
		_cameraPivot.rotation = Quaternion.Lerp(_cameraPivot.rotation, targetRotation, Time.deltaTime * _orbitDampening);

		if (_cameraTransform.localPosition.z != _cameraDistance * -1f)
		{
			_cameraTransform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(_cameraTransform.localPosition.z, _cameraDistance * -1f, Time.deltaTime * _scrollDampening));
		}
	}
}
