<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="LoginPage.aspx.cs" 
    Inherits="WebApplicationRooms.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Hotel Booking System</title>
    <style>
        :root {
            --primary: #89253e;
            --secondary: #3a6186;
            --accent: #e74c3c;
            --light: #ecf0f1;
            --dark: #2c3e50;
            --success: #2ecc71;
            --warning: #f39c12;
            --shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            --transition: all 0.3s ease;
        }
        
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        body {
           background: linear-gradient(135deg, var(--primary) 0%, var(--secondary) 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }
        
        .container {
            max-width: 450px;
            width: 100%;
            background: white;
            border-radius: 12px;
            box-shadow: var(--shadow);
            overflow: hidden;
            animation: fadeIn 0.8s ease;
        }
        
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        .header {
            background: var(--primary);
            color: white;
            padding: 25px 30px;
            position: relative;
            overflow: hidden;
            text-align: center;
        }
        
        .header::before {
            content: "";
            position: absolute;
            top: 0;
            right: 0;
            width: 100px;
            height: 100px;
            background: rgba(255, 255, 255, 0.1);
            border-radius: 50%;
            transform: translate(30px, -30px);
        }
        
        .header::after {
            content: "";
            position: absolute;
            bottom: -20px;
            right: -20px;
            width: 60px;
            height: 60px;
            background: rgba(255, 255, 255, 0.1);
            border-radius: 50%;
        }
        
        .header h2 {
            font-size: 28px;
            font-weight: 600;
            margin-bottom: 5px;
            position: relative;
            z-index: 1;
        }
        
        .header p {
            opacity: 0.8;
            font-size: 16px;
            position: relative;
            z-index: 1;
        }
        
        .form-container {
            padding: 30px;
        }
        
        .info-box {
            background: #f8f9fa;
            border-left: 4px solid var(--secondary);
            padding: 15px;
            margin-bottom: 25px;
            border-radius: 4px;
        }
        
        .info-title {
            font-weight: 600;
            margin-bottom: 8px;
            color: var(--dark);
            display: flex;
            align-items: center;
            gap: 8px;
        }
        
        .info-title i {
            color: var(--secondary);
        }
        
        .credentials {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 10px;
            margin-top: 10px;
        }
        
        .credential-item {
            padding: 8px 12px;
            background: white;
            border-radius: 4px;
            font-size: 14px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.05);
        }
        
        .credential-role {
            font-weight: 600;
            color: var(--primary);
        }
        
        .form-group {
            margin-bottom: 25px;
        }
        
        label {
            display: block;
            margin-bottom: 8px;
            font-weight: 500;
            color: var(--dark);
        }
        
        .input-group {
            position: relative;
        }
        
        .input-group i {
            position: absolute;
            left: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: #7f8c8d;
            z-index: 1;
        }
        
        input[type="text"], 
        input[type="password"] {
            width: 100%;
            padding: 14px 15px 14px 45px;
            border: 1px solid #ddd;
            border-radius: 8px;
            font-size: 16px;
            transition: var(--transition);
            background-color: #f9f9f9;
        }
        
        input:focus {
            outline: none;
            border-color: var(--secondary);
            box-shadow: 0 0 0 3px rgba(52, 152, 219, 0.2);
            background-color: white;
        }
        
        .btn-login {
            margin: 12px;
            width: 100%;
            padding: 14px;
            background: var(--secondary);
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: var(--transition);
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            box-shadow: 0 4px 6px rgba(52, 152, 219, 0.3);
        }
        
        .btn-login:hover {
            background: #2980b9;
            transform: translateY(-2px)
            box-shadow: 0 6px 8px rgba(52, 152, 219, 0.4);
            
        }
        
        .btn-login:active {
            transform: translateY(0);
        }
        
        .message {
            padding: 15px;
            border-radius: 8px;
            margin-top: 20px;
            text-align: center;
            font-weight: 500;
            animation: shake 0.5s ease;
        }
        
        @keyframes shake {
            0%, 100% { transform: translateX(0); }
            25% { transform: translateX(-5px); }
            75% { transform: translateX(5px); }
        }
        
        .message-error {
            background-color: rgba(231, 76, 60, 0.1);
            color: var(--accent);
            border: 1px solid rgba(231, 76, 60, 0.3);
        }
        
        .footer {
            text-align: center;
            margin-top: 25px;
            padding-top: 20px;
            border-top: 1px solid #eee;
            color: #7f8c8d;
            font-size: 14px;
        }
        
        @media (max-width: 480px) {
            .container {
                max-width: 100%;
            }
            
            .form-container {
                padding: 20px;
            }
            
            .credentials {
                grid-template-columns: 1fr;
            }
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
           
            <div>
                <h2 style="text-align: center" > &nbsp;</h2>
                <h3 style="text-align: center" > Login</h3>
            </div>
            
            <div class="form-container">

                <div class="form-group">
                    <label for="txtUsername">Username</label>
                    <div class="input-group">
                        <i class="fas fa-user"></i>
                        <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter username"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <div class="input-group">
                        <i class="fas fa-lock"></i>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter password"></asp:TextBox>
                    </div>
                </div>

               <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn-login" />
               
                <h3 style="text-align: center">
                   
                    Or</h3>
               <asp:Button ID="btnLoginAsGuest" runat="server" Text="Create Account"  CssClass="btn-login" OnClick="btnCreateAccount_Click" style="background-color: #2ecc71" />
                
                
                <asp:Label ID="lblError" runat="server" Visible="false" CssClass="message message-error"></asp:Label>
                
                <div class="footer">
                    &nbsp;Hotel Booking System
                </div>
            </div>
        </div>
    </form>
</body>
</html>