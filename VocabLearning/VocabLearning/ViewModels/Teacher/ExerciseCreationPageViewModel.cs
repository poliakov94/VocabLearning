﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using VocabLearning.Helpers;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class ExerciseCreationPageViewModel : BaseViewModel
	{
		IPageDialogService _pageDialogService;

		private Assignment Assignment { get; set; }
		private Exercise exercise;
		public Exercise Exercise
		{
			get { return exercise; }
			set { SetProperty(ref exercise, value); }
		}

		public ExerciseCreationPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
			: base(navigationService)
		{
			_pageDialogService = pageDialogService;
		}

		private DelegateCommand _saveExerciseCommand;
		public DelegateCommand SaveExerciseCommand =>
			_saveExerciseCommand ?? (_saveExerciseCommand = new DelegateCommand(ExecuteSaveExerciseCommand));

		async void ExecuteSaveExerciseCommand()
		{
			if (String.IsNullOrEmpty(Exercise.Word) 
				|| String.IsNullOrEmpty(Exercise.Definition) 
				|| String.IsNullOrEmpty(Exercise.TranslatedWord) 
				|| String.IsNullOrEmpty(Exercise.TranslatedPhrase) 
				|| String.IsNullOrEmpty(Exercise.Phrase))
			{
				await _pageDialogService.DisplayAlertAsync("Error", "Some fields are empty.", "Ok");
				return;
			}

			var exercisesTable = await _azureService.GetTableAsync<Exercise>();
			Exercise = await exercisesTable.UpsertItemAsync(Exercise);
			await _azureService.SyncOfflineCacheAsync();

			var navigationParams = new NavigationParameters
			{
				{ "model", Assignment }
			};
			await _navigationService.GoBackAsync(navigationParams);
		}

		private DelegateCommand _findDefiniction;
		public DelegateCommand FindDefinitionCommand =>
			_findDefiniction ?? (_findDefiniction = new DelegateCommand(ExecuteFindDefinitionCommand));

		async void ExecuteFindDefinitionCommand()
		{
			if (String.IsNullOrEmpty(Exercise.Word))
			{
				await _pageDialogService.DisplayAlertAsync("Error", "Please provide a word first.", "Ok");
				return;
			}

			var lookup = await DictLookup.Get(Exercise.Word);
			var sense = lookup.Results.FirstOrDefault().Senses.FirstOrDefault();
			if (sense.Examples != null)
				Exercise.Phrase = sense.Examples.FirstOrDefault().Text;
			Exercise.Definition = sense.Definition.FirstOrDefault();

			RaisePropertyChanged("Exercise");
		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Assignment = parameters["model"] as Assignment;				

				if (Assignment == null)
				{
					Exercise = parameters["model"] as Exercise;
					Assignment = Exercise.Assignment;
				}
				else
				{
					Exercise = new Exercise
					{
						Assignment_Id = Assignment.Id
					};
				}					
			}
		}
	}
}
