using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class ReloadingDisplayScript : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _reloadTimeText = null;

	private float _timeLeft;

	private bool _isReloading = false;

	private void Update()
	{
		if (_isReloading)
		{
			StartTimerCounter();
		}
	}

	public void ActivateTimer(float reloadingTime)
	{
		_timeLeft = reloadingTime;
		_isReloading = true;
	}

	private void StartTimerCounter()
	{
		_timeLeft -= Time.deltaTime;

		_reloadTimeText.text = _timeLeft.ToString().Substring(0, 3);

		if (_timeLeft < 0)
		{
			ResetTimer();
		}
	}

	private void ResetTimer()
	{
		_reloadTimeText.text = "0,0";
		_isReloading = false;
	}
}
