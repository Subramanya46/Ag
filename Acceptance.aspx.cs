using KKISAN.Model.DBT;
using KKISAN.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;// Web Service for Farmer Details
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kisan.Demonstration
{
    public partial class Acceptance : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString());
        // Global Declarations
        string ApplicationID = "";
        int DistrictID, TalukID, HobliID, VillageID;

        /*
        $(document)
        */
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
                    if (ApplicationID == "FM" || ApplicationID == "AP" || ApplicationID == "MI" || ApplicationID == "SOM" || ApplicationID == "DBT")
                    {
                        if (ApplicationID == "SOM")
                        {
                            FMMemberFunction.FinancialYear(ddlFinancialYearID, 20, 20, "kkisanconstr");
                        }
                        else
                        {
                            FMMemberFunction.ThreeFinancialYears(ddlFinancialYearID, 20, 20,21, "kkisanconstr");
                        }
                        divAcceptance.Visible = false;
                    }
                    else
                    {
                        Response.Redirect("~/Dashboard.aspx");
                    }
                }
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
                if ((ddlFinancialYearID.SelectedValue != "") && (ddlFinancialYearID.SelectedValue != null) && (ApplicationID != "") && (DistrictID.ToString() != "") && (TalukID.ToString() != ""))
                {
                    BindFarmersList();
                }
                else
                {
                    lblNotMsg.Text = "Select above details !";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);

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

        public void BindFarmersList()
        {
            try
            {
                DBTManager dbtManager = new DBTManager();
                int finyear = ddlFinancialYearID.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlFinancialYearID.SelectedItem.Value) : 0;
                List<FarmerRegistration> lstFarmerRegistrations = new List<FarmerRegistration>();
                if (finyear>20)
                {
                    lstFarmerRegistrations = dbtManager.GetFarmerRegistration2122(DistrictID, TalukID, HobliID, "ALL", 2);
                }
                else
                {
                    lstFarmerRegistrations = dbtManager.GetFarmerRegistration(DistrictID, TalukID, HobliID, "ALL", 2);
                }
                //List<FarmerRegistration> lstFarmerRegistrations = dbtManager.GetFarmerRegistration(DistrictID, TalukID, HobliID, "ALL", 2);

                if (lstFarmerRegistrations != null && lstFarmerRegistrations.Count > 0)
                {
                    grdFarmerAcceptance.DataSource = lstFarmerRegistrations;
                    grdFarmerAcceptance.DataBind();
                    grdFarmerAcceptance.Visible = true;
                    divAcceptance.Visible = true;
                }
                else
                {
                    grdFarmerAcceptance.Visible = false;
                    divAcceptance.Visible = false;
                    lblNotMsg.Text = "No Farmer(s) found !";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SelectFarmersGrid(grdFarmerAcceptance, 10);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void grdFarmerAcceptance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int IndexValue = Convert.ToInt32(e.CommandArgument) % grdFarmerAcceptance.PageSize;
                if (e.CommandName == "REJECT")
                {

                    ClearViewStateVar();
                    ViewState["FarmerID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFarmerID")).Value.ToString().Trim();
                    ViewState["FinancialYearID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFinancialYearID")).Value.ToString().Trim();
                    ViewState["FinYearID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFinYearID")).Value.ToString().Trim();
                    ViewState["FinancialYearName"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFinancialYearID")).Value.ToString().Trim();
                    ViewState["FarmerNameEng"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFarmerName")).Value.ToString().Trim();
                    ViewState["ID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnID")).Value.ToString().Trim();


                    // Bind Modal Popup
                    ClearModalDetails();
                    lblFarmerID.Text = ViewState["FarmerID"].ToString();
                    lblFinancialYear.Text = ViewState["FinancialYearName"].ToString();
                    lblFarmerNameEng.Text = ViewState["FarmerNameEng"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
                else if (e.CommandName == "DETAILS")
                {
                    ViewState["FarmerID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFarmerID")).Value.ToString().Trim();
                    ViewState["FinancialYearID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFinancialYearID")).Value.ToString().Trim();
                    ViewState["ItemCategoryID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnItemCategoryID")).Value.ToString().Trim();
                    ViewState["ItemID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnItemID")).Value.ToString().Trim();

                    string[] param = { ViewState["FarmerID"].ToString().Trim(), ViewState["FinancialYearID"].ToString().Trim(), ApplicationID };
                    if (Utilities.CheckNull(param) == true)
                    {
                        if (ApplicationID == "FM" || ApplicationID == "AP" || ApplicationID == "SOM")
                        {
                            Response.Redirect("ViewApplication.aspx?AID=" + ApplicationID + "&FYID=" + ViewState["FinancialYearID"].ToString().Trim() + "&FID=" + ViewState["FarmerID"].ToString().Trim() + "&ItemCategoryID=" + ViewState["ItemCategoryID"].ToString().Trim() + "&ItemID=" + ViewState["ItemID"].ToString().Trim() + "", false);
                        }
                        else if (ApplicationID == "MI")
                        {
                            Response.Redirect("~/MI/ViewApplication.aspx?AID=" + ApplicationID + "&FYID=" + ViewState["FinancialYearID"].ToString().Trim() + "&FID=" + ViewState["FarmerID"].ToString().Trim() + "", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Dashboard.aspx");
                    }
                }
                else if (e.CommandName == "MODIFY")
                {
                    ViewState["FarmerID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFarmerID")).Value.ToString().Trim();
                    ViewState["FinancialYearID"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFinancialYearID")).Value.ToString().Trim();
                    ViewState["FinancialYearName"] = ((HiddenField)grdFarmerAcceptance.Rows[IndexValue].FindControl("hdnFinancialYearName")).Value.ToString().Trim();
                    if (ApplicationID == "FM" || ApplicationID == "AP")
                    {
                        Response.Redirect("ApplicationEntry.aspx?AID=" + ApplicationID + "&FYID=" + ViewState["FinancialYearID"].ToString().Trim() + "&FID=" + ViewState["FarmerID"].ToString().Trim() + "&AppFor=" + e.CommandName, false);
                    }
                    else if (ApplicationID == "MI")
                    {
                        Response.Redirect("~/MI/ApplicationEntry.aspx?AID=" + ApplicationID + "&FYID=" + ViewState["FinancialYearID"].ToString().Trim() + "&FID=" + ViewState["FarmerID"].ToString().Trim() + "&AppFor=" + e.CommandName, false);
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                DBTManager dbtManager = new DBTManager();
                int finyear = Convert.ToInt32(ViewState["FinYearID"]);
                int resRej = 0;
                if (finyear>20)
                {
                     resRej = dbtManager.RejectApplication2122(Convert.ToInt32(ViewState["ID"]), txtReason.Text.Trim());
                }
                else
                {
                     resRej = dbtManager.RejectApplication(Convert.ToInt32(ViewState["ID"]), txtReason.Text.Trim());
                }
                
                if (resRej >= 1)
                {
                    lblNotMsg.Text = "Farmer Rejected successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                    BindFarmersList();
                }
                else
                {
                    lblNotMsg.Text = "Farmer not Rejected for modification !";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }

        public void ClearViewStateVar()
        {
            try
            {
                ViewState["FarmerID"] = null;
                ViewState["FinancialYearID"] = null;
                ViewState["FinancialYearName"] = null;
                ViewState["FarmerNameEng"] = null;
                ViewState["ID"] = null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public void ClearModalDetails()
        {
            try
            {
                lblFarmerID.Text = "";
                lblFarmerNameEng.Text = "";
                lblFinancialYear.Text = "";
                lblMsg.Text = "";
                txtReason.Text = "";

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }



        protected void btnAcceptance_Click(object sender, EventArgs e)
        {
            try
            {
                int TotalFarmer = 0, SelectedFarmers = 0, AcceptedFarmers = 0;
                foreach (GridViewRow row in grdFarmerAcceptance.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox cbSelected = ((CheckBox)row.FindControl("cbSelect"));
                        HiddenField hdnId = ((HiddenField)row.FindControl("hdnID"));
                        HiddenField hdnFinancialYearID = ((HiddenField)row.FindControl("hdnFinYearID"));
                        int finyear = ConversionUtil.ConvertToInt(hdnFinancialYearID.Value);
                        
                        TotalFarmer++;

                        if (cbSelected.Checked == true)
                        {
                            SelectedFarmers++;
                            DBTManager dbtManager = new DBTManager();
                            int resStatus = 0;
                            if (finyear>20)
                            {
                                resStatus = dbtManager.UpdateApplicationStatus2122(ConversionUtil.ConvertToInt(hdnId.Value), 4);
                                dbtManager.UpdateApplicationStatusDBTAproved2122(ConversionUtil.ConvertToInt(hdnId.Value), 1, Session["UserID"].ToString());
                            }
                            else
                            {
                                resStatus = dbtManager.UpdateApplicationStatus(ConversionUtil.ConvertToInt(hdnId.Value), 4);
                                dbtManager.UpdateApplicationStatusDBTAproved(ConversionUtil.ConvertToInt(hdnId.Value), 1, Session["UserID"].ToString());
                            }
                            
                            
                            if (resStatus >= 1)
                            {
                                AcceptedFarmers++;
                            }
                        }
                    }
                }


                if (SelectedFarmers > 0)
                {
                    BindFarmersList();

                    Utilities.AlertHandling(lblNotMsg, AcceptedFarmers + " Farmer(s) Accepted successfully!");
                }
                else
                {
                    Utilities.AlertHandling(lblNotMsg, "No Farmer(s) selected.");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }


        protected void grdFarmerAcceptance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                BindFarmersList();
                grdFarmerAcceptance.PageIndex = e.NewPageIndex;
                grdFarmerAcceptance.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        //protected void imagebutton1_click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        byte[] imgrtc;
        //        string str = ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString();
        //        SqlConnection con = new SqlConnection(str);
        //        if (con.State == ConnectionState.Closed)
        //            con.Open();
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //        int index = gvRow.RowIndex;
        //        string Id = (((HiddenField)grdFarmerAcceptance.Rows[index].FindControl("hdnID")).Value.Trim());
        //        string st = "Select AuditId,FileName from Mobile.Audit where [ReferenceId]='" + Id + "'";
        //        SqlDataAdapter ad = new SqlDataAdapter(st, con);
        //        DataTable dt = new DataTable();
        //        ad.Fill(dt);

        //        if (dt.Rows.Count > 0)
        //        {


        //            string filePath = "C:\\AuditImages\\";
        //            string fileName = "inspection-report.jpeg";
        //            //string filepath = path.combine("https://kkisan.karnataka.gov.in/kkisanapi/auditimages/", dt.rows[0]["filename"].tostring());

        //            string file = filePath + fileName;
                    
        //            if (File.Exists(filePath+fileName))
        //            {

        //                imgrtc = File.ReadAllBytes(filePath + fileName);

        //                if (imgrtc != null)
        //                {
        //                    Session["img"] = imgrtc;

        //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "wwww();", true);
        //                    show_main();

        //                }
        //            }
        //        }
        //        else
        //        {
        //            //label121.text = "file not found";
        //            // label121.visible = true;
        //            // label121.forecolor = system.drawing.color.red;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            //string meth = System.reflection.methodbase.getcurrentmethod().name;
        //            //string errmsg = "";
        //            //errmsg = ex.tostring();
        //        }
        //        catch
        //        {
        //            Response.Redirect("~/login.aspx");
        //        }
        //    }
        //    finally
        //    {
        //        //if (con != null) con.close();
        //    }
        //}


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                string str = ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString();
                SqlConnection con = new SqlConnection(str);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                string Id = (((HiddenField)grdFarmerAcceptance.Rows[index].FindControl("hdnID")).Value.Trim());
                string FinID = (((HiddenField)grdFarmerAcceptance.Rows[index].FindControl("hdnFinYearID")).Value.Trim());
                string st = "Select AuditId,FileName from Mobile.Audit where [ReferenceId]='" + Id + "'and FinancialYearID='" + FinID + "'";
                SqlDataAdapter ad = new SqlDataAdapter(st, con);
                DataTable dt = new DataTable();

                ad.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    string fileName = dt.Rows[0]["FileName"].ToString();
                    //string filePath = Path.Combine("https://kkisan.karnataka.gov.in/kkisanapi/AuditImages?ImageName=", dt.Rows[0]["FileName"].ToString());
                    string filePath = "F:\\AuditTest\\" + dt.Rows[0]["FileName"].ToString();


                    show_main(filePath);
                }
                else
                {
                    //Label121.Text = "File not found";
                    //Label121.Visible = true;
                    //Label121.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    string meth = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    string errmsg = "";
                    errmsg = ex.ToString();
                }
                catch
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            finally
            {
                //if (con != null) con.Close();
            }
        }
        
        public void show_main(string filePath)
        {

            string queryString = filePath;
            string newWin = "window.open('" + queryString + "','','width=800%,height=800%,left=200,scrollbars=yes');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        }




    }
}