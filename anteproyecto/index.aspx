<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="anteproyecto.portada" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="StyleSheetPortada.css" />
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="fondo">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="dEARmE-removebg-preview.png" class="logo" OnClick="irAIndex"/>
        </div>
        <div class="my_div">
            <h1 class="title">
                <span class="old-text">CREDITOS</span>
                <span class="new-text">IVÁN ALMENDROS LOZANO</span>
            </h1>
        </div>
    </form>
</body>
</html>
