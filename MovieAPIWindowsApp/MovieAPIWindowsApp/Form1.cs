using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;


namespace MovieAPIWindowsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
