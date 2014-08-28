namespace CoursesAPI.Services.Models.Entities
{
	/// <summary>
	/// An entity class for a course instance.
	/// </summary>
	public class Course
	{
		/// <summary>
		/// A database-generated ID of the course.
		/// </summary>
		public int    ID          { get; set; }

		/// <summary>
		/// The human-readable ID of a course. Example: "T-514-VEFT"
		/// </summary>
		public string CourseID    { get; set; }

		/// <summary>
		/// The name of the course, in Icelandic.
		/// </summary>
		public string Name        { get; set; }

		/// <summary>
		/// A description of the course.
		/// </summary>
		public string Description { get; set; }
	}
}
