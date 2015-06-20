using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Policy newPolicy = (Policy)Session["NewPolicy"];
        if (newPolicy != null)
        {
            ContinueWithNewQuoteHyperLink.Visible = true;
        }
        Policy existingPolicy = (Policy)Session["ExistingPolicy"];
        if (existingPolicy != null)
        {
            AccessLoggedInPolicyHyperLink.Visible = true;
        }
    }
}