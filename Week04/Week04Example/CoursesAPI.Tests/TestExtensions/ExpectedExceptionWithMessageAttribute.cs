using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoursesAPI.Tests.TestExtensions
{
	/// <summary>
	/// This class comes from this StackOverflow thread:
	/// http://stackoverflow.com/questions/4710129/how-do-i-enforce-exception-message-with-expectedexception-attribute
	/// </summary>
	public class ExpectedExceptionWithMessageAttribute : ExpectedExceptionBaseAttribute
	{
		public Type ExceptionType { get; set; }

		public string ExpectedMessage { get; set; }

		public ExpectedExceptionWithMessageAttribute(Type exceptionType)
		{
			this.ExceptionType = exceptionType;
		}

		public ExpectedExceptionWithMessageAttribute(Type exceptionType, string expectedMessage)
		{
			this.ExceptionType = exceptionType;

			this.ExpectedMessage = expectedMessage;
		}

		protected override void Verify(Exception e)
		{
			if (e.GetType() != this.ExceptionType)
			{
				Assert.Fail(String.Format(
								"ExpectedExceptionWithMessageAttribute failed. Expected exception type: {0}. Actual exception type: {1}. Exception message: {2}",
								this.ExceptionType.FullName,
								e.GetType().FullName,
								e.Message
								)
							);
			}

			var actualMessage = e.Message.Trim();

			if (this.ExpectedMessage != null)
			{
				Assert.AreEqual(this.ExpectedMessage, actualMessage);
			}

			Console.Write("ExpectedExceptionWithMessageAttribute:" + e.Message);
		}
	}
}
