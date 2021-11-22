<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" AutoEventWireup="true" CodeBehind="DemonstrationSearch2122.aspx.cs" Inherits="KKISANWEB.Demonstration.DemonstrationSearch2122" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/form.css" rel="stylesheet" />
    <script type="text/javascript">
        function showModal() {
            $("#myModalDcbill").modal('show');
        }
        function showNotModal() {
            $("#myModal_not").modal('show');
        }
    </script>
    <style>
        img {
            display: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function wwww() {
            var abc = "";
            var windowRef = window.open("Add.aspx", "", "directories=no,height=100,width=100,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,top=0,location=no");
            if (!windowRef || typeof windowRef.closed == 'undefined') {
                alert("Pop-up is blocked in your browser, please turn on popup blocker before to view");
            }
            else {
                windowRef.close();
            }
        }
    </script>
    <section class="content">
        <div class="container-fluid">
            <div class="block-header">
                <h2 class="top_heading">
                    <asp:Label ID="lblApplicationName" runat="server"></asp:Label>
                    Demonstration - Application Entry List / ಪ್ರಾತ್ಯಕ್ಷತೆಗಳು- ಅರ್ಜಿ ನಮೂದು ರೈತರ ಪಟ್ಟಿ 
                    <label runat="server" id="dctitle2" />
                    </h2>

            

            </div>
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card card_top">
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master">Enter Details /ವಿವರಗಳನ್ನು ನಮೂದಿಸಿ</span>
                            </h2>
                        </div>
                       
                        <div class="body scheme_body">
                            <div class="demo-masked-input">
                                <div id="divApplicationEntry" runat="server" class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <label for="email_address">&nbsp;</label>
                                            <div class="form-group">
                                                Click on the Button to add new demonstration Application Entry
                                                <asp:Button runat="server" ID="newDCBill" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Application Entry/ಅರ್ಜಿ ನಮೂದು" OnClick="btnNew_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <%--<div class="col-md-2 bottom_zero">
                                        <label for="email_address">Financial Year <span class="star_color">*</span> </label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlFinancialYear" class="form-control" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlFinancialYearID" runat="server" ErrorMessage="Select Financial Year" ValidationGroup="val"
                                                    ControlToValidate="ddlFinancialYear" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>--%>
                                     <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">ಯೋಜನೆ / Scheme</label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlScheme" class="form-control" OnSelectedIndexChanged="ddlSubScheme_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlItemCategory" runat="server" ErrorMessage="Select Scheme" ValidationGroup="valMD" SetFocusOnError="true" ControlToValidate="ddlScheme" CssClass="margin_label" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2 bottom_zero">
                                        <label for="email_address">District /ಜಿಲ್ಲೆ</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlDistrictID" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDistrictID" runat="server" ErrorMessage="Select District" ValidationGroup="val"
                                                ControlToValidate="ddlDistrictID" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="col-md-2 bottom_zero">

                                        <label for="email_address">Taluk /ತಾಲ್ಲೂಕು </label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlTalukID" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlTalukID" runat="server" ErrorMessage="Select Taluk" ValidationGroup="val"
                                                ControlToValidate="ddlTalukID" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="col-md-2 bottom_zero">
                                        <label for="email_address">Hobli /ಹೋಬಳಿ</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlHobliID" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlHobliID" runat="server" ErrorMessage="Select RSK" ValidationGroup="val"
                                                ControlToValidate="ddlHobliID" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-1 bottom_zero">
                                        <label for="email_address">Caste /ಜಾತಿ</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlCaste" class="form-control" runat="server">
                                                <asp:ListItem Selected="True" Text="---ALL---" Value="ALL"></asp:ListItem>
                                                <asp:ListItem  Text="Gen/OT" Value="O"></asp:ListItem>
                                                <asp:ListItem  Text="SC" Value="S"></asp:ListItem>
                                                <asp:ListItem  Text="ST" Value="T"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 bottom_zero" id="statusddl" runat="server">
                                        <label for="email_address">Status</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlStatus" class="form-control" runat="server">
                                                <asp:ListItem Selected="True" Text="---ALL---" Value="0"></asp:ListItem>
                                                <asp:ListItem  Text="Application Registered" Value="1"></asp:ListItem>
                                                <asp:ListItem  Text="Audit" Value="2"></asp:ListItem>
                                                <asp:ListItem  Text="Return for Modification" Value="3"></asp:ListItem>
                                                <asp:ListItem  Text="Approved for Payment" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">&nbsp;</label>
                                            <div class="form-group">
                                                <asp:Button runat="server" CausesValidation="true" ValidationGroup="val" ID="btnGo" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="GO" OnClick="btnGo_Click"></asp:Button>
                                                <asp:Button runat="server" ID="btnExit" class="btn topone_button btn-primary m-t-15 waves-effect" Text="Exit" OnClick="btnExit_Click"></asp:Button>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row clearfix" runat="server" id="divDCBill">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card card_top">
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master">List of Application Entry /ಅರ್ಜಿ ನಮೂದು ಪಟ್ಟಿ</span>
                            </h2>
                        </div>
                        <div class="body scheme_body">
                            <div class="demo-masked-input">
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                           
                                            <asp:GridView ID="grdDCBillList" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdItems_PageIndexChanging"  CssClass="table table-striped table-bordered table-hover">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            SI NO.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSINO" Text='<%# (grdDCBillList.PageSize * grdDCBillList.PageIndex) + Container.DisplayIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Year" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFinancialYearName" Text='<%# Eval("FinancialYear") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="FarmerID" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("FarmerId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FarmerName" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("FarmerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Caste" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("Caste") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="RD Number" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("CasteID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Main Scheme" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("MainSchemeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scheme" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("SchemeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Crop" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCrop" Text='<%# Eval("CropName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Variety" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCrop" Text='<%# Eval("VarietyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTEchnology" Text='<%# Eval("TechnologyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supply Agency" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSupplyAgency" Text='<%# Eval("SupplyAgencyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="D. Amount"  ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDemoAmount" Text='<%# Eval("DemoAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kit Amount"  ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblKitAmount" Text='<%# Eval("KitAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="D. Acre" Visible="false" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAcre" Text='<%# Eval("LandAcre") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="D. Gunta" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGunta" Text='<%# Eval("LandGunta") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Survey No" Visible="false" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSurvey" Text='<%# Eval("SurveyNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hissa No" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblHissa" Text='<%# Eval("HissaNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kit Quantity" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblkitQuantity" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Registered On" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGeneOn" Text='<%# Eval("GeneratedDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Application Status" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("ApplicationStatusName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No Records Found
                                                </EmptyDataTemplate>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#008542" ForeColor="white" />
                                                <PagerStyle CssClass="pagination-ys" />
                                                <PagerSettings Mode="Numeric" PageButtonCount="10" PreviousPageText="Previous" NextPageText="Next" Position="TopAndBottom" />
                                                <RowStyle BackColor="#EFF3FB" ForeColor="#000" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                            <asp:Button runat="server" CausesValidation="true" ValidationGroup="val" ID="Button1" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Export TO XL-Sheet" OnClick="btnExportXL_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          

           
            <div class="modal fade" id="myModal_not" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" runat="server">
                        <div class="modal-header header_models">
                            <button type="button" class="close" data-dismiss="modal">X</button>
                            <h4 class="modal-title header_modal">Notification</h4>
                        </div>
                        <div class="modal-body body_model">
                            <p class="p_model">
                                <asp:Label ID="lblNotMsg" CssClass="year_financial" runat="server"> </asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" Text="Close" class="btn btn-default deflt_btn" data-dismiss="modal"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>

</asp:Content>