using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace anteproyecto
{
    public partial class portada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void irAIndex(object sender, EventArgs e)
        {
            Response.Redirect("paginaPrincipal.aspx");
        }

    }
}