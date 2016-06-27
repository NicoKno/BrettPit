using Nancy;
using Nancy.Security;
using BrettPit.BusinessLogic;
using System.Dynamic;
using System.Linq;
using BrettPit.Models;

namespace BrettPit.Controller
{
    public class GameController : NancyModule
    {
        public GameController() : base("/games")
        {
            this.RequiresAuthentication();
            
            Get["/"] = GetGamesView;
            Get["/{id}"] = GetSpecificGameView;
        }

        private dynamic GetSpecificGameView(dynamic arg)
        {
            var gameId = (int)arg.id;
            gameId = 1;

            dynamic model = new ExpandoObject();
            var game = GameSetting.Get(gameId);

            model.GameId = game.Id;
            model.GameName = game.Name;
            model.GameDescription = game.Description;

            var currentUser = (UserModel)Context.CurrentUser;

            var scoreForAllUsers = GameSetting.GetScoreForAllUsers(gameId);
            model.UserScores = scoreForAllUsers;
            model.CurrentUserScore = scoreForAllUsers.FirstOrDefault(score => score.Username == currentUser.UserName);

            model.Matches = MatchSetting.GetMatches(gameId, currentUser.Id);

            return View["game", model];
        }

        private dynamic GetGamesView(dynamic parameters)
        {
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            //all games
            var allGamesRecord = GamesSetting.GetAll();

            if (allGamesRecord != null && allGamesRecord.Games.Any())
            {
                model.allGames = allGamesRecord.Games;
            }

            //users games
            string curUser = Context.CurrentUser.UserName;
            var myGamesRecord = GamesSetting.GetMy(curUser);

            if (myGamesRecord != null && myGamesRecord.Games.Any())
            {
                model.myGames = myGamesRecord.Games;
            }

            //filter on/off
            model.filter = false;

            return View["games", model];
        }
    }
}