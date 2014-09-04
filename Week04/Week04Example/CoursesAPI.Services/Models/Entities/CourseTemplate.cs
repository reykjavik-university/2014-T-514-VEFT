using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Services.Models.Entities
{
	/// <summary>
	/// A course template represents a given course which is taught
	/// in the school. Please don't confuse this with the
	/// course instance entity, which represents an instance, taught
	/// in a given semester. A course template states that
	/// the school will teach (or has taught) a given course
	/// on some semester, such that we can find out what courses
	/// are available, regardless of the semester.
	/// </summary>
	public class CourseTemplate
	{
		/// <summary>
		/// A human-readable ID of the course. Example: "T-514-VEFT".
		/// </summary>
		[Key]
		public string CourseID    { get; set; }

		/// <summary>
		/// The name of the course. Example: "Vefþjónustur".
		/// </summary>
		public string Name        { get; set; }

		/// <summary>
		/// A short description of the course.
		/// </summary>
		public string Description { get; set; }
	}
}
