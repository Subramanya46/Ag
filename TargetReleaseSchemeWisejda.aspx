<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" AutoEventWireup="true" CodeBehind="TargetReleaseSchemeWisejda.aspx.cs" Inherits="KKISANWEB.Demonstration.TargetReleaseSchemeWisejda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/form.css" rel="stylesheet" />
    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
        }
        function showNotModal() {
            $("#myModal_not").modal('show');
        }
        function showDeleteNotModal() {
            $("#myModal_Delete").modal('show');
        }
        function showDeleteCantModal() {
            $("#myModal_Delete1").modal('show');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content">
        <div class="container-fluid">
            <div class="block-header">
                <h2 class="top_heading">NFSM
                    <asp:Label ID="lblApplicationName" runat="server"></asp:Label>
                    - Target Entry</h2>
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

                                    <div class="col-md-2">
                                        <label for="email_address">Financial Year <span class="star_color">* </span></label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlFinancialYear" class="form-control" runat="server" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlFinancialYear" runat="server" ErrorMessage="Select Financial Year" ValidationGroup="valSel"
                                                    ControlToValidate="ddlFinancialYear" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Sector <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSector" class="form-control" runat="server" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlSector" runat="server" ErrorMessage="Select Sector" ValidationGroup="valSel"
                                                    ControlToValidate="ddlSector" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Scheme <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlScheme" class="form-control" runat="server" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ErrorMessage="Select Scheme" ValidationGroup="valSel"
                                                    ControlToValidate="ddlScheme" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                           <%-- <asp:Label runat="server" ID="lblHOA" for="email_address"></asp:Label>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Sub Scheme <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSubScheme" OnSelectedIndexChanged="ddlSubScheme_SelectedIndexChanged" class="form-control" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlSubScheme" runat="server" ErrorMessage="Select Sub Scheme" ValidationGroup="valSel"
                                                    ControlToValidate="ddlSubScheme" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="Label2" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Crop <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCrop" class="form-control" runat="server" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCrop" runat="server" ErrorMessage="Select Crop" ValidationGroup="valSel"
                                                    ControlToValidate="ddlCrop" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="Label3" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <label for="email_address">Technology <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlTechnology" class="form-control" runat="server" OnSelectedIndexChanged="ddlTechnology_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlTechnology" runat="server" ErrorMessage="Select Technology" ValidationGroup="valSel"
                                                    ControlToValidate="ddlTechnology" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="Label4" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">District <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlDistrict" class="form-control" runat="server">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlDistrict" runat="server" ErrorMessage="Select District" ValidationGroup="valSel"
                                                    ControlToValidate="ddlDistrict" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                              <asp:Label runat="server" ID="lblSubTreasuryCode" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server">
                                        <div class="input-group">
                                            <label for="email_address">Taluk <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlTaluk" class="form-control" runat="server" OnSelectedIndexChanged="ddlTaluk_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlTaluk" runat="server" ErrorMessage="Select Taluk" ValidationGroup="valSel"
                                                    ControlToValidate="ddlTaluk" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="lblDrawingOfficeCode" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    

                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <label for="email_address">Order No. <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtOrderNo" class="form-control" runat="server" MaxLength="50">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtOrderNo" runat="server" ErrorMessage="Enter Order No." ValidationGroup="valSel"
                                                    ControlToValidate="txtOrderNo" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <label for="email_address">Description <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDescription" class="form-control" runat="server" MaxLength="50" TextMode="MultiLine">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Category <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCategory" class="form-control" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem value="1" selected="True">GEN</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ErrorMessage="Select Category" ValidationGroup="valSel"
                                                    ControlToValidate="ddlCategory" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="Label1" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Physical Target Released by HOA</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedPhysicalHOA" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                            
                                        </div>
                                     </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Target Amount Released by HOA</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmountHOA" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Physical Target Balance</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedPhysical" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Target Amount Balance</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmount" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label id="lblPhysicalTargettxt" runat="server" for="email_address">Physical Target (Hectare) <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtPhysicalTarget" class="form-control" runat="server">
                                                </asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtPhysicalTarget" runat="server" ErrorMessage="Enter Physical Target" ValidationGroup="valSel"
                                                    ControlToValidate="txtPhysicalTarget" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <asp:FilteredTextBoxExtender ID="ftetxtPhysicalTarget" runat="server" FilterMode="ValidChars" ValidChars="{1,2,3,4,5,6,7,8,9,0,.}"
                                                    TargetControlID="txtPhysicalTarget">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                             <asp:Label runat="server" ID="lblPhysicalTarget" for="email_address"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Financial Target <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFinancialTarget" class="form-control" runat="server">
                                                </asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtFinancialTarget" runat="server" ErrorMessage="Enter Financial Target" ValidationGroup="valSel"
                                                    ControlToValidate="txtFinancialTarget" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <asp:FilteredTextBoxExtender ID="ftetxtFinancialTarget" runat="server" FilterMode="ValidChars" ValidChars="{1,2,3,4,5,6,7,8,9,0,.}"
                                                    TargetControlID="txtFinancialTarget">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Category <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCategory1" class="form-control" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem value="2" selected="True">SCP</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCategory1" runat="server" ErrorMessage="Select Category" ValidationGroup="valSel"
                                                    ControlToValidate="ddlCategory1" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="Label6" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Physical Target Released by HOA</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedPhysicalHOA1" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                            
                                        </div>
                                     </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Target Amount Released by HOA</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmountHOA1" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Physical Target Balance</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedPhysical1" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Target Amount Balance</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmount1" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label id="lblPhysicalTargettxt1" runat="server" for="email_address">Physical Target (Hectare) <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtPhysicalTarget1" class="form-control" runat="server">
                                                </asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtPhysicalTarget1" runat="server" ErrorMessage="Enter Physical Target" ValidationGroup="valSel"
                                                    ControlToValidate="txtPhysicalTarget1" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <asp:FilteredTextBoxExtender ID="ftetxtPhysicalTarget1" runat="server" FilterMode="ValidChars" ValidChars="{1,2,3,4,5,6,7,8,9,0,.}"
                                                    TargetControlID="txtPhysicalTarget1">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                             <asp:Label runat="server" ID="lblPhysicalTarget1" for="email_address"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Financial Target <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFinancialTarget1" class="form-control" runat="server">
                                                </asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator ID="rfvtxtFinancialTarget1" runat="server" ErrorMessage="Enter Financial Target" ValidationGroup="valSel"
                                                    ControlToValidate="txtFinancialTarget1" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <asp:FilteredTextBoxExtender ID="ftetxtFinancialTarget1" runat="server" FilterMode="ValidChars" ValidChars="{1,2,3,4,5,6,7,8,9,0,.}"
                                                    TargetControlID="txtFinancialTarget1">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Category <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCategory2" class="form-control" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem value="3" selected="True">TSP</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCategory2" runat="server" ErrorMessage="Select Category" ValidationGroup="valSel"
                                                    ControlToValidate="ddlCategory2" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label runat="server" ID="Label5" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Physical Target Released by HOA</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedPhysicalHOA2" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                            
                                        </div>
                                     </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Target Amount Released by HOA</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmountHOA2" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Physical Target Balance</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedPhysical2" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="col-md-1">
                                        <div class="input-group">
                                            <label for="email_address">Target Amount Balance</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmount2" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label id="lblPhysicalTargettxt2" runat="server" for="email_address">Physical Target (Hectare) <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtPhysicalTarget2" class="form-control" runat="server">
                                                </asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator ID="rfvtxtPhysicalTarget2" runat="server" ErrorMessage="Enter Physical Target" ValidationGroup="valSel"
                                                    ControlToValidate="txtPhysicalTarget2" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <asp:FilteredTextBoxExtender ID="ftetxtPhysicalTarget2" runat="server" FilterMode="ValidChars" ValidChars="{1,2,3,4,5,6,7,8,9,0,.}"
                                                    TargetControlID="txtPhysicalTarget2">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                             <asp:Label runat="server" ID="lblPhysicalTarget2" for="email_address"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Financial Target <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFinancialTarget2" class="form-control" runat="server">
                                                </asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator ID="rfvtxtFinancialTarget2" runat="server" ErrorMessage="Enter Financial Target" ValidationGroup="valSel"
                                                    ControlToValidate="txtFinancialTarget2" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <asp:FilteredTextBoxExtender ID="ftetxtFinancialTarget2" runat="server" FilterMode="ValidChars" ValidChars="{1,2,3,4,5,6,7,8,9,0,.}"
                                                    TargetControlID="txtFinancialTarget2">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <strong class="red"> &nbsp;&nbsp;&nbsp;Note : &nbsp;</strong> To release additional amount,  Please click on <strong class="red"> &nbsp;&nbsp;&nbsp;Clear &nbsp;</strong> button after selection and do the Transaction ! 
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="email_address">&nbsp;</label>
                                        <div class="form-group">
                                            <asp:Button ID="btnGo" runat="server" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Go" OnClick="btnGo_Click" CausesValidation="true" ValidationGroup="valSel" />
                                            <asp:Button ID="btnClear" runat="server" class="btn topgo_button btn-primary m-t-15 waves-effect" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                                            <asp:Button ID="btnExit" runat="server" class="btn topone_button btn-primary m-t-15 waves-effect" Text="Exit" OnClick="btnExit_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-------------------grid ---------------%>
            <div class="row clearfix" runat="server" id="divReleasedList">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card card_top">
                        <div class="header header_padding">
                            <h2 class="heading_padding">
                                <span class="scheme_master">Target Released Transaction List</span>
                            </h2>
                        </div>
                        <div class="body scheme_body">
                            <div class="demo-masked-input">
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdReleasedList" runat="server" AutoGenerateColumns="false" CellPadding="5" CellSpacing="10" Font-Size="Small"
                                                OnPageIndexChanging="grdReleasedList_PageIndexChanging" AllowPaging="true" PageSize="20" OnRowCommand="grdReleasedList_RowCommand">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            SI NO.
                                                        </HeaderTemplate>
                                                       <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSINO" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hdnIdentity1" Value='<%# Bind("ID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnApplicationID" Value='<%# Bind("ApplicationID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnFinancialYearID" Value='<%# Bind("FinancialYearID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnMainSchemeID" Value='<%# Bind("MainSchemeID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnSchemeID" Value='<%# Bind("SchemeID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnCropID" Value='<%# Bind("CropID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnTechnologyID" Value='<%# Bind("TechnologyID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnCategoryID" Value='<%# Bind("CategoryID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnDistrictID" Value='<%# Bind("DistrictID") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnTalukID" Value='<%# Bind("TalukID") %>' />
                                                           <asp:HiddenField runat="server" ID="hdnReleasedBy" Value='<%# Bind("ReleasedBy") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn-danger" Text="Delete" CommandArgument='<%#Container.DataItemIndex %>' CommandName="DELETE_" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" CssClass="btn-danger" Text="Edit" CommandArgument='<%#Container.DataItemIndex %>' CommandName="Edit_" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial_Year" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFinancialYearName" Text='<%# Eval("FinancialYearName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scheme" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblScheme" Text='<%# Eval("SchemeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="HOA" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblHOA" Text='<%# Eval("HOA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="District" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDistrict" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Taluk" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTaluk" Text='<%# Eval("TalukName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Physical arget" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPhysicalTarget" Text='<%# Eval("PhysicalTarget") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Target" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFinancialTarget" Text='<%# Eval("FinancialTarget") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblOrderNo" Text='<%# Eval("OrderNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Released Date" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCreatedOn" Text='<%# Eval("CreatedOn") %>'></asp:Label>
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
                                                         <pagersettings Mode="NumericFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" position="TopAndBottom"/> 

                                                <RowStyle BackColor="#EFF3FB" ForeColor="black" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade in" id="myModal_not" role="dialog">
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

             <div class="modal fade in" id="myModal_Delete" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" runat="server">
                        <div class="modal-header header_models">
                            <button type="button" class="close" data-dismiss="modal">X</button>
                            <h4 class="modal-title header_modal">Warning</h4>
                        </div>
                        <div class="modal-body body_model">
                            <p class="p_model">
                                Are sure want to delete released amount . Once you delete can not revert back ! 
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn btn-danger deflt_btn" OnClick="btnDelete_Click"></asp:Button>
                            <asp:Button runat="server" Text="Close" class="btn btn-default deflt_btn" data-dismiss="modal"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade in" id="myModal_Delete1" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" runat="server">
                        <div class="modal-header header_models">
                            <button type="button" class="close" data-dismiss="modal">X</button>
                            <h4 class="modal-title header_modal">Warning</h4>
                        </div>
                        <div class="modal-body body_model">
                            <p class="p_model">
                                Record can't be deleted, Because ADA already created targets!
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
