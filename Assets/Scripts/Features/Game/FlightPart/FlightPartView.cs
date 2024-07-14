using System.Collections.Generic;
using Features.Game.FlightPart.Character;
using Features.Game.FlightPart.Helicopter;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Game.FlightPart
{
	[RequireComponent(typeof(FligtPartInputController))]
	public class FlightPartView : GamePartView
	{
		public Transform MapCenterPosition => _mapCenterPosition;
		public HelicopterView Helicopter => _helicopter;
		public CharacterView Character => _character;
		
		public int FlightDuration => _flightDuration;
		public FligtPartInputController InputController => _inputController;

		public Collider GroundCollider => _groundCollider;
		[SerializeField] private HelicopterView _helicopter;
		[SerializeField] private CharacterView _character;
		[SerializeField] private List<Transform> _startPositions;
		[SerializeField] private Transform _mapCenterPosition;
		[SerializeField] private int _flightDuration = 30; //in seconds
		[SerializeField] private FligtPartInputController _inputController;
		[SerializeField] private Collider _groundCollider;
		
		public Vector3 GetSpawnPosition()
		{
			int randomPos = Random.Range(0, _startPositions.Count);
			return _startPositions[randomPos].position;
		}
		
	}
}
