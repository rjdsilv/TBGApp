<%@ Page  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="TBGApp.Questions" %>
<asp:Content ID="QuestionContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="InputPanel" CssClass="row" runat="server">
        <div class="tbg-fields-display">
            <div class="col-md-3 tbg-field-line">&nbsp;</div>
            <div class="col-md-4 tbg-field-line">
                <asp:TextBox ID="CardNumberTextBox" CssClass="tbg-input" TextMode="SingleLine" runat="server" placeholder="Card Number" />
            </div>
            <div class="col-md-2 tbg-field-line">
                <asp:Button ID="RetrieveQuestionButton" CssClass="tbg-button" Text="Retrieve Question" OnClick="RetrieveQuestionButton_Click" runat="server"  />
            </div>
            <div class="col-md-3 tbg-field-line">&nbsp;</div>

            <div class="col-md-3">&nbsp;</div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="CardNumberTextBox_RequiredField" ControlToValidate="CardNumberTextBox" Display="Dynamic" ErrorMessage="The field Card Number is required!" CssClass="tbg-error-message" runat="server" />
            </div>
            <div class="col-md-3">&nbsp;</div>
            <hr />
        </div>
    </asp:Panel>

    <asp:Panel ID="InvalidCardPanel" CssClass="row tbg-hidden" runat="server">
        <div class="tbg-invalid-display">
            <asp:Label ID="InvalidCardLabel" runat="server" />
        </div>
    </asp:Panel>

    <asp:Panel ID="QuestionPanel" CssClass="row tbg-hidden" runat="server">
        <div class="tbg-question-display">
            <div class="tbg-card">
                <div class="tbg-card-name">
                    <div class="col-md-2">
                        <b><asp:Label ID="CardNameTextLabel" Text="Card Name:" runat="server" /></b>
                    </div>
                    <div class="col-md-10">
                        <asp:Label ID="CardNameLabel" CssClass="tbg-card-attribute" runat="server" />
                    </div>
                </div>
                <div class="tbg-card-tags">
                    <div class="col-md-2">
                        <b><asp:Label ID="CardTagsTextLabel" Text="Card TAGs: " runat="server" /></b>
                    </div>
                    <div class="col-md-10">
                        <asp:Label ID="CardTagThemeLabel" CssClass="tbg-card-attribute" runat="server" />
                        <asp:Label ID="CardTagDifficultyLabel" CssClass="tbg-card-attribute" runat="server" />
                    </div>
                </div>
            </div>
            <div class="tbg-question">
                <div class="col-md-12">
                    <b><asp:Label ID="QuestionLabelText" Text="QUESTION" CssClass="tbg-card-attribute" style="margin: -20px" runat="server" /></b>
                </div>
                <br />
                <div class="col-md-12 tbg-question-text">
                    <asp:Label ID="QuestionLabel" runat="server" />
                </div>
                <div id="divAlternative01" class="col-md-12 tbg-alternative-text">
                    <asp:Label ID="QuestionAlternative01" runat="server" />
                </div>
                <div id="divAlternative02" class="col-md-12 tbg-alternative-text">
                    <asp:Label ID="QuestionAlternative02" runat="server" />
                </div>
                <div id="divAlternative03" class="col-md-12 tbg-alternative-text">
                    <asp:Label ID="QuestionAlternative03" runat="server" />
                </div>
                <div id="divAlternative04" class="col-md-12 tbg-alternative-text">
                    <asp:Label ID="QuestionAlternative04" runat="server" />
                </div>
            </div>
            <br />
            <div class="tbg-timer">
                <span id="timerText">00:30</span>
            </div>
        </div>
    </asp:Panel>

    <script>
        var timer   = 0;
        var seconds = 30;

        for (var i = 1000; i <= 32000; i += 1000) {
            timer = setTimeout(function () {
                seconds--;
                if (seconds >= 0) {
                    $("#timerText").text("00:" + ("0" + seconds).slice(-2));
                } else {
                    var correctAlternative = <%= Session["CorrectAlternative"] %>;
                    var correctAlternativeId = "#divAlternative0" + correctAlternative;
                    $(correctAlternativeId).addClass("tbg-correct-alternative");
                }

                clearTimeout(timer);
            }, i);
        }
    </script>
</asp:Content>
