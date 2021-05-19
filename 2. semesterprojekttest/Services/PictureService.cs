using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using _2._semesterprojekttest.Interfaces;
using _2._semesterprojekttest.Models;
using Microsoft.AspNetCore.Http;

namespace _2._semesterprojekttest.Services
{
    public class PictureService : IProfilePicture
    {
        private GrydenDBContext db = new GrydenDBContext();
        private const string ConnectionString = "Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!";

        public void AddPicture(Picture pic)
        {
            var idcheck = db.Pictures.Where(i => i.UserId == pic.UserId && i.TypeId == pic.TypeId).ToList(); //Laver en Liste for at tjekke om et profil- eller Bilbillede (hvad end man har valgt at uploade) allerede eksiterer for brugeren.
            List<int> liste = new List<int>();
                if (idcheck.Count()==1)//Hvis der findes et resultat vil den opdatere billede for den profil i stedet for at tilføje et nyt
                {
                    foreach (var picture in idcheck)
                    {
                        using (SqlConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            using (SqlCommand sql = new SqlCommand(
                                "select PictureID from Pictures Where (UserID = @UID) AND (TypeID = @Type)", conn))
                            {
                                sql.Parameters.AddWithValue("@UID", pic.UserId);
                                sql.Parameters.AddWithValue("@Type", pic.TypeId);
                            SqlDataReader reader = sql.ExecuteReader();
                                while (reader.Read())
                                {
                                    Picture p = new Picture();
                                    p.PictureId = reader.GetInt32(0);

                                    liste.Add(p.PictureId);
                                    pic.PictureId = liste[0];
                                    
                                }
                                reader.Close();
                            }

                            using (SqlCommand sql = new SqlCommand(
                                "UPDATE Pictures SET UserID = @UID, TypeID = @Type, FileType = @FT, Picture = @Pic WHERE PictureID = @PID", conn))
                            {
                                sql.Parameters.AddWithValue("@UID", pic.UserId);
                                sql.Parameters.AddWithValue("@Type", pic.TypeId);
                                sql.Parameters.AddWithValue("@FT", pic.FileType);
                                sql.Parameters.AddWithValue("@Pic", pic.Picture1);
                                sql.Parameters.AddWithValue("@PID", pic.PictureId);
                                int rows = sql.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    db.Pictures.Add(pic);
                    db.SaveChanges();
                }
            
        }

        public void UpdatePicture(Picture pic)
        {
            foreach (Picture picture in db.Pictures.ToList())
            {
                if ((picture.TypeId == pic.TypeId) && (picture.UserId == pic.UserId))
                {
                    pic.PictureId = picture.PictureId;
                    db.Update(pic);
                }
            }
        }

        public void DeletePicture(int userID, int typeID)
        {
        }

        public Picture GetProfilePicture(int userID)
        {
            //enity ser ud til at gemme dens egen cache eller noget og gjorde at hvis vi redirectede til en anden side efter at have uploadet
            //et nyt billede blev det først opdateret efter vi startede nyt op projekt.
            //var checkforpic = db.Pictures.Where(i => i.UserId == userID && i.TypeId == 1).ToList();
            //foreach (Picture pic in checkforpic)
            //{
            //    return pic;
            //}

            //return null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand sql = new SqlCommand("SELECT * FROM Pictures WHERE (UserID = @UID AND TypeID = 1)", conn))
                {
                    sql.Parameters.AddWithValue("@UID", userID);
                    SqlDataReader reader=sql.ExecuteReader();
                    if (reader.Read())
                    {
                        Picture p = new Picture();
                        p.Picture1 = (byte[])reader["Picture"];
                        return p;
                    }
                }

                return null;
            }

        }
        public Picture GetCarPicture(int userID)
        {
            //var checkforpic = db.Pictures.Where(i => i.UserId == userID && i.TypeId == 0).ToList();
            //foreach (Picture pic in checkforpic)
            //{
            //    return pic;
            //}

            //return null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand sql = new SqlCommand("SELECT * FROM Pictures WHERE (UserID = @UID AND TypeID = 2)", conn))
                {
                    sql.Parameters.AddWithValue("@UID", userID);
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.Read())
                    {
                        Picture p = new Picture();
                        p.Picture1 = (byte[])reader["Picture"];
                        return p;
                    }
                }

                return null;
            }
        }

    }
}