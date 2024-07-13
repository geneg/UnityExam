using System;
using Common;
using Features.Lobby.Events;
using UnityEngine;

public class BaseView : MonoBehaviour
{
	private void Awake()
	{
		EventBroadcaster.Broadcast(new ViewLoadedEvent(this));
	}
}