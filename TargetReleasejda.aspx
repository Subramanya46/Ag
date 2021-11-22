<%@ Page Title="" Language="C#" MasterPageFile="~/Application.master" AutoEventWireup="true" Inherits="Kisan.Demonstration.TargetReleasejda" Codebehind="TargetReleasejda.aspx.cs" %>

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

                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Sector <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSector" class="form-control" runat="server" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlSector" runat="server" ErrorMessage="Select Sector" ValidationGroup="valSel"
                                                    ControlToValidate="ddlSector" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <label for="email_address">Scheme <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlScheme" class="form-control" runat="server" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ErrorMessage="Select Scheme" ValidationGroup="valSel"
                                                    ControlToValidate="ddlScheme" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <asp:Label runat="server" ID="lblHOA" for="email_address"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
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
                                    <div class="col-md-3" runat="server">
                                        <div class="input-group">
                                            <label for="email_address">Taluk <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlTaluk" class="form-control" runat="server" OnSelectedIndexChanged="ddlTaluk_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlTaluk" runat="server" ErrorMessage="Select Taluk" ValidationGroup="valSel"
                                                    ControlToValidate="ddlTaluk" InitialValue="0" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <asp:Label runat="server" ID="lblDrawingOfficeCode" for="email_address"></asp:Label>
                                        </div>
                                    </div>
                                    </div>
                                <div class="row clearfix">
                                     <div class="col-md-3">
                                        <div class="input-group">
                                            <label for="email_address">Total Target Amount Released  by Head Office</label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmountHOA" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                            
                                        </div>
                                         </div>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <label for="email_address">Total Target Amount Released by JDA </label>
                                            <div class="form-group">
                                                <asp:Label ID="lblTotalReleasedAmount" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                    <%--<div class="col-md-3" runat="server">
                                        <div class="input-group">
                                            <label for="email_address"><strong class="red">Available Balance Amount for Sanction</strong></label>
                                            <div class="form-group">
                                                <asp:Label ID="lblBalAmount" class="form-control" runat="server" ReadOnly="true">
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <br />
                                <div class="row clearfix">

                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <label for="email_address">Physical Target (Hectare) <span class="star_color">* </span></label>
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

                                    <div class="col-md-3">
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

                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <label for="email_address">Order No. <span class="star_color">* </span></label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtOrderNo" class="form-control" runat="server" MaxLength="50">
                                                </asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvtxtOrderNo" runat="server" ErrorMessage="Enter Order No." ValidationGroup="valSel"
                                                    ControlToValidate="txtOrderNo" InitialValue="" CssClass="margin_label" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
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
                                                            <asp:HiddenField runat="server" ID="hdnSchemeID" Value='<%# Bind("SchemeID") %>' />
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
                                                    <asp:TemplateField HeaderText="HOA" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblHOA" Text='<%# Eval("HOA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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

        </div>

    </section>
</asp:Content>

