<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" AutoEventWireup="true" CodeBehind="ApplicationEntry2122.aspx.cs" Inherits="KKISANWEB.Demonstration.ApplicationEntry2122" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>
$(document).ready(function(){ 
    $("txtDBTGunta").keyup(function () {
        $.ajax({
            url: 'Demonstration/ApplicationEntry2122/CalculateLand',
         success: function(data) { alert(data); }, 
         statusCode : {
             404: function(content) { alert('cannot find resource'); },
             500: function(content) { alert('internal server error'); }
         }, 
         error: function(req, status, errorObj) {
               // handle status === "timeout"
               // handle other errors
         }
});
  });
});
</script>--%>
    <script type="text/javascript">
        function showNotModal() {
            $("#myModal_not").modal('show');
        }
    </script>
    <script type="text/javascript">
        function showNotModalCheckForRsk() {
            $("#showNotModalCheckForRsk").modal('show');
        }

        function showNotModalCheckForRsk1() {
            $("#showNotModalCheckForRsk1").modal('show');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("button").click(function () {
                $("p").hide();
            });
        });
        $(document).ready(function () {
            $("#hide").click(function () {
                $("p").hide();
            });
        });
        function DatePick() {
            alert('Please Select Date From Calender');
            return false;
        }

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a day greater than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value("")
            }
        }
    </script>
     <script type="text/javascript">
         function DisableButton() {
            document.getElementById('<%= btnSubmit.ClientID %>').disabled = "disabled";
            __doPostBack('<%= btnSubmit.ClientID %>', '');

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content top_content">
        <div class="container-fluid">
            <%-- <div class="container">--%>
            <div class="block-header header_tops">
                <h2 class="top_heading">Demonstration - Application Entry /ಪ್ರಾತ್ಯಕ್ಷತೆಗಳು - ಅರ್ಜಿ ನಮೂದು  </h2>
            </div>
            <br />
            <div class="row clearfix">

                <div class="col-md-2">
                    <h3 class="h3_top_app">ಹುಡುಕಿ / Search</h3>
                </div>
                <%-- <div class="col-sm-3">

                    <asp:RadioButtonList ID="rblIDType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblIDType_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="ರೈತನ ಐಡಿ / Farmer ID. " Value="FID" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="ಆಧಾರ್ ನಂಬರ್ / Aadhar No." Value="ANO" ></asp:ListItem>
                    </asp:RadioButtonList>
                </div>--%>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label col-md-4">ರೈತನ ಐಡಿ / Farmer ID</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtFarmerID" Class="form-control " runat="server" MaxLength="16" placeholder="ರೈತನ ನೋಂದಣಿ ಸಂಖ್ಯೆ ನಮೂದಿಸಿ / Enter Farmer ID." ToolTip="ರೈತನ ನೋಂದಣಿ ಸಂಖ್ಯೆ ನಮೂದಿಸಿ / Enter Farmer ID."></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtFarmerID" runat="server" ErrorMessage="Enter Farmer ID" ValidationGroup="val"
                                ControlToValidate="txtFarmerID" CssClass="margin_label" Display="Dynamic" Visible="false"></asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="ftetxtFarmerID" runat="server" FilterMode="ValidChars" FilterType="Numbers,LowercaseLetters, UppercaseLetters"
                                TargetControlID="txtFarmerID">
                            </asp:FilteredTextBoxExtender>
                        </div>
                    </div>
                    <%--<asp:TextBox ID="txtAadharNo" Class="form-control control_radius" placeholder="ಆಧಾರ್ ನಂಬರ್ ನಮೂದಿಸಿ / Enter Aadhar No." runat="server" MaxLength="16" ToolTip="Enter Aadhar No. / ಆಧಾರ್ ನಂಬರ್ ನಮೂದಿಸಿ"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtAadharNo" runat="server" ErrorMessage="Enter Aadhar Number" ValidationGroup="val"
                        ControlToValidate="txtAadharNo" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:FilteredTextBoxExtender ID="ftetxtAadharNo" runat="server" FilterMode="ValidChars" FilterType="Numbers"
                        TargetControlID="txtAadharNo">
                    </asp:FilteredTextBoxExtender>--%>
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnGetDetails" runat="server" CssClass="btn topgo_button btn-primary m-t-15 waves-effect" Text="Get Details/ವಿವರಗಳನ್ನು ಪಡೆಯಿರಿ" OnClick="btnGetDetails_Click" CausesValidation="true" ValidationGroup="val"></asp:Button>
                    <asp:Button ID="btnReset" Style="margin-right: 0px;" runat="server" CssClass="btn topone_button btn-primary m-t-15 waves-effect" Text="Clear" CausesValidation="false" OnClick="btnReset_Click"></asp:Button>
                    <asp:Button runat="server" Style="margin-left: 10px;" ID="btnExit" class="btn topone_button btn-primary m-t-15 waves-effect" Text="Exit/ನಿರ್ಗಮಿಸಿ" OnClick="btnExit_Click"></asp:Button>
                </div>

            </div>

            <div class="row clearfix" runat="server" id="divFarmerDetails">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card card_top">
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master">ರೈತನ ಮೂಲ ವಿವರಗಳು / Farmer Basic Details</span>
                            </h2>
                        </div>
                        <div class="body">
                            <div class="row">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ರೈತನ ಹೆಸರು / Farmer Name</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblFarmerNameEng" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ರೈತನ ಐಡಿ / Farmer ID </label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblFarmerID" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ಜಾತಿ / Caste</label>
                                                <div class="col-sm-7" style="left: 1px; top: 0px">
                                                    <asp:RadioButtonList ID="rbCaste" runat="server"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                        <asp:ListItem Text="GEN / OT " Value="O"></asp:ListItem>
                                                        <asp:ListItem Text="SC" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="ST" Value="T"></asp:ListItem>
                                                        <asp:ListItem Text="OBC" Value="B"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">RD Number</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblRDNumber" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="wizard_vertical">
                                <h2>ವೈಯಕ್ತಿಕ ವಿವರಗಳು / Personal Details</h2>
                                <section>
                                    <div class="row">
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ರೈತನ ಹೆಸರು / Farmer Name (Kannada)</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblFarmerNameKan" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5 caste_bottom">ಲಿಂಗ / Gender</label>
                                                <div class="col-sm-7 caste_bottom">
                                                    <asp:RadioButtonList ID="rbGender" runat="server"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                        <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ತಂದೆ ಹೆಸರು / Father Name(Kannada)</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblFatherNameKan" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ತಂದೆ ಹೆಸರು / Father Name(English)</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblFatherNameEng" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ಸ್ಥಿರ ದೂರವಾಣಿ ಸಂಖ್ಯೆ / Landline No. </label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblLandlineNo" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">ಮೊಬೈಲ್ ಸಂಖ್ಯೆ / Mobile No.</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Texttttt"></asp:Label>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </section>

                                <h2>ವಿಳಾಸ / Address</h2>
                                <section>
                                    <div class="row">
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಜಿಲ್ಲೆ / District</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblDistrictName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ತಾಲ್ಲೂಕು / Taluk</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblTalukName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಹೋಬಳಿ / Hobli</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblHobliName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಗ್ರಾಮ / Village</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblVillageName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4" style="padding-right: 0px;">ನಿವಾಸ ವಿಳಾಸ / Residence Address</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblResidenceAddress" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </section>

                                <h2>ಗುರುತಿನ ವಿವರಗಳು / Identification Details</h2>
                                <section>
                                    <div class="row">

                                        <div class="col-md-8">
                                            <div class="col-md-12 top_lineheight_second">
                                                <div class="form-group">
                                                    <label class="control-label col-sm-6 bottom">ಎಪಿಕ್ ಐಡಿ / Epic ID</label>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lblEpicID" runat="server" Text="Texttttt"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 top_lineheight_second">
                                                <div class="form-group top_lineheight">
                                                    <label class="control-label col-sm-6 bottom">ರೇಷನ್ ಕಾರ್ಡ್ ಸಂಖ್ಯೆ / Ration Card No.</label>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lblRationCardNo" runat="server" Text="Texttttt"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12 top_lineheight_second">
                                                <div class="form-group top_lineheight">
                                                    <label class="control-label col-sm-8 bottom">Caste RD Number</label>
                                                    <div class="col-sm-4">
                                                        <asp:Label ID="lblCasteRDNumber" runat="server" Text="Texttttt"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>



                                    </div>
                                </section>
                                <h2>ಭೂಮಿ ವಿವರಗಳು / Land Details</h2>
                                <section>
                                    <div class="col-md-12 top_lineheight">
                                        <div class="table-responsive">
                                            <div class="form-group">
                                                <asp:GridView ID="grdLandDetails" runat="server" AutoGenerateColumns="False" CellPadding="2" CellSpacing="2"
                                                    PageSize="10">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="District" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDistrictName" runat="server" Text='<%#Eval("DistrictName") %>' Font-Size="Small"></asp:Label>
                                                                <asp:HiddenField ID="hdnDistrictID" runat="server" Value='<%#Bind("districtCode") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Taluk" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTalukName" runat="server" Text='<%#Eval("TalukName") %>' Font-Size="Small"></asp:Label>
                                                                <asp:HiddenField ID="hdnTalukID" runat="server" Value='<%#Bind("talukCode") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hobli" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHobliName" runat="server" Text='<%#Eval("HobliName") %>' Font-Size="Small"></asp:Label>
                                                                <asp:HiddenField ID="hdnHobliID" runat="server" Value='<%#Bind("hobliCode") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Village" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVillageName" runat="server" Text='<%#Eval("VillageName") %>' Font-Size="Small"></asp:Label>
                                                                <asp:HiddenField ID="hdnVillageID" runat="server" Value='<%#Bind("villageCode") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Survey No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSurveyNo" runat="server" Text='<%#Eval("surveyno") %>' Font-Size="Small"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Surnoc">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSurnoc" runat="server" Text='<%#Eval("surnoc") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hissa No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHissaNo" runat="server" Text='<%#Eval("hissano") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Land Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLandCode" runat="server" Text='<%#Eval("LandCode") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Owner No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOwnerNo" runat="server" Text='<%#Eval("OwnerNo") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Main Owner No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMainOwnerNo" runat="server" Text='<%#Eval("MainOwnerNo") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Owner Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("OwnerName") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bincom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBincom" runat="server" Text='<%#Eval("bincom") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gender">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGender" runat="server" Text='<%#Eval("sex") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relationship">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRelationship" runat="server" Text='<%#Eval("relationship") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relativename">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRelativename" runat="server" Text='<%#Eval("relativename") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCropName" runat="server" Text='<%#Eval("Crop_Code") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ext Acre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExtAcre" runat="server" Text='<%#Eval("ext_acre") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ext Gunta">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExtGunta" runat="server" Text='<%#Eval("ext_gunta") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ext FGunta">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExtFGunta" runat="server" Text='<%#Eval("ext_fgunta") %>' Font-Size="Small"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No Records Found
                                                    </EmptyDataTemplate>
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#008542" ForeColor="white" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" ForeColor="green" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </section>
                                <h2>ಬ್ಯಾಂಕ್ ವಿವರಗಳು / Bank Details</h2>
                                <section>
                                    <div class="row">
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಬ್ಯಾಂಕ್ ಹೆಸರು / Bank Name</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblBankName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಖಾತೆ ಸಂಖ್ಯೆ / Account No.</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಐ ಎಫ್ ಎಸ್ ಸಿ ಕೋಡ್ / IFSC Code.</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblIFSC" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಬ್ಯಾಂಕ್ ಜಿಲ್ಲೆ / Bank District</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblDistrictBank" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಶಾಖೆ / Branch</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblBranch" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </section>
                                <h2>ಇತರೆ / Others</h2>
                                <section>
                                    <div class="row">
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4 caste_bottom">ಜಾತಿ / Caste</label>
                                                <div class="col-sm-8 caste_bottom" style="left: 1px; top: 0px">
                                                    <asp:RadioButtonList ID="rbCast" runat="server"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                        <asp:ListItem Text="GEN / OT " Value="O"></asp:ListItem>
                                                        <asp:ListItem Text="SC" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="ST" Value="T"></asp:ListItem>
                                                        <asp:ListItem Text="OBC" Value="B"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ವಿಕಲ ಚೇತನ / Specially Abled</label>
                                                <div class="col-sm-8 no_bottom">
                                                    <asp:RadioButtonList ID="rbPh" runat="server"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಅಲ್ಪಸಂಖ್ಯಾತರು / Minorities</label>
                                                <div class="col-sm-8 no_bottom">
                                                    <asp:RadioButtonList ID="rbMinorities" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 top_lineheight">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ಅಲ್ಪಸಂಖ್ಯಾತರ ವಿಧ / Minorities Type</label>
                                                <div class="col-sm-8 no_bottom">
                                                    <asp:RadioButtonList ID="rbMinoritiesType" runat="server"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                        <asp:ListItem Text="Muslim" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Christian" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Sikh" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Buddhist" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Parsi" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Jain" Value="6"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 top_lineheight">

                                            <div class="form-group">
                                                <label class="control-label col-sm-4">ರೈತನ ವರ್ಗ / Farmer Type</label>
                                                <div class="col-sm-8 no_bottom">
                                                    <asp:RadioButtonList ID="rbFarmerType" runat="server"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                        <asp:ListItem Text="Small" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="Marginal" Value="M"></asp:ListItem>
                                                        <asp:ListItem Text="Big" Value="B"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </section>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row clearfix" runat="server" id="divMachinaryDetails">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="card card_top">
                                <div class="header header_padding">
                                    <h2 class="heading_padding">
                                        <span class="scheme_master">ಭೂ ವಿವರಗಳು / Land Details</span>
                                    </h2>
                                </div>
                                <div class="body">
                                    <div class="demo-masked-input">
                                        <div class="row clearfix">
                                            <div class="col-md-9">
                                                <div class="row clearfix">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label for="email_address">ಆರ್ಥಿಕ ವರ್ಷ / Financial Year</label>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true" class="form-control" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlFinancialYear" runat="server" ErrorMessage="Select Financial Year" ValidationGroup="valMD" SetFocusOnError="true"
                                                                    ControlToValidate="ddlFinancialYear" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label for="email_address">ಯೋಜನೆ / Scheme</label>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlScheme" class="form-control" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlItemCategory" runat="server" ErrorMessage="Select Scheme" ValidationGroup="valMD" SetFocusOnError="true"
                                                                    ControlToValidate="ddlScheme" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label for="email_address">ಯೋಜನೆ /Sub Scheme</label>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlSubScheme" class="form-control" OnSelectedIndexChanged="ddlSubScheme_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Scheme" ValidationGroup="valMD" SetFocusOnError="true"
                                                                    ControlToValidate="ddlSubScheme" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label for="email_address">ಬೆಳೆ / Crop</label>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlCrop" class="form-control" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlItem" runat="server" ErrorMessage="Select Crop" ValidationGroup="valMD" SetFocusOnError="true"
                                                                    ControlToValidate="ddlCrop" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                                <br />
                                                <div class="row clearfix">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label for="email_address">Variety</label>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlVariety"  AutoPostBack="true" class="form-control" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Variety" ValidationGroup="valMD" SetFocusOnError="true"
                                                                    ControlToValidate="ddlVariety" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label for="email_address">ತಂತ್ರಜ್ಞಾನ / Technology</label>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlTechnology" OnSelectedIndexChanged="ddlTechnology_SelectedIndexChanged" AutoPostBack="true" class="form-control" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Technology" ValidationGroup="valMD" SetFocusOnError="true"
                                                                    ControlToValidate="ddlTechnology" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" id="ddlSpAg" runat="server">
                                                        <div class="input-group">
                                                            <label for="email_address">Supply Agency</label>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlSupplyAgency"  AutoPostBack="true" class="form-control" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Supply Agency" ValidationGroup="valMD" SetFocusOnError="true"
                                                                    ControlToValidate="ddlSupplyAgency" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label for="email_address">Sowing Date</label>
                                                            <div class="form-group">
                                                               <%-- <asp:TextBox Textmode="Date" ID="txtShowingDate"" class="form-control" runat="server" MaxLength="10"> </asp:TextBox>--%>
                                                                <asp:TextBox Textmode="Date" ID="txtShowingDate" runat="server" CssClass="form-control control_height" MaxLength="10" autocomplete="off"
                                                                    oncopy="return false;" oncut="return false;" onkeypress="javascript:return DatePick()"
                                                                    onkeydown="javascript:return DatePick();" onpaste="return false;"></asp:TextBox>
                                                               <%-- <cc1:CalendarExtender ID="CalendarExtenderPaid" runat="server" PopupButtonID="txtShowingDate"
                                                                    OnClientDateSelectionChanged="checkDate" TargetControlID="txtShowingDate" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>--%>
                                                                <asp:RequiredFieldValidator ID="rfvtxtPaidDate" runat="server" ControlToValidate="txtShowingDate"
                                                                    ErrorMessage="Select Sowing Date " CssClass="amount_date" ForeColor="Red" SetFocusOnError="True" ValidationGroup="valMD" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                                 <br />
                                                <div class="row clearfix">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label id="lblPhysicalTargettxt" runat="server"  for="email_address">Target Released (Hectare)</label>
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTargetReleased" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group">
                                                            <label id="lblPhysicalTargettxt1" runat="server" for="email_address">Target Achieved (Hectare)</label>
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTargetAchieved" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" id="dapgunta" runat="server">
                                                        <div class="input-group">
                                                            <label for="email_address">D.Amt(Per Gunta)</label>
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtPerGunta" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" id="tda" runat="server">
                                                        <div class="input-group" >
                                                            <label for="email_address">Total D.Amt</label>
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtDemoAmount" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3" id="kitdiv" runat="server">
                                                <div class="col-md-12 top_lineheight">
                                                    <label for="email_address">
                                                        Kit Details
                                                    </label>
                                                    <div class="table-responsive">
                                                        <div class="form-group">
                                                            <asp:GridView ID="grdKitAmount" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2"
                                                                PageSize="10">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                        <HeaderTemplate>
                                                                            Select
                                                                               <asp:CheckBox ID="cbKitSelectAll" OnCheckedChanged="cbKitSelectAll_CheckedChanged" AutoPostBack="true"  runat="server" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbKitSelect" OnCheckedChanged="cbKitSelect_CheckedChanged" runat="server" Font-Size="Small"  AutoPostBack="true" />
                                                                            <%--<asp:HiddenField ID="hdnLandIdentity" runat="server" Value='<%#Bind("ID") %>' />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Kit Name" ItemStyle-HorizontalAlign="center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblKitName" runat="server" Font-Size="Small" Text='<%# Eval("KitName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount (Rs.)" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblKitAmount" runat="server" Font-Size="Small" Text='<%# Eval("KitAmount") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <span class="no_records">No Records Found</span>
                                                                </EmptyDataTemplate>
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#008542" ForeColor="white" />
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EFF3FB" ForeColor="#000" Font-Bold="true" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-12">
                                                <div class="col-md-12 top_lineheight">
                                                    <label for="email_address">
                                                        ಪ್ರಯೋಜನ ಪಡೆದುಕೊಳ್ಳಲು ಸರ್ವೆ ಸಂಖ್ಯೆಗಳನ್ನು  ಆಯ್ಕೆಮಾಡಿ / Choose Survey No for which availing benefit
                                                    </label>
                                                    <div class="table-responsive">
                                                        <div class="form-group">
                                                            <asp:GridView ID="grdLandBenifits" runat="server" AutoGenerateColumns="False" CellPadding="2" CellSpacing="2"
                                                                PageSize="10">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                        <HeaderTemplate>
                                                                            Select
                                                                               <asp:CheckBox ID="cbSelectAll" AutoPostBack="true" OnCheckedChanged="cbSelectAll_CheckedChanged" runat="server" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbSelect" runat="server" Font-Size="Small" OnCheckedChanged="cbSelect_CheckedChanged" AutoPostBack="true" />
                                                                            <%--<asp:HiddenField ID="hdnLandIdentity" runat="server" Value='<%#Bind("ID") %>' />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="Forword" ItemStyle-HorizontalAlign="center">
                                                                        <ItemTemplate>
                                                                              <asp:CheckBox ID="cbForword" runat="server" AutoPostBack="true" OnCheckedChanged="cbForword_CheckedChanged" Font-Size="Small" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="District" ItemStyle-HorizontalAlign="center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDistrictName" runat="server" Text='<%#Eval("DistrictName") %>' Font-Size="Small"></asp:Label>
                                                                            <asp:HiddenField ID="hdnDistrictID" runat="server" Value='<%#Bind("districtCode") %>'></asp:HiddenField>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Taluk" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTalukName" runat="server" Text='<%#Eval("TalukName") %>' Font-Size="Small"></asp:Label>
                                                                            <asp:HiddenField ID="hdnTalukID" runat="server" Value='<%#Bind("talukCode") %>'></asp:HiddenField>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Hobli" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHobliName" runat="server" Text='<%#Eval("HobliName") %>' Font-Size="Small"></asp:Label>
                                                                            <asp:HiddenField ID="hdnHobliName" runat="server" Value='<%#Bind("HobliName") %>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="hdnHobliID" runat="server" Value='<%#Bind("hobliCode") %>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Village" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVillageName" runat="server" Text='<%#Eval("VillageName") %>' Font-Size="Small"></asp:Label>
                                                                            <asp:HiddenField ID="hdnVillageID" runat="server" Value='<%#Bind("villageCode") %>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Survey No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSurveyNo" runat="server" Text='<%#Eval("surveyno") %>' Font-Size="Small"></asp:Label>
                                                                            <asp:HiddenField ID="hdnSurveyNo" runat="server" Value='<%#Bind("surveyno") %>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surnoc" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSurnoc" runat="server" Text='<%#Eval("surnoc") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Hissa No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHissaNo" runat="server" Text='<%#Eval("hissano") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Land Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLandCode" runat="server" Text='<%#Eval("LandCode") %>' Font-Size="Small"></asp:Label>
                                                                            <asp:HiddenField ID="hdnLandCode" runat="server" Value='<%#Bind("LandCode") %>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Owner No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOwnerNo" runat="server" Text='<%#Eval("OwnerNo") %>' Font-Size="Small"></asp:Label>
                                                                            <asp:HiddenField ID="hdnOwnerNo" runat="server" Value='<%#Bind("OwnerNo") %>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Main Owner No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMainOwnerNo" runat="server" Text='<%#Eval("MainOwnerNo") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Owner Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOwnerName" runat="server" Text='<%#Eval("OwnerName") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bincom" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBincom" runat="server" Text='<%#Eval("bincom") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gender">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGender" runat="server" Text='<%#Eval("sex") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Relationship">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRelationship" runat="server" Text='<%#Eval("relationship") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Relativename" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRelativename" runat="server" Text='<%#Eval("relativename") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Crop Name" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCropName" runat="server" Text='<%#Eval("Crop_Code") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ext Acre">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExtAcre" runat="server" Text='<%#Eval("ext_acre") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ext Gunta">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExtGunta" runat="server" Text='<%#Eval("ext_gunta") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="D. Acre" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDBTAcre" runat="server" Width="50px" Text='<%#Eval("DBTAcre") %>' Font-Size="Small"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="D. Gunta">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDBTGunta" runat="server" Width="50px" OnTextChanged="txtDBTGunta_TextChanged" AutoPostBack="true" Text='<%#Eval("DBTGunta") %>' Font-Size="Small"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="No. Of Kits">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNonDemoNum" runat="server" Width="50px" OnTextChanged="txtDBTGunta_TextChanged" AutoPostBack="true" Text='<%#Eval("DBTGunta") %>' Font-Size="Small"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ext FGunta" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExtFGunta" runat="server" Text='<%#Eval("ext_fgunta") %>' Font-Size="Small"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No Records Found
                                                                </EmptyDataTemplate>
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#008542" ForeColor="white" />
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EFF3FB" ForeColor="green" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            </asp:GridView>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--<div class="col-md-3">
                                                <div class="col-md-12">
                                                    <div class="input-group">
                                                        <label for="email_address">Land (Acre)</label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtAcre" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Land" ValidationGroup="valMD" SetFocusOnError="true"
                                                                ControlToValidate="txtAcre" CssClass="margin_label" InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="col-md-12">
                                                    <div class="input-group">
                                                        <label for="email_address">Land (Gunta)</label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtGunta" ReadOnly="true" Class="form-control" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Land" ValidationGroup="valMD" SetFocusOnError="true"
                                                                ControlToValidate="txtGunta" CssClass="margin_label" InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <br />
                                        <div class="row clearfix">
                                            <div class="col-md-12 text-center">
                                                <asp:Button class="btn topgo_button btn-primary m-t-15 waves-effect" runat="server" ID="btnSubmit" Text="Submit" UseSubmitBehavior="false" OnClientClick="DisableButton();" OnClick="btnSubmit_Click" CausesValidation="true" ValidationGroup="valMD"></asp:Button>
                                                <asp:Button class="btn topone_button btn-primary m-t-15 waves-effect" runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CausesValidation="false"></asp:Button>
                                            </div>
                                            <br />
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <div class="modal fade" id="myModal_not" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" runat="server">
                    <div class="modal-header header_models">
                        <button type="button" class="close" data-dismiss="modal">X</button>
                        <h4 class="modal-title header_modal">Notification</h4>
                    </div>
                    <div class="modal-body body_model">
                        <p class="p_model">
                            <asp:Label ID="lblNotMsg" runat="server"> </asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" Text="Close" class="btn btn-default deflt_btn" data-dismiss="modal"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="showNotModalCheckForRsk" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" runat="server">
                    <div class="modal-header header_models">
                        <button type="button" class="close" data-dismiss="modal">X</button>
                        <h4 class="modal-title header_modal">Notification</h4>
                    </div>
                    <div class="modal-body body_model">
                        <p class="p_model">
                            <asp:Label ID="lblNotMsg1" runat="server"> </asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" Text="Close" class="btn btn-default deflt_btn" data-dismiss="modal"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
         <div class="modal fade" id="showNotModalCheckForRsk1" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content" runat="server">
                    <div class="modal-header header_models">
                        <button type="button" class="close" data-dismiss="modal">X</button>
                        <h4 class="modal-title header_modal">Notification</h4>
                    </div>
                    <div class="modal-body body_model">
                        <p class="p_model">
                            <asp:Label ID="lblNotMsg2" runat="server"> </asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" Text="Close" class="btn btn-default deflt_btn" data-dismiss="modal"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

    </section>

    <br />
    <br />
</asp:Content>
