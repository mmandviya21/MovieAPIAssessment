using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Text;


namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        // For testing purpose
        private string strMetaDataFilePath = "C:\\MovieAPIData\\metadata.csv";
        private string strPostMetaDataFilePath = "C:\\MovieAPIData\\postmetadata.csv";

        // After deployment we can refer.
        //string strMetaDataFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Data\\metadata.csv";
        //string strPostMetaDataFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Data\\postmetadata.csv";

        // GET: api/<MovieController>
        [HttpGet]
        public DataTable GetMovies()
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strMetaDataFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;

        }

        // GET api/<MovieController>
        [HttpGet("{movieId}")]
        public MovieData GetMovieDetails(int movieId)
        {
            MovieData objMovieData = new MovieData();
            DataTable dtMovies = GetMovies();
                        
           IEnumerable<DataRow> dr = dtMovies.AsEnumerable().Where(x => x.Field<string>("MovieId") == movieId.ToString()).OrderByDescending(row => row.Field<String>("Id")).Take(1);

            if (dr != null)
            {
                objMovieData.UniquieID = Convert.ToUInt16(dr.ElementAt(0)["Id"].ToString());
                objMovieData.MovieId = movieId;
                objMovieData.Title = dr.ElementAt(0)["Title"].ToString();
                objMovieData.Language = dr.ElementAt(0)["Language"].ToString();
                objMovieData.Duration = dr.ElementAt(0)["Duration"].ToString();
                objMovieData.ReleaseYear = Convert.ToUInt16(dr.ElementAt(0)["ReleaseYear"].ToString());
            }


            return objMovieData;
        }

        // POST api/<MovieController>
        [HttpPost]
        public Boolean AddMovieDetails([FromBody] MovieData objMovieData)
        {            
            Boolean blnResult = false;
            try
            {
                int intNumberofRecords = System.IO.File.ReadAllLines(strPostMetaDataFilePath).Length;
                objMovieData.UniquieID = intNumberofRecords;

                // [Id], [MovieId], [Title], [Language], [Duration], [ReleaseYear]
                using (TextWriter sw = new StreamWriter(strPostMetaDataFilePath,true))
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5}", objMovieData.UniquieID, objMovieData.MovieId,objMovieData.Title, objMovieData.Language, objMovieData.Duration, objMovieData.ReleaseYear);
                   
                }

               blnResult = true;
            }
            catch(Exception ex)
            {
                blnResult = false;
            }

            return blnResult;

        }
               
    }
}
