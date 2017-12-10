using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VocabLearning.Helpers;
using VocabLearning.Models;

namespace VocabLearning.ViewModels
{
	public class StudentProgressPageViewModel : BaseViewModel
	{
		private ObservableCollection<Progress> results;
		public ObservableCollection<Progress> Results
		{
			get { return results; }
			set { SetProperty(ref results, value); }
		}

		public StudentProgressPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{

		}

		public override async void OnNavigatedTo(NavigationParameters parameters)
		{
			var resultsTable = await _azureService.GetTableAsync<StudentExercise>();
			var results = (await resultsTable.ReadAllItemsAsync())
				.Where(r => r.Student_Id == _azureService.User.Id)
				.ToList();

			if (results == null)
				return;

			var exercisesTable = await _azureService.GetTableAsync<Exercise>();
			var exercises = (await exercisesTable.ReadAllItemsAsync()).ToList();

			var assignmentsTable = await _azureService.GetTableAsync<Assignment>();
			var assignments = (await assignmentsTable.ReadAllItemsAsync())
				.Where(a => a.StudentGroup_Id == _azureService.User.StudentGroup_Id);


			var progress = new List<Progress>();

			foreach (var assignment in assignments)
			{
				assignment.Exercises = exercises.Where(e => e.Assignment_Id == assignment.Id).ToList();
				var query = from r in results
						   join e in assignment.Exercises on r.Exercise_Id equals e.Id
						   select new { StudentExercise = r };

				var grouped = query.GroupBy(r => r.StudentExercise.Attempt);

				var passed = new List<int>();

				foreach (var group in grouped)
				{
					passed.Add(group.Where(r => r.StudentExercise.Passed).Count());
				}					

				progress.Add(new Progress
				{
					Assignment = assignment,
					Attempts = query.Select(r => r.StudentExercise.Attempt).Max(),
					Best = passed.Max(),
					Worst = passed.Min(),
					Average = passed.Average()
				});
			}

			Results = new ObservableCollection<Progress>(progress);
		}
	}

	public class Progress
	{
		public Assignment Assignment { get; set; }
		public double Average { get; set; }
		public int Best { get; set; }
		public int Worst { get; set; }
		public int Attempts { get; set; }
	}
}
