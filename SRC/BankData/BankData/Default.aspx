<%@ Page Title="Bank Data" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BankData._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>My bank transactions</h1>
        <p class="lead">Import your bank file (.OFX) here...</p>

        <asp:FileUpload ID="FileUploadBank" runat="server" class="form-control-file" />
        <asp:Button ID="btnImport" class="btn btn-info btnupload" runat="server" Text="Import File" OnClick="btnImport_Click" />
    </div>

    <asp:Panel ID="panelAlert" runat="server">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <asp:Literal ID="literalAlert" runat="server"></asp:Literal>
    </asp:Panel> 


    <div class="row">
        <div class="accordion" id="accordion2">
            <div class="accordion-group">
            <div class="accordion-heading">
                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne">
                Search Options
                </a>
            </div>
            <div id="collapseOne" class="accordion-body collapse">
                <div class="accordion-inner">
                    <div class="col-md-4 navbar-btn">
                        <label for="txtStartDate">Start Date </label>
                        <asp:TextBox class="form-control" name="txtStartDate" ID="txtStartDate" runat="server" placeholder="DD/MM/YYYY" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-md-4 navbar-btn">
                        <label for="txtEndDate">End Date  </label>
                        <asp:TextBox class="form-control" name="txtEndDate" ID="txtEndDate" runat="server" placeholder="DD/MM/YYYY" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-md-4 navbar-btn ">
                        <asp:Button ID="btnSearch" CssClass="btn btn-bottom " runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                    
                </div>
            </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <h2>Bank statement</h2>
            <asp:GridView ID="gv" runat="server" CssClass="table table-hover table-striped" AllowSorting="true" GridLines="None" EmptyDataText="No data found." AutoGenerateColumns="False" OnSorting="gv_Sorting" >
                <Columns>
                    <asp:BoundField DataField="BankId" HeaderText="Bank Id" SortExpression="BankId"/>
                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                    <asp:BoundField DataField="DatePosted" HeaderText="Transaction Date" SortExpression="DatePosted" />
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate><span <%# DisplayAmount(Eval("Amount")) %>><%# Eval("Amount") %></span></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Memo" HeaderText="Description" SortExpression="Memo" />
                </Columns>
            </asp:GridView>
            <asp:HiddenField ID="hdSortText" runat="server" />
        </div>
    </div>

</asp:Content>
