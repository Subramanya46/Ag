using KKISAN.Model.DBT;
using KKISAN.Model.Seed;
using KKISAN.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;// Web Service for Farmer Details
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace KKISANWEB.Demonstration
{
    public partial class ApplicationEntry2122 : System.Web.UI.Page
    {
        string ApplicationID = "DBT", AppFor = "", FarmerID = "", FinYear = "", ItemCategoryID = "", ItemID = "";
        int DistrictID, TalukID, HobliID, VillageID;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["kkisanconstr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["RoleID"].ToString() != "11" && Session["RoleID"].ToString() != "9")
                {
                    // ResetSession();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You are not authorized user to visit this page.');window.location ='Login.aspx';", true);
                }
                else
                {
                    // ApplicationID = Request.QueryString["AID"] != null ? Request.QueryString["AID"] : "";
                    AppFor = Request.QueryString["AppFor"] != null ? Request.QueryString["AppFor"] : "";
                    DistrictID = Convert.ToInt32(Session["DistrictID"]);
                    TalukID = Convert.ToInt32(Session["TalukID"]);
                    HobliID = Convert.ToInt32(Session["HobliID"]);
                    if (!IsPostBack)
                    {
                        // ViewState["KitsAmount"] = new List<DBTKitDetail>();
                        if (ApplicationID == "FM" || ApplicationID == "AP" || ApplicationID == "SOM" || ApplicationID == "DBT")
                        {
                            //Common.ApplicationTypeLabel(lblApplicationName, ApplicationID, "E", "kkisanconstr");
                            //  Common.ApplicationTypeLabel(lblApplicationNameKan, ApplicationID, "K", "kkisanconstr");
                            ClearMachinaryDetails();
                            divFarmerDetails.Visible = false;
                            divMachinaryDetails.Visible = false;

                            if (ApplicationID == "SOM")
                            {
                                FMMemberFunction.FinancialYear(ddlFinancialYear, 21, 21, "kkisanconstr");
                            }
                            else
                            {
                                FMMemberFunction.FinancialYearSEED(ddlFinancialYear, 21, 21, "kkisanconstr");
                            }

                            if (AppFor == "MODIFY")
                            {

                                // rblIDType.SelectedValue = "FID";
                                // txtAadharNo.Visible = false;
                                //rfvtxtAadharNo.Visible = false;
                                txtFarmerID.Visible = true;
                                rfvtxtFarmerID.Visible = true;


                                FarmerID = Request.QueryString["FID"] != null ? Request.QueryString["FID"] : "";
                                FinYear = Request.QueryString["FYID"] != null ? Request.QueryString["FYID"] : "";

                                ItemCategoryID = Request.QueryString["ItemCategoryID"] != null ? Request.QueryString["ItemCategoryID"] : "";
                                ItemID = Request.QueryString["ItemID"] != null ? Request.QueryString["ItemID"] : "";

                                Session["ItemCategoryID_OLD"] = ItemCategoryID;
                                Session["ItemID_OLD"] = ItemID;

                                txtFarmerID.Text = FarmerID;
                                btnSubmit.Text = "Modify / Update";

                                DataTable DTFD = new DataTable();
                                DataTable DTLD = new DataTable();
                                DTFD = WSFarmerDetail.GetFarmerDetail("", txtFarmerID.Text.Trim());
                                DTLD = WSFarmerDetail.GetLandDetail("", txtFarmerID.Text.Trim());
                                
                                ViewState["DTFD"] = DTFD;
                                ViewState["DTLD"] = DTLD;

                                // Bind Farmer Details from Webservice
                                BindFarmerDetail(DTFD);
                                BindLandDetail(DTLD);
                                BindLandBenifits(DTLD);

                                // Bind Implement Details 
                                BindLandBenifits("FID", Convert.ToInt32(FinYear), ApplicationID, FarmerID, "0");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModalCheckForRsk();", false);
                                // write logic for binding Methods
                            }
                            else
                            {
                                btnSubmit.Text = "Submit";
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Dashboard.aspx", false);
                        }
                    }
                    else
                    {
                        // go further
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void BindCropDetails()
        {
            ddlCrop.Items.Clear();
            ddlTechnology.Items.Clear();
            if (ddlSubScheme.SelectedItem != null && ddlSubScheme.SelectedIndex > 0)
            {
                DBTManager marketMgr = new DBTManager();
                List<DBTCropDetail> CropDetails = marketMgr.GetCropDetails(KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value), KKISAN.Utils.ConversionUtil.ConvertInt(ddlScheme.SelectedItem.Value));
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

        //private void BindKitDetails()
        //{
        //    DBTManager marketMgr = new DBTManager();
        //    List<DBTKitDetail> kitDetails = marketMgr.GetInputKits();
        //    if (kitDetails != null && kitDetails.Count > 0)
        //    {
        //        ddlInputKits.DataSource = kitDetails;
        //        ddlInputKits.DataTextField = "KitName";
        //        ddlInputKits.DataValueField = "KitId";
        //    }
        //    else
        //    {
        //        ddlCrop.DataSource = new List<DBTKitDetail>();
        //    }

        //    ddlInputKits.DataBind();
        //    ListItem li = new ListItem("------Select------", "0");
        //    ddlInputKits.Items.Insert(0, li);
        //}
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

        private void BindVarieties()
        {
            try
            {
                ddlVariety.Items.Clear();
                if (ddlCrop.SelectedItem != null && ddlCrop.SelectedIndex > 0)
                {
                    DBTManager marketMgr = new DBTManager();
                    int CropId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlCrop.SelectedItem.Value);
                    int MainSchemeID = KKISAN.Utils.ConversionUtil.ConvertInt(ddlScheme.SelectedItem.Value);
                    List<Varieties> VarietyDetails = marketMgr.GetVarietyDetails(CropId, MainSchemeID);
                    if (VarietyDetails != null && VarietyDetails.Count > 0)
                    {
                        ddlVariety.DataSource = VarietyDetails;
                        ddlVariety.DataTextField = "VarietyNameEng";
                        ddlVariety.DataValueField = "VarietyId";
                    }
                    else
                    {
                        ddlVariety.DataSource = new List<Varieties>();
                    }
                }
                else
                {
                    ddlVariety.DataSource = new List<Varieties>();
                }
                ddlVariety.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlVariety.Items.Insert(0, li);
            }
            catch(Exception ex)
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

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "select  isnull(sum(LandGunta),0) as TotalGunta  from dbt.FarmerRegistration2122 where  ApplicationStatus not in (3) and FinacialYearId=21  and FarmerId='" + txtFarmerID.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //txttotalstockrecivedrsk.Text = dt1.Rows[0]["RecivedStock"].ToString();
                    double TotalGunta = Convert.ToDouble(dt.Rows[0]["TotalGunta"].ToString());
                    ViewState["TotalGunta"] = dt.Rows[0]["TotalGunta"].ToString();
                }
                double TotalGunta1 = Convert.ToDouble(ViewState["TotalGunta"].ToString());

                //if (TotalGunta1 < 200)
                //{
                    DataTable DTFD = new DataTable();
                    DataTable DTLD = new DataTable();

                    DTFD = WSFarmerDetail.GetFarmerDetail("", txtFarmerID.Text.Trim());
                    DTLD = WSFarmerDetail.GetLandDetail("", txtFarmerID.Text.Trim());

                    ViewState["DTFD"] = DTFD;
                    ViewState["DTLD"] = DTLD;

                    if (Convert.ToInt32(DTLD.Rows.Count) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Alert", "alert('Farmer Land Details not found !  Please Update Land Details in Fruits Portal for this Farmer ! ')", true);
                    }

                    for (int i = 0; i < Convert.ToInt32(DTLD.Rows.Count); i++)
                    {

                        if ((DTLD.Rows[i]["districtCode"].ToString() == DistrictID.ToString() && DTLD.Rows[i]["talukCode"].ToString() == TalukID.ToString() && DTLD.Rows[i]["hobliCode"].ToString() == HobliID.ToString()) ||
                            (DTFD.Rows[0]["DistrictID"].ToString() == DistrictID.ToString() && DTFD.Rows[0]["TalukID"].ToString() == TalukID.ToString() && DTFD.Rows[0]["HobliID"].ToString() == HobliID.ToString()))
                        {
                            BindFarmerDetail(DTFD);
                            BindLandDetail(DTLD);
                            BindLandBenifits(DTLD);
                            BindMachinaryDetail();
                            ClearMachinaryDetails();
                        }
                        else
                        {
                            lblNotMsg1.Text = "Your not Belongs to this RSK";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModalCheckForRsk();", true);
                        }
                    }
                //}
                //else
                //{
                //    lblNotMsg2.Text = "This Farmer aleready Taken Maximum Amount Of 200 Gunta ";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModalCheckForRsk1();", true);

                //}
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public void BindFarmerDetail(DataTable dtfd)
        {
            try
            {
                if (dtfd.Rows.Count == 1)
                {
                    btnGetDetails.Enabled = false;
                    txtFarmerID.ReadOnly = true;
                    lblFarmerNameEng.Text = dtfd.Rows[0]["FarmerNameEng"].ToString().Trim();
                    lblFarmerNameKan.Text = dtfd.Rows[0]["FarmerNameKan"].ToString().Trim();
                    lblFarmerID.Text = dtfd.Rows[0]["FarmerID"].ToString().Trim();
                    if (dtfd.Rows[0]["GenderID"].ToString().Contains("F") || dtfd.Rows[0]["GenderID"].ToString().Contains("M") || dtfd.Rows[0]["GenderID"].ToString().Contains("O"))
                    {
                        rbGender.SelectedValue = dtfd.Rows[0]["GenderID"].ToString().Trim();
                    }
                    else
                    {
                        rbGender.ClearSelection();
                    }
                    lblFatherNameKan.Text = dtfd.Rows[0]["FatherNameKan"].ToString().Trim();
                    lblFatherNameEng.Text = dtfd.Rows[0]["FatherNameEng"].ToString().Trim();
                    lblLandlineNo.Text = dtfd.Rows[0]["LandLine"].ToString().Trim();
                    //lblMobileNo.Text = dtfd.Rows[0]["Mobile"].ToString().Trim();

                    // Address
                    lblDistrictName.Text = dtfd.Rows[0]["DistrictName"].ToString().Trim();
                    lblTalukName.Text = dtfd.Rows[0]["TalukName"].ToString().Trim();
                    lblHobliName.Text = dtfd.Rows[0]["HobliName"].ToString().Trim();
                    lblVillageName.Text = dtfd.Rows[0]["VillageName"].ToString().Trim();
                    lblResidenceAddress.Text = dtfd.Rows[0]["ResidentialAddress"].ToString().Trim();
                    //Identification Details
                    lblEpicID.Text = dtfd.Rows[0]["EPICID"].ToString().Trim();
                    lblRationCardNo.Text = dtfd.Rows[0]["RationCard"].ToString().Trim();
                    lblCasteRDNumber.Text = lblRDNumber.Text = dtfd.Rows[0]["Caste_ID"].ToString().Trim();
                    // Land Details
                    //Bank Details
                    lblBankName.Text = dtfd.Rows[0]["BankName"].ToString().Trim();
                    lblAccountNo.Text = dtfd.Rows[0]["AccountNo"].ToString().Trim();
                    lblIFSC.Text = dtfd.Rows[0]["IFSC"].ToString().Trim();
                    lblDistrictBank.Text = dtfd.Rows[0]["BranchDistrictName"].ToString().Trim();
                    lblBranch.Text = dtfd.Rows[0]["BranchName"].ToString().Trim();
                    // Other Details
                    if (dtfd.Rows[0]["CategoryID"].ToString().Contains("O") || dtfd.Rows[0]["CategoryID"].ToString().Contains("S") || dtfd.Rows[0]["CategoryID"].ToString().Contains("T") || dtfd.Rows[0]["CategoryID"].ToString().Contains("B"))
                    {
                        rbCast.SelectedValue = rbCaste.SelectedValue = dtfd.Rows[0]["CategoryID"].ToString().Trim();
                        // rdbtnSubsidyClaimCategory.SelectedValue = dtfd.Rows[0]["CategoryID"].ToString().Trim();

                        ViewState["CategoryID"] = dtfd.Rows[0]["CategoryID"].ToString().Trim();
                        //rbCast.SelectedValue = "S";
                    }
                    else
                    {
                        //rbCast.SelectedValue = "S";
                        rbCast.ClearSelection();
                        rbCaste.ClearSelection();
                    }

                    if (dtfd.Rows[0]["PH"].ToString().Contains("Y") || dtfd.Rows[0]["PH"].ToString().Contains("N"))
                    {
                        rbPh.SelectedValue = dtfd.Rows[0]["PH"].ToString().Trim();
                    }
                    else
                    {
                        rbPh.ClearSelection();
                    }

                    if (dtfd.Rows[0]["MinorityType"].ToString().Contains("Y") || dtfd.Rows[0]["MinorityType"].ToString().Contains("N"))
                    {
                        rbMinorities.SelectedValue = dtfd.Rows[0]["MinorityType"].ToString().Trim();
                    }
                    else
                    {
                        rbMinorities.ClearSelection();

                    }
                    
                    if (dtfd.Rows[0]["MinoritiesID"].ToString().Contains("1") || dtfd.Rows[0]["MinoritiesID"].ToString().Contains("2") || dtfd.Rows[0]["MinoritiesID"].ToString().Contains("3") ||
                        dtfd.Rows[0]["MinoritiesID"].ToString().Contains("4") || dtfd.Rows[0]["MinoritiesID"].ToString().Contains("5") || dtfd.Rows[0]["MinoritiesID"].ToString().Contains("6"))
                    {
                        rbMinoritiesType.SelectedValue = dtfd.Rows[0]["MinoritiesID"].ToString().Trim();
                    }
                    else
                    {
                        rbMinoritiesType.ClearSelection();
                    }

                    if (dtfd.Rows[0]["FarmerTypeID"].ToString().Contains("S") || dtfd.Rows[0]["FarmerTypeID"].ToString().Contains("M") || dtfd.Rows[0]["FarmerTypeID"].ToString().Contains("B"))
                    {
                        rbFarmerType.SelectedValue = dtfd.Rows[0]["FarmerTypeID"].ToString().Trim();
                    }
                    else
                    {
                        rbFarmerType.ClearSelection();
                    }

                    divFarmerDetails.Visible = true;
                    divMachinaryDetails.Visible = true;
                }
                else
                {
                    Utilities.AlertHandling(lblNotMsg, "Farmer Details not found ! &nbsp;&nbsp;&nbsp;&nbsp;<a href='" + ConfigurationManager.AppSettings["FRUITSLINK"] + "' target='_Blank' style='color:White;text-decoration:underline'>Click here to register farmer  </a> ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModal();", true);
                    divFarmerDetails.Visible = false;
                    divMachinaryDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {

                divFarmerDetails.Visible = false;
                divMachinaryDetails.Visible = false;
                ExceptionHandler.ExceptionHistory(Session["UserID"].ToString().Trim(), Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }
        public void BindLandDetail(DataTable dtld)
        {
            try
            {
                if (dtld.Rows.Count > 0)
                {
                    grdLandDetails.DataSource = dtld;
                    grdLandDetails.DataBind();
                    grdLandDetails.Visible = true;
                }
                else
                {
                    grdLandDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {
                grdLandDetails.Visible = false;
                ExceptionHandler.ExceptionHistory(Session["UserID"].ToString().Trim(), Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public void BindLandBenifits(DataTable dtld)
        {
            try
            {
                dtld.Columns.Add("DBTAcre", typeof(double));
                dtld.Columns.Add("DBTGunta", typeof(double));
                if (dtld.Rows.Count > 0)
                {
                    grdLandBenifits.DataSource = dtld;
                    grdLandBenifits.DataBind();
                    grdLandBenifits.Visible = true;
                }
                else
                {
                    grdLandBenifits.Visible = false;
                }
            }
            catch (Exception ex)
            {

                grdLandDetails.Visible = false;
                ExceptionHandler.ExceptionHistory(Session["UserID"].ToString().Trim(), Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }

        public void BindLandBenifits(string IDType, int FinancialYearID, string ApplicationID, string FarmerID, string AadharNo)
        {
            try
            {

                DataTable dtld2 = new DataTable();
                dtld2 = Common.BindFarmerLandDetails("FID", FarmerID, "0", ApplicationID, FinancialYearID, "E", "kkisanconstr");
                foreach (GridViewRow row in grdLandBenifits.Rows)
                {
                    for (int i = 0; i < dtld2.Rows.Count; i++)
                    {
                        string BDistrictID = ((HiddenField)row.FindControl("hdnDistrictID")).Value;
                        string BTalukID = ((HiddenField)row.FindControl("hdnTalukID")).Value;
                        string BHobliID = ((HiddenField)row.FindControl("hdnHobliID")).Value;
                        string BVillageID = ((HiddenField)row.FindControl("hdnVillageID")).Value;
                        string BSurveyNo = ((HiddenField)row.FindControl("hdnSurveyNo")).Value;
                        string BLandCode = ((HiddenField)row.FindControl("hdnLandCode")).Value;
                        string BOwnerNo = ((HiddenField)row.FindControl("hdnOwnerNo")).Value;
                        
                        string ADistrictID = dtld2.Rows[i]["DistrictID"].ToString();
                        string ATalukID = dtld2.Rows[i]["TalukID"].ToString();
                        string AHobliID = dtld2.Rows[i]["HobliID"].ToString();
                        string AVillageID = dtld2.Rows[i]["VillageID"].ToString();
                        string ASurveyNo = dtld2.Rows[i]["SurveyNo"].ToString();
                        string ALandCode = dtld2.Rows[i]["LandCode"].ToString();
                        string AOwnerNo = dtld2.Rows[i]["OwnerNo"].ToString();

                        if (BDistrictID == ADistrictID && BTalukID == ATalukID && BHobliID == AHobliID && BVillageID == AVillageID && BSurveyNo == ASurveyNo && BLandCode == ALandCode && BOwnerNo == AOwnerNo)
                        {
                            ((CheckBox)row.FindControl("cbSelect")).Checked = true;
                            // ((CheckBox)row.FindControl("cbForword")).Checked = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                // grdLandBenifits.Visible = false;
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }

        public void BindMachinaryDetail()
        {
            try
            {
                BindMainScheme();
                //BindSchemes();
                //BindKitDetails();
            }
            catch (Exception ex)
            {

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
        //private void BindSchemes()
        //{
        //    try
        //    {
        //        DBTManager dbtManager = new DBTManager();
        //        List<DBTSchemeDetail> schemeDetails = dbtManager.GetSchemeDetails();
        //        if (schemeDetails != null && schemeDetails.Count > 0)
        //        {
        //            ddlSubScheme.DataSource = schemeDetails;
        //            ddlSubScheme.DataTextField = "SchemeName";
        //            ddlSubScheme.DataValueField = "SchemeId";
        //        }
        //        else
        //        {
        //            ddlSubScheme.DataSource = new List<DBTSchemeDetail>();
        //        }
        //        ddlSubScheme.DataBind();
        //        ListItem li = new ListItem("------Select------", "0");
        //        ddlSubScheme.Items.Insert(0, li);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)

        {
            try
            {
                if ((Page.IsValid == true))
                {
                    if (ddlScheme.SelectedItem.Value == "1")
                    {
                        //int DperguntaAmount = !string.IsNullOrEmpty(txtPerGunta.Text) ? KKISAN.Utils.ConversionUtil.ConvertInt(txtPerGunta.Text) : 0 ;
                        //int DguntaAmounttotal = !string.IsNullOrEmpty(txtDemoAmount.Text) ? KKISAN.Utils.ConversionUtil.ConvertInt(txtDemoAmount.Text) : 0;

                        int RDNumberCheck = 0;

                        DataTable dt = new DataTable();
                        dt = (DataTable)ViewState["DTFD"];

                        string FarmerID = dt.Rows[0]["FarmerID"].ToString();

                        string CategoryID = dt.Rows[0]["CategoryID"].ToString();

                        string CasteID = dt.Rows[0]["Caste_ID"].ToString();

                        if (((CategoryID == "S") || (CategoryID == "T")) && (CasteID != ""))
                        {
                            RDNumberCheck = 1;
                        }
                        else if ((CategoryID == "B") || (CategoryID == "O"))
                        {
                            RDNumberCheck = 1;
                        }
                        else
                        {
                            RDNumberCheck = 0;
                        }
                        CalculateLand();
                        double landAcre = (double)ViewState["LandAcre"];
                        double landGunta = (double)ViewState["LandGunta"];
                        double landselected = (double)ViewState["LandSelected"];
                        double ExtGunta = (double)ViewState["ExtGunta"];
                        double ExtAcre = (double)ViewState["ExtAcre"];
                        double landGunta1 = (landGunta / 98.8);
                        double totalGuntas = (ExtAcre * 40) + ExtGunta;
                        //List<DBTKitDetail> KitDetails = (List<DBTKitDetail>)ViewState["KitsAmount"];
                        GetKitAmount();
                        double KitAmount = ViewState["KitAmount"] != null ? (double)ViewState["KitAmount"] : 0;
                        double TargetReleasedHectare = !string.IsNullOrEmpty(txtTargetReleased.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(txtTargetReleased.Text) : 0;
                        double TargetAchievedHectare = !string.IsNullOrEmpty(txtTargetAchieved.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(txtTargetAchieved.Text) : 0;
                        double TargetAchievedHectare1 = TargetAchievedHectare + landGunta1;
                        double DBTTotal = ViewState["TotalDBTAmount"] != null ? (double)ViewState["TotalDBTAmount"] : 0;
                        if (DistrictID == 10 || DistrictID == 16 || DistrictID == 24)
                        {
                            if (landGunta < 4)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minimum 4 Guntas required for DBT !')", true);
                            }
                        }
                        else
                        {
                            if (landGunta < 20)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minimum 20 Guntas required for DBT !')", true);
                            }
                        }
                        if (RDNumberCheck == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('RD Number not updated, Please Update RD Number in Fruits Application ! (Applicable for SC and ST Farmer Ids)')", true);
                        }
                        else if (landselected == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Land!)')", true);
                        }
                        else if (landGunta == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Demo Gunta!)')", true);
                        }
                        else if (landGunta > totalGuntas)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('D.Gunta exceeds the available Gunta !')", true);
                        }
                        //else if(DperguntaAmount>0 && DguntaAmounttotal==0)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please check the Total D.Amt. It might not be calculated.Try again!')", true);
                        //}
                        //else if (landGunta < 20 && (DistrictID != 10 || DistrictID != 16 || DistrictID != 24))
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minimum 20 Guntas are required for DBT !')", true);
                        //}
                        //else if (landGunta < 4 && (DistrictID == 10 || DistrictID == 16 || DistrictID == 24))
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minimum 4 Guntas are required for DBT !')", true);
                        //}
                        else if (landGunta > 200)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Maximum 200 Guntas are allowed for DBT !')", true);
                        }
                        else if (TargetReleasedHectare == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Release the target!')", true);
                        }
                        else if (TargetAchievedHectare1 > TargetReleasedHectare)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Exceeding the Target Released!')", true);
                        }
                        else
                        {
                            btnSubmit.Enabled = false;
                            int FDRes = Common.FarmerDetailsInsert((DataTable)ViewState["DTFD"], ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), "kkisanconstr");
                            if (FDRes >= 1)
                            {
                                int ALDRes = ApplicationLandDetailsInsert((DataTable)ViewState["DTFD"]);
                                if (ALDRes >= 1)
                                {
                                    string SowingDate = KKISAN.Utils.ConversionUtil.ConvertDateTime(txtShowingDate.Text.ToString().Trim()).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                                    //string SowingDate = Utilities.GetDateConversion(txtShowingDate.Text.ToString().Trim());
                                    FarmerRegistration farmerRegistration = new FarmerRegistration()
                                    {
                                        FinancialYearId = ddlFinancialYear.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlFinancialYear.SelectedItem.Value) : 0,
                                        FarmerId = FarmerID,
                                        ApplicationId = ApplicationID,
                                        CropId = ddlCrop.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlCrop.SelectedItem.Value) : 0,
                                        SchemeId = ddlSubScheme.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value) : 0,
                                        TechnologyId = ddlTechnology.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlTechnology.SelectedItem.Value) : 0,
                                        MainSchemeId = ddlScheme.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlScheme.SelectedItem.Value) : 0,
                                        VarietyId = ddlVariety.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlVariety.SelectedItem.Value) : 0,
                                        SowingDate = KKISAN.Utils.ConversionUtil.ConvertDate(SowingDate),
                                        //DemoAmount = !string.IsNullOrEmpty(txtDemoAmount.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(txtDemoAmount.Text) : 0,
                                        DemoAmount = DBTTotal,
                                        DBTAmountPerGunta = !string.IsNullOrEmpty(txtPerGunta.Text) ? KKISAN.Utils.ConversionUtil.ConvertInt(txtPerGunta.Text) : 0,
                                        KitAmount = KitAmount,
                                        LandAcre = (double)ViewState["LandAcre"],
                                        LandGunta = (double)ViewState["LandGunta"],
                                        ApplicationStatus = 1
                                    };
                                    DBTManager dbtManager = new DBTManager();
                                    int ADRes = dbtManager.AddRegistrationDetails2122(farmerRegistration);
                                    if (ADRes >= 1)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "alert('Farmer Registration Success for DBT!');window.location ='DemonstrationSearch2122.aspx?AID=DBT';", true);
                                        // Response.Redirect("~/Demonstration/DemonstrationSearch.aspx?AID=DBT", false);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Application is not submitted , Please check entered details !')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Application (Land Details) not submitted , Please check entered details !')", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Application (Farmer Personnel Details) not submitted , Please check entered details !')", true);
                            }
                            btnSubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        
                        int RDNumberCheck = 0;
                        DataTable dt = new DataTable();
                        dt = (DataTable)ViewState["DTFD"];
                        string FarmerID = dt.Rows[0]["FarmerID"].ToString();
                        string CategoryID = dt.Rows[0]["CategoryID"].ToString();
                        string CasteID = dt.Rows[0]["Caste_ID"].ToString();
                        if (((CategoryID == "S") || (CategoryID == "T")) && (CasteID != ""))
                        {
                            RDNumberCheck = 1;
                        }
                        else if ((CategoryID == "B") || (CategoryID == "O"))
                        {
                            RDNumberCheck = 1;
                        }
                        else
                        {
                            RDNumberCheck = 0;
                        }
                        CalculateNonDemo();
                        double landselected = (double)ViewState["LandSelected"];
                        int Quantity = (int)ViewState["KITQTY"];
                        string SurveyNo = (string)ViewState["SurveyNo"];
                        string HissaNo = (string)ViewState["HissaNo"];
                        double TargetReleasedNumber = !string.IsNullOrEmpty(txtTargetReleased.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(txtTargetReleased.Text) : 0;
                        double TargetAchievedNumber = !string.IsNullOrEmpty(txtTargetAchieved.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(txtTargetAchieved.Text) : 0;
                        double TargetAchievedNumber1 = TargetAchievedNumber + Quantity;
                        int eligiblility = FarmerEligibility();
                        if (eligiblility > 0)
                        {
                            if (eligiblility == 2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Farmer is already taken the Accessible Quantity for this Crop!')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Farmer is already taken the Accessible Quantity for his FID!')", true);
                            }
                        }
                        else if (RDNumberCheck == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('RD Number not updated, Please Update RD Number in Fruits Application ! (Applicable for SC and ST Farmer Ids')", true);
                        }
                        else if (landselected == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Land!')", true);
                        }
                        else if (Quantity == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Kit Quantity!')", true);
                        }
                        else if (Quantity >1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Kit Quantity Should not be greater than 1 !')", true);
                        }
                        else if (TargetReleasedNumber == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Release the target!')", true);
                        }
                        else if (TargetAchievedNumber1 > TargetReleasedNumber)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Exceeding the Target Released!')", true);
                        }
                        else
                        {
                            btnSubmit.Enabled = false;
                            int FDRes = Common.FarmerDetailsInsert((DataTable)ViewState["DTFD"], ApplicationID, Convert.ToInt32(ddlFinancialYear.SelectedValue), "kkisanconstr");
                            if (FDRes >= 1)
                            {
                                int ALDRes = ApplicationLandDetailsInsert((DataTable)ViewState["DTFD"]);
                                if (ALDRes >= 1)
                                {
                                    string SowingDate = KKISAN.Utils.ConversionUtil.ConvertDateTime(txtShowingDate.Text.ToString().Trim()).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                                    NonDemoFarmerRegistration farmerRegistration = new NonDemoFarmerRegistration()
                                    {
                                        FinancialYearId = ddlFinancialYear.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlFinancialYear.SelectedItem.Value) : 0,
                                        FarmerId = FarmerID,
                                        ApplicationId = ApplicationID,
                                        CategoryID = CategoryID,
                                        CropId = ddlCrop.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlCrop.SelectedItem.Value) : 0,
                                        SchemeId = ddlSubScheme.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value) : 0,
                                        MainSchemeId = ddlScheme.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlScheme.SelectedItem.Value) : 0,
                                        VarietyId = ddlVariety.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlVariety.SelectedItem.Value) : 0,
                                        TechnologyID = ddlTechnology.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlTechnology.SelectedItem.Value) : 0,
                                        SupplyAgencyID = ddlSupplyAgency.SelectedItem != null ? KKISAN.Utils.ConversionUtil.ConvertInt(ddlSupplyAgency.SelectedItem.Value) : 0,
                                        SowingDate = KKISAN.Utils.ConversionUtil.ConvertDate(SowingDate),
                                        SurveyNo = (string)ViewState["SurveyNo"],
                                        HissaNo = (string)ViewState["HissaNo"],
                                        Quantity=(int)ViewState["KITQTY"],
                                        ApplicationStatus = 1
                                    };
                                    DBTManager dbtManager = new DBTManager();
                                    int ADRes = dbtManager.AddNonDemoRegistrationDetails2122(farmerRegistration);
                                    if (ADRes >= 1)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "alert('Farmer Registration Success for DBT!');window.location ='DemonstrationSearch2122.aspx?AID=DBT';", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Application is not submitted , Please check entered details !')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Application (Land Details) not submitted , Please check entered details !')", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Application (Farmer Personnel Details) not submitted , Please check entered details !')", true);
                            }
                            btnSubmit.Enabled = true;
                        }
                    }
                }
                else
                {

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
                ClearMachinaryDetails();

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private double GetKitAmount()
        {
            double KitAmount = 0;
            foreach (GridViewRow row in grdKitAmount.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbKitSelect");
                if (ChkBoxRows.Checked)
                {
                    Label lblKitAmount = (Label)row.FindControl("lblKitAmount");
                    KitAmount = KitAmount + (!string.IsNullOrEmpty(lblKitAmount.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(lblKitAmount.Text) : 0);
                }
            }
            ViewState["KitAmount"] = KitAmount;
            return KitAmount;
        }

        private void CalculateLand()
        {
            double LandSelected = 0;
            double DAcre = 0;
            double DGunta = 0;
            double ExtGunta = 0;
            double ExtAcre = 0;
            double TotalDBTAmount = 0;
            foreach (GridViewRow row in grdLandBenifits.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbSelect");
                if (ChkBoxRows.Checked)
                {
                    LandSelected = LandSelected + 1;
                    TextBox txtDAcre = (TextBox)row.FindControl("txtDBTAcre");
                    TextBox txtDGunta = (TextBox)row.FindControl("txtDBTGunta");
                    Label lblEAcre = (Label)row.FindControl("lblExtAcre");
                    Label lblEGunta = (Label)row.FindControl("lblExtGunta");

                    double _extAcre = (!string.IsNullOrEmpty(lblEAcre.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(lblEAcre.Text) : 0);
                    double _extGunta = (!string.IsNullOrEmpty(lblEGunta.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(lblEGunta.Text) : 0);
                    double _dAcre = (!string.IsNullOrEmpty(txtDAcre.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(txtDAcre.Text) : 0);
                    double _dGunta = (!string.IsNullOrEmpty(txtDGunta.Text) ? KKISAN.Utils.ConversionUtil.ConvertDouble(txtDGunta.Text) : 0);

                    double TotalGunta2 = Convert.ToDouble(ViewState["TotalGunta"].ToString());

                    if (_dGunta + TotalGunta2 <= 200)
                    {
                        double TotalAvailableGuntas = (_extAcre * 40) + _extGunta;
                        if (_dGunta > TotalAvailableGuntas)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('D. Gunta exceeds the available Gunta !')", true);
                            txtDGunta.Text = TotalAvailableGuntas.ToString();
                            _dGunta = TotalAvailableGuntas;
                        }

                        ExtAcre = ExtAcre + _extAcre;
                        ExtGunta = ExtGunta + _extGunta;
                        DAcre = DAcre + _dAcre;
                        DGunta = DGunta + _dGunta;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Entered Gunta Exceed the 200 Gunta Please Enter avialble Gunta is " + (200 - TotalGunta2) + "')", true);
                    }
                }
            }

            if (DGunta > 0)
            {
                double perGuntaAmount = KKISAN.Utils.ConversionUtil.ConvertDouble(txtPerGunta.Text);
                double totExtGuntas = (ExtAcre * 40) + DGunta;
                if (DistrictID == 10 || DistrictID == 16 || DistrictID == 24)
                {
                    if (DGunta < 4)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minimum 4 Guntas required for DBT !')", true);
                    }
                    else
                    {
                        txtDemoAmount.Text = (DGunta * perGuntaAmount).ToString();
                        TotalDBTAmount = DGunta * perGuntaAmount;
                    }
                }
                else
                {
                    if (DGunta < 20)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Minimum 20 Guntas required for DBT !')", true);
                    }
                    else
                    {
                        txtDemoAmount.Text = (DGunta * perGuntaAmount).ToString();
                        TotalDBTAmount = DGunta * perGuntaAmount;
                    }
                }

                if (DGunta > totExtGuntas)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('D.Gunta exceeds the available Gunta !')", true);
                }
                
                else if (DGunta > 200)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Maximum 200 Guntas are allowed for DBT !')", true);
                    txtDemoAmount.Text = (200 * perGuntaAmount).ToString();
                    TotalDBTAmount = 200 * perGuntaAmount;
                }
                else
                {
                    txtDemoAmount.Text = (DGunta * perGuntaAmount).ToString();
                    TotalDBTAmount = DGunta * perGuntaAmount;
                }
            }
            ViewState["LandAcre"] = DAcre;
            ViewState["LandGunta"] = DGunta;
            ViewState["LandSelected"] = LandSelected;
            ViewState["ExtGunta"] = ExtGunta;
            ViewState["ExtAcre"] = ExtAcre;
            ViewState["TotalDBTAmount"] = TotalDBTAmount;
        }
        private void CalculateNonDemo()
        {
            double LandSelected = 0;
            int KitQty = 0;
            string SurveyNo = "";
            string HissaNo = "";
            foreach (GridViewRow row in grdLandBenifits.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbSelect");
                if (ChkBoxRows.Checked)
                {
                    LandSelected = LandSelected + 1;
                    TextBox txtkitQty = (TextBox)row.FindControl("txtNonDemoNum");
                    Label lblSurvey = (Label)row.FindControl("lblSurveyNo");
                    Label lblHissa = (Label)row.FindControl("lblHissaNo");
                    KitQty = (!string.IsNullOrEmpty(txtkitQty.Text) ? KKISAN.Utils.ConversionUtil.ConvertInt(txtkitQty.Text) : 0);
                    SurveyNo = (!string.IsNullOrEmpty(lblSurvey.Text) ? KKISAN.Utils.ConversionUtil.ConvertString(lblSurvey.Text) : "");
                    HissaNo = (!string.IsNullOrEmpty(lblHissa.Text) ? KKISAN.Utils.ConversionUtil.ConvertString(lblHissa.Text) : "");
                    double TotalQty = Convert.ToDouble(ViewState["TotalGunta"].ToString());
                }
            }
            ViewState["KITQTY"] = KitQty;
            ViewState["LandSelected"] = LandSelected;
            ViewState["SurveyNo"] = SurveyNo;
            ViewState["HissaNo"] = HissaNo;
         }

        protected void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value) == 1)
            {
                CalculateLand();
            }
            else
            {
                CalculateNonDemo();
            }
            //CalculateLand();
        }

        protected void ddlSubScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCropDetails();
            if (KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value) == 1)
            {
                CalculateLand();
            }
            else
            {
                CalculateNonDemo();
            }
            //CalculateLand();
        }
        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlScheme.SelectedItem.Value=="1")
            {
                lblPhysicalTargettxt.InnerText = "Target Released (Hectare)";
                lblPhysicalTargettxt1.InnerText = "Target Achieved (Hectare)";
                grdLandBenifits.Columns[20].Visible = true;
                grdLandBenifits.Columns[21].Visible = false;
                BindSubScheme();
                DemoSelected();
            }
            else
            {
                lblPhysicalTargettxt.InnerText = "Target Released (Kit In Numbers)";
                lblPhysicalTargettxt1.InnerText = "Target Achieved (Kit In Numbers)";
                grdLandBenifits.Columns[20].Visible = false;
                grdLandBenifits.Columns[21].Visible = true;
                BindSubScheme();
                SeedMiniKitSelected();
                BindSupplyAgencies();
            }
        }
        protected void ddlCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTechnologies();
            BindVarieties();
            if(KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value)==1)
            {
                CalculateLand();
            }
            else
            {
                CalculateNonDemo();
            }
            
        }

        protected void ddlTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDemoAmount();
            BindKitAmount();
            if (KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value) == 1)
            {
                CalculateLand();
            }
            else
            {
                CalculateNonDemo();
            }
            BindTargetProgress();
        }

        private void BindDemoAmount()
        {
            txtPerGunta.Text = string.Empty;
            if ((ddlSubScheme.SelectedItem != null && ddlSubScheme.SelectedIndex > 0) && (ddlCrop.SelectedItem != null && ddlCrop.SelectedIndex > 0)
                && (ddlTechnology.SelectedItem != null && ddlTechnology.SelectedIndex > 0))
            {
                DBTManager marketMgr = new DBTManager();
                int SchemeId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value);
                int CropId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlCrop.SelectedItem.Value);
                int TechnologyId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlTechnology.SelectedItem.Value);
                FarmerRegistration demoDetails = marketMgr.GetDemoAmount(SchemeId, CropId, TechnologyId);
                //if (demoDetails != null && demoDetails.DAmoutGunta > 0)
                if (demoDetails != null)
                {
                    txtPerGunta.Text = demoDetails.DAmoutGunta.ToString();
                }
            }
        }
        
        private void BindKitAmount()
        {
            // _loa
            // grdKitAmount.DataSource = new List<DBTKitDetail>();
            if ((ddlSubScheme.SelectedItem != null && ddlSubScheme.SelectedIndex > 0) && (ddlCrop.SelectedItem != null && ddlCrop.SelectedIndex > 0)
                && (ddlTechnology.SelectedItem != null && ddlTechnology.SelectedIndex > 0))
            {
                DBTManager marketMgr = new DBTManager();
                int SchemeId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value);
                int CropId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlCrop.SelectedItem.Value);
                int TechnologyId = KKISAN.Utils.ConversionUtil.ConvertInt(ddlTechnology.SelectedItem.Value);
                List<DBTKitDetail> kitDetails = marketMgr.GetKitAmount(SchemeId, CropId, TechnologyId);
                //ViewState["KitAmount"] = kitDetails;
                if (kitDetails != null && kitDetails.Count > 0)
                {
                    grdKitAmount.DataSource = kitDetails;
                }
                else
                {
                    grdKitAmount.DataSource = new List<DBTKitDetail>();
                }
                grdKitAmount.DataBind();
                // ViewState["KitsAmount"] = kitDetails;
            }
        }

        protected void txtDBTGunta_TextChanged(object sender, EventArgs e)
        {
            if (KKISAN.Utils.ConversionUtil.ConvertInt(ddlSubScheme.SelectedItem.Value) == 1)
            {
                CalculateLand();
            }
            else
            {
                CalculateNonDemo();
            }
            //CalculateLand();
        }

        public void ClearMachinaryDetails()
        {
            try
            {
                ddlFinancialYear.ClearSelection();
                //ddlFarmerShareType.Items.Clear();
                // ddlItemCategory.Items.Clear();
                //  ddlItem.Items.Clear();
                // ddlSubItem.Items.Clear();
                // ddlManufacture.Items.Clear();
                //  ddlDealer.Items.Clear();
                // ddlManufacture.Items.Clear();
                // lblSubsidy.Text = ""; lblSubItemCost.Text = ""; lblFarmerShare.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindTargetProgress();
        }

        protected void cbKitSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            SelectKitGrid();
        }

        public void SelectKitGrid()
        {
            try
            {
                CheckBox ChkBoxHeader = (CheckBox)grdKitAmount.HeaderRow.FindControl("cbKitSelectAll");
                foreach (GridViewRow row in grdKitAmount.Rows)
                {
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbKitSelect");
                    if (ChkBoxHeader.Checked == true)
                    {
                        ChkBoxRows.Checked = true;
                    }
                    else
                    {
                        ChkBoxRows.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory("", "Common", ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        protected void cbKitSelect_CheckedChanged(object sender, EventArgs e)
        {

        }

        public int ApplicationLandDetailsInsert(DataTable vdtfd)
        {
            try
            {
                int TotalFarmer = 0, SelectedFarmers = 0, LandInserted = 0, LDres = 0;
                foreach (GridViewRow row in grdLandBenifits.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox cbSelected = ((CheckBox)row.FindControl("cbSelect"));
                        if (cbSelected.Checked == true)
                        {
                            SelectedFarmers++;
                            string FarmerID = vdtfd.Rows[0]["FarmerID"].ToString();
                            string DistrictID = ((HiddenField)row.FindControl("hdnDistrictID")).Value.ToString();
                            string TalukID = ((HiddenField)row.FindControl("hdnTalukID")).Value.ToString();
                            string HobliID = ((HiddenField)row.FindControl("hdnHobliID")).Value.ToString();
                            string VillageID = ((HiddenField)row.FindControl("hdnVillageID")).Value.ToString();
                            string SurveyNo = ((Label)row.FindControl("lblSurveyNo")).Text.ToString();
                            string SurveyNoc = ((Label)row.FindControl("lblSurnoc")).Text.ToString();
                            string HissaNo = ((Label)row.FindControl("lblHissaNo")).Text.ToString();
                            string LandCode = ((Label)row.FindControl("lblLandCode")).Text.ToString();
                            string OwnerNo = ((Label)row.FindControl("lblOwnerNo")).Text.ToString();
                            string MainOwnerNo = ((Label)row.FindControl("lblMainOwnerNo")).Text.ToString();
                            string OwnerName = ((Label)row.FindControl("lblOwnerName")).Text.ToString();
                            string Sex = ((Label)row.FindControl("lblGender")).Text.ToString();
                            string RelationShip = ((Label)row.FindControl("lblRelationship")).Text.ToString();
                            string RelativeName = ((Label)row.FindControl("lblRelativename")).Text.ToString();
                            string ExtAcre = ((Label)row.FindControl("lblExtAcre")).Text.ToString();
                            string ExtGunta = ((Label)row.FindControl("lblExtGunta")).Text.ToString();
                            string ExtFGunta = ((Label)row.FindControl("lblExtFGunta")).Text.ToString();

                            string[] Par = { "@FarmerID","@ApplicationID", "@FinancialYearID" ,"@DistrictID","@TalukID" ,"@HobliID","@VillageID" ,"@SurveyNo","@SurveyNoc" ,"@HissaNo" ,"@LandCode",
                            "@OwnerNo", "@MainOwnerNo","@OwnerName","@Sex","@RelationShip","@RelativeName" ,"@ExtAcre" ,"@ExtGunta","@ExtFGunta" };
                            string[] ParVal = { FarmerID,ApplicationID, ddlFinancialYear.SelectedValue, DistrictID,TalukID,HobliID,VillageID,SurveyNo,SurveyNoc,HissaNo,LandCode,
                            OwnerNo,MainOwnerNo,OwnerName,Sex,RelationShip,RelativeName,ExtAcre,ExtGunta,ExtFGunta  };
                            LDres = QueryExecution.ExeData("SP", "[Trans].[SP@ApplicationLandEntry]", Par, ParVal, "kkisanconstr");
                            if (LDres >= 1)
                            {
                                LandInserted++;
                            }
                        }
                        TotalFarmer++;
                    }
                }
                return LandInserted;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
        }

        protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Common.SelectFarmersGrid(grdLandBenifits, 10);
            }

            catch (Exception ex)
            {

            }
        }

        protected void rblIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HandleID();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public void HandleID()
        {
            try
            {
                //if (rblIDType.SelectedValue == "FID")
                //{
                //    txtAadharNo.Visible = false;
                //    rfvtxtAadharNo.Visible = false;
                //    txtFarmerID.Visible = true;
                //    rfvtxtFarmerID.Visible = true;

                //}
                //else if (rblIDType.SelectedValue == "ANO")
                //{
                //    txtAadharNo.Visible = true;
                //    rfvtxtAadharNo.Visible = true;
                //    txtFarmerID.Visible = false;
                //    rfvtxtFarmerID.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
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
        protected void rdbtnSubsidyClaimCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // need to write login
                // ddlManufacture.ClearSelection();
                // ddlDealer.Items.Clear();
                //lblSubsidy.Text = ""; lblSubItemCost.Text = ""; lblFarmerShare.Text = "";
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void BindTargetProgress()
        {
            try
            {
                if (ddlFinancialYear.SelectedItem != null && ddlFinancialYear.SelectedIndex > 0 && DistrictID > 0 && TalukID > 0 && HobliID > 0)
                {
                    int CategotyId = 0;
                    if (rbCast.SelectedValue == "O" || rbCast.SelectedValue == "B")
                    {
                        CategotyId = 1;
                    }
                    else if (rbCast.SelectedValue == "S")
                    {
                        CategotyId = 2;
                    }
                    else if (rbCast.SelectedValue == "T")
                    {
                        CategotyId = 3;
                    }

                    DBTManager dbtManager = new DBTManager();
                    TargetRelease targetRelease = dbtManager.GetTargetReleased2122(ConversionUtil.ConvertToInt(ddlFinancialYear.SelectedValue), DistrictID, TalukID, HobliID, ConversionUtil.ConvertToInt(ddlScheme.SelectedValue), ConversionUtil.ConvertToInt(ddlSubScheme.SelectedValue), ConversionUtil.ConvertToInt(ddlCrop.SelectedValue), ConversionUtil.ConvertToInt(ddlTechnology.SelectedValue), CategotyId);
                    //int FinancialYearId, int DistrictId, int TalukId, int HobliId,int SchemeId, int SubSchemeId,int CropId, int TechnologyId,int CategoryId
                    if (targetRelease != null && targetRelease.TargetReleased > 0)
                    {
                        txtTargetReleased.Text = targetRelease.TargetReleased.ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Release the target from ADA!')", true);
                        txtTargetReleased.Text = "0";
                    }
                    TargetRelease targetAchieved = new TargetRelease();
                    if (ConversionUtil.ConvertToInt(ddlScheme.SelectedValue) == 1)
                    {
                        targetAchieved = dbtManager.GetTargetAchieved2122(ConversionUtil.ConvertToInt(ddlFinancialYear.SelectedValue), DistrictID, TalukID, HobliID, ConversionUtil.ConvertToInt(ddlScheme.SelectedValue), ConversionUtil.ConvertToInt(ddlSubScheme.SelectedValue), ConversionUtil.ConvertToInt(ddlCrop.SelectedValue), ConversionUtil.ConvertToInt(ddlTechnology.SelectedValue), CategotyId);
                    }
                    else
                    {
                        targetAchieved = dbtManager.GetTargetAchieved2122NonDemo(ConversionUtil.ConvertToInt(ddlFinancialYear.SelectedValue), DistrictID, TalukID, HobliID, ConversionUtil.ConvertToInt(ddlScheme.SelectedValue), ConversionUtil.ConvertToInt(ddlSubScheme.SelectedValue), ConversionUtil.ConvertToInt(ddlCrop.SelectedValue), ConversionUtil.ConvertToInt(ddlTechnology.SelectedValue), CategotyId);
                    }
                    
                    if (targetAchieved != null)
                    {
                        txtTargetAchieved.Text = targetAchieved.TargetAchieved.ToString();
                    }
                    else
                    {
                        txtTargetAchieved.Text = "0";
                    }

                }
                else
                {
                    txtTargetReleased.Text = txtTargetAchieved.Text = "0";
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void BindFarmerDetails(string IDType, int FinancialYearID, string ApplicationID, string FarmerID, string AadharNo)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Common.BindFarmerDetails(IDType, FarmerID, AadharNo, ApplicationID, FinancialYearID, "E", "kkisanconstr");
                if (dt.Rows.Count == 1)
                {
                    // ---------------

                    btnGetDetails.Enabled = false;
                    //rblIDType.Enabled = false;
                    txtFarmerID.ReadOnly = true;
                    // txtAadharNo.Enabled = false;


                    //Personal Details
                    lblFarmerNameEng.Text = dt.Rows[0]["FarmerNameEng"].ToString().Trim();
                    lblFarmerNameKan.Text = dt.Rows[0]["FarmerNameKan"].ToString().Trim();
                    lblFarmerID.Text = dt.Rows[0]["FarmerID"].ToString().Trim();
                    rbGender.SelectedValue = dt.Rows[0]["GenderID"].ToString().Trim();
                    lblFatherNameKan.Text = dt.Rows[0]["FarmerNameKan"].ToString().Trim();
                    lblFatherNameEng.Text = dt.Rows[0]["FarmerNameEng"].ToString().Trim();
                    lblLandlineNo.Text = dt.Rows[0]["LandLine"].ToString().Trim();
                    // lblMobileNo.Text = dt.Rows[0]["Mobile"].ToString().Trim();

                    // Address
                    lblDistrictName.Text = dt.Rows[0]["District"].ToString().Trim();
                    lblTalukName.Text = dt.Rows[0]["Taluk"].ToString().Trim();
                    lblHobliName.Text = dt.Rows[0]["Hobli"].ToString().Trim();
                    lblVillageName.Text = dt.Rows[0]["Village"].ToString().Trim();
                    lblResidenceAddress.Text = dt.Rows[0]["ResidentialAddress"].ToString().Trim();

                    //Identification Details
                    lblEpicID.Text = dt.Rows[0]["EPIC"].ToString().Trim();
                    lblRationCardNo.Text = dt.Rows[0]["RationCardID"].ToString().Trim();

                    lblCasteRDNumber.Text = lblRDNumber.Text = dt.Rows[0]["Caste_ID"].ToString().Trim();

                    // Land Details


                    //Bank Details
                    lblBankName.Text = dt.Rows[0]["Bank"].ToString().Trim();
                    lblAccountNo.Text = dt.Rows[0]["AccountNo"].ToString().Trim();
                    lblIFSC.Text = dt.Rows[0]["IFSC"].ToString().Trim();
                    lblDistrictBank.Text = dt.Rows[0]["BankDistrict"].ToString().Trim();
                    lblBranch.Text = dt.Rows[0]["Branch"].ToString().Trim();

                    // Other Details
                    rbCast.SelectedValue = rbCaste.SelectedValue = dt.Rows[0]["CategoryID"].ToString().Trim();
                    rbPh.SelectedValue = dt.Rows[0]["PH"].ToString().Trim();
                    rbMinorities.SelectedValue = dt.Rows[0]["MinorityType"].ToString().Trim();
                    rbMinoritiesType.SelectedValue = dt.Rows[0]["MinoritiesID"].ToString().Trim();


                    rbFarmerType.SelectedValue = dt.Rows[0]["FarmerTypeID"].ToString().Trim();

                    divFarmerDetails.Visible = true;

                }
                else
                {
                    divFarmerDetails.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            finally
            {

            }
        }
        private void SeedMiniKitSelected()
        {
            kitdiv.Visible = false;
            dapgunta.Visible = false;
            tda.Visible = false;
            ddlSpAg.Visible = true;
        }
        private void DemoSelected()
        {
            kitdiv.Visible = true;
            dapgunta.Visible = true;
            tda.Visible = true;
            ddlSpAg.Visible = false;
        }
        
        private void BindSupplyAgencies()
        {
            try
            {
                ddlSupplyAgency.Items.Clear();
                DBTManager marketMgr = new DBTManager();
                List<SupplyAgency> SupplyAgencyDetails = marketMgr.GetSupplyAgencyDetails();
                if (SupplyAgencyDetails != null && SupplyAgencyDetails.Count > 0)
                {
                    ddlSupplyAgency.DataSource = SupplyAgencyDetails;
                    ddlSupplyAgency.DataTextField = "SupplyAgencyNameEng";
                    ddlSupplyAgency.DataValueField = "SupplyAgencyID";
                }
                else
                {
                    ddlSupplyAgency.DataSource = new List<Varieties>();
                }
               
                ddlSupplyAgency.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlSupplyAgency.Items.Insert(0, li);
            }
            catch (Exception ex)
            {

            }
        }
        private int FarmerEligibility()
        {
            string FID=txtFarmerID.Text.Trim();
            double TotalLand = FarmerTotalLand();
            int finYear=ConversionUtil.ConvertToInt(ddlFinancialYear.SelectedValue);
            int Subscheme = ConversionUtil.ConvertToInt(ddlSubScheme.SelectedValue);
            int Crop = ConversionUtil.ConvertToInt(ddlCrop.SelectedValue);
            DBTManager dbtManager = new DBTManager();
            int Applicationcount = dbtManager.GetFarmerApplicationCount(FID, finYear);
            int Applicationcountwithcrop = dbtManager.GetFarmerApplicationCount(FID, finYear, Subscheme, Crop);
            if (Applicationcount>=3)
            {
                return 1;
            }
            else if (Applicationcountwithcrop >= 1)
            {
                return 2;
            }
            else
            {
                if(TotalLand<=2.5 && Applicationcount>=1)
                {
                    return 1;
                }
                if (TotalLand <=5 && Applicationcount >= 2)
                {
                    return 1;                                                                                         
                }
            }
            return 0;
        }
        private double FarmerTotalLand()
        {
            double Land = 0;
            foreach (GridViewRow row in grdLandBenifits.Rows)
            {
                int acre = 0;
                int gunta = 0;
                Label acrelbl = (Label)row.FindControl("lblExtAcre");
                Label guntalbl = (Label)row.FindControl("lblExtGunta");
                acre = (!string.IsNullOrEmpty(acrelbl.Text) ? KKISAN.Utils.ConversionUtil.ConvertInt(acrelbl.Text) : 0);
                gunta = (!string.IsNullOrEmpty(guntalbl.Text) ? KKISAN.Utils.ConversionUtil.ConvertInt(guntalbl.Text) : 0);
                Land = Land+(acre + (gunta / 40));
                
            }
            return Land;
        }
    }
}