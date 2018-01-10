using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace VocabLearning.Services.Mocks
{
	public class MockPageDialogService : IPageDialogService
	{
		public Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
		{
			throw new NotImplementedException();
		}

		public Task DisplayActionSheetAsync(string title, params IActionSheetButton[] buttons)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton)
		{
			return true;
		}

		public Task DisplayAlertAsync(string title, string message, string cancelButton)
		{
			return null;
		}
	}
}
