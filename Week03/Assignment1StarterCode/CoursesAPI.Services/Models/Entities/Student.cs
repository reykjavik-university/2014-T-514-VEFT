namespace CoursesAPI.Services.Models.Entities
{
	/// <summary>
	/// An entity class for students.
	/// </summary>
	public class Student
	{
		/// <summary>
		/// A database-generated ID of the student.
		/// </summary>
		public int    ID    { get; set; }

		/// <summary>
		/// The full name of the student.
		/// </summary>
		public string Name  { get; set; }

		/// <summary>
		/// The SSN (ísl.: kennitala) of the student. Should NOT include spaces, dashes etc.
		/// </summary>
		public string SSN   { get; set; }

		/// <summary>
		/// The email of the student.
		/// </summary>
		public string Email { get; set; }
	}
}
