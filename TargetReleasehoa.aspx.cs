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
    public partial class TargetReleasehoa : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["kkisanconstr"].ConnectionString);
        string ApplicationID = "";
        int DistrictID, TalukID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ApplicationID = Request.QueryString["AID"] != null ? Request.QueryString["AID"] : "";
                DistrictID = Convert.ToInt32(Session["DistrictID"]);
                TalukID = Convert.ToInt32(Session["TalukID"]);

                if (IsPostBack == false)
                {
                    if (ApplicationID == "FM" || ApplicationID == "AP" || ApplicationID == "DBT" || ApplicationID == "MI" || ApplicationID == "SD" || ApplicationID == "PPE" || ApplicationID == "PS" || ApplicationID == "MN" || ApplicationID == "KBY" || ApplicationID == "SOM")
                    {
                        //Check
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
                                FMMemberFunction.FinancialYear(ddlFinancialYear, 19, 20, "kkisanconstr");
                            }



                        }
                        CommonDropdown.MasterSectorZP(ddlSector, "E", "kkisanconstr");
                        Common.BindDropdownByRole(ddlDistrict, DistrictID, Convert.ToInt32(Session["RoleID"]));
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
                //koteppa added update function --->
                if (btnGo.Text == "Update")

                {
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlDistrict.SelectedValue, "0", "0", txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "Update" };
                    int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease]", par, parval, "kkisanconstr");
                    if (res >= 1)
                    {
                        Clear();

                        lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

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
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlDistrict.SelectedValue, "0", "0", txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "insert" };
                    int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease]", par, parval, "kkisanconstr");
                    if (res >= 1)
                    {
                        Clear();

                        lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

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
                BindSchemeReleaseTransactionList();
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
                //Common.SectorwiseScheme(ddlScheme, ddlSector.SelectedValue, ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), "E", "kkisanconstr");
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
                if (ddlFinancialYear.SelectedValue != "0" && ddlSector.SelectedValue != "0" && ddlScheme.SelectedValue != "0" && ddlDistrict.SelectedValue != "0")
                {
                    DataTable dt = new DataTable();
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, "0", "0", Session["RoleID"].ToString(), "E" };
                    dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList]", par, parval, "kkisanconstr");
                    if (dt.Rows.Count >= 1)
                    {
                        Common.GridBind(grdReleasedList, dt);
                        divReleasedList.Visible = true;
                    }
                    else
                    {
                        divReleasedList.Visible = true;
                        grdReleasedList.DataSource = null;
                        grdReleasedList.DataBind();
                        //lblNotMsg.Text = "No Amount released transaction(s) found";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                    }
                }
                else
                {
                    //divReleasedList.Visible = false;
                    DataTable dt = new DataTable();
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@DistrictID", "@SchemeID", "@RoleID", "@LangType" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), "0", ddlScheme.SelectedValue.ToString().Trim(), Session["RoleID"].ToString(), "E" };
                    dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionListGeneral]", par, parval, "kkisanconstr");
                    if (dt.Rows.Count >= 1)
                    {
                        Common.GridBind(grdReleasedList, dt);
                        divReleasedList.Visible = true;
                    }
                    else
                    {
                        divReleasedList.Visible = true;
                        grdReleasedList.DataSource = null;
                        grdReleasedList.DataBind();
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
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Clear();
                BindSchemeReleaseTransactionList();
                lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblPhysicalTarget.Text = ddlDistrict.SelectedItem.Text;
                IsTargetsExists();
            }
            catch (Exception ex)
            {

            }
        }

        public void IsTargetsExists()
        {
            try
            {
                DataTable dt = new DataTable();
                string[] par = { "@ApplicationID", "@FinancialYearID", "@SchemeID", "@DistrictID", "@TalukID", "@RoleID", "@Method" };
                string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, "0", Session["RoleID"].ToString(), "IsExist" };
                dt = QueryExecution.DataAdapt("SP", "[DBT].[SP@SchemeTargetRelease]", par, parval, "kkisanconstr");
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
                Clear();

                BindSchemeReleaseTransactionList();
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

        protected void grdReleasedList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // int IndexValue = Convert.ToInt32(e.CommandArgument);

                int IndexValue = Convert.ToInt32(e.CommandArgument) % grdReleasedList.PageSize;

                if (e.CommandName == "DELETE_")
                {
                    ClearDeleteViewstate();
                    ViewState["hdnIdentity1"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnIdentity1")).Value.ToString().Trim();
                    ViewState["hdnApplicationID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnApplicationID")).Value.ToString().Trim();
                    ViewState["hdnFinancialYearID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnFinancialYearID")).Value.ToString().Trim();
                    ViewState["hdnSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnSchemeID")).Value.ToString().Trim();
                    ViewState["hdnDistrictID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnDistrictID")).Value.ToString().Trim();
                    ViewState["hdnTalukID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTalukID")).Value.ToString().Trim();
                    // ViewState["hdnQuarterID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnQuarterID")).Value.ToString().Trim();
                    ViewState["hdnReleasedBy"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnReleasedBy")).Value.ToString().Trim();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDeleteNotModal();", true);


                }
                //koteppa added Edit function --->
                if (e.CommandName == "Edit_")
                {

                    Clear();
                    BindSchemeReleaseTransactionList();
                    lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    txtPhysicalTarget.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    txtFinancialTarget.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    txtOrderNo.Text= Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    txtDescription.Text= Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                    IsTargetsExists();
                }
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
                    lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
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
    }
}
