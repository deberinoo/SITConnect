<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SITConnect.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous"/>
</head>
<body>
    <!-- Responsive navbar-->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand" href="Home.aspx">SITConnect</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation"><span class="navbar-toggler-icon"></span></button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                    <li class="nav-item"><a class="nav-link" href="Home.aspx">Home</a></li>
                    <li class="nav-item"><a class="nav-link active" href="Profile.aspx" id="profileBtn" runat="server">Profile</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Page content-->
    <div class="container">
        <div class="text-center mt-5 mb-3">
            <h1>Change password</h1>
        </div>
        <form id="form1" runat="server">
            <div class="mb-3">
                <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Current password</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="password" class="form-control" ID="tb_cpassword" placeholder="Current password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your current password" ControlToValidate="tb_cpassword" ForeColor="Red"></asp:RequiredFieldValidator>                    
                </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">New password</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="password" class="form-control" ID="tb_npassword" placeholder="New password"></asp:TextBox>
                    <small id="passwordHelp" class="form-text text-muted">Password needs a combination of at least 12 lower-case, upper-case, numbers & special characters.</small>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter your new password" ControlToValidate="tb_npassword" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter a password that fulfils all the requirements" ForeColor="Red" Display="Dynamic" EnableClientScript="False"
                        ControlToValidate="tb_npassword" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Confirm password</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="password" class="form-control" ID="tb_cfpassword" placeholder="Confirm password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter your new password again" ControlToValidate="tb_cfpassword" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a password that fulfils all the requirements" ForeColor="Red" Display="Dynamic" EnableClientScript="False"
                        ControlToValidate="tb_cfpassword" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group row">
                <asp:Button ID="btn_update" runat="server" class="btn btn-primary btn-block" Text="Change password" OnClick="btn_update_Click" />
            </div>
        </form>
    </div>
    <!-- Bootstrap core JS-->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
