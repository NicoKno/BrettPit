using System;
using System.Dynamic;
using BrettPit.BusinessLogic;
using Nancy;
using Nancy.Security;
using Nancy.Extensions;
using BrettPit.Models;

namespace BrettPit.Controller
{
    public class AdminController : NancyModule
    {
        public AdminController() : base("/administration")
        {
            this.RequiresAuthentication();

            Get["/"] = AdminView;
            Post["/delete"] = AdminDelUser;
            Post["/resetpw"] = AdminResetPw;
        }

        private dynamic AdminView(dynamic arg)
        {
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            //User Information for Navigation
            var currentUser = (UserModel)Context.CurrentUser;

            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;
            model.UserEmail = currentUser.Email;

            //alle User holen
            model.AllUsers = UserSetting.All();
            //model.AllGames = GamesSetting.GetAll();

            return View["admin", model];
        }

        private dynamic AdminDelUser(dynamic arg)
        {
            //delete user
            UserSetting.DeleteAccount((int)Request.Form.DeleteUser);

            //refresh view
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            //User Information for Navigation
            var currentUser = (UserModel)Context.CurrentUser;

            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;
            model.UserEmail = currentUser.Email;

            //alle User holen
            model.AllUsers = UserSetting.All();
            //model.AllGames = GamesSetting.GetAll();

            return View["admin", model];
        }

        private dynamic AdminResetPw(dynamic arg)
        {
            //delete user
            UserSetting.ResetPassword((int)Request.Form.DeleteUser);

            //refresh view
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            //User Information for Navigation
            var currentUser = (UserModel)Context.CurrentUser;

            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;
            model.UserEmail = currentUser.Email;

            //alle User holen
            model.AllUsers = UserSetting.All();
            model.AllGames = GamesSetting.GetAll();

            return View["admin", model];
        }
    }
}