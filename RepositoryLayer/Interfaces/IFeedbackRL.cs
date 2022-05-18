using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        public AddFeedback AddFeedback(AddFeedback addAddress, int userId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);
    }
}
