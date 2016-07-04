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

            var username = (string)Request.Form.Username;
            var oldPassword = (string)Request.Form.OldPassword;
            var newPassword = (string)Request.Form.NewPassword;
            var repeat = (string)Request.Form.Repeat;
            var email = (string)Request.Form.Email;

            var message = "Changes saved";

            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;
            model.UserEmail = currentUser.Email;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || newPassword != repeat)
            {
                //return Context.GetRedirect("~/login?repeatError=true");
                //error message
                message = "Username and Email must not be empty, new password and repeat must be the same";
            }
            else
            {
                if (oldPassword.CalculateMd5Hash() != currentUser.Password)
                {
                    //error message
                    message = "Password wrong";
                }
                else
                {
                    //no errors detected
                    if(string.IsNullOrEmpty(newPassword))
                    {
                        if (!UserSetting.ChangeNameAndEmail(currentUser.Id, username, email))
                            message = "Userdata could not be saved";
                    }
                    else
                    {
                        if (!UserSetting.ChangePassword(currentUser.Id, newPassword))
                            message = "Password could not be changed";
                    }
                }
            }

            model.Message = message;

            return View["user", model];
        }

        private dynamic UserDelete(dynamic arg)
        {
            //User Information for Navigation
            var currentUser = (UserModel)Context.CurrentUser;

            var oldPassword = (string)Request.Form.OldPassword;

            var message = string.Empty;

            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;
            model.UserEmail = currentUser.Email;
            model.Message = message;

            if (oldPassword.CalculateMd5Hash() != currentUser.Password)
            {
                //error message
                message = "Wrong password";
            }
            else
            {
                if (!UserSetting.DeleteAccount(currentUser.Id))
                    message = "Account could not be deleted";
            }

            if (message == string.Empty)
            {
                //deletion successful -> redirect to the login page
                return Context.GetRedirect("~/login");
            }
            
            //deletion unsuccessful -> show the user view with the error message
            model.Message = message;
            return View["user", model];
        }
    }
}