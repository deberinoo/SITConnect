﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SITConnect.Homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
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
                    <li class="nav-item"><a class="nav-link active" href="Home.aspx">Home</a></li>
                    <li class="nav-item"><a class="nav-link" href="Profile.aspx" id="profileBtn" runat="server">Profile</a></li>
                    <li class="nav-item"><a class="nav-link" href="Login.aspx" id="loginBtn" runat="server">Login</a></li>
                    <li class="nav-item"><a class="nav-link" href="Register.aspx" id="registerBtn" runat="server">Register</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Page content-->
    <div class="container">
        <div class="text-center mt-5">
            <h1>SITConnect</h1>
            <p class="lead">Your most trusted stationary store</p>
            <p>Designed by Deborah Lu</p>
        </div>
    </div>
    <!-- Bootstrap core JS-->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
