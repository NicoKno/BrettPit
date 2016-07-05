using System;
using Nancy;
using Nancy.Security;
using BrettPit.BusinessLogic;
using System.Dynamic;
using System.Linq;
using BrettPit.Models;
using Nancy.ModelBinding;

namespace BrettPit.Controller
{
    public class GameController : NancyModule
    {
        public GameController() : base("/games")
        {
            this.RequiresAuthentication();
            
            Get["/"] = GetGamesView;
            Get["/{id}"] = GetSpecificGameView;
            Get["/{id}/search"] = GetSearchView;
            Get["/{id}/searchMatchUser"] = GetMatchUserView;
            Get["/{id}/matches/{matchid}/accept"] = AcceptMatch;
            Get["/{id}/matches/{matchid}/decline"] = DeclineMatch;
            Post["/{id}/matches/"] = AddPlayerMatch;
        }

        private dynamic GetMatchUserView(dynamic arg)
        {
            var searchTerm = (string)Request.Query.searchTerm ?? string.Empty;
            dynamic model = new ExpandoObject();
            var currentUser = (UserModel) Context.CurrentUser;

            model.MatchUsers = UserSetting.All().Where(user => user.Id != currentUser.Id).Where(user => user.UserName.IndexOf(searchTerm, StringComparison.CurrentCultureIgnoreCase) > -1).ToList();

            return View["searchMatchUser", model];
        }

        private dynamic AddPlayerMatch(dynamic arg)
        {
            var match = this.Bind<MatchUserResultModel>();
            var currentUser = (UserModel)Context.CurrentUser;
            var gameId = (int) arg.id;

            MatchSetting.Save(match.UserId, match.Result, currentUser.Id, gameId);

            return null;
        }

        private dynamic DeclineMatch(dynamic arg)
        {
            var gameId = (int)arg.id;
            var matchId = (int)arg.matchid;

            MatchSetting.ChangeMatchState(gameId, matchId, 2);

            return null;
        }

        private dynamic AcceptMatch(dynamic arg)
        {
            var gameId = (int) arg.id;
            var matchId = (int) arg.matchid;

            MatchSetting.ChangeMatchState(gameId, matchId, 1);

            var match = MatchSetting.Get(matchId);

            EloSetting.RecalcRatings(match.uid1, match.uid2, match.result, gameId);

            return null;
        }

        private dynamic GetSearchView(dynamic arg)
        {
            var searchTerm = (string) Request.Query.searchTerm ?? string.Empty;
            var gameId = (int)arg.id;
            dynamic model = new ExpandoObject();

            var scoreForAllUsers = GameSetting.GetScoreForAllUsers(gameId);

            model.UserScores = scoreForAllUsers.Where(score => score.Username.IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();

            return View["search", model];
        }

        private dynamic GetSpecificGameView(dynamic arg)
        {
            var gameId = (int)arg.id;

            dynamic model = new ExpandoObject();
            var game = GameSetting.Get(gameId);

            model.GameId = game.Id;
            model.GameName = game.Name;
            model.GameDescription = game.Description;

            var currentUser = (UserModel)Context.CurrentUser;

            if (!EloSetting.Exists(currentUser.Id, gameId))
            {
                EloSetting.CreateElo(currentUser.Id, gameId);
            }

            var scoreForAllUsers = GameSetting.GetScoreForAllUsers(gameId);
            model.UserScores = scoreForAllUsers;

            var currentUserScoreModel = scoreForAllUsers.FirstOrDefault(score => score.Username == currentUser.UserName);
            if (currentUserScoreModel != null)
            {
                model.CurrentUserScore = currentUserScoreModel.Score;
            }

            model.Matches = MatchSetting.GetMatches(gameId, currentUser.Id);

            //User Information for Navigation
            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;

            return View["game", model];
        }

        private dynamic GetGamesView(dynamic parameters)
        {
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;

            //all games
            var allGamesRecord = GamesSetting.GetAll();

            if (allGamesRecord != null && allGamesRecord.Any())
            {
                model.allGames = allGamesRecord;
            }

            //users games
            var currentUser = (UserModel)Context.CurrentUser;
            var myGameRecords = GamesSetting.GetMyGames(currentUser.Id);

            model.myGames = myGameRecords;

            //filter on/off
            model.filter = false;

            //User Information for Navigation
            model.Username = currentUser.UserName;
            model.UserId = currentUser.Id;
            model.UserIsAdmin = currentUser.IsAdmin;

            return View["games", model];
        }
    }
}