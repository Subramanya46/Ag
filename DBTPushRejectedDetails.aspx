<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Kisan.Demonstration.DBTPushRejectedDetails" CodeBehind="DBTPushRejectedDetails.aspx.cs" %>

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
                    Demonstration -  Rejected FARMERS List
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
                                    <div class="col-md-2 bottom_zero">
                                        <label for="email_address">Financial Year <span class="star_color">*</span> </label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlFinancialYear" class="form-control" runat="server" OnTextChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlFinancialYearID" runat="server" ErrorMessage="Select Financial Year" ValidationGroup="val"
                                                    ControlToValidate="ddlFinancialYear" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2 bottom_zero">

                                        <label for="email_address">District</label>
                                        <div class="form-group">

                                            <asp:DropDownList ID="ddlDistrictID" class="form-control" runat="server" OnSelectedIndexChanged="ddlDistrictID_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvddlDistrictID" runat="server" ErrorMessage="Select District" ValidationGroup="val"
                                                ControlToValidate="ddlDistrictID" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="col-md-2 bottom_zero">

                                        <label for="email_address">Taluk</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlTalukID" class="form-control" runat="server" OnSelectedIndexChanged="ddlTalukID_SelectedIndexChanged"  AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlTalukID" runat="server" ErrorMessage="Select Taluk" ValidationGroup="val"
                                                ControlToValidate="ddlTalukID" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="col-md-2 bottom_zero">
                                        <label for="email_address">Hobli</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlHobliID" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlHobliID" runat="server" ErrorMessage="Select RSK" ValidationGroup="val"
                                                ControlToValidate="ddlHobliID" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator>
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
                                <span class="scheme_master">List of Rejected Farmers  From Fruits Or DBT </span>
                            </h2>
                        </div>
                        <div class="body scheme_body">
                            <div class="demo-masked-input">
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                           
                                            <asp:GridView ID="grdRejectfarmers" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdItems_PageIndexChanging"  CssClass="table table-striped table-bordered table-hover">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            SI NO.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSINO" Text='<%# (grdRejectfarmers.PageSize * grdRejectfarmers.PageIndex) + Container.DisplayIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                   
                                                    <asp:TemplateField HeaderText="FarmerID" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("FarmerId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DBT Rejected Message" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("DBTRejectedMessage") %>'></asp:Label>
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
          

           
           

        </div>
    </section>

</asp:Content>



