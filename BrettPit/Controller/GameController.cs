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

            var gameRecord = GameSetting.Get(1);
            if (gameRecord != null)
            {
                model.game = gameRecord.Name;
            }

            var gamesRecord = GamesSetting.GetAll();

            if (gamesRecord != null && gamesRecord.Games.Any())
            {
                model.games = gamesRecord.Games[0].name;
                model.gamelist = gamesRecord.Games;
            }

            return View["games", model];
        }
    }
}