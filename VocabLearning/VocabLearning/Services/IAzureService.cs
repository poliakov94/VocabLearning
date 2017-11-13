using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearning.Models;

namespace VocabLearning.Services
{
	interface IAzureService
	{
		Task SeedLocalDataAsync();

		bool LocalDBExists { get; }
		bool IsSeeded { get; }

		Task SynchronizeStudentsAsync();
		Task SynchronizeGroupsAsync();
		Task SynchronizeAssignmentsAsync();
		Task SynchronizeExercisesAsync();

		Task SaveStudentAsync(Student item);
		Task SaveGroupAsync(StudentGroup item);
		Task DeleteGroupAsync(StudentGroup item);
		Task SaveAssignmentAsync(Assignment item);
		Task DeleteAssignmentAsync(Assignment item);
		Task SaveExerciseAsync(Exercise item);
		Task DeleteExerciseAsync(Exercise item);
		
		Task<IEnumerable<Student>> GetStudentAsync(string id);
		Task<IEnumerable<Student>> GetStudentsAsync(string teacherId);
		Task<IEnumerable<StudentGroup>> GetGroupAsync(string id);
		Task<IEnumerable<StudentGroup>> GetGroupsAsync(string teacherId);
		Task<IEnumerable<Assignment>> GetAssignmentAsync(string id);
		Task<IEnumerable<Assignment>> GetAssignmentsAsync(string groupId);
		Task<IEnumerable<Exercise>> GetExerciseAsync(string id);
		Task<IEnumerable<Exercise>> GetExercisesAsync(string assignmentId);

	}
}
