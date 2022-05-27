<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppAldingV2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <div class="panel panel-default">
          <div class="panel-heading">CRUD User</div>
          <div class="panel-body">
                <div class="col-lg-4 form-group">
                    <asp:HiddenField ID="hdfUserId" runat="server" Value="0" />
                    <div>
                        <label>User Name</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Field Empty" ControlToValidate="txtUserName" ForeColor="#FF3300" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <label>Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" 
                            ToolTip="For the Password to be valid it is necessary: Field cannot be empty.
                            length = 8, must have at least 2 upper case, 3 special characters and 1 number."></asp:TextBox>
                         <div id="dvChangePassword" runat="server" visible="false"><asp:CheckBox ID="chbChangePassword" runat="server" OnCheckedChanged="chbChangePassword_CheckedChanged" /><label>&nbsp;Change</label></div>
                        <div id="PasswordAlert" class="alert-danger" runat="server" visible="false">
                            <p>For the Password to be valid it is necessary:
                                Field cannot be empty.
                                length = 8, must have at least 2 upper case, 3 special characters and 1 number.
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 form-group">
                    <div>
                        <label>Gender</label>
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Male">&nbsp;Male&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Female">&nbsp;Female&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem>&nbsp;Other</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Field Empty" ControlToValidate="rblGender" ForeColor="#FF3300" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:CheckBox ID="chbActive" runat="server" /><label>&nbsp;Active</label>
                    </div>
                </div>
                <div class="col-lg-4 form-group">
                    <div>
                         <label>Province</label>
                        <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>AB</asp:ListItem>
                            <asp:ListItem>BC</asp:ListItem>
                            <asp:ListItem>ON</asp:ListItem>
                            <asp:ListItem>QC</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Field Empty" ControlToValidate="ddlProvince" ForeColor="#FF3300" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <label>Registration</label>
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="glyphicon glyphicon-calendar"></i>
                            </div>
                             <asp:TextBox ID="txtRegistration" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Field Empty" ControlToValidate="txtRegistration" ForeColor="#FF3300" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                    </div>
                </div>
          </div>
            <div class="panel-footer">
                <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="AddUser" />&nbsp;
                <asp:Button ID="btnClear" CssClass="btn btn-primary" runat="server" Text="Clear fields" OnClick="btnClear_Click" />&nbsp;
                <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>
    
    <div class="row">
        
        <div class="col-lg-2">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" title="Name User"></asp:TextBox>
        </div>
        <div class="col-lg-2">
            <asp:DropDownList ID="ddlSearchPro" runat="server" CssClass="form-control">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>AB</asp:ListItem>
                <asp:ListItem>BC</asp:ListItem>
                <asp:ListItem>ON</asp:ListItem>
                <asp:ListItem>QC</asp:ListItem>
        </asp:DropDownList>
        </div>
        <div class="col-lg-2">
            <asp:CheckBox ID="chbSearch" runat="server" /><label>&nbsp;Active</label>
        </div>
        <div class="col-lg-2">
            <asp:RadioButtonList ID="rblGenderSearch" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="Male">&nbsp;Male&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="Female">&nbsp;Female&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem>&nbsp;Other</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <div class="col-lg-2">
        <div class="input-group date">
            <div class="input-group-addon">
                <i class="glyphicon glyphicon-calendar"></i>
            </div>
            <asp:TextBox ID="txtRegistrationSearch" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        </div>
        <div class="col-lg-2">
            <button type="button" class="btn btn-default" id="btnSearch" OnServerClick="btnSearch_Click" runat="server" ><span class="glyphicon glyphicon-zoom-in"></span>&nbsp;&nbsp;Search</button>
        </div>
              
    </div>

    <div class="row">
         <div class="panel panel-default">
          <div class="panel-body">
                <asp:GridView ID="gvUsers" runat="server" class="table table-bordered table-condensed table-responsive table-hover " AutoGenerateColumns="False" DataKeyNames="UserAldingId" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" EditImageUrl="~/Images/addlist.png" SelectImageUrl="~/Images/addlist.png" >
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Id" InsertVisible="False" SortExpression="UserAldingId">
                        <EditItemTemplate>
                            <asp:Label ID="lblIdUser" runat="server" Text='<%# Eval("UserAldingId") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIdUser" runat="server" Text='<%# Bind("UserAldingId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserName" HeaderText="Name" SortExpression="UserName" />
                    <asp:TemplateField HeaderText="Password" SortExpression="UserPassword" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserPassword") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserPassword") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="UserActive" HeaderText="Active" SortExpression="UserActive" />
                    <asp:BoundField DataField="UserGender" HeaderText="Gender" SortExpression="UserGender" />
                    <asp:BoundField DataField="UserProvince" HeaderText="Province" SortExpression="UserProvince" />
                    <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" SortExpression="RegistrationDate" DataFormatString="{0:d}" />
                </Columns>
                <SelectedRowStyle Font-Bold="True" VerticalAlign="Bottom" BackColor="Silver"/>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:CRUDModel %>" SelectCommand="SELECT [UserAldingId], [UserName], [UserPassword], [UserActive], [UserGender], [UserProvince], [RegistrationDate] FROM [UserAlding]">
            </asp:SqlDataSource>

              <asp:SqlDataSource ID="sdsUsersSearch" runat="server" ConnectionString="<%$ ConnectionStrings:CRUDModel %>" SelectCommand="SELECT [UserAldingId], [UserName], [UserPassword], [UserActive], [UserGender], [UserProvince], [RegistrationDate] FROM [UserAlding] WHERE (([UserName] LIKE '%' + @UserName + '%') OR ([UserProvince] LIKE '%' + @UserProvince + '%'))">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtSearch" Name="UserName" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtSearch" Name="UserProvince" PropertyName="Text" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>
          </div>

        </div>
        
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
    <script type="text/javascript">
        $(function () {
            $('[id*=txtRegistrationSearch]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "mm/dd/yyyy",
                language: "en"
            });
        });
    </script>
</asp:Content>
