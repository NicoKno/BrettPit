using Nancy;
using Nancy.Extensions;
using BrettPit.BusinessLogic;

namespace BrettPit.Controller
{
    public class ResetPwController : NancyModule
    {
        public ResetPwController() : base("/resetpwd")
        {
            //this.RequiresAuthentication();

            Get["/"] = GetResetPwdView;
            Post["/"] = ResetPwd;
        }

        private dynamic GetResetPwdView(dynamic arg)
        {
            return View["resetpwd"];
        }

        private dynamic ResetPwd(dynamic arg)
        {
            var email = (string)Request.Form.email;
            //hier validierungschecks für die email adresse einfügen

            UserSetting.ResetPassword(email);
            return Context.GetRedirect("~/login");
        }
    }
}