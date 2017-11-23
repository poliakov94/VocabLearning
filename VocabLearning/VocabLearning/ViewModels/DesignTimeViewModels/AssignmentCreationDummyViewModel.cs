using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VocabLearning.Models;

namespace VocabLearning.ViewModels.DesignTimeViewModels
{
	public class AssignmentCreationDummyViewModel : BaseViewModel
	{

		private Assignment _assignment;
		public Assignment Assignment
		{
			get { return _assignment; }
			set
			{
				_assignment = value;
				RaisePropertyChanged("Assignment");
			}
		}
		public TimeSpan ValidFromTime { get; set; }
		public TimeSpan ValidUntilTime { get; set; }

		public ObservableCollection<StudentGroup> _groups = new ObservableCollection<StudentGroup>();
		public ObservableCollection<StudentGroup> Groups { get { return _groups; } set { _groups = value; RaisePropertyChanged("Groups"); } }

		private StudentGroup _selectedGroup;
		public StudentGroup SelectedGroup
		{
			get { return _selectedGroup; }
			set
			{
				if (_selectedGroup != value)
					_selectedGroup = value;				
			}
		}
		
		public AssignmentCreationDummyViewModel()
		{
			Assignment = new Assignment()
			{
				ValidFrom = System.DateTime.Now,
				ValidUntil = System.DateTime.Now,
				Name = "Enter a name"
			};

			ValidFromTime = Assignment.ValidFrom.TimeOfDay;
			ValidUntilTime = Assignment.ValidUntil.TimeOfDay;
			RaisePropertyChanged("ValidFromTime");
			RaisePropertyChanged("ValidUntilTime");

			var groups = new List<StudentGroup>()
			{
				new StudentGroup()
				{
					Name = "First group"
				},
				new StudentGroup()
				{
					Name = "Second group"
				},
				new StudentGroup()
				{
					Name = "Third group"
				},
				new StudentGroup()
				{
					Name = "Fourth group"
				}
			};

			Groups = new ObservableCollection<StudentGroup>(groups);
		}

		private DelegateCommand _createAssignment;
		public DelegateCommand CreateAssignmentCommand =>
			_createAssignment ?? (_createAssignment = new DelegateCommand(ExecuteCreateAssignmentCommand));

		void ExecuteCreateAssignmentCommand()
		{
			Assignment.ValidFrom = Assignment.ValidFrom.Date + ValidFromTime;
			Assignment.ValidUntil = Assignment.ValidUntil.Date + ValidUntilTime;
			Assignment.StudentGroup = SelectedGroup;
			Assignment.StudentGroup_Id = SelectedGroup.Id;
		}
	}
}
