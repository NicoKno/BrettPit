using Nancy;
using Nancy.Security;

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
            return View["games"];
        }
    }
}