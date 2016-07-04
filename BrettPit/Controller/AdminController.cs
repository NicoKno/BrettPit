﻿using System;
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
            Post["/deleteuser"] = AdminDelUser;
            Post["/resetpw"] = AdminResetPw;
            Post["/deletegame"] = AdminDelGame;
            Post["/addgame"] = AdminAddGame;
            Post["/changegame"] = AdminChangeGame;
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

        private dynamic AdminDelGame(dynamic arg)
        {
            //delete game
            GameSetting.DeleteGame((int)Request.Form.DeleteGame);

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
            //add game
            GameSetting.AddGame((string)Request.Form.addGameName, (string)Request.Form.addGameDesc);

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
            //add game
            GameSetting.ChangeGame((int)Request.Form.ChangeGame, (string)Request.Form.gamename, (string)Request.Form.gamedescription);

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
    }
}