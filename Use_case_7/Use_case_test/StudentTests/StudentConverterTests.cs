using System.Collections;
using Use_case_7.Student;

namespace Use_case_test.StudentTests
{
    public class StudentConverterTests
    {
        [Theory]
        [ClassData(typeof(PositiveTestCases))]
        public void ConvertStudents_WithPositiveData_ReturnsExpectedResult(int age, int grade, Func<Student, bool> expected)
        {
            // arrange
            var student = new Student()
            {
                Age = age,
                Grade = grade,
                Name = "John Depp"
            };

            // act
            var actual = StudentConverter.ConvertStudents(new List<Student> {student}).First();

            // assert
            Assert.True(expected(actual));
        }

        [Fact]
        public void ConvertStudents_WithEmptyList_ReturnsEmptyResult()
        {
            // arrange          

            // act
            var actual = StudentConverter.ConvertStudents(new List<Student>());

            // assert
            Assert.True(!actual.Any());
        }

        [Fact]        
        public void ConvertStudents_WithNullList_ThrowsArgumentNullException()
        {
            // arrange          

            // act
            var ex = Record.Exception(() =>  StudentConverter.ConvertStudents(null));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
        }
    }


    public class PositiveTestCases : IEnumerable<object[]>
    {
        public static readonly List<object[]> testCases = new List<object[]>
    {
        //Case 1: High Achiever 
        new object[]{ 22, 91,  new Func<Student, bool>(s=>s.HonorRoll) },
        // Case 2: Exceptional Young High Achiever
        new object[]{20, 91, new Func<Student, bool>(s => s.Exceptional) },
        // Case 3: Passed Student 
        new object[]{22, 71, new Func<Student, bool>(s => s.Passed) },
        new object[]{22, 90, new Func<Student, bool>(s => s.Passed) },
        new object[]{19, 71, new Func<Student, bool>(s => s.Passed) },
        new object[]{19, 90, new Func<Student, bool>(s => s.Passed) },
        // Case 4: Failed Student 
        new object[]{19, 70, new Func<Student, bool>(s => !s.Passed) },
        new object[]{19, 69, new Func<Student, bool>(s => !s.Passed) },
        new object[]{22, 69, new Func<Student, bool>(s => !s.Passed) },
        new object[]{21, 70, new Func<Student, bool>(s => !s.Passed) },
    };

        public IEnumerator<object[]> GetEnumerator() => testCases.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
