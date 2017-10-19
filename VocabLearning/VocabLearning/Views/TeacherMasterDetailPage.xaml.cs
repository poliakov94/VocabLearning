using System.Collections.Generic;
using System.Collections.ObjectModel;
using VocabLearning.Models;
using VocabLearning.ViewModels;
using Xamarin.Forms;

namespace VocabLearning.Views
{
	public partial class TeacherMasterDetailPage : MasterDetailPage
	{
		public TeacherMasterDetailPageViewModel ViewModel { get { return BindingContext as TeacherMasterDetailPageViewModel; } }

	public TeacherMasterDetailPage()
		{
			InitializeComponent();			
		}
	}
}