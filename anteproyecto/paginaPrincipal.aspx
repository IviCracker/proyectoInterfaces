<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paginaPrincipal.aspx.cs" Inherits="anteproyecto.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>diario-personal</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link rel="stylesheet" type="text/css" href="StyleSheetIndex.css" />
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
</head>

<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div id="navegador">
                    <nav class="navMenu">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="nav-link" OnClick="LinkButtonView1_Click" CommandArgument="View1"><ion-icon name="person-add-outline"></ion-icon></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="nav-link" OnClick="LinkButtonView3_Click" CommandArgument="View3"><ion-icon name="add-outline"></ion-icon></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="nav-link" OnClick="LinkButtonView4_Click" CommandArgument="View4"><ion-icon name="newspaper-outline"></ion-icon> </asp:LinkButton>
                        <div class="dot"></div>
                    </nav>
                </div>

                <div class="navigation">
                    <a runat="server" id="lnkLogout" class="button" href="index.aspx">

                        <img class="imagenlogout" src="logout.png" />
                        <div class="logout">LOGOUT</div>
                    </a>
                </div>
                <div class="col">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="container text-center my-5">
                                <header class="headerView1"></header>
                                <div class="mi-clase-especial">
                                    <div>
                                        <h1 class="display-4">DIARIO PERSONAL</h1>
                                    </div>
                                    <div class="form-group">
                                        <p>
                                            Usuario:
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </p>
                                        <p>
                                            Contraseña:
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                                            <span id="togglePassword" class="toggle-password"><i class="fas fa-eye"></i></span>
                                        </p>
                                    </div>
                                    <div>
                                        <asp:Button ID="Button1" runat="server" Text="Iniciar Sesión" OnClick="Button1_Click" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button2" runat="server" Text="Registrarse" OnClick="Button2_Click" CssClass="btn btn-secondary" />
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <header class="headerGeneral"></header>
                            <div class="mi-clase-especial">
                                <div>
                                    <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red"></asp:Label>
                                    <p>
                                        Usuario:
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </p>
                                    <p>
                                        Contraseña:
                                        <asp:TextBox ID="TextBox4" runat="server" type="password"></asp:TextBox>
                                        <span id="togglePassword1" class="toggle-password"><i class="fas fa-eye"></i></span>
                                    </p>
                                    <p>
                                        Repetir Contraseña:
                                        <asp:TextBox ID="TextBox5" runat="server" type="password"></asp:TextBox>
                                        <span id="togglePassword2" class="toggle-password"><i class="fas fa-eye"></i></span>
                                    </p>
                                </div>
                                <div>
                                    <asp:Button ID="Button3" runat="server" Text="Registrarse" OnClick="Button3_Click" />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="Button4" runat="server" Text="Regresar" OnClick="Button4_Click" />
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <header class="headerGeneral"></header>
                            <div class="mi-clase-especial">
                                <h1>Bienvenido a tu diario personal
                                    <asp:Label ID="lblNombreUsuario" runat="server" /></h1>
                                <h2>Aquí podrás escribir tus entradas </h2>
                                <br />
                                <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Rows="10" Style="width: 100%; height: 50vh;"></asp:TextBox>
                                <asp:Button ID="Button5" runat="server" Text="Añadir entrada" OnClick="Button5_Click" />
                                <asp:Button ID="Button6" runat="server" Text="Ver entradas" OnClick="Button6_Click" />
                            </div>
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <header class="headerGeneral"></header>
                            <div class="mi-clase-especial">
                                <h1>Bienvenido a tu diario personal
                                <asp:Label ID="lblNombreUsuario2" runat="server" /></h1>
                                <div class="calendario-container">
                                    <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar-container">
                                        <DayHeaderStyle CssClass="calendar-header" />
                                        <DayStyle CssClass="calendar-day" />
                                        <OtherMonthDayStyle CssClass="calendar-day other-month" />
                                        <SelectedDayStyle CssClass="calendar-daySelected" ForeColor="White" BackColor="#fddb3a" />
                                        <TodayDayStyle CssClass="calendar-day today" />
                                    </asp:Calendar>
                                    <br />
                                    <br />
                                    <h3>AÑADIR IMAGEN</h3>
                                    <p class="alinearBtn">
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="buttonFU" />
                                        <br />
                                        <br />
                                        <asp:Button ID="Button7" runat="server" Text="Añadir Imagen" OnClick="Button8_Click" CssClass="botones" />
                                        <asp:Button ID="Button8" runat="server" Text="Borrar Imagen" OnClick="Button9_Click" CssClass="botones" />
                                        <asp:Button ID="Button9" runat="server" Text="Borrar Entrada" OnClick="Button10_Click" CssClass="botones" />
                                    </p>
                                    <h2>Aquí podrás ver tus entradas</h2>
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </form>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById('togglePassword').addEventListener('click', function () {
                var passwordField = document.getElementById('TextBox2');
                var fieldType = passwordField.getAttribute('type');

                if (fieldType === 'password') {
                    passwordField.setAttribute('type', 'text');
                    this.innerHTML = '<i class="fas fa-eye-slash"></i>';
                } else {
                    passwordField.setAttribute('type', 'password');
                    this.innerHTML = '<i class="fas fa-eye"></i>';
                }
            });
        });

        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById('togglePassword1').addEventListener('click', function () {
                var passwordField = document.getElementById('TextBox4');
                var fieldType = passwordField.getAttribute('type');

                if (fieldType === 'password') {
                    passwordField.setAttribute('type', 'text');
                    this.innerHTML = '<i class="fas fa-eye-slash"></i>';
                } else {
                    passwordField.setAttribute('type', 'password');
                    this.innerHTML = '<i class="fas fa-eye"></i>';
                }
            });
        });

        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById('togglePassword2').addEventListener('click', function () {
                var passwordField = document.getElementById('TextBox5');
                var fieldType = passwordField.getAttribute('type');

                if (fieldType === 'password') {
                    passwordField.setAttribute('type', 'text');
                    this.innerHTML = '<i class="fas fa-eye-slash"></i>';
                } else {
                    passwordField.setAttribute('type', 'password');
                    this.innerHTML = '<i class="fas fa-eye"></i>';
                }
            });
        });

    </script>
</body>
</html>
