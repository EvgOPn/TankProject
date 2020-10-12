using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TankTrackMoveScript : MonoBehaviour
{
	private const string MAIN_TEXTURE = "_MainTex";

	[SerializeField] private Renderer _rightTrackMeshRenderer = null;
	[SerializeField] private Renderer _leftTrackMeshRenderer = null;

	[SerializeField] private float _trackOffsetSpeed = 5f;

	private Material _rightTrackMaterial;
	private Material _leftTrackMaterial;

	private float _trackOffsetDirection = -1;
	private float _startTime;
	private float _offsetDuration = 20f;

	private void Start()
	{
		_rightTrackMaterial = _rightTrackMeshRenderer.material;
		_leftTrackMaterial = _leftTrackMeshRenderer.material;

		_startTime = Time.time;
	}

	private void Update()
	{
		SetTrackTexturesOffset();
	}

	private void SetTrackTexturesOffset()
	{
		float smoothTime = (Time.time - _startTime) / _offsetDuration;
		float offset = Time.time * _trackOffsetSpeed * _trackOffsetDirection;
		offset = Mathf.SmoothStep(_rightTrackMaterial.GetTextureOffset(MAIN_TEXTURE).x, offset, smoothTime);

		_rightTrackMaterial.SetTextureOffset(MAIN_TEXTURE, new Vector2(offset, 0f));
		_leftTrackMaterial.SetTextureOffset(MAIN_TEXTURE, new Vector2(offset, 0f));
	}

	public void TrackMoveForwardOffset()
	{
		_trackOffsetDirection = -1;
	}

	public void TrackMoveBackOffset()
	{
		_trackOffsetDirection = 1;
	}

	public void TrackStopOffset()
	{
		_trackOffsetDirection = 0;
	}
}
