namespace CoursesAPI.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class CourseInstanceDTO
	{
		public int    CourseInstanceID { get; set; }
		public string CourseID         { get; set; }
		public string Name             { get; set; }

		public string MainTeacher      { get; set; }
	}
}
