using System.Linq;
using System.Collections.Generic;
using BrettPit.DataAccess;
using System.Web;

namespace BrettPit.BusinessLogic
{
    public class GamesSetting
    {
        public static IEnumerable<GameModel> GetAll()
        {
            IEnumerable<GameModel> resultGames;

            using (var db = new DataAccessContext())
            {
                var query = db.game_systems.Select(game => new GameModel
                {
                    Id = game.id,
                    Name = game.name,
                    Description = game.description
                });

                resultGames = query.ToList();
            }
            return resultGames;
        }

        public static IEnumerable<GameModel> GetMyGames(int uid)
        {
            IEnumerable<GameModel> resultGames;
            //request the games list for that user

            using (var db = new DataAccessContext())
            {
                var query = db.game_systems
                    .Where(game => game.pairings.Any(match => match.uid1 == uid || match.uid2 == uid))
                    .Select(game => new GameModel
                    {
                        Id = game.id,
                        Name = game.name,
                        Description = game.description
                    }).Distinct();

                resultGames = query.ToList();
            }

            return resultGames;
        }
    }
};