using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Use_case_7.Player.Tests
{
    public class PlayerAnalyzerTests
    {
        public static IEnumerable<object[]> TestData => new List<object[]>
    {
        new object[] { 25, 5, new[] { 2, 2, 2 }, 250.0 },
        new object[] { 15, 3, new[] { 3, 3, 3 }, 67.5 },
        new object[] { 35, 15, new[] { 4, 4, 4 }, 2520.0 }        
    };

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateScoreTest_ReturnsCorrectScoreForPlayers(int age, int exp, int[] skills, double score)
        {
            // Arrange
            var player = new Player()
            {
                Age = age,
                Experience = exp,
                Name = "John Depp",
                Skills = new List<int>(skills)
            };

            // Act
            double actualScore = PlayerAnalyzer.CalculateScore(new List<Player> { player });

            // Assert
            Assert.Equal(score, actualScore);
        }


        [Fact]      
        public void CalculateScoreTest_ReturnsCorrectScoreForAllPlayer()
        {
            // arrange 
            List<Player> list = TestData.Select(d=>new Player { 
                Age = (int)d[0],
                Experience = (int)d[1],
                Name = "J D",
                Skills = ((int[])d[2]).ToList()
                
            }).ToList();

            double expectedScore = TestData.Sum(d => (double)d[3]);

            // act
            double actualScore = PlayerAnalyzer.CalculateScore(list);

            // assert
            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void CalculateScoreTest_NullSkills_ThrowsError()
        {
            // arrange 
            var player = new Player
            {
                Age = 21,
                Experience = 1,
                Name = "J D",
                Skills = null

            };
                      
            // act
            var ex = Record.Exception(()=> PlayerAnalyzer.CalculateScore(new List<Player> { player }));

            // assert
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void CalculateScore_EmptyList_Returns_0()
        {
            // act
            var actual = PlayerAnalyzer.CalculateScore(new List<Player>());

            // assert
            Assert.Equal(0.0, actual);
        }
    }
    
}