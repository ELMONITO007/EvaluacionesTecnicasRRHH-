using Entities;
using Evaluaciones.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones_Tecnicas.Filter
{
    public class VerificarSesion :ActionFilterAttribute

    {
        private Usuarios usuarios ;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.Controller is HomeController )
                {

                }
                else
                {
                    base.OnActionExecuting(filterContext);
                    usuarios = (Usuarios)HttpContext.Current.Session["UserName"];
                    if (usuarios is null)
                    {
                        if (filterContext.Controller is LoginController == false)
                        {
                            filterContext.HttpContext.Response.Redirect("/login/index");
                        }



                    }
                }

               
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("/login/index");
            }
           
        }




    }
}