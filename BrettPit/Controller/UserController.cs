using System;
using System.Dynamic;
using BrettPit.BusinessLogic;
using Nancy;
using Nancy.Security;
using Nancy.Extensions;
using BrettPit.Models;

namespace BrettPit.Controller
{
    public class UserController : NancyModule
    {
        public UserController() : base("/user")
        {
            this.RequiresAuthentication();

            Get["/"] = UserView;
            Post["/save"] = UserSave;
            Post["/delete"] = UserDelete;
        }

        private dynamic UserView(dynamic arg)
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
            model.Message = "";

            return View["user", model];
        }

        private dynamic UserSave(dynamic arg)
        {
            //User Information for Navigation
            var currentUser = (UserModel)Context.CurrentUser;

            var Username = (string)Request.Form.Username;
            var OldPassword = (string)Request.Form.OldPassword;
            var NewPassword = (string)Request.Form.NewPassword;
            var Repeat = (string)Request.Form.Repeat;
            var Email = (string)Request.Form.Email;

            string message = "changes saved";

            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;
            model.UserEmail = currentUser.Email;
            model.Message = message;

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || NewPassword != Repeat)
            {
                //return Context.GetRedirect("~/login?repeatError=true");
                //error message
                message = "Username and Email must not be empty";
            }
            else
            {
                if (OldPassword.CalculateMd5Hash() != currentUser.Password)
                {
                    //error message
                    message = "Password wrong";
                }
                else
                {
                    //no errors detected
                    if(string.IsNullOrEmpty(NewPassword))
                    {
                        if (!UserSetting.ChangeNameAndEmail(currentUser.Id, Username, Email))
                            message = "userdata could not be saved";
                    }
                    else
                    {
                        if (!UserSetting.ChangePassword(currentUser.Id, NewPassword))
                            message = "password could not be changed";
                    }
                }
            }
            return View["user", model];
        }

        private dynamic UserDelete(dynamic arg)
        {
            //User Information for Navigation
            var currentUser = (UserModel)Context.CurrentUser;

            var Username = (string)Request.Form.Username;
            var OldPassword = (string)Request.Form.OldPassword;
            var NewPassword = (string)Request.Form.NewPassword;
            var Repeat = (string)Request.Form.Repeat;
            var Email = (string)Request.Form.Email;

            string message = "";

            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;
            model.UserEmail = currentUser.Email;
            model.Message = message;

            if (OldPassword.CalculateMd5Hash() != currentUser.Password)
            {
                //error message
                message = "wrong password";
            }
            else
            {
                if (!UserSetting.DeleteAccount(currentUser.Id))
                    message = "account could not be deleted";
            }
            if(message == "")
            {
                //deletion successful -> redirect to the login page
                return Context.GetRedirect("~/login");
            }
            else
            {
                //deletion unsuccessful -> show the user view with the error message
                model.Message = message;
                return View["user", model];
            }
        }
    }
}