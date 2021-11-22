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

namespace KKISANWEB.Demonstration
{
    public partial class TargetReleaseSchemeWisejda : System.Web.UI.Page
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                                FMMemberFunction.FinancialYear(ddlFinancialYear, 18, 19, 20, "kkisanconstr");
                            }


                        }
                        BindMainScheme();
                      //  BindSubScheme();
                        //BindCategory();
                        //CommonDropdown.MasterSectorZP(ddlSector, "E", "kkisanconstr");
                        Common.BindDropdownByRole(ddlDistrict, ddlTaluk, DistrictID, Convert.ToInt32(Session["RoleID"]));
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

                if (Page.IsValid == true)
                {

                    Double HOAAmount_New = Convert.ToDouble(Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr"));
                    Double JDAAmount_New = Convert.ToDouble(Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr"));
                    Double HOAPhysical_New = Convert.ToDouble(Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr"));
                    Double JDAPhysical_New = Convert.ToDouble(Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr"));
                    Double HOAAmount_New1 = Convert.ToDouble(Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr"));
                    Double JDAAmount_New1 = Convert.ToDouble(Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr"));
                    Double HOAPhysical_New1 = Convert.ToDouble(Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr"));
                    Double JDAPhysical_New1 = Convert.ToDouble(Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr"));
                    Double HOAAmount_New2 = Convert.ToDouble(Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr"));
                    Double JDAAmount_New2 = Convert.ToDouble(Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr"));
                    Double HOAPhysical_New2 = Convert.ToDouble(Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr"));
                    Double JDAPhysical_New2 = Convert.ToDouble(Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr"));

                    //String HOAAmount = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");

                    //String JDAAmount = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    Double Release_New = 0.00;
                    Double ReleasePhysical_New = 0.00;
                    Double Release_New1 =0.00;
                    Double ReleasePhysical_New1 = 0.00;
                    Double Release_New2 = 0.00;
                    Double ReleasePhysical_New2 = 0.00;

                    //koteppa added update function --->
                    if (btnGo.Text == "Update GEN" || btnGo.Text == "Update SCP" || btnGo.Text == "Update TSP")
                    {
                        string amount = "";
                        string hectare = "";
                        if (btnGo.Text == "Update GEN")
                        {
                             Release_New = Convert.ToDouble(txtFinancialTarget.Text.Trim());
                             ReleasePhysical_New = Convert.ToDouble(txtPhysicalTarget.Text.Trim());
                            amount = txtFinancialTarget.Text.Trim();
                            hectare = txtPhysicalTarget.Text.Trim();
                        }
                        if (btnGo.Text == "Update SCP")
                        {
                             Release_New1 = Convert.ToDouble(txtFinancialTarget1.Text.Trim());
                             ReleasePhysical_New1 = Convert.ToDouble(txtPhysicalTarget1.Text.Trim());
                            HOAAmount_New = HOAAmount_New1;
                            JDAAmount_New = JDAAmount_New1;
                            Release_New = Release_New1;
                            HOAPhysical_New = HOAPhysical_New1;
                            JDAPhysical_New = JDAPhysical_New1;
                            amount = txtFinancialTarget1.Text.Trim();
                            hectare = txtPhysicalTarget1.Text.Trim();
                        }
                        if (btnGo.Text == "Update TSP")
                        {
                             Release_New2 = Convert.ToDouble(txtFinancialTarget2.Text.Trim());
                             ReleasePhysical_New2 = Convert.ToDouble(txtPhysicalTarget2.Text.Trim());
                            HOAAmount_New = HOAAmount_New2;
                            JDAAmount_New = JDAAmount_New2;
                            Release_New = Release_New2;
                            HOAPhysical_New = HOAPhysical_New2;
                            JDAPhysical_New = JDAPhysical_New2;
                            amount = txtFinancialTarget2.Text.Trim();
                            hectare = txtPhysicalTarget2.Text.Trim();
                        }
                        Double temp_JDAAmount_New =Convert.ToDouble(ViewState["UpdatingTargetAmount"].ToString());
                        Double temp_JDAPhysical_New = Convert.ToDouble(ViewState["UpdatingPhysicalTarget"].ToString());
                        if (HOAAmount_New >= (JDAAmount_New - temp_JDAAmount_New + Release_New))
                        {
                            if (HOAPhysical_New >= Convert.ToDouble(String.Format("{0:0.000}", (JDAPhysical_New - temp_JDAPhysical_New + ReleasePhysical_New))))
                            {
                                String ADAAmount = Common.TotalTargetReleaseAmountDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"].ToString()), Convert.ToInt32(ViewState["hdnMainSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnCropID"].ToString()), Convert.ToInt32(ViewState["hdnTechnologyID"].ToString()), Convert.ToInt32(ViewState["hdnCategoryID"].ToString()), Convert.ToInt32(ViewState["hdnDistrictID"].ToString()), Convert.ToInt32(ViewState["hdnTalukID"].ToString()), 0, 9, "kkisanconstr");
                                String ADAPhysical = Common.TotalTargetReleasePhysicalDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"].ToString()), Convert.ToInt32(ViewState["hdnMainSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnSchemeID"].ToString()), Convert.ToInt32(ViewState["hdnCropID"].ToString()), Convert.ToInt32(ViewState["hdnTechnologyID"].ToString()), Convert.ToInt32(ViewState["hdnCategoryID"].ToString()), Convert.ToInt32(ViewState["hdnDistrictID"].ToString()), Convert.ToInt32(ViewState["hdnTalukID"].ToString()), 0, 9, "kkisanconstr");

                                //String ADAAmount = Common.TotalTargetReleaseAmountDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"]), Convert.ToInt32(ViewState["hdnMainSchemeID"]), Convert.ToInt32(ViewState["hdnSchemeID"]), Convert.ToInt32(ViewState["hdnCropID"]), Convert.ToInt32(ViewState["hdnTechnologyID"]), Convert.ToInt32(ViewState["hdnCategoryID"]), Convert.ToInt32(ViewState["hdnDistrictID"]), Convert.ToInt32(ViewState["hdnTalukID"]), 0, 9, "kkisanconstr");
                                Double ADAAmount_New = Convert.ToDouble(ADAAmount);
                                Double ADAPhysicalt_New = Convert.ToDouble(ADAPhysical);
                                Double amountnew = Convert.ToDouble(amount);
                                Double hectarenew = Convert.ToDouble(hectare);
                                //String ADAPhysical = Common.TotalTargetReleasePhysicalDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"]), Convert.ToInt32(ViewState["hdnMainSchemeID"]), Convert.ToInt32(ViewState["hdnSchemeID"]), Convert.ToInt32(ViewState["hdnCropID"]), Convert.ToInt32(ViewState["hdnTechnologyID"]), Convert.ToInt32(ViewState["hdnCategoryID"]), Convert.ToInt32(ViewState["hdnDistrictID"]), Convert.ToInt32(ViewState["hdnTalukID"]), 0, 9, "kkisanconstr");
                                //Double ADAPhysical_New = Convert.ToDouble(ADAPhysical);
                                if (ADAAmount_New > amountnew || ADAPhysicalt_New > hectarenew)
                                {
                                    lblNotMsg.Text = "The ADA already recieved your target and assigned interns to RSK. And you are trying to edit the Target which is less than those targets. ADA Physical Target Released is " + ADAPhysical + " and ADA Financial Target Released is " + ADAAmount + ". Please Try editing with values which are greater than ada releases! ";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);

                                }
                                else
                                { 
                                        
                                        string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                                        string[] parval = { ViewState["hdnApplicationID"].ToString(), ViewState["hdnFinancialYearID"].ToString(), ViewState["hdnMainSchemeID"].ToString(), ViewState["hdnSchemeID"].ToString(), ViewState["hdnCropID"].ToString(), ViewState["hdnTechnologyID"].ToString(), ViewState["hdnCategoryID"].ToString(), ViewState["hdnDistrictID"].ToString(), ViewState["hdnTalukID"].ToString(), "0", hectare, amount, Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "Update" };
                                        int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                                        if (res >= 1)
                                        {

                                        ClearAll();
                                        BindSchemeReleaseTransactionList();
                                        Filldata();
                                        IsTargetsExists();
                                        //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                                        //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                                        // lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                                        //txtFinancialTarget.Text = "";
                                        //    txtOrderNo.Text = "";
                                        //    txtDescription.Text = "";

                                            lblNotMsg.Text = "Targets Updated successfully !";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                            
                                        }
                                        else
                                        {
                                            lblNotMsg.Text = "Targets not saved / failed. Check your details ! ";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                        }

                                }
                                
                                    
                            }
                            else
                            {
                                lblNotMsg.Text = " JDA Physical Targets Release Should be less than HOA Physical Target Release ! ";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                BindSchemeReleaseTransactionList();
                            }
                        }
                        else
                        {
                            lblNotMsg.Text = " JDA Targets Release Amount Should be less than HOA Target Release Amount ! ";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                            BindSchemeReleaseTransactionList();
                        }

                    }
                    //koteppa added update function --->
                    else
                    {
                       
                        int res = 0;

                        if (txtPhysicalTarget.Text != "" && txtFinancialTarget.Text != "")
                        {
                            _log.Info("Inside gen");
                            Release_New = Convert.ToDouble(txtFinancialTarget.Text.Trim());
                            ReleasePhysical_New = Convert.ToDouble(txtPhysicalTarget.Text.Trim());
                            if (HOAAmount_New >= Convert.ToDouble(String.Format("{0:0.000}", (JDAAmount_New + Release_New))))
                            {
                                if (HOAPhysical_New >= Convert.ToDouble(String.Format("{0:0.000}", (JDAPhysical_New + ReleasePhysical_New))))
                                {
                                    _log.Info("Inside gen insertion");
                                    string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "insert" };
                                    res = res + QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");

                                }
                                else
                                {
                                    lblNotMsg.Text = " JDA Physical Targets Release for GEN Should be less than HOA Physical Target Release for GEN ! ";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                    BindSchemeReleaseTransactionList();
                                }
                            }
                            else
                            {
                                lblNotMsg.Text = " JDA Targets Release Amount for GEN Should be less than HOA Target Release Amount for GEN ! ";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                BindSchemeReleaseTransactionList();
                            }
                        }
                        if (txtPhysicalTarget1.Text != "" && txtFinancialTarget1.Text != "")
                        {
                            _log.Info("Inside scp");
                            Release_New1 = Convert.ToDouble(txtFinancialTarget1.Text.Trim());
                            ReleasePhysical_New1 = Convert.ToDouble(txtPhysicalTarget1.Text.Trim());
                            if (HOAAmount_New1 >= Convert.ToDouble(String.Format("{0:0.000}", (JDAAmount_New1 + Release_New1))))
                            {
                                if (HOAPhysical_New1 >= Convert.ToDouble(String.Format("{0:0.000}", (JDAPhysical_New1 + ReleasePhysical_New1))))
                                {
                                    _log.Info("Inside scp insertion");
                                    string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory1.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", txtPhysicalTarget1.Text.Trim(), txtFinancialTarget1.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "insert" };
                                    res = res + QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");

                                }
                                else
                                {
                                    lblNotMsg.Text = " JDA Physical Targets Release for SCP Should be less than HOA Physical Target Release for SCP ! ";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                    BindSchemeReleaseTransactionList();
                                }
                            }
                            else
                            {
                                lblNotMsg.Text = " JDA Targets Release Amount for SCP Should be less than HOA Target Release Amount for SCP! ";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                BindSchemeReleaseTransactionList();
                            }
                        }
                        if (txtPhysicalTarget2.Text != "" && txtFinancialTarget2.Text != "")
                        {
                            _log.Info("Inside tsp");
                            Release_New2 = Convert.ToDouble(txtFinancialTarget2.Text.Trim());
                            ReleasePhysical_New2 = Convert.ToDouble(txtPhysicalTarget2.Text.Trim());
                            if (HOAAmount_New2 >= Convert.ToDouble(String.Format("{0:0.000}", (JDAAmount_New2 + Release_New2))))
                            {
                                if (HOAPhysical_New2 >= Convert.ToDouble(String.Format("{0:0.000}", (JDAPhysical_New2 + ReleasePhysical_New2))))
                                {
                                    _log.Info("Inside tsp insertion");
                                    string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliID", "@PhysicalTarget", "@FinancialTarget", "@ReleasedBy", "@OrderNo", "@Description", "@Method" };
                                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory2.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", txtPhysicalTarget2.Text.Trim(), txtFinancialTarget2.Text.Trim(), Session["RoleID"].ToString(), txtOrderNo.Text.Trim(), txtDescription.Text.Trim(), "insert" };
                                    res = res + QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");

                                }
                                else
                                {
                                    lblNotMsg.Text = " JDA Physical Targets Release for TSP Should be less than HOA Physical Target Release for TSP ! ";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                    BindSchemeReleaseTransactionList();
                                }
                            }
                            else
                            {
                                lblNotMsg.Text = " JDA Targets Release for TSP Amount Should be less than HOA Target Release Amount for TSP ! ";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                                BindSchemeReleaseTransactionList();
                            }
                        }
                        if(res>=1)
                        {
                            _log.Info(res);
                            ClearAll();
                            BindSchemeReleaseTransactionList();
                            Filldata();
                            IsTargetsExists();
                            lblNotMsg.Text = "Targets saved successfully !";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                            
                        }
                        else
                        {
                            lblNotMsg.Text = "Targets not saved / failed. Check your details ! ";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
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
                //if (isReleaseOverflow() == true)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('JDA can not release amount more than the Head Office amount for a Scheme !');", true);
                //    valid = 0;
                //}
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
                string[] par = { "@Method", "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@PhysicalTarget", "@FinancialTarget", "@OrderNo", "@ReleasedBy", "@Description" };
                string[] parval = { "isReleaseOverflow", ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory.SelectedValue, ddlDistrict.SelectedValue, "0", txtPhysicalTarget.Text.Trim(), txtFinancialTarget.Text.Trim(), txtOrderNo.Text.Trim(), Session["RoleID"].ToString(), txtDescription.Text.Trim() };
                dt = QueryExecution.DataAdapt("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
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
                BindSubScheme();
                int schemeid = Convert.ToInt32(ddlScheme.SelectedValue);
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

                // lblHOA.Text = "HOA : " + Common.SchemeHOAName(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), "kkisanconstr");

                if (ddlDistrict.SelectedValue != "" && ddlDistrict.SelectedValue != "0" && ddlScheme.SelectedValue != "0")
                {
                    //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    lblTotalReleasedPhysicalHOA.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");

                }


                if (ddlDistrict.SelectedValue != "" && ddlDistrict.SelectedValue != "0" && ddlTaluk.SelectedValue != "" && ddlTaluk.SelectedValue != "0" && ddlScheme.SelectedValue != "0")
                {
                    lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    lblTotalReleasedPhysical.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                    // lblTotalReleasedAmountHOA.Text = Common.TotalSchemeReleaseAmount(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 1, "kkisanconstr");
                    //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    // lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    BindSchemeReleaseTransactionList();
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

               // lblHOA.Text = "";
                Clear();
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

        private void BindSectorwiseScheme()
        {
            try
            {
                string str = ConfigurationManager.ConnectionStrings["kkisanconstr"].ToString();
                SqlConnection con = new SqlConnection(str);
                string com = "select SchemeID as ValueField,SchemeNameEng as ItemField from Master.MasterSectorScheme where ApplicationID='" + ApplicationID + "' and FinancialYearID='" + Convert.ToInt32(ddlFinancialYear.SelectedValue) + "'"; //and SectorID='" + ddlSector.SelectedValue + "'";

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
                //if (ddlFinancialYear.SelectedValue != "0" && ddlSector.SelectedValue != "0" && ddlScheme.SelectedValue != "0" && ddlDistrict.SelectedValue != "0" && ddlTaluk.SelectedValue != "0")
                //{
                if (ddlFinancialYear.SelectedValue != "0" && ddlScheme.SelectedValue != "0" && ddlDistrict.SelectedValue != "0" && ddlTaluk.SelectedValue != "0")
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    string[] par = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", Session["RoleID"].ToString(), "E" };
                    dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList2122]", par, parval, "kkisanconstr");
                    string[] par1 = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval1 = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory1.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", Session["RoleID"].ToString(), "E" };
                    dt1 = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList2122]", par1, parval1, "kkisanconstr");
                    string[] par2 = { "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@HobliId", "@RoleID", "@LangType" };
                    string[] parval2 = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory2.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", Session["RoleID"].ToString(), "E" };
                    dt2 = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionList2122]", par2, parval2, "kkisanconstr");
                    dt.Merge(dt1);
                    dt.Merge(dt2);
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
                    divReleasedList.Visible = false;
                    //DataTable dt = new DataTable();
                    //string[] par = { "@ApplicationID", "@FinancialYearID", "@DistrictID", "@SchemeID", "@RoleID", "@LangType" };
                    //string[] parval = { ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlDistrict.SelectedValue, ddlScheme.SelectedValue.ToString().Trim(), Session["RoleID"].ToString(), "E" };
                    //dt = QueryExecution.DataAdapt("SP", "[DBT].[Sp@SchemeTargetTransactionListGeneral]", par, parval, "kkisanconstr");
                    //if (dt.Rows.Count >= 1)
                    //{
                    //    Common.GridBind(grdReleasedList, dt);
                    //    divReleasedList.Visible = true;
                    //}
                    //else
                    //{
                    //    divReleasedList.Visible = false;
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
        protected void ddlTaluk_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                BindSchemeReleaseTransactionList();
                lblSubTreasuryCode.Text = " Sub Treasury Cd : " + Common.SubTreasuryCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                lblDrawingOfficeCode.Text = "Drawing Office Cd: " + Common.DrawingOfficeCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                Filldata();
                //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                //lblTotalReleasedPhysicalHOA.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedPhysical.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                // lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                // lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                lblPhysicalTarget.Text = ddlTaluk.SelectedItem.Text;
                lblPhysicalTarget1.Text = ddlTaluk.SelectedItem.Text;
                lblPhysicalTarget2.Text = ddlTaluk.SelectedItem.Text;

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
                TargetRelese2122 target = dbt.GetTargetRelease(ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", Session["RoleID"].ToString());
                TargetRelese2122 target1 = dbt.GetTargetRelease(ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory1.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", Session["RoleID"].ToString());
                TargetRelese2122 target2 = dbt.GetTargetRelease(ApplicationID, ddlFinancialYear.SelectedValue.ToString().Trim(), ddlScheme.SelectedValue, ddlSubScheme.SelectedValue, ddlCrop.SelectedValue, ddlTechnology.SelectedValue, ddlCategory2.SelectedValue, ddlDistrict.SelectedValue, ddlTaluk.SelectedValue, "0", Session["RoleID"].ToString());
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
                    //ViewState["UpdatingTargetAmount"]= target.FinancialTarget.ToString();
                    //ViewState["UpdatingPhysicalTarget"]= target.PhysicalTarget.ToString();
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
                    //ViewState["UpdatingTargetAmount"] = target1.FinancialTarget.ToString();
                    //ViewState["UpdatingPhysicalTarget"] = target1.PhysicalTarget.ToString();
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
                    //ViewState["UpdatingTargetAmount"] = target2.FinancialTarget.ToString();
                    //ViewState["UpdatingPhysicalTarget"] = target2.PhysicalTarget.ToString();
                    val++;
                }
                if (val == 0)
                {
                    txtOrderNo.ReadOnly = false;
                    txtDescription.ReadOnly = false;
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
                ViewState["rowupdateID"] = "";
                ViewState["PhysicalTargetold"] = "";
                ViewState["FinancialTargetold"] = "";

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
                //ddlSector.ClearSelection();
                //ddlScheme.Items.Clear();
               // lblHOA.Text = "";
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
                Clear1();
                Clear2();
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
                    ViewState["hdnMainSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnMainSchemeID")).Value.ToString().Trim();
                    ViewState["hdnSchemeID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnSchemeID")).Value.ToString().Trim();
                    ViewState["hdnCropID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnCropID")).Value.ToString().Trim();
                    ViewState["hdnTechnologyID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTechnologyID")).Value.ToString().Trim();
                    ViewState["hdnCategoryID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnCategoryID")).Value.ToString().Trim();
                    ViewState["hdnDistrictID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnDistrictID")).Value.ToString().Trim();
                    ViewState["hdnTalukID"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnTalukID")).Value.ToString().Trim();
                    ViewState["hdnReleasedBy"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnReleasedBy")).Value.ToString().Trim();
                    String ADAAmount = Common.TotalTargetReleaseAmountDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"]), Convert.ToInt32(ViewState["hdnMainSchemeID"]), Convert.ToInt32(ViewState["hdnSchemeID"]), Convert.ToInt32(ViewState["hdnCropID"]), Convert.ToInt32(ViewState["hdnTechnologyID"]), Convert.ToInt32(ViewState["hdnCategoryID"]), Convert.ToInt32(ViewState["hdnDistrictID"]), Convert.ToInt32(ViewState["hdnTalukID"]), 0, 9, "kkisanconstr");
                    Double ADAAmount_New = Convert.ToDouble(ADAAmount);
                    if (ADAAmount_New > 0)
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
                else if (e.CommandName == "Edit_")
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
                    ViewState["hdnReleasedBy"] = ((HiddenField)grdReleasedList.Rows[IndexValue].FindControl("hdnReleasedBy")).Value.ToString().Trim();
                    DBTManager dbt = new DBTManager();
                    TargetRelese2122 target = dbt.GetTargetRelease(ViewState["hdnApplicationID"].ToString(), ViewState["hdnFinancialYearID"].ToString(), ViewState["hdnMainSchemeID"].ToString(), ViewState["hdnSchemeID"].ToString(), ViewState["hdnCropID"].ToString(), ViewState["hdnTechnologyID"].ToString(), ViewState["hdnCategoryID"].ToString(), ViewState["hdnDistrictID"].ToString(), ViewState["hdnTalukID"].ToString(), "0", Session["RoleID"].ToString());
                    //String JDAAmount = Common.TotalTargetReleaseAmountDBT2122(ViewState["hdnApplicationID"].ToString(), Convert.ToInt32(ViewState["hdnFinancialYearID"]), Convert.ToInt32(ViewState["hdnMainSchemeID"]), Convert.ToInt32(ViewState["hdnSchemeID"]), Convert.ToInt32(ViewState["hdnCropID"]), Convert.ToInt32(ViewState["hdnTechnologyID"]), Convert.ToInt32(ViewState["hdnCategoryID"]), Convert.ToInt32(ViewState["hdnDistrictID"]), 0, 0, 5, "kkisanconstr");
                    // JDAAmount_New = Convert.ToDouble(JDAAmount);
                    ViewState["UpdatingTargetAmount"] = target.FinancialTarget.ToString();
                    ViewState["UpdatingPhysicalTarget"] = target.PhysicalTarget.ToString();
                    if (Convert.ToInt32(ViewState["hdnCategoryID"]) == 1)
                    {
                        ClearAlltxt();
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
                        ClearAlltxt();
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
                        ClearAlltxt();
                        txtOrderNo.ReadOnly = true;
                        txtDescription.ReadOnly = true;
                        btnGo.Text = "Update TSP";
                        txtPhysicalTarget2.Text = target.PhysicalTarget.ToString();
                        txtFinancialTarget2.Text = target.FinancialTarget.ToString();
                        txtOrderNo.Text = target.OrderNo.ToString();
                        txtDescription.Text = target.Description.ToString();
                    }

                    //Clear();

                    //lblSubTreasuryCode.Text = " Sub Treasury Cd : " + Common.SubTreasuryCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                    //lblDrawingOfficeCode.Text = "Drawing Office Cd: " + Common.DrawingOfficeCode(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), "kkisanconstr");
                    //Filldata();
                    ////lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    ////lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    ////lblTotalReleasedPhysicalHOA.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    ////lblTotalReleasedPhysical.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                    //ViewState["UpdatingTargetAmount"] = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    //ViewState["UpdatingPhysicalTarget"] = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                    ////lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    ////lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    //// lblBalAmount.Text = Common.TotalSchemeTargetAvailabilityDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    //lblPhysicalTarget.Text = ddlTaluk.SelectedItem.Text;

                    //BindSchemeReleaseTransactionList();
                    //IsTargetsExists();

                }
                //koteppa Edit update function --->
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
                lblTotalReleasedAmountHOA.Text = "";
                lblTotalReleasedAmount.Text = "";
                lblTotalReleasedPhysicalHOA.Text = "";
                lblTotalReleasedPhysical.Text = "";
                lblTotalReleasedAmountHOA1.Text = "";
                lblTotalReleasedAmount1.Text = "";
                lblTotalReleasedPhysicalHOA1.Text = "";
                lblTotalReleasedPhysical1.Text = "";
                lblTotalReleasedAmountHOA2.Text = "";
                lblTotalReleasedAmount2.Text = "";
                lblTotalReleasedPhysicalHOA2.Text = "";
                lblTotalReleasedPhysical2.Text = "";


            }
            catch (Exception ex)
            {

            }
        }
        public void ClearAlltxt()
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

                string[] par = { "@Method", "@ID", "@ApplicationID", "@FinancialYearID", "@MainSchemeID", "@SchemeID", "@CropID", "TechnologyID", "CategoryID", "@DistrictID", "@TalukID", "@ReleasedBy" };
                string[] parval = { "DELETE",ViewState["hdnIdentity1"].ToString().Trim(), ViewState["hdnApplicationID"].ToString().Trim(), ViewState["hdnFinancialYearID"].ToString().Trim(),
            ViewState["hdnMainSchemeID"].ToString().Trim(),ViewState["hdnSchemeID"].ToString().Trim(),ViewState["hdnCropID"].ToString().Trim(),ViewState["hdnTechnologyID"].ToString().Trim(),ViewState["hdnCategoryID"].ToString().Trim(),
                    ViewState["hdnDistrictID"].ToString().Trim(), ViewState["hdnTalukID"].ToString().Trim(), ViewState["hdnReleasedBy"].ToString().Trim() };
                int res = QueryExecution.ExeData("SP", "[DBT].[SP@SchemeTargetRelease2122]", par, parval, "kkisanconstr");
                if (res >= 1)
                {
                    ClearAll();
                    ClearDeleteViewstate();
                    Filldata();
                    //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    //lblTotalReleasedPhysicalHOA.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    //lblTotalReleasedPhysical.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

                    //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                    //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                    //lblBalAmount.Text = Common.TotalSchemeTargetAvailability(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");


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

        public void Filldata()
        {
            try
            {
                //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                //lblTotalReleasedPhysicalHOA.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedPhysical.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                //lblTotalReleasedAmountHOA1.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedAmount1.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                //lblTotalReleasedPhysicalHOA1.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedPhysical1.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                //lblTotalReleasedAmountHOA2.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedAmount2.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
                //lblTotalReleasedPhysicalHOA2.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                //lblTotalReleasedPhysical2.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");


                string amount = lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");                
                lblTotalReleasedAmount.Text = ((Convert.ToDouble(amount)) - (Convert.ToDouble((Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr")).ToString()))).ToString();
                string physical =lblTotalReleasedPhysicalHOA.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                lblTotalReleasedPhysical.Text = (Convert.ToDouble(physical) - Convert.ToDouble((Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr")).ToString())).ToString();
                string amount1 = lblTotalReleasedAmountHOA1.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                lblTotalReleasedAmount1.Text = (Convert.ToDouble(amount1) - Convert.ToDouble((Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr")).ToString())).ToString();
                string physical1 = lblTotalReleasedPhysicalHOA1.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                lblTotalReleasedPhysical1.Text = (Convert.ToDouble(physical1) - Convert.ToDouble((Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr")).ToString())).ToString();
                string amount2 = lblTotalReleasedAmountHOA2.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                lblTotalReleasedAmount2.Text = (Convert.ToDouble(amount2) - Convert.ToDouble((Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr")).ToString())).ToString();
                string physical2 = lblTotalReleasedPhysicalHOA2.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
                lblTotalReleasedPhysical2.Text = (Convert.ToDouble(physical2) - Convert.ToDouble((Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory2.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr")).ToString())).ToString();



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
            Filldata();
            IsTargetsExists();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filldata();
            //lblTotalReleasedAmountHOA.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
            //lblTotalReleasedAmount.Text = Common.TotalTargetReleaseAmountDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");
            //lblTotalReleasedPhysicalHOA.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), 0, 0, 1, "kkisanconstr");
            //lblTotalReleasedPhysical.Text = Common.TotalTargetReleasePhysicalDBT2122(ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSubScheme.SelectedValue), Convert.ToInt32(ddlCrop.SelectedValue), Convert.ToInt32(ddlTechnology.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlTaluk.SelectedValue), 0, Convert.ToInt32(Session["RoleID"].ToString()), "kkisanconstr");

            BindSchemeReleaseTransactionList();
        }
    }
}