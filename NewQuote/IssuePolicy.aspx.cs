using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewQuote_IssuePolicy : System.Web.UI.Page
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
        else if (policy.policyNumber == 0)
        {
            Response.Redirect("Payment");
        }
        SetLabelText(policy);
        Session["NewPolicy"] = null;
    }

    public void SetLabelText(Policy policy)
    {
        PaymentLabel.Text = "The payment of £" + policy.quotation.ToString() + " has successfully been made.";
        PolicyNumberLabel.Text = "You have been issued with " + policy.policyNumber + 
            " for the Policy Number. Use this along with your password to gain access to your policy details in the future.";
    }
}