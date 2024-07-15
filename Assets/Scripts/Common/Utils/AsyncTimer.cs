using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Utils
{
	public class AsyncTimer
	{
		public event Action OnTimerEnded;
		public event Action<int> OnSecondElapsed;
		
		private CancellationTokenSource _cancellationTokenSource;
		
		public async void StartCountdownTimer(int seconds)
		{
			// Cancel any existing timer
			if (_cancellationTokenSource != null)
			{
				_cancellationTokenSource.Cancel();
			}

			_cancellationTokenSource = new CancellationTokenSource();
			CancellationToken token = _cancellationTokenSource.Token;
			
			int remainingTime = seconds;

			try
			{
				while (remainingTime > 0)
				{
					if (token.IsCancellationRequested)
					{
					
						return;
					}
				
					OnSecondElapsed?.Invoke(remainingTime);
					await Task.Delay(1000, token);
					remainingTime--;
				}

				OnTimerEnded?.Invoke();
			}
			catch (TaskCanceledException e)
			{
				//timer canceled
			}
			
		}

		public void StopTimer()
		{
			if (_cancellationTokenSource != null)
			{
				_cancellationTokenSource.Cancel();
			}
		}
	}
}
