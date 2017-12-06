using System;
using TBGApp.Database.Models;
using TBGApp.Helpers;

namespace TBGApp
{
    public partial class Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Questions";
        }

        protected void RetrieveQuestionButton_Click(object sender, EventArgs e)
        {
            // Disables the button.
            RetrieveQuestionButton.Enabled = false;

            Question question;
            Card card;
            DatabaseHelper.RetrieveRandomQuestionByCardId(int.Parse(CardNumberTextBox.Text), out question, out card);

            Session["CorrectAlternative"] = 0;

            if (question.Id > 0)
            {
                QuestionPanel.CssClass = "row tbg-visible";
                InvalidCardPanel.CssClass = "row tbg-hidden";

                // Setting the card properties on screen.
                CardNameLabel.Text = card.Name;
                CardTagThemeLabel.Text = card.Theme;
                CardTagDifficultyLabel.Text = card.Difficulty;

                // Setting up the question properties on the screen.
                QuestionLabel.Text = question.Description;
                QuestionAlternative01.Text = "a) " + question.Alternatives[0].Description;
                QuestionAlternative02.Text = "b) " + question.Alternatives[1].Description;
                QuestionAlternative03.Text = "c) " + question.Alternatives[2].Description;
                QuestionAlternative04.Text = "d) " + question.Alternatives[3].Description;

                // Setting the correct question.
                foreach (Alternative alternative in question.Alternatives)
                {
                    if (alternative.IsCorrect)
                    {
                        Session["CorrectAlternative"] = alternative.Order;
                    }
                }
            }
            else
            {
                QuestionPanel.CssClass = "row tbg-hidden";
                InvalidCardPanel.CssClass = "row tbg-visible";
                InvalidCardLabel.Text = string.Format("The Card {0} is invalid!!!", CardNumberTextBox.Text);
            }
        }
    }
}