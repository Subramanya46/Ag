using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

using KKISAN.Helper;
using KKISAN.Model.FM;
using KKISAN.Model.MI;
using KKISAN.Services;
using KKISAN.Utils;
using System.ComponentModel;
using KKISAN.Model.DBT;


namespace KKISANWEB.Demonstration
{
    public partial class DemonstrationSearch2122 : System.Web.UI.Page
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["kkisanconstr"].ConnectionString;

        string ApplicationID = "";
        int DistrictID, TalukID, HobliID, VillageID;
        public byte[] imgrtc;
        public string lat;
        public string lan;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ApplicationID = Request.QueryString["AID"] != null ? Request.QueryString["AID"] : "";
                DistrictID = Convert.ToInt32(Session["DistrictID"]);
                TalukID = Convert.ToInt32(Session["TalukID"]);
                HobliID = Convert.ToInt32(Session["HobliID"]);
                if (IsPostBack == false)
                {
                    divApplicationEntry.Visible = Session["RoleId"].ToString() == "11";
                    BindMainScheme();
                    Common.BindDropdownByRoleHobli(ddlDistrictID, ddlTalukID, ddlHobliID, DistrictID, TalukID, HobliID, "ALL", Convert.ToInt32(Session["RoleID"]));
                    // FMMemberFunction.FinancialYearSEED(ddlFinancialYear, 20, 20, "kkisanconstr");

                    LoadLastestDCBills();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }
        private void BindMainScheme()
        {
            try
            {
                string str = ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString();
                SqlConnection con = new SqlConnection(str);
                string com = "select MainSchemeId as ValueField,MainSchemeName as ItemField from DBT.MainScheme";

                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddlScheme.DataSource = dt;
                ddlScheme.DataBind();
                ddlScheme.DataTextField = "ItemField";
                ddlScheme.DataValueField = "ValueField";
                ddlScheme.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void LoadLastestDCBills()
        {
            grdDCBillList.Visible = true;
            divDCBill.Visible = true;

            if (ApplicationID == "DBT")
            {
                int mainSchemeID = ddlScheme.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlScheme.SelectedValue) : 0;
                int districtId = ddlDistrictID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlDistrictID.SelectedValue) : 0;
                int talukId = ddlTalukID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlTalukID.SelectedValue) : 0;
                int hobliId = ddlHobliID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlHobliID.SelectedValue) : 0;
                List < FarmerRegistration > searchResult = new List<FarmerRegistration>();
                DBTManager dbtMgr = new DBTManager();
                if(mainSchemeID==2)
                {
                    searchResult = dbtMgr.GetFarmerRegistrationNonDemo(districtId, talukId, hobliId, ddlCaste.SelectedValue, KKISAN.Utils.ConversionUtil.ConvertInt(ddlStatus.SelectedValue));
                    grdDCBillList.Columns[9].Visible = false;
                    grdDCBillList.Columns[10].Visible = true;
                    grdDCBillList.Columns[11].Visible = false;
                    grdDCBillList.Columns[12].Visible = false;
                    grdDCBillList.Columns[14].Visible = false;
                    grdDCBillList.Columns[17].Visible = true;
                    grdDCBillList.Columns[19].Visible = false;
                    grdDCBillList.Columns[15].Visible = true;
                    grdDCBillList.Columns[16].Visible = true;
                }

                else
                {
                    searchResult = dbtMgr.GetFarmerRegistration2122(districtId, talukId, hobliId, ddlCaste.SelectedValue, KKISAN.Utils.ConversionUtil.ConvertInt(ddlStatus.SelectedValue));
                    grdDCBillList.Columns[9].Visible = true;
                    grdDCBillList.Columns[10].Visible = false;
                    grdDCBillList.Columns[11].Visible = true;
                    grdDCBillList.Columns[12].Visible = true;
                    grdDCBillList.Columns[17].Visible = false;
                    grdDCBillList.Columns[14].Visible = true;
                    grdDCBillList.Columns[19].Visible = true;
                    grdDCBillList.Columns[15].Visible = false;
                    grdDCBillList.Columns[16].Visible = false;
                }
                if (searchResult != null && searchResult.Count > 0)
                {
                    grdDCBillList.DataSource = searchResult;
                    grdDCBillList.DataBind();
                }
                else
                {
                    grdDCBillList.DataSource = new List<FarmerRegistration>();
                    grdDCBillList.DataBind();
                }
            }
        }


        protected void grdItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDCBillList.PageIndex = e.NewPageIndex;

            LoadLastestDCBills();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Demonstration/ApplicationEntry2122.aspx?AID=" + ApplicationID, false);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    LoadLastestDCBills();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Dashboard.aspx", false);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void btnExportXL_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();

        }

        private void ExportGridToExcel()
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //string FileName = "Farmer" + DateTime.Now + ".xls";
            ////StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //grdDCBillList.GridLines = GridLines.Both;
            //grdDCBillList.HeaderStyle.Font.Bold = true;
            //grdDCBillList.AllowPaging = true;
            // grdDCBillList.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End();


            Response.Clear();
            Response.Buffer = true;
            string FileName = "FarmerDetails" + DateTime.Now + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdDCBillList.AllowPaging = false;
                this.LoadLastestDCBills();


                foreach (TableCell cell in grdDCBillList.HeaderRow.Cells)
                {
                    cell.BackColor = grdDCBillList.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdDCBillList.Rows)
                {

                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdDCBillList.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdDCBillList.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdDCBillList.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddlSubScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            int schemeID = ddlScheme.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlScheme.SelectedValue) : 0;
            if (schemeID==1)
            {
                statusddl.Visible = true;
            }
            else
            {
                statusddl.Visible = false;
            }
            
            //CalculateLand();
        }
    }
}