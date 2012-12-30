using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace snake.Graphics
{
	internal class TimeChecker
	{
		private int invokeCount;
		private int maxCount;

		public TimeChecker(int count)
		{
			invokeCount = 0;
			maxCount = count;
		}

		// This method is called by the timer delegate.
		public void CheckStatus(Object stateInfo)
		{
			AutoResetEvent autoEvent = (AutoResetEvent) stateInfo;
			++invokeCount;
			if (invokeCount == maxCount)
			{
				// Reset the counter and signal Main.
				invokeCount = 0;
				autoEvent.Set();
			}
		}
	}
}
