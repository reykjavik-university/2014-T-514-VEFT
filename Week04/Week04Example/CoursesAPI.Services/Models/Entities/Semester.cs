using System;

namespace CoursesAPI.Services.Models.Entities
{
	/// <summary>
	/// A semester represents a period in which courses are taught.
	/// Each semester has an ID, which is the year when the semester
	/// is taught, followed by a "period ID", one of the following:
	/// 1 - spring semester
	/// 2 - summer semester
	/// 3 - fall semester
	/// </summary>
	public class Semester
	{
		/// <summary>
		/// The ID of the semester. Example: "20143".
		/// </summary>
		public string   ID         { get; set; }

		/// <summary>
		/// The name of the semester. Example: "Haustönn 2014".
		/// </summary>
		public string   Name       { get; set; }

		/// <summary>
		/// The exact date when the semester starts.
		/// </summary>
		public DateTime DateBegins { get; set; }

		/// <summary>
		/// The exact end date of the semester.
		/// </summary>
		public DateTime DateEnds   { get; set; }
	}
}
