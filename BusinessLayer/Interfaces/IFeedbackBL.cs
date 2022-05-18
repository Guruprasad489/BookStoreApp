using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public AddFeedback AddFeedback(AddFeedback addAddress, int userId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);
    }
}
