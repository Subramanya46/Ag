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
using KKISAN.Model.Farmer;
using KKISAN.Model;
using KKISANDBT;

namespace Kisan.Demonstration
{
    public partial class NFSMDBTPush : System.Web.UI.Page
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string connstr = ConfigurationManager.ConnectionStrings["kkisanconstr"].ConnectionString;

        string ApplicationID = "";
        int DistrictID, TalukID, HobliID;
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
                    //ViewState["ApprovedFarmerDetails"] = new List<FarmerRegistration>();
                    FMMemberFunction.ThreeFinancialYears(ddlFinancialYear, 20, 20, 21, "kkisanconstr");
                    //BindFinanciyear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModalCheckForRsk();", true);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }
        private void LoadDBTFarmerDetails()
        {
            grdDBTFarmersList.Visible = true;
            divDCBill.Visible = true;

            if (ApplicationID == "DBT")
            {
                DBTManager dbtMgr = new DBTManager();
                List<FarmerRegistration> ApprovedFarmerDetails = new List<FarmerRegistration>();
                if(Convert.ToInt32(ddlFinancialYear.SelectedValue) <21)
                {
                    ApprovedFarmerDetails = dbtMgr.GetApprovedFarmersForDBT();
                }
                else
                {
                    ApprovedFarmerDetails = dbtMgr.GetApprovedFarmersForDBT(Convert.ToInt32(ddlFinancialYear.SelectedValue));
                }

                if (ApprovedFarmerDetails != null && ApprovedFarmerDetails.Count > 0)
                {
                    grdDBTFarmersList.DataSource = ApprovedFarmerDetails;
                    grdDBTFarmersList.DataBind();
                    lblcount.Text = Convert.ToString(ApprovedFarmerDetails.Count);
                }
                else
                {
                    grdDBTFarmersList.DataSource = new List<FarmerRegistration>();
                    grdDBTFarmersList.DataBind();
                }
            }
        }

        protected void btnPushToDBT_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    GenerateSanctionDetails();
                    //List<FarmerRegistration> ApprovedFarmerDetails = (List<FarmerRegistration>)ViewState["ApprovedFarmerDetails"];

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


