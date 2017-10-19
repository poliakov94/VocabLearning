using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VocabLearning.ViewModels
{
	public class SpeakPageViewModel : BindableBase
	{
		private string _textToSay = "Hello Prism";
		public string PropertyName
		{
			get { return _textToSay = "Hello Prism"; }
			set { SetProperty(ref _textToSay, value); }
		}

		public DelegateCommand SpeakCommand { get; set; }
		public SpeakPageViewModel()
		{
			SpeakCommand = new DelegateCommand(Speak);
		}

		public void Speak()
		{

		}
	}
}
