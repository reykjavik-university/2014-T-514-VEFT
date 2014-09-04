using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesAPI.Services.Models.Entities
{
	/// <summary>
	/// Each person in the school, be it a student or a teacher,
	/// has a single Person object associated with him/her.
	/// </summary>
	[Table("Persons")]
	public class Person
	{
		/// <summary>
		/// A database-generated ID of the person.
		/// </summary>
		public int    ID    { get; set; }

		/// <summary>
		/// The SSN of the person.
		/// </summary>
		public string SSN   { get; set; }

		/// <summary>
		/// The full name of the person.
		/// </summary>
		public string Name  { get; set; }

		/// <summary>
		/// The email of the person.
		/// </summary>
		public string Email { get; set; }
	}
}
