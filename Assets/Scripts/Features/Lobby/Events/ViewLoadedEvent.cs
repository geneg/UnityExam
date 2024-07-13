namespace Features.Lobby.Events
{
	public class ViewLoadedEvent
	{
		public BaseView View => _view;
		private readonly BaseView _view;
		
		public ViewLoadedEvent(BaseView lobbyView)
		{
			_view = lobbyView;
		}
	}
}
