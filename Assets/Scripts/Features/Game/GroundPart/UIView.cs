using System;
using TMPro;
using UnityEngine;

namespace Features.Game.GroundPart
{
	public class UIView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _timerText;
		[SerializeField] private TMP_Text _itemsCount;
		public void SetTimerText(int seconds)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
			_timerText.text = timeSpan.ToString(@"mm\:ss");
		}
		public void SetItemsText(string s)
		{
			_itemsCount.text = s;
		}
	}
}
