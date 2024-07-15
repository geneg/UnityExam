using System;

namespace Features.Items
{
	public interface ICollectableItem
	{
		event Action<ICollectableItem> OnCollected;
		void Dispose();
	}
}
