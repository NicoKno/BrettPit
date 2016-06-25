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

                //int count = games.Count();
                //if (games != null)
                //{
                //    resultGame = new GameModel
                //    {
                //        Id = games[0].id,
                //        Name = games[0].name,
                //        Description = games[0].description
                //    };
                //}

                //var dbGame = db.game_systems.Find(1);

                //if (dbGame != null)
                //{
                //    resultGame = new GameModel
                //    {
                //        Id = dbGame.id,
                //        Name = dbGame.name,
                //        Description = dbGame.description
                //    };
                //}
            }
            return resultGames;
        }
    }
};