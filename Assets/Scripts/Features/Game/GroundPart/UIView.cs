using System;
using TMPro;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class UIView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _timerText;
		public void SetTimerText(int seconds)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
			_timerText.text = timeSpan.ToString(@"mm\:ss");
		}
	}
}
