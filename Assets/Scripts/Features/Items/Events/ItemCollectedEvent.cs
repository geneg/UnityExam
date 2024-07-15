namespace Features.Items.Events
{
	public class ItemCollectedEvent
	{
		public int CurrentCount => _current;
		public int Total => _total;
		
		private readonly int _current;
		private readonly int _total;
		public ItemCollectedEvent(int current, int total)
		{
			_current = current;
			_total = total;
		}

	}
}
