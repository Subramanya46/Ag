using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Configuration;
using System.Web.Security;
using System.Data;
using System.Web.UI.HtmlControls;

namespace Kisan.Demonstration
{
    public partial class TargetReleaseada : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["kkisanconstr"].ConnectionString);
        string ApplicationID = "";
        int DistrictID, TalukID, HobliID;
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
                    if (ApplicationID == "FM" || ApplicationID == "AP" || ApplicationID == "DBT" || ApplicationID == "MI" || ApplicationID == "SD" || ApplicationID == "PPE" || ApplicationID == "PS" || ApplicationID == "MN" || ApplicationID == "KBY" || ApplicationID == "SOM")
                    {
                        Common.ApplicationTypeLabel(lblApplicationName, ApplicationID, "kkisanconstr");
                        if (ApplicationID == "SOM")
                        {
                            FMMemberFunction.FinancialYear(ddlFinancialYear, 20, 20, "kkisanconstr");
                        }
                        else
                        {
                            //CommonDropdown.FinancialYear(ddlFinancialYear, 18, 18, "kkisanconstr");

                            if (ApplicationID == "DBT")
                            {
                                FMMemberFunction.FinancialYearSEED(ddlFinancialYear, 20, 20, "kkisanconstr");
                            }
                            else
                            {
                                FMMemberFunction.FinancialYear(ddlFinancialYear, 18, 19, 20, "kkisanconstr");
                            }


                        }
                        CommonDropdown.MasterSectorZP(ddlSector, "E", "kkisanconstr");
                        Common.BindDropdownByRoleHobli(ddlDistrict, ddlTaluk, ddlHobli, DistrictID, TalukID, HobliID, "", Convert.ToInt32(Session["RoleID"]));
                        divReleasedList.Visible = false;
                    }
                    else
                    {
                        Response.Redirect("~/Dashboard.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", System.IO.Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
             {

                if (Page.IsValid == true && ValidateForm() == 1)
                {


                    String HOAAmount = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    String JDAAmount = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(ddlHobli.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    String Release = txtFinancialTarget.Text.Trim();


                    //string NewFiancial = txtaddfinacialtaget.Text.Trim();

                    Double HOAAmount_New = Convert.ToDouble(HOAAmount);
                    Double JDAAmount_New = Convert.ToDouble(JDAAmount);
                    Double Release_New = Convert.ToDouble(Release);

                    //Double NewFiancial_New = Convert.ToDouble(NewFiancial);

                    //koteppa added update function --->
                    if (btnGo.Text == "Update")

                    {

                       
                            string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                            string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, ddlHobli.SelectedValue, txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "Update" };
                            int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease]", par, parval, "kkisanconstr");
                            if (res >= 1)
                            {
                                Clear();

                                lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                                lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(ddlHobli.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                                //lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                                txtFinancialTarget.Text = "";
                                txtOrderNo.Text = "";
                                txtDescription.Text = "";

                                lblNotMsg.Text = "Targets Updated seccussfully !";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                BindSchemeReleaseTransactionList();
                            }
                            else
                            {
                                lblNotMsg.Text = "Targets not Updated / failed. Check your details ! ";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                            }
                        


                    }
                    //koteppa added update function --->

                    else
                    { 
                    
                        if (HOAAmount_New >= (JDAAmount_New + Release_New))
                    {
                        string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                        string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, ddlHobli.SelectedValue, txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "insert" };
                        int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease]", par, parval, "kkisanconstr");
                        if (res >= 1)
                        {
                            Clear();

                            lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(ddlHobli.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            //lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                            txtFinancialTarget.Text = "";
                            txtOrderNo.Text = "";
                            txtDescription.Text = "";

                            lblNotMsg.Text = "Targets saved seccussfully !";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                            BindSchemeReleaseTransactionList();
                        }
                        else
                        {
                            lblNotMsg.Text = "Targets not saved / failed. Check your details ! ";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                        }
                    }

                    else
                    {
                        lblNotMsg.Text = " ADA Targets Release Amount Should be less than JDA Target Release Amount ! ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                        BindSchemeReleaseTransactionList();
                    }
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }


        public int ValidateForm()
        {
            try
            {

                int valid = 0;
                
                if (Convert.ToDouble(txtFinancialTarget.Text == "" ? "0" : txtFinancialTarget.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount to be Release can not be zero !');", true);
                    valid = 0;
                }
                else
                {
                    valid = 1;
                }

                if (valid == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
        }

        public bool isReleaseOverflow()
        {
            try
            {
                DataTable dt = new DataTable();
                string[] par = { "@Method", "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@PhysicalTarget", "@FinancialTarget", "@OrderNo", "@ReleasedBy", "@Description" };
                string[] parval = { "isReleaseOverflow", ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, "0", txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), txtOrderNo.Text.Trim(), Session["RoleID"].ToString(), txtDescription.Text.Trim() };
                dt = QueryExecution.DataAdapt("SP", "[DBT].[SP@SchemeTargetRelease]", par, parval, "kkisanconstr");
                if (dt.Rows[0]["Exist"].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Dashboard.aspx");
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", System.IO.Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblHOA.Text = "HOA : " + Common.SchemeHOAName(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), "kkisanconstr");

                if (ddlDistrict.SelectedValue != "" && ddlDistrict.SelectedValue != "0" && ddlScheme.SelectedValue != "0")
                {
                    lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                }

                // BindSchemeReleaseTransactionList();
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSectorwiseScheme();

                // Common.SectorwiseScheme(ddlScheme, ddlSector.SelectedValue, ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), "E", "kkisanconstr");
                //Common.SectorwiseSchemeNEW(ddlScheme, ddlSector.SelectedValue, ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue),"RC1", "E", "kkisanconstr");

                lblHOA.Text = "";
                Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindSectorwiseScheme()
        {
            try
            {
                string str = ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString();
                SqlConnection con = new SqlConnection(str);
                string com = "select SchemeID as ValueField,SchemeNameEng as ItemField from Master.MasterSectorScheme where ApplicationID='" + ApplicationID + "' and FinancialYearID='" + Convert.ToInt32(ddlFinancialYear.SelectedValue) + "' and SectorID='" + ddlSector.SelectedValue + "'";

                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddlScheme.DataSource = dt;
                ddlScheme.DataBind();
                ddlScheme.DataTextField = "ItemField";
                ddlScheme.DataValueField = "ValueField";
                ddlScheme.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlScheme.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public void BindSchemeReleaseTransactionList()
        {
            try
            {
                if (ddlFinancialYear.SelectedValue != "0" && ddlSector.SelectedValue != "0" && ddlScheme.SelectedValue != "0" && ddlDistrict.SelectedValue != "0" && ddlTaluk.SelectedValue != "0" && ddlHobli.SelectedValue != "0")
                {
                    DataTable dt = new DataTable();
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, ddlHobli.SelectedValue, Session["RoleID"].ToString(), "E" };
                    dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList]", par, parval, "kkisanconstr");
                    if (dt.Rows.Count >= 1)
                    {
                        Common.GridBind(grdReleasedList, dt);
                        divReleasedList.Visible = true;
                    }
                    else
                    {
                        divReleasedList.Visible = false;
                        //lblNotMsg.Text = "No Amount released transaction(s) found";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                    }
                }
                else
                {

                    DataTable dt = new DataTable();
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@DistrictID", "@SchemeID", "@RoleID", "@LangType" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, ddlScheme.SelectedValue.ToString().Trim(), Session["RoleID"].ToString(), "E" };
                    dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionListGeneral]", par, parval, "kkisanconstr");
                    if (dt.Rows.Count >= 1)
                    {
                        Common.GridBind(grdReleasedList, dt);
                        divReleasedList.Visible = true;
                    }
                    else
                    {
                        divReleasedList.Visible = false;
                        //lblNotMsg.Text = "No Amount released transaction(s) found";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        protected void ddlTaluk_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void IsTargetsExists()
        {
            try
            {
                DataTable dt = new DataTable();
                string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@HobliID", "@RoleID" };
                string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, ddlHobli.SelectedValue, Session["RoleID"].ToString() };
                dt = QueryExecution.DataAdapt("SP", "[DBT].[SP@IsExistSchemeTargetRelease]", par, parval, "kkisanconstr");
                if (dt.Rows.Count == 1)
                {
                    Clear();
                    txtOrderNo.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    btnGo.Text = "Update";
                    txtPhysicalTarget.Text = dt.Rows[0]["PhysicalTarget"].ToString().Trim();
                    txtFinancialTarget.Text = dt.Rows[0]["FinancialTarget"].ToString().Trim();
                    txtOrderNo.Text = dt.Rows[0]["OrderNo"].ToString().Trim();
                    txtDescription.Text = dt.Rows[0]["Description"].ToString().Trim();
                    ViewState["rowupdateID"] = dt.Rows[0]["ID"].ToString().Trim();
                    ViewState["PhysicalTargetold"] = dt.Rows[0]["PhysicalTarget"].ToString().Trim();
                    ViewState["FinancialTargetold"] = dt.Rows[0]["FinancialTarget"].ToString().Trim();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }


        public void Clear()
        {
            try
            {
                txtOrderNo.ReadOnly = false;
                txtDescription.ReadOnly = false;
                btnGo.Text = "GO";
                txtPhysicalTarget.Text = "";
                txtFinancialTarget.Text = "";
                txtOrderNo.Text = "";
                txtDescription.Text = "";
                ViewState["rowupdateID"] = "";
                ViewState["PhysicalTargetold"] = "";
                ViewState["FinancialTargetold"] = "";

            }
            catch (Exception ex)
            {

            }
        }

        protected void grdReleasedList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                BindSchemeReleaseTransactionList();
                grdReleasedList.PageIndex = e.NewPageIndex;
                grdReleasedList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlSector.ClearSelection();
                ddlScheme.Items.Clear();
                lblHOA.Text = "";
                ddlDistrict.ClearSelection();
                lblDrawingOfficeCode.Text = "";
                lblSubTreasuryCode.Text = "";
                ddlTaluk.ClearSelection();
                Clear();

                BindSchemeReleaseTransactionList();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtOrderNo.ReadOnly = false;
                txtDescription.ReadOnly = false;
                Clear();
            }
            catch (Exception ex)
            {

            }
        }

        protected void grdReleasedList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
               
                int IndexValue = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "DELETE_")
                {
                    ClearDeleteViewstate();
                    ViewState["hdnIdentity1"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnIdentity1")).Value.ToString().Trim();
                    ViewState["hdnApplicationID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnApplicationID")).Value.ToString().Trim();
                    ViewState["hdnFinancialYearID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnFinancialYearID")).Value.ToString().Trim();
                    ViewState["hdnSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnSchemeID")).Value.ToString().Trim();
                    ViewState["hdnDistrictID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnDistrictID")).Value.ToString().Trim();
                    ViewState["hdnTalukID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTalukID")).Value.ToString().Trim();
                    ViewState["hdnReleasedBy"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnReleasedBy")).Value.ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDeleteNotModal();", true);
                }
                //koteppa added Edit function --->
                else if (e.CommandName == "Edit_")
                {

                    Clear();
                    //txtaddfinacialtaget.Enabled = true;
                    //txtFinancialTarget.ReadOnly = true;
                    //lbladdfinacialtarget.Visible = true;
                    //btnGo.Style.Add("display", "block");
                    //btnGo.Visible = true;


                    lblSubTreasuryCode.Text = " Sub Treasury Cd : " + Common.SubTreasuryCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                    lblDrawingOfficeCode.Text = "Drawing Office Cd: " + Common.DrawingOfficeCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                    lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(ddlHobli.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    // lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    lblPhysicalTarget.Text = ddlHobli.SelectedItem.Text;
                 

                    BindSchemeReleaseTransactionList();
                    IsTargetsExists();

                }
                //koteppa added update function --->

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                string[] par = { "@Method", "@ID", "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@ReleasedBy" };
                string[] parval = { "DELETE",ViewState["hdnIdentity1"].ToString().Trim(), ViewState["hdnApplicationID"].ToString().Trim(), ViewState["hdnFinancialYearID"].ToString().Trim(),
            ViewState["hdnSchemeID"].ToString().Trim(), ViewState["hdnDistrictID"].ToString().Trim(), ViewState["hdnTalukID"].ToString().Trim(),
            ViewState["hdnReleasedBy"].ToString().Trim() };
                int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease]", par, parval, "kkisanconstr");
                if (res >= 1)
                {
                    Clear();
                    ClearDeleteViewstate();
                    lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(ddlHobli.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    // lblBalAmount.Text = Common.TotalSchemeTargetAvailability(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");


                    //  lblNotMsg.Text = "Record deleted successfully !";
                    Response.Write("<script>alert('Record deleted successfully')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    BindSchemeReleaseTransactionList();

                }
                else
                {
                    //lblNotMsg.Text = "Record not deleted !";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    Response.Write("<script>alert('Record not deleted !')</script>");
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void ddlHobli_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Clear();
                //txtaddfinacialtaget.Enabled = false;
                //lbladdfinacialtarget.Visible = false;
                //btnGo.Style.Add("display","none");
                //btnGo.Visible = false;

                lblSubTreasuryCode.Text = " Sub Treasury Cd : " + Common.SubTreasuryCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                lblDrawingOfficeCode.Text = "Drawing Office Cd: " + Common.DrawingOfficeCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(ddlHobli.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                // lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblPhysicalTarget.Text = ddlHobli.SelectedItem.Text;

                BindSchemeReleaseTransactionList();
                IsTargetsExists();

            }
            catch (Exception ex)
            {

            }
        }

        public void ClearDeleteViewstate()
        {
            try
            {
                ViewState["hdnIdentity1"] = "";
                ViewState["hdnApplicationID"] = "";
                ViewState["hdnFinancialYearID"] = "";
                ViewState["hdnSchemeID"] = "";
                ViewState["hdnDistrictID"] = "";
                ViewState["hdnTalukID"] = "";
                ViewState["hdnQuarterID"] = "";
                ViewState["hdnReleasedBy"] = "";
            }
            catch (Exception ex)
            {

            }
        }


        protected void ddlQuarterID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSchemeReleaseTransactionList();
            }
            catch (Exception ex)
            {

            }
        }

    }
}

