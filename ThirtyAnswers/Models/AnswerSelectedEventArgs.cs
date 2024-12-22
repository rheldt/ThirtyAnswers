using System;

namespace ThirtyAnswers.Models
{
    public class AnswerSelectedEventArgs : EventArgs
    {
        public string CategoryName { get; set; }
        public int Amount { get; set; }
        public CategoryItem CategoryItem { get; set; }
    }
}