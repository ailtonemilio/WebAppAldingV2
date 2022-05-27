<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppAldingV2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <asp:HiddenField ID="hdfUserId" runat="server" />
        <div class="form-group">
            <label>User Name</label>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Field Empty" ControlToValidate="txtUserName" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label>Password</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Gender</label>
            <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Male">Male</asp:ListItem>
                <asp:ListItem Value="Female">Female</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Field Empty" ControlToValidate="rblGender" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:CheckBox ID="chbActive" runat="server" /><label>Active</label>
        </div>
        <div class="form-group">
             <label>Province</label>
            <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>AB</asp:ListItem>
                <asp:ListItem>BC</asp:ListItem>
                <asp:ListItem>ON</asp:ListItem>
                <asp:ListItem>QC</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Field Empty" ControlToValidate="ddlProvince" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label>Registration</label>
            <div class="input-group date">
                <div class="input-group-addon">
                    <i class="glyphicon glyphicon-calendar"></i>
                </div>
                 <asp:TextBox ID="txtRegistration" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Field Empty" ControlToValidate="txtRegistration" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
       
        
        <script type="text/javascript">
            $(function () {
                $('[id*=txtRegistration]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "mm/dd/yyyy",
                    language: "en"
                });
            });
        </script>
    </div>

    <div class="row">
        <asp:GridView ID="gvUsers" runat="server" class="table table-bordered table-condensed table-responsive table-hover " AutoGenerateColumns="False" DataKeyNames="UserAldingId" DataSourceID="sdsUsers" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="UserAldingId" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="UserAldingId" />
                <asp:BoundField DataField="UserName" HeaderText="Name" SortExpression="UserName" />
                <asp:BoundField DataField="UserPassword" HeaderText="Password" SortExpression="UserPassword" />
                <asp:CheckBoxField DataField="UserActive" HeaderText="Active" SortExpression="UserActive" />
                <asp:BoundField DataField="UserGender" HeaderText="Gender" SortExpression="UserGender" />
                <asp:BoundField DataField="UserProvince" HeaderText="Province" SortExpression="UserProvince" />
                <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" SortExpression="RegistrationDate" DataFormatString="{0:d}" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="sdsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:CRUDModel %>" SelectCommand="SELECT [UserAldingId], [UserName], [UserPassword], [UserActive], [UserGender], [UserProvince], [RegistrationDate] FROM [UserAlding]"></asp:SqlDataSource>
    </div>

</asp:Content>
