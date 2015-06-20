using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewQuote_Password : System.Web.UI.Page
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
        else if (!(policy.password.Length < 1))
        {
            Response.Redirect("Payment");
        }
    }
    protected void Payment_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && ValidForm())
        {
            Policy policy = (Policy)Session["NewPolicy"];
            policy.password = Password1TextBox.Text;
            Session["NewPolicy"] = policy;
            Response.Redirect("Payment");
        }
    }

    public bool ValidForm()
    {
        if (!ValidPassword1())
        {
            Password2ErrorLabel.Text = "";
            return false;
        }
        if (!ValidPassword2())
            return false;
        return true;
    }

    public bool ValidPassword1()
    {
        if (Password1TextBox.Text.Trim().Length < 4)
        {
            Password1ErrorLabel.Text = "You must enter a Password of at least 4 characters";
            return false;
        }
        else
        {
            Password1ErrorLabel.Text = "";
            return true;
        } 
    }

    public bool ValidPassword2()
    {
        if (!(Password1TextBox.Text.Equals(Password2TextBox.Text)))
        {
            Password2ErrorLabel.Text = "Your retyped password must match the first password";
            return false;
        }
        else
        {
            Password2ErrorLabel.Text = "";
            return true;
        }  
    }
}