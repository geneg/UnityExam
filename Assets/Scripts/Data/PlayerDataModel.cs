using Common.Services;

namespace Data
{
	public class PlayerDataModel : DataModel
	{
		private int _currentLevel;
		private int _collectedItems;
		
		public int CurrentLevel => _currentLevel;
		public int CollectedItems => _collectedItems;
		
		public void SetNextLevel()
		{
			_currentLevel++;
		}

		public void SetItemCollected()
		{
			_collectedItems++;
		}

		public void ResetCollectedItems()
		{
			_collectedItems = 0;
		}
	}
}
