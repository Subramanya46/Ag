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
using KKISAN.Services;
using KKISAN.Utils;
using System.ComponentModel;
using KKISAN.Model.DBT;
using KKISAN.Model.FRUITS;
using System.Globalization;

namespace Kisan.Demonstration
{
    public partial class KKISANBenificiaryPushtoFRUITS : System.Web.UI.Page
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["kkisanconstr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    ViewState["SanctionDetails"] = new List<SanctionData>();
                    FMMemberFunction.FinancialYear(ddlFinancialYear, 19, 20, "kkisanconstr");
                    BindSchemes();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHistory(Session["UserID"] != null ? Session["UserID"].ToString() : "", Path.GetFileName(Request.Path), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
        }

        private void LoadFarmersdetails()
        {
            grdDCBillList.Visible = true;
            divDCBill.Visible = true;

            FruitsSyncSanctionService fruitsMgr = new FruitsSyncSanctionService();
            List<SanctionData> SanctionDetails = fruitsMgr.GetSanctionDetails(ddlapplication.SelectedValue.Trim(), Convert.ToInt32(ddlFinancialYear.SelectedValue.Trim()));
            ViewState["SanctionDetails"] = SanctionDetails;
            if (SanctionDetails != null && SanctionDetails.Count > 0)
            {
                grdDCBillList.DataSource = SanctionDetails;
                grdDCBillList.DataBind();
                lblApplicationName.Text = Convert.ToString(SanctionDetails.Count);
            }
            else
            {
                grdDCBillList.DataSource = new List<SanctionData>();
                grdDCBillList.DataBind();
            }
        }



        protected void grdItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDCBillList.PageIndex = e.NewPageIndex;

            LoadFarmersdetails();
        }



