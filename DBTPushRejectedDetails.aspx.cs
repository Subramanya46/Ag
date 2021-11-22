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

namespace Kisan.Demonstration
{
    public partial class DBTPushRejectedDetails : System.Web.UI.Page
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
                    BindFinanciyear();
                   
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }

        private void BindFinanciyear()
        {
            try
            {
                DBTManager dbtManager = new DBTManager();
                List<DBTFinancialYear> finacialdetails = dbtManager.GetFinancialyryear();
                if (finacialdetails != null && finacialdetails.Count > 0)
                {
                    ddlFinancialYear.DataSource = finacialdetails;
                    ddlFinancialYear.DataTextField = "FinancialYearName";
                    ddlFinancialYear.DataValueField = "FinancialYearID";
                }
                else
                {
                    ddlFinancialYear.DataSource = new List<DBTFinancialYear>();
                }
                ddlFinancialYear.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlFinancialYear.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }


        protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Common.BindDropdownByRoleHobli(ddlDistrictID, ddlTalukID, ddlHobliID, DistrictID, TalukID, HobliID, "ALL", Convert.ToInt32(Session["RoleID"]));
                LoadRejectedFarmersDetails();

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", System.IO.Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        protected void ddlDistrictID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Common.TalukAll(ddlTalukID, "ALL", Convert.ToInt32(ddlDistrictID.SelectedValue), "E", "kkisanconstr");
                LoadRejectedFarmersDetails();
                //  ddlHobliID.Items.Clear();
                //ddlHobliID.Items.Insert(0, new ListItem("------ALL------", "0"));
                //ddlVillageID.Items.Clear();
                // ddlVillageID.Items.Insert(0, new ListItem("------ALL------", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", System.IO.Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void ddlTalukID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Common.HobliAll(ddlHobliID, "ALL", Convert.ToInt32(ddlDistrictID.SelectedValue), Convert.ToInt32(ddlTalukID.SelectedValue), "E", "kkisanconstr");
                LoadRejectedFarmersDetails();
                // ddlVillageID.Items.Clear();
                // ddlVillageID.Items.Insert(0, new ListItem("------ALL------", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", System.IO.Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void LoadRejectedFarmersDetails()
        {
            grdRejectfarmers.Visible = true;
            divDCBill.Visible = true;

            if (ApplicationID == "DBT")
            {
                DBTManager dbtMgr = new DBTManager();
                int DistrictID = ddlDistrictID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlDistrictID.SelectedValue) : 0;
                int TalukID = ddlTalukID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlTalukID.SelectedValue) : 0;
                int HobliID = ddlHobliID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlHobliID.SelectedValue) : 0;
                List<FarmerRegistration> searchResult = dbtMgr.GetAprovedFarmers(DistrictID, TalukID, HobliID);
                if (searchResult != null && searchResult.Count > 0)
                {
                    grdRejectfarmers.DataSource = searchResult;
                    grdRejectfarmers.DataBind();
                }
                else
                {
                    grdRejectfarmers.DataSource = new List<FarmerRegistration>();
                    grdRejectfarmers.DataBind();
                }
            }
        }


        protected void grdItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRejectfarmers.PageIndex = e.NewPageIndex;
           
            LoadRejectedFarmersDetails();
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
                Response.Redirect("~/Demonstration/ApplicationEntry.aspx?AID=" + ApplicationID, false);
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
                    LoadRejectedFarmersDetails();
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
            //grdRejectfarmers.GridLines = GridLines.Both;
            //grdRejectfarmers.HeaderStyle.Font.Bold = true;
            //grdRejectfarmers.AllowPaging = true;
            // grdRejectfarmers.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End();


            Response.Clear();
            Response.Buffer = true;
            string FileName = "FarmerDetails" + DateTime.Now + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename="+FileName);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdRejectfarmers.AllowPaging = false;
                this.LoadRejectedFarmersDetails();


                foreach (TableCell cell in grdRejectfarmers.HeaderRow.Cells)
                {
                    cell.BackColor = grdRejectfarmers.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdRejectfarmers.Rows)
                {
                    
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdRejectfarmers.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdRejectfarmers.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdRejectfarmers.RenderControl(hw);

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

    }
}