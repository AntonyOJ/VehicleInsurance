using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Policy_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Policy policy = (Policy)Session["ExistingPolicy"];
        if (policy != null)
        {
            // Response.Redirect("Details");
        }
    }
    protected void Login_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && ValidForm())
        {
            string policyNumber = PolicyNumberTextBox.Text;
            string password = PasswordTextBox.Text;
            Policy policy = new Policy();
            bool matchFound = policy.AuthenticatedLogin(policyNumber, password);
            if (matchFound)
            {
                Session["ExistingPolicy"] = policy;
                Response.Redirect("Details");
            }
            else
            {
                ErrorLabel.Text = "A corresponding Policy could not be found of the entered Policy Number and Password";
            }
        }
    }

    public bool ValidForm()
    {
        bool result = true;
        if (!ValidPolicyNumber() | !ValidPassword())
            result = false;
        return result;
    }

    public bool ValidPolicyNumber()
    {
        int input;
        if (PolicyNumberTextBox.Text.Trim().Length < 1)
        {
            PolicyNumberErrorLabel.Text = "You must enter a Policy Number";
            return false;
        }
        else if (!Int32.TryParse(PolicyNumberTextBox.Text, out input))
        {
            PolicyNumberErrorLabel.Text = "You must enter a number for the Policy Number";
            return false;
        }
        else
        {
            PolicyNumberErrorLabel.Text = "";
            return true;
        }        
    }

    public bool ValidPassword()
    {
        if (PasswordTextBox.Text.Trim().Length < 1)
        {
            PasswordErrorLabel.Text = "You must enter a Password";
            return false;
        }
        else
        {
            PasswordErrorLabel.Text = "";
            return true;
        }        
    }
}