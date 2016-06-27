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