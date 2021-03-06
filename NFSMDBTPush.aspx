<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Kisan.Demonstration.NFSMDBTPush" CodeBehind="NFSMDBTPush.aspx.cs" %>

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
    <script type="text/javascript">
        function showNotModalCheckForRsk() {
            $("#showNotModalCheckForRsk").modal('show');
        }

        function showNotModalCheckForRsk1() {
            $("#showNotModalCheckForRsk1").modal('show');
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
                           
                            <asp:TextBox ID="lblNotMsg1" CssClass="form-control form-control-sm" runat="server"> </asp:TextBox>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" ID="check" Text="Check" OnClick="Unnamed_Click" CssClass="btn btn-primary btn-sm" />
                        <asp:Button runat="server" Text="Close" class="btn btn-default deflt_btn" data-dismiss="modal"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section  class="content" runat="server" id="checkStatus">
         <asp:Label runat="server" ForeColor="Red" ID="lblMsg"></asp:Label>
        <br />
        <asp:Button runat="server" ID="openModal" CssClass="btn btn-primary btn-sm" OnClick="openModal_Click" Text="Enter Code" />
    </section>

    <section class="content" runat="server" id="NFSMPush" visible="false">

        <div class="container-fluid">
            <div class="block-header">
                <h2 class="top_heading">
                    <asp:Label ID="lblApplicationName" runat="server"></asp:Label>
                    Demonstration -  APPROVAL FOR DBT 
                    <label runat="server" id="dctitle2" />
                </h2>
            </div>
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card card_top">
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master">Enter Details</span>
                            </h2>
                        </div>

                        <div class="body scheme_body">
                            <div class="demo-masked-input">

                                <div class="row clearfix">
                                    <div class="col-md-3 bottom_zero">
                                        <label for="email_address">Financial Year <span class="star_color">*</span> </label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlFinancialYear" class="form-control" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlFinancialYearID" runat="server" ErrorMessage="Select Financial Year" ValidationGroup="val"
                                                    ControlToValidate="ddlFinancialYear" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2 bottom_zero">--%>


                                    <%-- <div class="form-group">
                                            <label for="email_address">Schemes</label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlScheme" class="form-control" runat="server" OnSelectedIndexChanged="ddlschemes_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>

                                    <%-- </div>--%>

                                    <%-- <div class="col-md-2 bottom_zero">
                                        <label for="email_address">Catagory <span class="star_color"></span></label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlcatagory" class="form-control" runat="server">
                                                    <asp:ListItem Selected="True" Text="---ALL---" Value="ALL"></asp:ListItem>
                                                    <asp:ListItem Text="Gen" Value="O"></asp:ListItem>
                                                     <asp:ListItem Text=" OBC" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="SC" Value="S"></asp:ListItem>
                                                    <asp:ListItem Text="ST" Value="T"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-md-3 bottom_zero">
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
        </div>


        <div class="row clearfix" runat="server" id="divDCBill">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card card_top">
                    <div class="header header_padding">
                        <h2 class="heading_padding">
                            <span class="scheme_master">List of Approved Farmers</span>
                        </h2>
                    </div>
                    <div class="header header_padding">
                        <h2 class="heading_padding">
                            <span class="scheme_master" style="text-align: center">Total Count Farmers  :
                                    <asp:Label ID="lblcount" class="scheme_master" runat="server"></asp:Label></span>
                        </h2>
                    </div>
                    <div class="body scheme_body">
                        <div class="demo-masked-input">
                            <div class="row clearfix">
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:GridView ID="grdDBTFarmersList" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2"
                                            AllowPaging="true" PageSize="10" OnPageIndexChanging="grdItems_PageIndexChanging" CssClass="table table-striped table-bordered table-hover">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        SI NO.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSINO" Text='<%# (grdDBTFarmersList.PageSize * grdDBTFarmersList.PageIndex) + Container.DisplayIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FarmerID" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblFID" Text='<%# Eval("FarmerId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          <%--      <asp:TemplateField HeaderText="Scheme" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Eval("SchemeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Crop" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCrop" Text='<%# Eval("CropName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Technology" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTEchnology" Text='<%# Eval("TechnologyName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="D. Amount" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDemoAmount" Text='<%# Eval("DemoAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Kit Amount" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblKitAmount" Text='<%# Eval("KitAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                              <%--  <asp:TemplateField HeaderText="D. Acre" Visible="false" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAcre" Text='<%# Eval("LandAcre") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="D. Gunta" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblGunta" Text='<%# Eval("LandGunta") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Generated On" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblGeneOn" Text='<%# Eval("GeneratedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("ApplicationStatusName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                        <asp:Button runat="server" CausesValidation="true" ValidationGroup="val" ID="Button2" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Push TO Fruits" OnClick="btnPushToDBT_Click"></asp:Button>
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



