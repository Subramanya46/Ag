using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KKISANWEB.Demonstration
{
    public partial class AuditImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                byte[] b = null;
                if (Session["AuditImage"].ToString() != "")
                {
                    b = ((byte[])Session["AuditImage"]);
                    Response.ContentType = "image/jpeg";
                    //writes binary array with binary write
                    Response.BinaryWrite(b);
                    //session["img"] = "";
                }
            }
            catch (Exception ex)
            {
                Session.Abandon();
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-30);
                Response.Redirect("~/Login.aspx");
            }
            finally
            {
            }
        }
    }
}