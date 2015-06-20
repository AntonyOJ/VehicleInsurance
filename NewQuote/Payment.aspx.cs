using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewQuote_Payment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Policy policy = (Policy)Session["NewPolicy"];
        if (policy == null)
        {
            Response.Redirect("Details");
        }
        else if (policy.agreeToQuotation == false)
        {
            Response.Redirect("Quote");
        }
        else if (policy.password.Length < 1)
        {
            Response.Redirect("Password");
        }
        if (!Page.IsPostBack)
        {
            PaymentAmountLabel.Text = "Amount to be paid: £" + policy.quotation;
            CardTypeDropDownList.Items.Insert(0, new ListItem("Visa/Delta/Electron", "2"));
            CardTypeDropDownList.Items.Insert(0, new ListItem("MasterCard", "1"));
            CardTypeDropDownList.Items.Insert(0, new ListItem("Select Card Type", "0"));
        }
    }

    protected void MakePayment_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && ValidForm())
        {
            Policy policy = (Policy)Session["NewPolicy"];
            policy.WriteAllToDatabase();
            Session["NewPolicy"] = policy;
            Response.Redirect("IssuePolicy");
        }
    }

    public bool ValidForm()
    {
        if (!ValidCardType() | !ValidCardNumber() | !ValidCardHoldersName() | !ValidExpirationDate())
            return false;
        return true;
    }

    public bool ValidCardType()
    {
        if (CardTypeDropDownList.SelectedValue.Equals("0"))
        {
            CardTypeErrorLabel.Text = "You must select a Card Type";
            return false;
        }
        else
        {
            CardTypeErrorLabel.Text = "*";
            return true;
        } 
    }

    public bool ValidCardNumber()
    {
        if (CardNumberTextBox.Text.Trim().Length < 1)
        {
            CardNumberErrorLabel.Text = "You must enter a Card Number";
            return false;
        }
        else if (CardNumberTextBox.Text.Trim().Length < 16)
        {
            CardNumberErrorLabel.Text = "You must enter a 16 digit Card Number";
            return false;
        }
        else if (CardNumberTextBox.Text.Trim().Length == 16)
        {
            long input;
            if (!long.TryParse(CardNumberTextBox.Text, out input))
            {
                CardNumberErrorLabel.Text = "You must enter a 16 digit Card Number in '9999-9999-9999-9999 or '9999999999999999' format";
                return false;
            }
        }
        else
        {
            // Matches the format of 9999-9999-9999-9999
            Regex pattern = new Regex(@"\d{4}(-\d{4}){3}");
            if (!pattern.IsMatch(CardNumberTextBox.Text))
            {
                CardNumberErrorLabel.Text = "TestYou must enter a 16 digit Card Number in '9999-9999-9999-9999' or '9999999999999999' format";
                return false;
            }
        }
        CardNumberErrorLabel.Text = "*";
        return true;
    }

    public bool ValidCardHoldersName()
    {
        if (CardHoldersNameTextBox.Text.Trim().Length < 1)
        {
            CardHoldersNameErrorLabel.Text = "You must enter the Cardholder's Name";
            return false;
        }
        else
        {
            CardHoldersNameErrorLabel.Text = "*";
            return true;
        }
    }

    public bool ValidExpirationDate()
    {
        DateTime input;
        if (ExpirationDateTextBox.Text.Trim().Length < 1)
        {
            ExpirationDateErrorLabel.Text = "You must enter the Expiration Date";
            return false;
        }
        else if (!DateTime.TryParse(ExpirationDateTextBox.Text, out input))
        {
            ExpirationDateErrorLabel.Text = "You must enter a valid date format for the Expiration Date";
            return false;
        }
        else if (input < DateTime.Today)
        {
            ExpirationDateErrorLabel.Text = "You must enter an unexpired Expiration Date";
            return false;
        }
        else
        {
            ExpirationDateErrorLabel.Text = "*";
            return true;
        }
    }
}