using System.Linq;
using System.Collections.Generic;
using BrettPit.DataAccess;
using System.Web;

namespace BrettPit.BusinessLogic
{
    public class GamesSetting
    {
        public static GamesModel GetAll()
        {
            GamesModel resultGames = null;

            using (var db = new DataAccessContext())
            {
                if (db != null)
                {
                    var query =
                        from game in db.game_systems
                        select new GameModel()
                        {
                            Id = game.id,
                            Name = game.name,
                            Description = game.description
                        };

                    if (query.Count() > 0)
                    {
                        resultGames = new GamesModel();
                        query = query.Distinct();
                        resultGames.Games = query.ToList();
                    }
                }
            }
            return resultGames;
        }

        public static GamesModel GetMy(string curUser)
        {
            //request the uid from the current user from the database
            int uid = 0;
            using (var userDb = new DataAccessContext())
            {
                if (userDb != null)
                {

                    var userDS = userDb.users.FirstOrDefault(user => string.Equals(user.name, curUser));
                    if (userDb != null)
                    {
                        uid = userDS.id;
                    }
                }
            }
            //request the games list for that user
            GamesModel resultGames = null;

            using (var db = new DataAccessContext())
            {
                if (db != null)
                {
                    var query =
                        from match in db.pairings
                        join game in db.game_systems on match.game_system_id equals game.id
                        where match.uid1 == uid || match.uid2 == uid
                        select new GameModel()
                        {
                            Id = game.id,
                            Name = game.name,
                            Description = game.description
                        };

                    if (query.Count() > 0)
                    {
                        resultGames = new GamesModel();
                        query = query.Distinct();
                        resultGames.Games = query.ToList();
                    }
                }
            }
            
            return resultGames;
        }
    }
};