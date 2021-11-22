<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" AutoEventWireup="true" CodeBehind="DemonstrationRegistrationListJDA.aspx.cs" Inherits="KKISANWEB.Demonstration.DemonstrationRegistrationListJDA" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/form.css" rel="stylesheet" />
    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
        }
        function showNotModal() {
            //$(document).ready(function () {
            //    $("button").click(function () {
            //        $("p").slideToggle();
            //    });
            //});

            $("#myModal_not").modal('show');
        }
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
        $("table").on("scroll", function () {
            $("table > *").width($("table").width() + $("table").scrollLeft());
        });

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
    <%--<style>
        .col-md-6 {
            margin-bottom: 15px !important;
        }

        table {
            width: 100%;
            overflow-x: scroll;
            display: block;
        }

        thead {
            display: block;
        }

        tbody {
            height: 510px;
        }
    </style>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content">
        <div class="container-fluid">
            <div class="block-header">
                <h2 class="top_heading">Demonstration - Farmer Registration List</h2>
            </div>

            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card card_top">
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master">Enter Details /ವಿವರಗಳನ್ನು ನಮೂದಿಸಿ </span>
                            </h2>
                        </div>
                        <div class="body scheme_body">
                            <div class="demo-masked-input">
                                <div class="row clearfix">
                                    <div class="col-md-2">
                                        <label>Financial Year /ಆರ್ಥಿಕ ವರ್ಷ <span class="star_color">*</span> </label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlFinancialYearID" class="form-control" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlFinancialYearID" runat="server" ErrorMessage="Select Financial Year" ValidationGroup="valAccept"
                                                    ControlToValidate="ddlFinancialYearID" InitialValue="0" CssClass="margin_label" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">District <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlDistrict" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Taluk  </label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlTaluk" runat="server" class="form-control" OnSelectedIndexChanged="ddlTaluk_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Hobli <span class="star_color">*</span> </label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlHobli" class="form-control" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Category </label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCategory" class="form-control" runat="server" AutoPostBack="true">
                                                    <asp:ListItem value="ALL" selected="True">All</asp:ListItem>
                                                     <asp:ListItem value="O">GEN</asp:ListItem>
                                                    <asp:ListItem value="B">OBC</asp:ListItem>
                                                     <asp:ListItem value="S">SCP</asp:ListItem>
                                                     <asp:ListItem value="T">TSP</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ErrorMessage="Select Category" ValidationGroup="valSel"
                                                    ControlToValidate="ddlCategory" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="lblcategory" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Application Status</label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlApplicationStatus" class="form-control" runat="server" AutoPostBack="true">
                                                     <asp:ListItem value="4" selected="True" >Accepted</asp:ListItem>
                                                     <asp:ListItem value="2">Pending for Acceptance</asp:ListItem>
                                                     <asp:ListItem value="3">Rejected</asp:ListItem>
                                                    <asp:ListItem value="1">Application Entry</asp:ListItem>
                                                    <asp:ListItem value="0">All Status</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Category" ValidationGroup="valSel"
                                                    ControlToValidate="ddlApplicationStatus" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="Label1" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <label>&nbsp;</label>
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnGo" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Get Details/ವಿವರಗಳನ್ನು ಪಡೆಯಿರಿ" OnClick="btnGo_Click" CausesValidation="true" ValidationGroup="valAccept"></asp:Button>
                                                <asp:Button runat="server" ID="btnExit" class="btn topone_button btn-primary m-t-15 waves-effect" Text="Exit/ನಿರ್ಗಮಿಸಿ" OnClick="btnExit_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row clearfix" runat="server" id="divAcceptance">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card card_top">
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master">List of Farmers</span>
                            </h2>
                        </div>
                        <div class="body scheme_body">
                            <div class="demo-masked-input">
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdFarmerAcceptance" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2"
                                                AllowPaging="true" PageSize="10" CssClass="table table-striped table-bordered table-hover"
                                                OnRowCommand="grdFarmerAcceptance_RowCommand" OnPageIndexChanging="grdFarmerAcceptance_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            SI NO.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSINO" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Preview Image" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton Style="width: 40%;" ID="imgselect" runat="server" ImageUrl="~/images/img.png" OnClick="ImageButton1_Click"
                                                                CommandArgument='<%# (grdFarmerAcceptance.PageSize * grdFarmerAcceptance.PageIndex) + Container.DisplayIndex %>' CommandName="fileu"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Year" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                             <asp:HiddenField runat="server" ID="hdnFarmerID" Value='<%# Bind("FarmerId") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnFarmerName" Value='<%# Bind("FarmerName") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnFinancialYearID" Value='<%# Bind("FinancialYear") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnFinYearID" Value='<%# Bind("FinancialYearId") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnApplicationID" Value='<%# Bind("ApplicationId") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnID" Value='<%# Bind("ID") %>' />
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
                                                    <asp:TemplateField HeaderText="Taluk Name" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("TalukName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hobli Name" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("HobliName") %>'></asp:Label>
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
                                                            <asp:Label runat="server" ID="lblVariety" Text='<%# Eval("VarietyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTechnology" Text='<%# Eval("TechnologyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="D. Amount" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDemoAmt" Text='<%# Eval("DemoAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="D. Acre" Visible="false" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLandAcre" Text='<%# Eval("LandAcre") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="D. Gunta" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLandGunta" Text='<%# Eval("LandGunta") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kit Amount" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblkitamount" Text='<%# Eval("KitAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Registration Date" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSubmissionDate" Text='<%# Eval("GeneratedDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Application Status" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("ApplicationStatusName") %>'></asp:Label>
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
                                <asp:Label ID="lblNotMsg" runat="server"> </asp:Label>
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