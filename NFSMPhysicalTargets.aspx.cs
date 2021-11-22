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
    public partial class NFSMPhysicalTargets : System.Web.UI.Page
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["kkisanconstr"].ConnectionString;

        int DistrictID, TalukID, HobliID, VillageID;
        public byte[] imgrtc;
        public string lat;
        public string lan;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DistrictID = Convert.ToInt32(Session["DistrictID"]);
                TalukID = Convert.ToInt32(Session["TalukID"]);
                HobliID = Convert.ToInt32(Session["HobliID"]);
                if (IsPostBack == false)
                {
                    Common.BindDropdownByRoleHobli(ddlDistrictID, ddlTalukID, ddlHobliID, DistrictID, TalukID, HobliID, "ALL", Convert.ToInt32(Session["RoleID"]));
                    FMMemberFunction.FinancialYearSEED(ddlFinancialYear, 20, 20, "kkisanconstr");
                    LoadDemoProgress();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }

        private void LoadDemoProgress()
        {
            grdDCBillList.Visible = true;
            divDCBill.Visible = true;

            DBTManager dbtMgr = new DBTManager();
            int districtId = ddlDistrictID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlDistrictID.SelectedValue) : 0;
            int talukId = ddlTalukID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlTalukID.SelectedValue) : 0;
            int hobliId = ddlHobliID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlHobliID.SelectedValue) : 0;

            List<KKISAN.Model.DBT.NFSMPhysicalTarget> searchResult = dbtMgr.GetNFSMPhysicalTargets(Convert.ToInt32(ddlFinancialYear.SelectedValue), "DBT", Convert.ToInt32(ddlschemes.SelectedValue), districtId, talukId, hobliId);
            if (searchResult != null && searchResult.Count > 0)
            {
                grdDCBillList.DataSource = searchResult;
                grdDCBillList.DataBind();
                int i = 0;
                foreach (GridViewRow row in grdDCBillList.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label TalukName = (row.Cells[0].FindControl("lblTaluk") as Label);
                        Label HobliName = (row.Cells[0].FindControl("lblHobli") as Label);
                        if (TalukName.Text == "")
                        {
                            grdDCBillList.Columns[3].Visible = false;

                        }
                        else
                        {
                            grdDCBillList.Columns[3].Visible = true;

                        }
                        if (HobliName.Text == "")
                        {
                            grdDCBillList.Columns[4].Visible = false;
                        }
                        else
                        {
                            grdDCBillList.Columns[4].Visible = true;
                        }
                        i++;

                    }
                }
            }
            else
            {
                grdDCBillList.DataSource = new List<NFSMPhysicalTarget>();
                grdDCBillList.DataBind();
            }
        }


        protected void ddlschemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDemoProgress();
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
                LoadDemoProgress();
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

        protected void ddlfinancial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bindschemes();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", System.IO.Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void Bindschemes()
        {
            try
            {
                DBTManager dbtManager = new DBTManager();
                List<DBTSchemes> Schemes = dbtManager.GetschemesforDBT();
                if (Schemes != null && Schemes.Count > 0)
                {
                    ddlschemes.DataSource = Schemes;
                    ddlschemes.DataTextField = "SchemeNameEng";
                    ddlschemes.DataValueField = "SchemeID";
                }
                else
                {
                    ddlschemes.DataSource = new List<DBTSchemes>();
                }
                ddlschemes.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlschemes.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void ddlTalukID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Common.HobliAll(ddlHobliID, "ALL", Convert.ToInt32(ddlDistrictID.SelectedValue), Convert.ToInt32(ddlTalukID.SelectedValue), "E", "kkisanconstr");
                LoadDemoProgress();
                // ddlVillageID.Items.Clear();
                // ddlVillageID.Items.Insert(0, new ListItem("------ALL------", "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", System.IO.Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void grdItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDCBillList.PageIndex = e.NewPageIndex;

            LoadDemoProgress();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    LoadDemoProgress();
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
                this.LoadDemoProgress();


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
    }
}