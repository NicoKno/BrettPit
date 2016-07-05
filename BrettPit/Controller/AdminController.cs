using System;
using System.Dynamic;
using BrettPit.BusinessLogic;
using Nancy;
using Nancy.Security;
using Nancy.Extensions;
using BrettPit.Models;
using System.Linq;

namespace BrettPit.Controller
{
    public class AdminController : NancyModule
    {
        public AdminController() : base("/administration")
        {
            this.RequiresAuthentication();

            Get["/"] = AdminView;
            Post["/deleteuser"] = AdminDelUser;
            Post["/resetpw"] = AdminResetPw;
            Post["/changeadmin"] = AdminState;
            Post["/deletegame"] = AdminDelGame;
            Post["/addgame"] = AdminAddGame;
            Post["/changegame"] = AdminChangeGame;
            Get["/searchUser"] = GetSearchUserView;
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

            //has the user admin rights?
            if (currentUser.IsAdmin)
            {
                //get all users
                model.AllUsers = UserSetting.All();
                //get all games
                model.AllGames = GamesSetting.GetAll();
                //permission
                model.permission = true;
            }
            else
                model.permission = false;
            return View["admin", model];

        }

        private dynamic AdminDelUser(dynamic arg)
        {

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

            //has the user admin rights?
            if (currentUser.IsAdmin)
            {
                //delete user
                var UserToDelete = (int)Request.Form.DeleteUser;
                UserSetting.DeleteAccount(UserToDelete);
                //get all users
                model.AllUsers = UserSetting.All();
                //get all games
                model.AllGames = GamesSetting.GetAll();
                //permission
                model.permission = true;
                if(UserToDelete == currentUser.Id)
                {
                    model.permission = false;
                    return Context.GetRedirect("~/logout");
                }
            }
            else
                model.permission = false;
            return View["admin", model];
        }

        private dynamic AdminResetPw(dynamic arg)
        {
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

            //has the user admin rights?
            if (currentUser.IsAdmin)
            {
                //resetpw
                UserSetting.ResetPassword((int)Request.Form.ResetPw);

                //get all users
                model.AllUsers = UserSetting.All();
                //get all games
                model.AllGames = GamesSetting.GetAll();
                //permission
                model.permission = true;
            }
            else
                model.permission = false;
            return View["admin", model];
        }

        private dynamic AdminDelGame(dynamic arg)
        {
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

            //has the user admin rights?
            if (currentUser.IsAdmin)
            {
                //delete game
                GameSetting.DeleteGame((int)Request.Form.DeleteGame);

                //get all users
                model.AllUsers = UserSetting.All();
                //get all games
                model.AllGames = GamesSetting.GetAll();
                //permission
                model.permission = true;
            }
            else
                model.permission = false;
            return View["admin", model];
        }

        private dynamic AdminAddGame(dynamic arg)
        {
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

            //has the user admin rights?
            if (currentUser.IsAdmin)
            {
                //add game
                GameSetting.AddGame((string)Request.Form.addGameName, (string)Request.Form.addGameDesc);

                //get all users
                model.AllUsers = UserSetting.All();
                //get all games
                model.AllGames = GamesSetting.GetAll();
                //permission
                model.permission = true;
            }
            else
                model.permission = false;
            return View["admin", model];
        }

        private dynamic AdminChangeGame(dynamic arg)
        {
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

            //has the user admin rights?
            if (currentUser.IsAdmin)
            {
                //change game
                GameSetting.ChangeGame((int)Request.Form.ChangeGame, (string)Request.Form.gamename, (string)Request.Form.gamedescription);
                //get all users
                model.AllUsers = UserSetting.All();
                //get all games
                model.AllGames = GamesSetting.GetAll();
                //permission
                model.permission = true;
            }
            else
                model.permission = false;
            return View["admin", model];
        }


        private dynamic GetSearchUserView(dynamic arg)
        {
            var searchTerm = (string)Request.Query.searchTerm ?? string.Empty;
            dynamic model = new ExpandoObject();

            var AllUsers = UserSetting.GetAll();

            model.Users = AllUsers.Where(users => users.UserName.IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();

            return View["searchUser", model];
        }

        private dynamic AdminState(dynamic arg)
        {
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

            //has the user admin rights?
            if (currentUser.IsAdmin)
            {
                // change admin state
                var uidUserToChange = (int)Request.Form.chAdmStateUid;
                UserSetting.ChangeAdminState(uidUserToChange, currentUser.Id);
                //get all users
                model.AllUsers = UserSetting.All();
                //get all games
                model.AllGames = GamesSetting.GetAll();
                //permission
                model.permission = true;

                if(currentUser.Id == uidUserToChange)
                {
                    //affected account is the user himself
                    model.permission = false;
                    currentUser.IsAdmin = false;
                }
            }
            else
                model.permission = false;
            return View["admin", model];
        }
    }
}