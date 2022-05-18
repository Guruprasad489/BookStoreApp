using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId)
        {
            try
            {
                return feedbackRL.AddFeedback(addFeedback, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FeedbackResponse> GetAllFeedbacks(int bookId)
        {
            try
            {
                return feedbackRL.GetAllFeedbacks(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
