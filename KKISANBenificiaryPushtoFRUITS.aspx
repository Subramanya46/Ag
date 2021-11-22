<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Kisan.Demonstration.KKISANBenificiaryPushtoFRUITS" CodeBehind="KKISANBenificiaryPushtoFRUITS.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: lightgray;
            z-index: 99;
            opacity: 0.1;
            filter: alpha(opacity=80);
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px dashed #f00;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
            margin: 0 auto;
            text-align: center;
            padding-top: 35px;
        }

        .modal-backdrop.in {
            position: unset !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content">
        <div class="container-fluid">
            <div class="block-header">
                <h2 class="top_heading">Pushing Sanction Data to FRUITS
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
                                    <div class="col-md-3">
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
                                    <div class="col-md-3">
                                        <label for="email_address">Applications <span class="star_color">*</span> </label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlapplication" class="form-control" runat="server">
                                                    <%--<asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="AP">AP</asp:ListItem>
                                                    <asp:ListItem Value="FM">FM</asp:ListItem>
                                                    <asp:ListItem Value="MI">MI</asp:ListItem>
                                                    <asp:ListItem Value="KBY">KBY</asp:ListItem>
                                                    <asp:ListItem Value="SD">SD</asp:ListItem>
                                                    <asp:ListItem Value="SOM">SOM</asp:ListItem>
                                                     <asp:ListItem Value="FM-DIST">Distribution(FM)</asp:ListItem>
                                                     <asp:ListItem Value="AP-DIST">Distribution(AP)</asp:ListItem>
                                                    <asp:ListItem Value="DBT">DBT</asp:ListItem>
                                                    <asp:ListItem Value="PMK">PM-KISAN</asp:ListItem>--%>                                                    
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlApplication" runat="server" ErrorMessage="Select Application Type" ValidationGroup="val"
                                                    ControlToValidate="ddlApplication" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4">
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
                                <span class="scheme_master">Farmer Details</span>
                            </h2>
                        </div>
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master" style="text-align: center">Total Count Farmers  :
                                    <asp:Label ID="lblApplicationName" class="scheme_master" runat="server"></asp:Label></span>
                            </h2>
                        </div>
                        <div class="body scheme_body">
                            <div class="demo-masked-input">
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdDCBillList" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdItems_PageIndexChanging" CssClass="table table-striped table-bordered table-hover">
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
                                                    <asp:TemplateField HeaderText="ApplicationID" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblApplicationID" Text='<%# Eval("ApplicationID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FarmerID" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFarmerID" Text='<%# Eval("FID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SchemeType" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSchemeType" Text='<%# Eval("SchemeType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="FinYear1" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFinYear1" Text='<%# Eval("FinYear1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HOA1" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblHOA1" Text='<%# Eval("HOA1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Scheme1 Amount" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSchemeType" Text='<%# Eval("SchemeID1SubAmt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="FinYear2" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFinYear2" Text='<%# Eval("FinYear2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HOA2" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblHOA2" Text='<%# Eval("HOA2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Scheme2 Amount" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblScheme2Amt" Text='<%# Eval("SchemeID2SubAmt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     
                                                    <asp:TemplateField HeaderText="Generated Date" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFarmerID" Text='<%# Eval("PaymentDate") %>'></asp:Label>
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

                                            <div class="form-group bottom_zero">
                                            <div class="loading">
                                                <div>
                                                    Loading. Please wait.<br />
                                                    <br />
                                                    <img id="div3" src="../../images/loading.gif" alt="loading" />
                                                </div>
                                            </div>

                                            <asp:Button runat="server" CausesValidation="true" ValidationGroup="val" ID="Button1" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Export TO XL-Sheet" OnClick="btnExportXL_Click"></asp:Button>
                                            <asp:Button runat="server" CausesValidation="true" ValidationGroup="val" ID="Button2" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Push TO Fruits" OnClick="PushToFruits_Click" OnClientClick="ShowProgress();"></asp:Button>
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


        <script type="text/javascript">
            function ShowProgress() {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.css("vertical-align", "middle");
                    loading.css("display", "table-cell");
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
            $(document).ready(function () {
                $("button").click(function () {
                    //$("#div1").fadeIn();
                    //$("#div2").fadeIn("slow");
                    $("#div3").fadeIn(5000);
                });
            });
        </script>
    </section>

</asp:Content>


