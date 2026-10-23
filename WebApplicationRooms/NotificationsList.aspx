<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotificationsList.aspx.cs" Inherits="WebApplicationRooms.NotificationsList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Notifications | Hotel System</title>
    <style>
        :root {
            --primary: #89253e;
            --secondary: #3a6186;
            --light: #f8f9fa;
            --dark: #2d3748;
            --border: #e2e8f0;
        }
        
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', Arial, sans-serif;
        }
        
        body {
            background: var(--light);
            min-height: 100vh;
            color: var(--dark);
        }
        
        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
        }
        
        .header {
            background: white;
            padding: 20px;
            margin-bottom: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        
        .header h1 {
            color: var(--primary);
            font-size: 24px;
        }
        
        .back-btn {
            padding: 8px 16px;
            background: var(--primary);
            color: white;
            text-decoration: none;
            border-radius: 5px;
            font-weight: 600;
        }

        .notifications-container {
            display: grid;
            gap: 15px;
        }
        
        .notification-card {
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            border-left: 4px solid var(--primary);
            position: relative;
        }

        .notification-header {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            margin-bottom: 10px;
        }

        .notification-title {
            font-size: 16px;
            font-weight: 600;
            color: var(--dark);
        }

        .notification-time {
            font-size: 12px;
            color: #718096;
        }

        .notification-message {
            color: #4a5568;
            line-height: 1.5;
            margin-bottom: 10px;
        }

        .delete-btn {
            background: #dc3545;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 3px;
            cursor: pointer;
            font-size: 12px;
        }

        .delete-btn:hover {
            background: #c82333;
        }

        .empty-state {
            text-align: center;
            padding: 40px 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }
        
        .empty-state i {
            font-size: 48px;
            margin-bottom: 15px;
            color: #cbd5e0;
        }

        .empty-state h3 {
            color: var(--dark);
            margin-bottom: 10px;
        }

        .empty-state p {
            color: #718096;
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1><i class="fas fa-bell"></i> My Notifications</h1>
                <a href="BookingsDashboard.aspx" class="back-btn">
                    <i class="fas fa-arrow-left"></i> Back to Dashboard
                </a>
            </div>

            <div class="notifications-container">
                <asp:Repeater ID="rptNotifications" runat="server" OnItemCommand="rptNotifications_ItemCommand">
                    <ItemTemplate>
                        <div class="notification-card">
                            <div class="notification-header">
                                <div class="notification-title"><%# Eval("Title") %></div>
                                <div class="notification-time"><%# Convert.ToDateTime(Eval("CreatedAt")).ToString("MMM dd, yyyy hh:mm tt") %></div>
                            </div>
                            
                            <div class="notification-message">
                                <%# Eval("Message") %>
                            </div>
                            
                            <div style="text-align: right;">
                                <asp:Button ID="btnDelete" runat="server" 
                                    Text="Delete" 
                                    CssClass="delete-btn"
                                    CommandName="Delete" 
                                    CommandArgument='<%# Eval("Id")%>'
                                    OnClientClick="return confirm('Are you sure you want to delete this notification?');" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:Panel ID="pnlNoNotifications" runat="server" Visible="false">
                    <div class="empty-state">
                        <i class="fas fa-bell-slash"></i>
                        <h3>No Notifications</h3>
                        <p>You don't have any notifications at the moment.</p>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>