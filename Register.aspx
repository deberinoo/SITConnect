<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SITConnect.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Account</title>
    <link rel="stylesheet" href="site.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
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
                    <li class="nav-item"><a class="nav-link" href="Login.aspx">Login</a></li>
                    <li class="nav-item"><a class="nav-link active" href="Register.aspx">Register</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Page content-->
    <div class="container"> 
         <div class="text-center mt-5 mb-3">
            <h1>Welcome to SITConnect!</h1>
            <h3 class="lead">Create your account</h3>
        </div>
        <form id="form1" runat="server">
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">First Name</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="text" class="form-control" ID="tb_fname" placeholder="First Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your first name" ControlToValidate="tb_fname" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Last Name</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="text" class="form-control" ID="tb_lname" placeholder="Last Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter your last name" ControlToValidate="tb_lname" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Email Address</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="text" class="form-control" ID="tb_email" placeholder="Email Address"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter your email address" ControlToValidate="tb_email" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>                
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a valid email address" ForeColor="Red" Display="Dynamic" EnableClientScript="False"
                        ControlToValidate="tb_email" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Password</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="password" class="form-control" ID="tb_password" placeholder="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter a password" ControlToValidate="tb_password" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter a password that fulfils all the requirements" ForeColor="Red" Display="Dynamic" EnableClientScript="False"
                        ControlToValidate="tb_email" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$"></asp:RegularExpressionValidator>
                    <span class="show-pass" onclick="toggle()">
                        <i class="far fa-eye" onclick="myFunction(this)"></i>
                    </span>
                    <!-- Password Complexity-->
                    <div id="popover-password">
                    <p><span id="result"></span></p>
                    <div class="progress">
                        <div id="password-strength"
                             class="progress-bar"
                             role="progressbar"
                             aria-valuenow="40"
                             aria-valuemin="0"
                             aria-valuemax="100"
                             style="width:0%">
                        </div>
                    </div>
                    <ul class="list-unstyled">
                        <li class="">
                            <span class="low-upper-case">
                                <i class="fas fa-circle" aria-hidden="true"></i>
                                &nbsp;Lowercase &amp; Uppercase
                            </span>
                        </li>
                        <li class="">
                            <span class="one-number">
                                <i class="fas fa-circle" aria-hidden="true"></i>
                                &nbsp;Number (0-9)
                            </span>
                        </li>
                        <li class="">
                            <span class="one-special-char">
                                <i class="fas fa-circle" aria-hidden="true"></i>
                                &nbsp;Special Character (!#$%^&*)
                            </span>
                        </li>
                        <li class="">
                            <span class="eight-character">
                                <i class="fas fa-circle" aria-hidden="true"></i>
                                &nbsp;At least 12 Characters
                            </span>
                        </li>
                    </ul>
                </div>
            </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Date of Birth</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" type="date" class="form-control" ID="tb_dob" placeholder="Date of Birth"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter your date of birth" ControlToValidate="tb_dob" ForeColor="Red"></asp:RequiredFieldValidator>                   
                </div>
            </div>
            <hr />
            <div class="form-group row">
                    <label class="col-sm-2 col-form-label">Card Information</label>
                    <div class="col-5">
                        <asp:TextBox runat="server" type="text" class="form-control" ID="tb_cardnum" placeholder="Card Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter your card number" ControlToValidate="tb_cardnum" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter a valid card number" ForeColor="Red" Display="Dynamic" EnableClientScript="false"
                            ControlToValidate="tb_email" ValidationExpression="^4[0-9]{12}(?:[0-9]{3})?$">
                        </asp:RegularExpressionValidator>
                    </div>
                    <div class="col">
                        <asp:TextBox runat="server" type="text" class="form-control" ID="tb_cardexp" placeholder="Expiry Date (MMYY)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter your card expiry date" ControlToValidate="tb_cardexp" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Enter a valid card expiry date" ForeColor="Red" Display="Dynamic" EnableClientScript="false"
                            ControlToValidate="tb_email" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col">
                        <asp:TextBox runat="server" type="text" class="form-control" ID="tb_cardcvv" placeholder="CVV"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter your card CVV" ControlToValidate="tb_cardcvv" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Enter a valid card number" ForeColor="Red" Display="Dynamic" EnableClientScript="false"
                            ControlToValidate="tb_email" ValidationExpression="\d{3}"></asp:RegularExpressionValidator>
                    </div>
            </div>
            <input type="hidden" id="g-recaptcha-reponse" name="g-recaptcha-response" />
            <div class="form-group row">
                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-block" Text="Submit" OnClick="btn_Submit_Click" />
            </div>
        </form>
    </div>
    <!-- Bootstrap core JS-->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://kit.fontawesome.com/1c2c2462bf.js" crossorigin="anonymous"></script>
    <script src="site.js"></script>
</body>
</html>
