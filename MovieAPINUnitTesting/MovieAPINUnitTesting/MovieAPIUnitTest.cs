using NUnit.Framework;
using MovieAPI.Controllers;

namespace MovieAPINUnitTesting
{
    public class MovieAPIUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetMovies()
        {
            MovieController objMovieController = new MovieController();
            System.Data.DataTable dtMovies = objMovieController.GetMovies();
            if (dtMovies == null)
            {
                Assert.Fail("Return type of method GetMovies return null");
            }
            else if (dtMovies.Rows.Count != 149)
            {
                Assert.Fail("Expected Rows from GetMovies method was 149 and GetMovies method returned " + dtMovies.Rows.Count.ToString());
            }
            else
            {
                Assert.Pass("TestGetMovies method executed as expected");
            }
        }

        [Test]
        public void TestAddMovies()
        {
            MovieController objMovieController = new MovieController();
            MovieAPI.MovieData objMovieData = new MovieAPI.MovieData();
            objMovieData.Language = "ENG";
            objMovieData.MovieId = 26;
            objMovieData.Title = "Harry Pottar 26";
            objMovieData.ReleaseYear = 2008;
            objMovieData.Duration = "3:00";

            bool blnResult = objMovieController.AddMovieDetails(objMovieData);

            if (blnResult == true)
            {
                Assert.Pass("One Record added successfully");
            }
            else
            {
                Assert.Fail("Record not added due to some error");
            }
        }

        [Test]
        public void GetMovieDetails()
        {
            MovieController objMovieController = new MovieController();
            MovieAPI.MovieData objMovieData = new MovieAPI.MovieData();

            objMovieData = objMovieController.GetMovieDetails(4);
            if (objMovieData.UniquieID > 0)
            {
                Assert.Pass("Movie details fetched successfuly");
            }
            else
            {
                Assert.Fail("No record found");
            }
        }
    }
}