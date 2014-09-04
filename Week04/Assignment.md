# Week 4 assignment

Note: you don't have to hand in anything in this assignment.

## LINQ join

Using the [example provided in week 4](Week04Example), write the following queries:

* list all courses taught in a given semester
* list all teachers which teach a course in semester "20143"
* list all courses taught in fall 2014 (i.e. with semester == "20143"), and include the name of the main teacher, or an empty string if the given course has no main teacher.

Note that most of those were covered in the last lecture from last week.

## Entity relationships

Modify the class CourseInstance such that it uses the __virtual__ keyword to specify related entities, i.e. the semester
in which the course is taught, and also to specify what teachers are registered in a given course.

## Auto mapper

Add Auto Mapper to the project, and implement the GetStudentsInCourse method using mapping provided by that library.
