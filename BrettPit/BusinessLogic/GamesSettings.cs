using System.Linq;
using BrettPit.DataAccess;

namespace BrettPit.BusinessLogic
{
    public class GamesSetting
    {
        public static GamesModel GetAll()
        {
            GamesModel resultGames = null;

            using (var db = new DataAccessContext())
            {
                resultGames = new GamesModel { Games = db.game_systems.ToList() };
            }
            return resultGames;
        }
    }
};