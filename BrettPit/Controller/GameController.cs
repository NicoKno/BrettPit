using Nancy;
using Nancy.Security;
using BrettPit.BusinessLogic;
using System.Dynamic;
using System.Linq;

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