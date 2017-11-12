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
		
		Task<IEnumerable<Student>> GetStudentAsync(int id);
		Task<IEnumerable<Student>> GetStudentsAsync(int? teacherId);
		Task<IEnumerable<StudentGroup>> GetGroupAsync(int id);
		Task<IEnumerable<StudentGroup>> GetGroupsAsync(int? teacherId);
		Task<IEnumerable<Assignment>> GetAssignmentAsync(int id);
		Task<IEnumerable<Assignment>> GetAssignmentsAsync(int? groupId);
		Task<IEnumerable<Exercise>> GetExerciseAsync(int id);
		Task<IEnumerable<Exercise>> GetExercisesAsync(int? assignmentId);

	}
}