        public void GenerateSanctionDetails()
        {
            try
            {
                UserManager _userMgr = new UserManager();
                UserProfile _userProfile = _userMgr.GetUserProfile("HOA");
                DBTService _dbtService = new DBTService();
                DBTManager _dbtMgr = new DBTManager();
                //FRUITSFarmerService _wsService = new FRUITSFarmerService();

                List<FarmerRegistration> farmersList = new List<FarmerRegistration>();

                if (Convert.ToInt32(ddlFinancialYear.SelectedValue) < 21)
                {
                     farmersList = _dbtMgr.GetApprovedFarmersForDBT();
                }
                else
                {
                     farmersList = _dbtMgr.GetApprovedFarmersForDBT(Convert.ToInt32(ddlFinancialYear.SelectedValue));
                }
                SanctionService _service = new SanctionService();
                int count = 0;
                string farmerRegNos = "'0'";
                string month = DateTime.Now.Month > 9 ? DateTime.Now.Month + "" : "0" + DateTime.Now.Month;

                if (farmersList.Count > 0)
                {
                    _log.Debug("Inside Farmer Count : " + farmersList.Count);

                    foreach (var _farmer in farmersList)
                    {
                        _log.Debug("All tasks completed for Farmer : " + _farmer.FarmerId);
                        farmerRegNos += ",'" + _farmer.FarmerId + "'";
                        _log.Debug(farmerRegNos);
                        FarmerDetail _fdetail = WSFarmerDetail.GetFarmerDetailsFromFRUITS(_farmer.FarmerId);
                        List<FarmerLandDetail> landDetails = WSFarmerDetail.GetFarmerLandDetails(_farmer.FarmerId);
                        if (landDetails.Count > 0)
                        {
                            _log.Debug("Inside landDetails : " + landDetails.Count);

                            FarmerLandDetail _landDetail = landDetails[0];

                            _log.Debug("Category : " + _fdetail.CategoryID);

                            if (_userProfile != null)
                            {
                                BatchDetail _batchdetail = _dbtService.GenerateBatch(ConversionUtil.ConvertInt(month), 2021);
                                try
                                {
                                    if (Convert.ToInt32(ddlFinancialYear.SelectedValue) < 21)
                                    {
                                        SanctionDetail _detail = new SanctionDetail();
                                        _detail.DeptCode = "1";
                                        _detail.FarmerRegNo = _farmer.FarmerId;
                                        _detail.BeneficiaryID = "24" + month + "20" + _farmer.FarmerId.Substring(3, 13);
                                        _detail.ApplCode = 5;
                                        _detail.SchemeID = 24;
                                        _detail.ComponentTypeID = 2;
                                        _detail.ComponentID = 1;
                                        if (_fdetail.CategoryID.Trim().ToUpper() == "S")
                                            _detail.SubComponentID = 2;
                                        else if (_fdetail.CategoryID.Trim().ToUpper() == "T")
                                            _detail.SubComponentID = 3;
                                        else
                                            _detail.SubComponentID = 1;

                                        _detail.PeriodFrom = "01/02/2021";
                                        _detail.PeriodTo = "28/02/2021";
                                        _detail.DDOCode = "14379D";

                                        if (_farmer.DemoAmount > 6000)
                                            _farmer.DemoAmount = 6000;

                                        _detail.SanctionAmount = Convert.ToInt32(_farmer.DemoAmount);
                                        _detail.LGDistrict = _dbtService.GetLGDistrict(_fdetail.DistrictID);
                                        _detail.LGTaluk = _dbtService.GetLGTaluk(_fdetail.DistrictID, _fdetail.TalukID);
                                        _detail.SanctionNo = _batchdetail.SanctionNo;
                                        _detail.FinYear = "2020-21";
                                        _detail.UpdatedOn = DateTime.Now;
                                        _detail.Status = 0;
                                        _detail.Message = "Benefincary ID Generated";
                                        _detail.UpdatedBy = _userProfile.UserID;
                                        _detail.PayMentMode = "K";

                                        DataTable dt2 = _service.GetSanctionDetailsByFarmer("'" + _farmer.FarmerId + "'");
                                        if (dt2.Rows.Count == 0)
                                        {
                                            _service.AddSanctionDetail(_detail);
                                        }

                                        SanctionFarmerDetail _sfarmerDetail = new SanctionFarmerDetail();
                                        _sfarmerDetail.FarmerID = _farmer.FarmerId;
                                        _sfarmerDetail.DeptCode = "1";
                                        _sfarmerDetail.BeneficiaryID = _detail.BeneficiaryID;
                                        _sfarmerDetail.DistrictCode = _landDetail.DistrictID;
                                        _sfarmerDetail.TalukCode = _landDetail.TalukID;
                                        _sfarmerDetail.HobliCode = _landDetail.HobliID;
                                        _sfarmerDetail.VillageCode = _landDetail.VillageID;
                                        _sfarmerDetail.Surveyno = _landDetail.SurveyNo;
                                        _sfarmerDetail.surnoc = _landDetail.SurveyNoc;
                                        _sfarmerDetail.hissano = _landDetail.HissaNo;
                                        _sfarmerDetail.LandCode = _landDetail.LandCode;
                                        _sfarmerDetail.OwnerNo = _landDetail.OwnerNo;
                                        _sfarmerDetail.MainOwenerNo = _landDetail.MainOwnerNo;
                                        _sfarmerDetail.OwnerName = _landDetail.OwnerName;
                                        _sfarmerDetail.ext_acre = _landDetail.ExtAcre;
                                        _sfarmerDetail.ext_gunta = _landDetail.ExtGunta;
                                        _sfarmerDetail.ext_fgunta = _landDetail.ExtFGunta;
                                        _sfarmerDetail.dev_acre = 0;
                                        _sfarmerDetail.dev_gunta = Convert.ToInt32(_farmer.LandGunta);
                                        _sfarmerDetail.dev_fgunta = 0;

                                        var _surveyno = _landDetail.SurveyNo + "/" + _landDetail.SurveyNoc + "/" + _landDetail.HissaNo;
                                        _sfarmerDetail.CSurveryNo = _surveyno;
                                        _sfarmerDetail.DistrictLG = _dbtService.GetLGDistrict(_fdetail.DistrictID);
                                        _sfarmerDetail.TalukLG = _dbtService.GetLGTaluk(_fdetail.DistrictID, _fdetail.TalukID);
                                        _sfarmerDetail.Batch = 0;
                                        _sfarmerDetail.SurveyId = 0;
                                        _sfarmerDetail.UpdatedOn = DateTime.Now;
                                        _sfarmerDetail.UpdatedBy = _userProfile.UserID;

                                        DataTable dt3 = new DataTable();
                                        dt3 = _service.GetSanctionFarmerDetailsByFarmer("'" + _farmer.FarmerId + "'");

                                        if (dt3.Rows.Count == 0)
                                        {
                                            _service.AddSanctionFarmerDetail(_sfarmerDetail);
                                        }
                                        count++;
                                    }
                                    else
                                    {
                                        SanctionDetail _detail = new SanctionDetail();
                                        _detail.DeptCode = "1";
                                        _detail.FarmerRegNo = _farmer.FarmerId;
                                        _detail.BeneficiaryID = "18" + month + "21" + _farmer.FarmerId.Substring(3, 13);
                                        _detail.ApplCode = 5;
                                        _detail.SchemeID = 24;
                                        _detail.ComponentTypeID = 2;
                                        _detail.ComponentID = 1;
                                        if (_fdetail.CategoryID.Trim().ToUpper() == "S")
                                            _detail.SubComponentID = 2;
                                        else if (_fdetail.CategoryID.Trim().ToUpper() == "T")
                                            _detail.SubComponentID = 3;
                                        else
                                            _detail.SubComponentID = 1;

                                        _detail.PeriodFrom = "18/10/2021";
                                        _detail.PeriodTo = "18/11/2021";
                                        _detail.DDOCode = "14379D";

                                        if (_farmer.DemoAmount > 6000)
                                            _farmer.DemoAmount = 6000;

                                        _detail.SanctionAmount = Convert.ToInt32(_farmer.DemoAmount);
                                        _detail.LGDistrict = _dbtService.GetLGDistrict(_fdetail.DistrictID);
                                        _detail.LGTaluk = _dbtService.GetLGTaluk(_fdetail.DistrictID, _fdetail.TalukID);
                                        _detail.SanctionNo = _batchdetail.SanctionNo;
                                        _detail.FinYear = "2021-22";
                                        _detail.UpdatedOn = DateTime.Now;
                                        _detail.Status = 0;
                                        _detail.Message = "Benefincary ID Generated";
                                        _detail.UpdatedBy = _userProfile.UserID;
                                        _detail.PayMentMode = "K";

                                        DataTable dt2 = _service.GetSanctionDetailsByFarmer("'" + _farmer.FarmerId + "'");
                                        if (dt2.Rows.Count == 0)
                                        {
                                            _service.AddSanctionDetail(_detail);
                                        }

                                        SanctionFarmerDetail _sfarmerDetail = new SanctionFarmerDetail();
                                        _sfarmerDetail.FarmerID = _farmer.FarmerId;
                                        _sfarmerDetail.DeptCode = "1";
                                        _sfarmerDetail.BeneficiaryID = _detail.BeneficiaryID;
                                        _sfarmerDetail.DistrictCode = _landDetail.DistrictID;
                                        _sfarmerDetail.TalukCode = _landDetail.TalukID;
                                        _sfarmerDetail.HobliCode = _landDetail.HobliID;
                                        _sfarmerDetail.VillageCode = _landDetail.VillageID;
                                        _sfarmerDetail.Surveyno = _landDetail.SurveyNo;
                                        _sfarmerDetail.surnoc = _landDetail.SurveyNoc;
                                        _sfarmerDetail.hissano = _landDetail.HissaNo;
                                        _sfarmerDetail.LandCode = _landDetail.LandCode;
                                        _sfarmerDetail.OwnerNo = _landDetail.OwnerNo;
                                        _sfarmerDetail.MainOwenerNo = _landDetail.MainOwnerNo;
                                        _sfarmerDetail.OwnerName = _landDetail.OwnerName;
                                        _sfarmerDetail.ext_acre = _landDetail.ExtAcre;
                                        _sfarmerDetail.ext_gunta = _landDetail.ExtGunta;
                                        _sfarmerDetail.ext_fgunta = _landDetail.ExtFGunta;
                                        _sfarmerDetail.dev_acre = 0;
                                        _sfarmerDetail.dev_gunta = Convert.ToInt32(_farmer.LandGunta);
                                        _sfarmerDetail.dev_fgunta = 0;

                                        var _surveyno = _landDetail.SurveyNo + "/" + _landDetail.SurveyNoc + "/" + _landDetail.HissaNo;
                                        _sfarmerDetail.CSurveryNo = _surveyno;
                                        _sfarmerDetail.DistrictLG = _dbtService.GetLGDistrict(_fdetail.DistrictID);
                                        _sfarmerDetail.TalukLG = _dbtService.GetLGTaluk(_fdetail.DistrictID, _fdetail.TalukID);
                                        _sfarmerDetail.Batch = 0;
                                        _sfarmerDetail.SurveyId = 0;
                                        _sfarmerDetail.UpdatedOn = DateTime.Now;
                                        _sfarmerDetail.UpdatedBy = _userProfile.UserID;

                                        DataTable dt3 = new DataTable();
                                        dt3 = _service.GetSanctionFarmerDetailsByFarmer("'" + _farmer.FarmerId + "'");

                                        if (dt3.Rows.Count == 0)
                                        {
                                            _service.AddSanctionFarmerDetail(_sfarmerDetail);
                                        }
                                        count++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _log.Debug(ex.Message);
                                    _log.Debug("Error in Sanction Farmer Id: " + _farmer.FarmerId + " SurveryId : " + _farmer.Caste);
                                }

                                if (count >= 1)
                                {
                                    UploadDataToWebService(farmerRegNos);
                                    count = 0;
                                    farmerRegNos = "'0'";
                                    DBTBatchService _batch = new DBTBatchService();
                                    _batchdetail = _batch.GenerateSanctionNo(_fdetail.DistrictID, _fdetail.TalukID);
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records found');", true);
                }



                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully sent for Payment');", true);
                LoadDBTFarmerDetails();
                //_dbtService.PushDataToDBTFromSanction(_userProfile);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", "alert('" + ex.Message + "')", true);
                _log.Error(ex.Message);
            }
        }


        public void UploadDataToWebService(string farmerIds)
        {
            _log.Debug("Pushing data to DBT Server...");

            SanctionService _service = new SanctionService();
            DataSet ds = new DataSet();
            DataTable dt2 = new DataTable();
            dt2.TableName = "Sanction Details";
            dt2 = _service.GetSanctionDetailsByFarmer(farmerIds);
            ds.Tables.Add(dt2);

            DataTable dt3 = new DataTable();
            dt3 = _service.GetSanctionFarmerDetailsByFarmer(farmerIds);
            ds.Tables.Add(dt3);

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                try
                {
                    // Actions
                    _log.Debug("Pushing data to DBT Server...");
                    DataSet ResposeDS = new DataSet();
                    KKISANWEB.PushSanctionDetailsSoap.PushSanctionDetailsSoapClient client = new KKISANWEB.PushSanctionDetailsSoap.PushSanctionDetailsSoapClient();
                    ResposeDS = client.SanctionDetails("1", "Agri-Maize", "Maize@2020", ds, dt2.Rows.Count, "P");

                    try
                    {
                        if (ResposeDS != null)
                        {
                            var messageTable = ResposeDS.Tables["MESSAGES"];
                            if (messageTable != null && messageTable.Rows.Count > 0)
                            {
                                if (messageTable.Rows[0] != null)
                                {
                                    var _status = Convert.ToString(messageTable.Rows[0]["Processing_Status"]);
                                }
                            }

                            var dataTable = ResposeDS.Tables["Data"];
                            if (dataTable != null && dataTable.Rows.Count > 0)
                            {
                                _log.Debug("Updating sanction details...");
                                foreach (DataRow _row in dataTable.Rows)
                                {
                                    string _farmerRegNo = Convert.ToString(_row["FarmerRegNo"]);
                                    int _status = Convert.ToInt32(_row["Status"]);
                                    string _statusMsg = Convert.ToString(_row["Error"]);

                                    _service.UpdateSanctionStatus(_farmerRegNo, _status, _statusMsg);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", "alert('" + ex.Message + "')", true);
                        _log.Debug("Error while updating DBT status : " + ex.Message);
                        _log.Debug(ex.StackTrace);
                        _log.Error(ex);
                    }

                    DBTManager _fmSerice = new DBTManager();
                    if (Convert.ToInt32(ddlFinancialYear.SelectedValue) < 21)
                    {
                        _fmSerice.UpdateFarmerDetail(farmerIds);
                    }
                    else
                    {
                        _fmSerice.UpdateFarmerDetail(farmerIds, Convert.ToInt32(ddlFinancialYear.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    _log.Debug("Error while pushing to DBT : " + ex.Message);
                    _log.Debug(ex.StackTrace);
                    _log.Error(ex);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", "alert('" + ex.Message + "')", true);
                }
            }
        }


        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    LoadDBTFarmerDetails();
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

        protected void grdItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDBTFarmersList.PageIndex = e.NewPageIndex;

            LoadDBTFarmerDetails();
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

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            if (lblNotMsg1.Text == "KSDACCNFSM2021")
            {
                lblNotMsg1.Text = "";
                checkStatus.Visible = false;
                NFSMPush.Visible = true;
            }
            else
            {
                lblNotMsg1.Text = "";
                checkStatus.Visible = true;
                lblMsg.Text = "Invalid Code. Please Try Again!!";
            }
        }

        protected void openModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showNotModalCheckForRsk();", true);
        }

        private void ExportGridToExcel()
        {



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
                grdDBTFarmersList.AllowPaging = false;
                this.LoadDBTFarmerDetails();


                foreach (TableCell cell in grdDBTFarmersList.HeaderRow.Cells)
                {
                    cell.BackColor = grdDBTFarmersList.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdDBTFarmersList.Rows)
                {

                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdDBTFarmersList.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdDBTFarmersList.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdDBTFarmersList.RenderControl(hw);

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