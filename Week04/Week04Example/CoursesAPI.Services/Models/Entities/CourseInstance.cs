namespace CoursesAPI.Services.Models.Entities
{
	/// <summary>
	/// A course instance is an instance of a course template, taught
	/// on a given semester.
	/// </summary>
	public class CourseInstance
	{
		/// <summary>
		/// A database-generated ID.
		/// </summary>
		public int    ID         { get; set; }

		/// <summary>
		/// A reference to the course template. Example: "T-514-VEFT".
		/// </summary>
		public string CourseID   { get; set; }

		/// <summary>
		/// A reference to the semester in which the given course instance
		/// is taught. Example: "20143" (fall 2014).
		/// </summary>
		public string SemesterID { get; set; }
	}
}
