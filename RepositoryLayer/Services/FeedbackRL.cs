using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRL : IFeedbackRL
    {
        private readonly IConfiguration configuration;

        public FeedbackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddFeedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Rating", addFeedback.Rating);
                    cmd.Parameters.AddWithValue("@Comment", addFeedback.Comment);
                    cmd.Parameters.AddWithValue("@BookId", addFeedback.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                    if (result != 1)
                    {
                        return addFeedback;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<FeedbackResponse> GetAllFeedbacks(int bookId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    List<FeedbackResponse> feedbackResponse = new List<FeedbackResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllFeedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            FeedbackResponse feedback = new FeedbackResponse();
                            FeedbackResponse temp;
                            temp = ReadData(feedback, rdr);
                            feedbackResponse.Add(temp);
                        }
                        con.Close();
                        return feedbackResponse;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public FeedbackResponse ReadData(FeedbackResponse feedback, SqlDataReader rdr)
        {
            feedback.FeedbackId = Convert.ToInt32(rdr["FeedbackId"] == DBNull.Value ? default : rdr["FeedbackId"]);
            feedback.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            feedback.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            feedback.Comment = Convert.ToString(rdr["Comment"] == DBNull.Value ? default : rdr["Comment"]);
            feedback.Rating = Convert.ToInt32(rdr["Rating"] == DBNull.Value ? default : rdr["Rating"]);
            feedback.FullName = Convert.ToString(rdr["FullName"] == DBNull.Value ? default : rdr["FullName"]);

            return feedback;
        }
    }
}
