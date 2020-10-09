using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class ShootScript : MonoBehaviour
{
	[SerializeField] private UnityEvent OnShootStart = null;
	[SerializeField] private Transform _shellSpawnPoint = null;
	[SerializeField] private GameObject _shellPrefab = null;
	[SerializeField] private GameObject _shellSpawnEffect = null;

	[SerializeField] private float _shellForce = 1500f;
	[SerializeField] private float _reloadingTime = 3f;

	private float _moveSpeedCorrector = 10f;
	private float _timeToDestroyPrefabs = 5f;

	private GameObject _spawnedShell;

	private bool _isReloading = false;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !_isReloading)
		{
			OnShootStart?.Invoke();
			SpawnShell();
			SpawnShootEffect();
			StartCoroutine(StartReloading());
		}
	}

	private void SpawnShell()
	{
		_spawnedShell = Instantiate(_shellPrefab, _shellSpawnPoint.position, _shellSpawnPoint.rotation);
		_spawnedShell.GetComponent<Rigidbody>()?.AddForce(_shellSpawnPoint.forward * _shellForce * _moveSpeedCorrector);
		Destroy(_spawnedShell, _timeToDestroyPrefabs);
	}

	private void SpawnShootEffect()
	{
		GameObject spawnedEffect = Instantiate(_shellSpawnEffect, _shellSpawnPoint);
		Destroy(spawnedEffect, _timeToDestroyPrefabs);
	}

	private IEnumerator StartReloading()
	{
		_isReloading = true;
		yield return new WaitForSeconds(_reloadingTime);
		_isReloading = false;
	}
}
