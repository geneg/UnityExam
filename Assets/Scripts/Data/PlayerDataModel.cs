using Common.Services;

namespace Data
{
	public class PlayerDataModel : DataModel
	{
		private int _currentLevel;
		public int CurrentLevel => _currentLevel;

		public void SetNextLevel()
		{
			_currentLevel++;
		}
	}
}