        protected void PushToFruits_Click(object sender, EventArgs e)
        {
            try
            {
                string UserId = "agri";
                string Password = "A@gri0603";

                FruitsSyncSanctionService fruitsMgr = new FruitsSyncSanctionService();
                List<SanctionData> SanctionDetails = (List<SanctionData>)ViewState["SanctionDetails"];
                if (SanctionDetails != null && SanctionDetails.Count > 0)
                {
                    foreach (SanctionData sanctionData in SanctionDetails)
                    {
                        DataSet ds = new DataSet();
                        DataTable dtRTCDetails = new DataTable();
                        dtRTCDetails.TableName = "RTCDetails";

                        string Query2 = "";

                        if (sanctionData.ApplicationID == "SD")
                        {
                            Query2 = @"select '1' as DeptCode, SD.DISTRICT as DistrictCode,SD.TALUK as TalukCode,SD.RSK as HobliCode,FD.VillageID as VillageCode,'' as Surveyno,'' as Surnoc,'' as hissano,'' as LandCode,'' as OwnerNo,'' as MainOwenerNo,'' as OwnerName,'' as ext_acre,'' as ext_gunta,'' as ext_fgunta,0 as Dev_Acre,0 as Dev_Gunta,0 as Dev_Fgunta from [SEED].[SeedIssueDetails] as SD inner join Farmer.FarmerDetails FD on FD.ApplicationID=SD.INPUTTYPE and FD.FinancialYearID=SD.FINCYEAR and FD.FarmerID=SD.FARMERID where SD.INPUTTYPE=@ApplicationID AND SD.Farmerid=@FarmerID AND SD.FINCYEAR=@FinancialYearID";
                        }
                        else
                        {
                            Query2 = @"select '1' as DeptCode, DistrictID as DistrictCode,TalukID as TalukCode,HobliID as HobliCode,VillageID as VillageCode,Surveyno as Surveyno,SurveyNoc as Surnoc,hissano,LandCode,OwnerNo,MainOwnerNo as MainOwenerNo,OwnerName,ExtAcre as ext_acre,ExtGunta as ext_gunta,ExtFGunta as ext_fgunta,0 as Dev_Acre,0 as Dev_Gunta,0 as Dev_Fgunta from Farmer.FarmerLandDetails where ApplicationID=@ApplicationID AND farmerid=@FarmerID";
                        }


                        string[] paramNames2 = { "@ApplicationID", "@FarmerID" , "@FinancialYearID" };
                        string[] paramValues2 = { sanctionData.ApplicationID, sanctionData.FID, sanctionData.FinYearId1.ToString() };
                        dtRTCDetails = QueryExecution.DataAdapt("QR", Query2, paramNames2, paramValues2, "kkisanconstr");
                        if (dtRTCDetails != null && dtRTCDetails.Rows.Count > 0)
                        {
                            ds.Tables.Add(dtRTCDetails);

                            ComponentDetail components = fruitsMgr.GetFruitsComponents(sanctionData.SchemeID1, sanctionData.ApplicationID);
                            if (components != null)
                            {

                                String date_transaction = sanctionData.PaymentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                                
                                KKISANWEB.SanctionDataToFruits.SanctionOrderDetailsSoapClient client = new KKISANWEB.SanctionDataToFruits.SanctionOrderDetailsSoapClient();
                                string Response = client.PushBeneficiaryDetails(UserId, Password, sanctionData.FID, string.Concat(components.SchemeId, sanctionData.FinYear1, sanctionData.FID.Substring(3, 13)), "1",
                                     components.SchemeId, components.ComponentTypeId, components.ComponenetId, components.SubComponentId, sanctionData.FinYear1,
                                     sanctionData.SchemeID1SubAmt, "123", date_transaction, sanctionData.ID.ToString(), sanctionData.PaymentMode,
                                     sanctionData.HOA1, "", ds);

                                if (!string.IsNullOrEmpty(Response) && Response.Trim().Contains("Successfully Saved"))
                                {
                                    fruitsMgr.UpdatePushedStatus(sanctionData.ID, sanctionData.ApplicationID);
                                    if (sanctionData.SchemeType == "M" && sanctionData.ApplicationID == "FM")
                                    {
                                        ComponentDetail components2 = fruitsMgr.GetFruitsComponents(sanctionData.SchemeID2, sanctionData.ApplicationID);
                                        if (components != null)
                                        {
                                            string Response2 = client.PushBeneficiaryDetails(UserId, Password, sanctionData.FID, string.Concat(components2.SchemeId, sanctionData.FinYear2, sanctionData.FID.Substring(3, 13),sanctionData.SchemeID2), "1",
                                     components2.SchemeId, components2.ComponentTypeId, components2.ComponenetId, components2.SubComponentId, sanctionData.FinYear2,
                                     sanctionData.SchemeID2SubAmt, "123", sanctionData.PaymentDate.ToString("dd/MM/yyyy"), string.Concat(sanctionData.ID.ToString(),sanctionData.SchemeID2.ToString()), sanctionData.PaymentMode,
                                     sanctionData.HOA2, "", ds);
                                            if (!string.IsNullOrEmpty(Response2) && Response2.Trim().Contains("Successfully Saved"))
                                            {
                                                fruitsMgr.UpdatePushedStatus(sanctionData.ID, sanctionData.ApplicationID);
                                            }
                                            else
                                            {
                                                fruitsMgr.AddErrorDetails(sanctionData.ID, sanctionData.ApplicationID, sanctionData.FinYearId2, sanctionData.FID, sanctionData.SchemeID2, Response2);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    fruitsMgr.AddErrorDetails(sanctionData.ID, sanctionData.ApplicationID, sanctionData.FinYearId1, sanctionData.FID, sanctionData.SchemeID1, Response);
                                }
                            }
                        }
                    }
                    LoadFarmersdetails();
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
                try
                {
                    LoadFarmersdetails();
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


        private void BindSchemes()
        {
            try
            {
                DBTManager dbtManager = new DBTManager();
                List<Applications> schemeDetails = dbtManager.GetApplicationsIds();
                if (schemeDetails != null && schemeDetails.Count > 0)
                {
                    ddlapplication.DataSource = schemeDetails;
                    ddlapplication.DataTextField = "ApplicationID";
                    ddlapplication.DataValueField = "ApplicationID";
                }
                else
                {
                    ddlapplication.DataSource = new List<Applications>();
                }
                ddlapplication.DataBind();
                ListItem li = new ListItem("------Select------", "0");
                ddlapplication.Items.Insert(0, li);
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
                this.LoadFarmersdetails();


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