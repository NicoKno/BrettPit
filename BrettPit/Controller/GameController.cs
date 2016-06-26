using Nancy;
using Nancy.Security;
using BrettPit.BusinessLogic;
using System;
using System.Dynamic;
using Nancy.Authentication.Forms;
using Nancy.Extensions;

namespace BrettPit.Controller
{
    public class GameController : NancyModule
    {
        public GameController() : base("/games")
        {
            this.RequiresAuthentication();
            
            Get["/"] = GetGamesView;
        }

        private dynamic GetGamesView(dynamic parameters)
        {
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            var gameRecord = GameSetting.Get(1);
            model.game = gameRecord.Name;

            var gamesRecord = GamesSetting.GetAll();
            model.games = gamesRecord.Games[0].name;
            model.gamelist = gamesRecord.Games;
            model.filter = true;
            return View["games", model];
        }
    }
}