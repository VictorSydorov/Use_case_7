namespace Use_case_7.Student
{
    public class StudentConverter
    {
        public List<Student> ConvertStudents(List<Student> students)
        {
            return students.Select(student =>
            {
                var result = new Student
                {
                    Name = student.Name,
                    Age = student.Age,
                    Grade = student.Grade
                };

                if (student.Grade > 90)
                {
                    if (student.Age < 21)
                    {
                        result.Exceptional = true;
                    }
                    else
                    {
                        result.HonorRoll = true;
                    }
                }
                else if (student.Grade > 70)
                {
                    result.Passed = true;
                }
                else
                {
                    result.Passed = false;
                }

                return result;
            }).ToList();
        }
    }
}
