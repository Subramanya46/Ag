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
using KKISAN.Services;
using KKISAN.Model.DBT;

namespace Kisan.Demonstration
{
    public partial class TargetReleaseSchemeWisehoa : System.Web.UI.Page
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
                                FMMemberFunction.FinancialYearSEED(ddlFinancialYear, 21, 21, "kkisanconstr");
                            }
                            else
                            {
                                FMMemberFunction.FinancialYear(ddlFinancialYear, 19, 20, "kkisanconstr");
                            }
                        }
                        BindMainScheme();
                        //  BindSubScheme();
                        //BindCategory();
                        //  CommonDropdown.MasterSectorZP(ddlSector, "E", "kkisanconstr");
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
                if (btnGo.Text == "Update GEN" || btnGo.Text == "Update SCP" || btnGo.Text == "Update TSP")
                {
                    string amount = "";
                    string hectare = "";
                    if (btnGo.Text == "Update GEN")
                    {
                        amount = txtFinancialTarget.Text.Trim();
                        hectare = txtPhysicalTarget.Text.Trim();
                    }
                    if (btnGo.Text == "Update SCP")
                    {
                        amount = txtFinancialTarget1.Text.Trim();
                        hectare = txtPhysicalTarget1.Text.Trim();
                    }
                    if (btnGo.Text == "Update TSP")
                    {
                        amount = txtFinancialTarget2.Text.Trim();
                        hectare = txtPhysicalTarget2.Text.Trim();
                    }
                    String JDAAmount = Common.TotalTargetReleaseAmountDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"].ToString()), Convert.ToInt32(ViewState["hdnMainSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnCropID"].ToString()), Convert.ToInt32(ViewState["hdnTechnologyID"].ToString()), Convert.ToInt32(ViewState["hdnCategoryID"].ToString()), Convert.ToInt32(ViewState["hdnDistrictID"].ToString()), 0, 0, 5, "kkisanconstr");
                    String JDAPhysical = Common.TotalTargetReleasePhysicalDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"].ToString()), Convert.ToInt32(ViewState["hdnMainSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnCropID"].ToString()), Convert.ToInt32(ViewState["hdnTechnologyID"].ToString()), Convert.ToInt32(ViewState["hdnCategoryID"].ToString()), Convert.ToInt32(ViewState["hdnDistrictID"].ToString()), 0, 0, 5, "kkisanconstr");

                    //String JDAAmount = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ViewState["hdnMainSchemeID"]), Convert.ToInt32(ViewState["hdnSchemeID"]), Convert.ToInt32(ViewState["hdnCropID"]), Convert.ToInt32(ViewState["hdnTechnologyID"]), Convert.ToInt32(ViewState["hdnCategoryID"]), Convert.ToInt32(ViewState["hdnDistrictID"]), 0, 0, 5, "kkisanconstr");
                    Double JDAAmount_New = Convert.ToDouble(JDAAmount);
                    Double JDAPhysical_New = Convert.ToDouble(JDAPhysical);
                    Double amountnew = Convert.ToDouble(amount);
                    Double hectarenew = Convert.ToDouble(hectare);

                    if (JDAAmount_New > amountnew || JDAPhysical_New > hectarenew)
                    {
                        lblNotMsg.Text = "The JDA already recieved your target and assigned interns to ADA. And you are trying to edit the Target which is less than those targets. JDA Physical Target Released is "+ JDAPhysical + " and JDA Financial Target Released is "+ JDAAmount + ". Please Try editing with values which are greater than jda releases! ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);

                    }
                    else
                    {
                        string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                        string[] parval = { ViewState["hdnApplicationID"].ToString(), ViewState["hdnFinancialYearID"].ToString(), ViewState["hdnMainSchemeID"].ToString(), ViewState["hdnSchemeID"].ToString(), ViewState["hdnCropID"].ToString(), ViewState["hdnTechnologyID"].ToString(), ViewState["hdnCategoryID"].ToString(), ViewState["hdnDistrictID"].ToString(), "0", "0", hectare, amount, Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "Update" };
                        int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                        if (res >= 1)
                        {
                            Clear();
                            BindSchemeReleaseTransactionList();
                            lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            lblTotalPhysicalTargetReleased.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            lblTotalReleasedAmount1.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            lblTotalPhysicalTargetReleased1.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            lblTotalReleasedAmount2.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            lblTotalPhysicalTargetReleased2.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                            IsTargetsExists();
                            lblNotMsg.Text = "Targets Updated seccussfully !";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                        }
                        else
                        {
                            lblNotMsg.Text = "Targets not Updated / failed. Check your details ! ";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                        }

                    }


                }
                //koteppa added update function --->
                else
                {
                    int val = 0;
                    int res = 0;
                    if (txtPhysicalTarget.Text != "" && txtFinancialTarget.Text != "")
                    {
                        string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                        string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory.SelectedValue, ddlDistrict.SelectedValue, "0", "0", txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "INSERT" };
                        res = res + QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                        val++;
                    }
                    if (txtPhysicalTarget1.Text != "" && txtFinancialTarget1.Text != "")
                    {
                        string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                        string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory1.SelectedValue, ddlDistrict.SelectedValue, "0", "0", txtPhysicalTarget1.Text.Trim(), txtFinancialTarget1.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "INSERT" };
                        res = res + QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                        val++;
                    }
                    if (txtPhysicalTarget2.Text != "" && txtFinancialTarget2.Text != "")
                    {
                        string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                        string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory2.SelectedValue, ddlDistrict.SelectedValue, "0", "0", txtPhysicalTarget2.Text.Trim(), txtFinancialTarget2.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "INSERT" };
                        res = res + QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                        val++;
                    }

                    if (val == 0)
                    {
                        lblNotMsg.Text = "Atleast anyone of the targets should be filled ! ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                    }
                    else if (res >= 1)
                    {
                        Clear();
                        BindSchemeReleaseTransactionList();
                        lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                        lblTotalPhysicalTargetReleased.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                        lblTotalReleasedAmount1.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                        lblTotalPhysicalTargetReleased1.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                        lblTotalReleasedAmount2.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                        lblTotalPhysicalTargetReleased2.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                        IsTargetsExists();
                        lblNotMsg.Text = "Targets saved seccussfully !";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);

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
                //lblHOA.Text = "HOA : " + Common.SchemeHOAName(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), "kkisanconstr");
                int schemeid= Convert.ToInt32(ddlScheme.SelectedValue);
                if (schemeid == 1)
                {
                    lblPhysicalTargettxt.InnerHtml = "Physical Target (Hectare) <span class='star_color'>*</span>";
                    lblPhysicalTargettxt1.InnerHtml = "Physical Target (Hectare) <span class='star_color'>*</span>";
                    lblPhysicalTargettxt2.InnerHtml = "Physical Target (Hectare) <span class='star_color'>*</span>";
                }
                else
                {
                    lblPhysicalTargettxt.InnerHtml = "Physical Target (Kit In Numbers) <span class='star_color'>*</span>";
                    lblPhysicalTargettxt1.InnerHtml = "Physical Target (Kit In Numbers) <span class='star_color'>*</span>";
                    lblPhysicalTargettxt2.InnerHtml = "Physical Target (Kit In Numbers) <span class='star_color'>*</span>";
                }
                BindSubScheme();

                BindSchemeReleaseTransactionList();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindTechnologies()
        {
            if (ddlScheme.SelectedItem.Value == "1")
            {
                ddlTechnology.Items.Clear();
                if ((ddlSubScheme.SelectedItem != null && ddlSubScheme.SelectedIndex > 0) && (ddlCrop.SelectedItem != null && ddlCrop.SelectedIndex > 0))
                {
                    DBTManager marketMgr = new DBTManager();
                    int SchemeId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value);
                    int CropId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlCrop.SelectedItem.Value);
                    List<TechnologyDetail> technologyDetails = marketMgr.GetTechnologyDetails(SchemeId, CropId);
                    if (technologyDetails != null && technologyDetails.Count > 0)
                    {
                        ddlTechnology.DataSource = technologyDetails;
                        ddlTechnology.DataTextField = "TechnologyName";
                        ddlTechnology.DataValueField = "TechnologyId";
                    }
                    else
                    {
                        ddlTechnology.DataSource = new List<TechnologyDetail>();
                    }
                }
                else
                {
                    ddlTechnology.DataSource = new List<TechnologyDetail>();
                }
                ddlTechnology.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlTechnology.Items.Insert(0, li);
            }
            else
            {
                ddlTechnology.Items.Clear();
                ListItem li = new ListItem("------Select------", "0");
                ddlTechnology.Items.Insert(0, li);
                ListItem li1 = new ListItem("Seed Mini Kit", "1000");
                ddlTechnology.Items.Insert(1, li1);
            }
        }

        private void BindCropDetails()
        {
            ddlCrop.Items.Clear();
            ddlTechnology.Items.Clear();
            if (ddlSubScheme.SelectedItem != null && ddlSubScheme.SelectedIndex > 0)
            {
                DBTManager marketMgr = new DBTManager();
                List<DBTCropDetail> CropDetails = marketMgr.GetCropDetails(KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value),KKISAN.Utils.ConversionUtil.ConvertInt(ddlScheme.SelectedItem.Value));
                if (CropDetails != null && CropDetails.Count > 0)
                {
                    ddlCrop.DataSource = CropDetails;
                    ddlCrop.DataTextField = "CropName";
                    ddlCrop.DataValueField = "CropId";
                }
                else
                {
                    ddlCrop.DataSource = new List<DBTCropDetail>();
                }
            }
            else
            {
                ddlCrop.DataSource = new List<DBTCropDetail>();
            }
            ddlCrop.DataBind();
            ListItem li = new ListItem("------Select------", "0");
            ddlCrop.Items.Insert(0, li);
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
                ListItem li = new ListItem("------Select------", "0");
                ddlScheme.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void BindCategory()
        {
            try
            {
                string str = ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString();
                SqlConnection con = new SqlConnection(str);
                string com = "select CategoryId as ValueField,CategoryName as ItemField from DBT.Category";

                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddlCategory.DataSource = dt;
                ddlCategory.DataBind();
                ddlCategory.DataTextField = "ItemField";
                ddlCategory.DataValueField = "ValueField";
                ddlCategory.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlCategory.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void BindSubScheme()
        {
            try
            {
                string str = ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString();
                SqlConnection con = new SqlConnection(str);
                string com = string.Empty;
                if (ddlScheme.SelectedValue == "1")
                {
                    com = "select SchemeId as ValueField,SchemeNameEng as ItemField from DBT.Scheme";
                }
                else
                {
                    com = "select SchemeId as ValueField,SchemeNameEng as ItemField from DBT.Scheme where SchemeId in(5,6)";
                }

                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddlSubScheme.DataSource = dt;
                ddlSubScheme.DataBind();
                ddlSubScheme.DataTextField = "ItemField";
                ddlSubScheme.DataValueField = "ValueField";
                ddlSubScheme.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlSubScheme.Items.Insert(0, li);
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
                if (ddlFinancialYear.SelectedValue != "0" && ddlScheme.SelectedValue != "0" && ddlSubScheme.SelectedValue != "0" && ddlCrop.SelectedValue != "0" && ddlTechnology.SelectedValue != "0" && ddlCategory.SelectedValue != "0" && ddlDistrict.SelectedValue != "0")
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "@TechnologyID", "@CategoryID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlSubScheme.SelectedValue.ToString().Trim(), ddlCrop.SelectedValue.ToString().Trim(), ddlTechnology.SelectedValue.ToString().Trim(), ddlCategory.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, "0", "0", Session["RoleID"].ToString(), "E" };
                    dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList2122]", par, parval, "kkisanconstr");
                    string[] par1 = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "@TechnologyID", "@CategoryID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval1 = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlSubScheme.SelectedValue.ToString().Trim(), ddlCrop.SelectedValue.ToString().Trim(), ddlTechnology.SelectedValue.ToString().Trim(), ddlCategory1.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, "0", "0", Session["RoleID"].ToString(), "E" };
                    dt1 = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList2122]", par1, parval1, "kkisanconstr");
                    string[] par2 = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "@TechnologyID", "@CategoryID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval2 = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue.ToString().Trim(), ddlSubScheme.SelectedValue.ToString().Trim(), ddlCrop.SelectedValue.ToString().Trim(), ddlTechnology.SelectedValue.ToString().Trim(), ddlCategory2.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, "0", "0", Session["RoleID"].ToString(), "E" };
                    dt2 = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList2122]", par2, parval2, "kkisanconstr");
                    dt.Merge(dt1);
                    dt.Merge(dt2);
                    if (dt != null && dt.Rows.Count >= 1)
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

                    divReleasedList.Visible = false;
                    //DataTable dt = new DataTable();
                    //string[] par = { "@ApplicationID", "@FinancialYearID", "@DistrictID", "@SchemeID", "@RoleID", "@LangType" };
                    //string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), "0", ddlScheme.SelectedValue.ToString().Trim(), Session["RoleID"].ToString(), "E" };
                    //dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionListGeneral]", par, parval, "kkisanconstr");
                    //if (dt.Rows.Count >= 1)
                    //{
                    //    Common.GridBind(grdReleasedList, dt);
                    //    divReleasedList.Visible = true;
                    //}
                    //else
                    //{
                    //    divReleasedList.Visible = true;
                    //    grdReleasedList.DataSource = null;
                    //    grdReleasedList.DataBind();
                    //    //lblNotMsg.Text = "No Amount released transaction(s) found";
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                    //}
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

                ClearAll();

                BindSchemeReleaseTransactionList();
                lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblTotalPhysicalTargetReleased.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblTotalReleasedAmount1.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblTotalPhysicalTargetReleased1.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblTotalReleasedAmount2.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblTotalPhysicalTargetReleased2.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblPhysicalTarget.Text = ddlDistrict.SelectedItem.Text;
                lblPhysicalTarget1.Text = ddlDistrict.SelectedItem.Text;
                lblPhysicalTarget2.Text = ddlDistrict.SelectedItem.Text;
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
                DBTManager dbt = new DBTManager();
                TargetRelese2122 target = dbt.GetTargetRelease(ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory.SelectedValue, ddlDistrict.SelectedValue, "0", "0", Session["RoleID"].ToString());
                TargetRelese2122 target1 = dbt.GetTargetRelease(ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory1.SelectedValue, ddlDistrict.SelectedValue, "0", "0", Session["RoleID"].ToString());
                TargetRelese2122 target2 = dbt.GetTargetRelease(ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory2.SelectedValue, ddlDistrict.SelectedValue, "0", "0", Session["RoleID"].ToString());
                int val = 0;
                if (target != null && target.PhysicalTarget != 0)
                {
                    Clear();
                    txtOrderNo.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    //btnGo.Text = "Update";
                    txtPhysicalTarget.Text = target.PhysicalTarget.ToString();
                    txtFinancialTarget.Text = target.FinancialTarget.ToString();
                    txtOrderNo.Text = target.OrderNo.ToString();
                    txtDescription.Text = target.Description.ToString();
                    val++;
                }
                if (target1 != null && target1.PhysicalTarget != 0)
                {
                    Clear1();
                    txtOrderNo.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    //btnGo.Text = "Update";
                    txtPhysicalTarget1.Text = target1.PhysicalTarget.ToString();
                    txtFinancialTarget1.Text = target1.FinancialTarget.ToString();
                    txtOrderNo.Text = target1.OrderNo.ToString();
                    txtDescription.Text = target1.Description.ToString();
                    val++;
                }
                if (target2 != null && target2.PhysicalTarget != 0)
                {
                    Clear2();
                    txtOrderNo.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    //btnGo.Text = "Update";
                    txtPhysicalTarget2.Text = target2.PhysicalTarget.ToString();
                    txtFinancialTarget2.Text = target2.FinancialTarget.ToString();
                    txtOrderNo.Text = target2.OrderNo.ToString();
                    txtDescription.Text = target2.Description.ToString();
                    val++;
                }
                if (val == 0)
                {
                    txtOrderNo.ReadOnly = false;
                    txtDescription.ReadOnly = false;
                }

                //DataTable dt = new DataTable();
                //string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "@TechnologyID", "@CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@ReleasedBy", "@Method" };
                //string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue,
                //    ddlCategory.SelectedValue, ddlDistrict.SelectedValue, "0", "0",  Session["RoleID"].ToString(), "IsExist" };
                //dt = QueryExecution.DataAdapt("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                //if (dt.Rows.Count == 1)
                //{
                //    Clear();
                //    txtOrderNo.ReadOnly = true;
                //    txtDescription.ReadOnly = true;
                //    btnGo.Text = "Update";
                //    txtPhysicalTarget.Text = dt.Rows[0]["PhysicalTarget"].ToString().Trim();
                //    txtFinancialTarget.Text = dt.Rows[0]["FinancialTarget"].ToString().Trim();
                //    txtOrderNo.Text = dt.Rows[0]["OrderNo"].ToString().Trim();
                //    txtDescription.Text = dt.Rows[0]["Description"].ToString().Trim();
                //}
                //else
                //{

                //}
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
        public void Clear1()
        {
            try
            {
                txtOrderNo.ReadOnly = false;
                txtDescription.ReadOnly = false;
                btnGo.Text = "GO";
                txtPhysicalTarget1.Text = "";
                txtFinancialTarget1.Text = "";
                txtOrderNo.Text = "";
                txtDescription.Text = "";

            }
            catch (Exception ex)
            {

            }
        }
        public void CompleteClear()
        {
            try
            {
                lblTotalPhysicalTargetReleased.Text = "";
                lblTotalReleasedAmount.Text = "";
                lblTotalPhysicalTargetReleased.Text = "";
                lblTotalReleasedAmount.Text = "";
                lblTotalPhysicalTargetReleased.Text = "";
                lblTotalReleasedAmount.Text = "";
                txtPhysicalTarget.Text = "";
                txtFinancialTarget.Text = "";
                txtPhysicalTarget1.Text = "";
                txtFinancialTarget1.Text = "";
                txtPhysicalTarget2.Text = "";
                txtFinancialTarget2.Text = "";

            }
            catch (Exception ex)
            {

            }
        }
        public void Clear2()
        {
            try
            {
                txtOrderNo.ReadOnly = false;
                txtDescription.ReadOnly = false;
                btnGo.Text = "GO";
                txtPhysicalTarget2.Text = "";
                txtFinancialTarget2.Text = "";
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
                    ViewState["hdnMainSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnMainSchemeID")).Value.ToString().Trim();
                    ViewState["hdnSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnSchemeID")).Value.ToString().Trim();
                    ViewState["hdnCropID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnCropID")).Value.ToString().Trim();
                    ViewState["hdnTechnologyID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTechnologyID")).Value.ToString().Trim();
                    ViewState["hdnCategoryID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnCategoryID")).Value.ToString().Trim();
                    ViewState["hdnDistrictID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnDistrictID")).Value.ToString().Trim();
                    ViewState["hdnTalukID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTalukID")).Value.ToString().Trim();
                    // ViewState["hdnQuarterID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnQuarterID")).Value.ToString().Trim();
                    ViewState["hdnReleasedBy"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnReleasedBy")).Value.ToString().Trim();
                    String JDAAmount = Common.TotalTargetReleaseAmountDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"]), Convert.ToInt32(ViewState["hdnMainSchemeID"]), Convert.ToInt32(ViewState["hdnSchemeID"]), Convert.ToInt32(ViewState["hdnCropID"]), Convert.ToInt32(ViewState["hdnTechnologyID"]), Convert.ToInt32(ViewState["hdnCategoryID"]), Convert.ToInt32(ViewState["hdnDistrictID"]), 0, 0, 5, "kkisanconstr");
                    Double JDAAmount_New = Convert.ToDouble(JDAAmount);
                    if (JDAAmount_New > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDeleteCantModal();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDeleteNotModal();", true);
                    }
                    Clear();
                    Clear1();
                    Clear2();
                    IsTargetsExists();

                }
                //koteppa added Edit function --->
                if (e.CommandName == "Edit_")
                {
                    ClearDeleteViewstate();
                    ViewState["hdnIdentity1"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnIdentity1")).Value.ToString().Trim();
                    ViewState["hdnApplicationID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnApplicationID")).Value.ToString().Trim();
                    ViewState["hdnFinancialYearID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnFinancialYearID")).Value.ToString().Trim();
                    ViewState["hdnMainSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnMainSchemeID")).Value.ToString().Trim();
                    ViewState["hdnSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnSchemeID")).Value.ToString().Trim();
                    ViewState["hdnCropID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnCropID")).Value.ToString().Trim();
                    ViewState["hdnTechnologyID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTechnologyID")).Value.ToString().Trim();
                    ViewState["hdnCategoryID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnCategoryID")).Value.ToString().Trim();
                    ViewState["hdnDistrictID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnDistrictID")).Value.ToString().Trim();
                    ViewState["hdnTalukID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTalukID")).Value.ToString().Trim();
                    // ViewState["hdnQuarterID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnQuarterID")).Value.ToString().Trim();
                    ViewState["hdnReleasedBy"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnReleasedBy")).Value.ToString().Trim();
                    DBTManager dbt = new DBTManager();
                    TargetRelese2122 target = dbt.GetTargetRelease(ViewState["hdnApplicationID"].ToString(), ViewState["hdnFinancialYearID"].ToString(), ViewState["hdnMainSchemeID"].ToString(), ViewState["hdnSchemeID"].ToString(), ViewState["hdnCropID"].ToString(), ViewState["hdnTechnologyID"].ToString(), ViewState["hdnCategoryID"].ToString(), ViewState["hdnDistrictID"].ToString(), "0", "0", Session["RoleID"].ToString());
                    //String JDAAmount = Common.TotalTargetReleaseAmountDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"]), Convert.ToInt32(ViewState["hdnMainSchemeID"]), Convert.ToInt32(ViewState["hdnSchemeID"]), Convert.ToInt32(ViewState["hdnCropID"]), Convert.ToInt32(ViewState["hdnTechnologyID"]), Convert.ToInt32(ViewState["hdnCategoryID"]), Convert.ToInt32(ViewState["hdnDistrictID"]), 0, 0, 5, "kkisanconstr");
                    // JDAAmount_New = Convert.ToDouble(JDAAmount);
                    if (Convert.ToInt32(ViewState["hdnCategoryID"]) == 1)
                    {
                        ClearAll();
                        txtOrderNo.ReadOnly = true;
                        txtDescription.ReadOnly = true;
                        btnGo.Text = "Update GEN";
                        txtPhysicalTarget.Text = target.PhysicalTarget.ToString();
                        txtFinancialTarget.Text = target.FinancialTarget.ToString();
                        txtOrderNo.Text = target.OrderNo.ToString();
                        txtDescription.Text = target.Description.ToString();
                    }
                    if (Convert.ToInt32(ViewState["hdnCategoryID"]) == 2)
                    {
                        ClearAll();
                        txtOrderNo.ReadOnly = true;
                        txtDescription.ReadOnly = true;
                        btnGo.Text = "Update SCP";
                        txtPhysicalTarget1.Text = target.PhysicalTarget.ToString();
                        txtFinancialTarget1.Text = target.FinancialTarget.ToString();
                        txtOrderNo.Text = target.OrderNo.ToString();
                        txtDescription.Text = target.Description.ToString();
                    }
                    if (Convert.ToInt32(ViewState["hdnCategoryID"]) == 3)
                    {
                        ClearAll();
                        txtOrderNo.ReadOnly = true;
                        txtDescription.ReadOnly = true;
                        btnGo.Text = "Update TSP";
                        txtPhysicalTarget2.Text = target.PhysicalTarget.ToString();
                        txtFinancialTarget2.Text = target.FinancialTarget.ToString();
                        txtOrderNo.Text = target.OrderNo.ToString();
                        txtDescription.Text = target.Description.ToString();
                    }


                    //Clear();
                    //BindSchemeReleaseTransactionList();
                    //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    // txtPhysicalTarget.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    // txtFinancialTarget.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    // txtOrderNo.Text= Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    // txtDescription.Text= Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                    //IsTargetsExists();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public void ClearAll()
        {
            try
            {
                txtPhysicalTarget.Text = "";
                txtFinancialTarget.Text = "";
                txtPhysicalTarget1.Text = "";
                txtFinancialTarget1.Text = "";
                txtPhysicalTarget2.Text = "";
                txtFinancialTarget2.Text = "";

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                string[] par = { "@Method", "@ID", "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "@TechnologyID", "@CategoryID", "@DistrictID", "@TalukID", "@ReleasedBy" };
                string[] parval = { "DELETE",ViewState["hdnIdentity1"].ToString().Trim(), ViewState["hdnApplicationID"].ToString().Trim(), ViewState["hdnFinancialYearID"].ToString().Trim(),
                ViewState["hdnMainSchemeID"].ToString().Trim(),ViewState["hdnSchemeID"].ToString().Trim(),ViewState["hdnCropID"].ToString().Trim(),ViewState["hdnTechnologyID"].ToString().Trim(),ViewState["hdnCategoryID"].ToString().Trim(),
                    ViewState["hdnDistrictID"].ToString().Trim(), ViewState["hdnTalukID"].ToString().Trim(), ViewState["hdnReleasedBy"].ToString().Trim() };
                int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                if (res >= 1)
                {
                    ClearAll();
                    CompleteClear();
                    ClearDeleteViewstate();
                    lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
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
                Clear1();
                Clear2();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTechnologies();
            BindSchemeReleaseTransactionList();
        }

        protected void ddlSubScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCropDetails();
            BindSchemeReleaseTransactionList();
        }

        protected void ddlTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
            BindSchemeReleaseTransactionList();
            lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
            lblTotalPhysicalTargetReleased.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
            lblTotalReleasedAmount1.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
            lblTotalPhysicalTargetReleased1.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
            lblTotalReleasedAmount2.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
            lblTotalPhysicalTargetReleased2.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
            IsTargetsExists();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSchemeReleaseTransactionList();
        }

        public void ClearDeleteViewstate()
        {
            try
            {
                ViewState["hdnIdentity1"] = "";
                ViewState["hdnApplicationID"] = "";
                ViewState["hdnFinancialYearID"] = "";
                ViewState["hdnMainSchemeID"] = "";
                ViewState["hdnSchemeID"] = "";
                ViewState["hdnCropID"] = "";
                ViewState["hdnTechnologyID"] = "";
                ViewState["hdnCategoryID"] = "";
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
