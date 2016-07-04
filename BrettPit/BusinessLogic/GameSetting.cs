using System.Collections.Generic;
using System.Linq;
using BrettPit.DataAccess;
using BrettPit.Models;

namespace BrettPit.BusinessLogic
{
    public class GameSetting
    {
        public static GameModel Get(int gameId)
        {
            GameModel resultGame = null;

            using (var db = new DataAccessContext())
            {
                var dbGame = db.game_systems.Find(gameId);

                if (dbGame != null)
                {
                    resultGame = new GameModel
                    {
                        Id = dbGame.id,
                        Name = dbGame.name,
                        Description = dbGame.description
                    };
                }
            }

            return resultGame;
        }
        public static List<UserScoreModel> GetScoreForAllUsers(int gameId)
        {
            using (var db = new DataAccessContext())
            {
                var dbUserScores = db.eloes.Where(elo => elo.game_systems.id == gameId).OrderByDescending(elo => elo.elo).Select(elo => new UserScoreModel
                {
                    Username = elo.users.name,
                    Score = elo.elo
                });

                return dbUserScores.ToList();
            }
        }

        public static IEnumerable<GameModel> All()
        {
            using (var db = new DataAccessContext())
            {
                var dbGames = db.game_systems.Select(game => new GameModel
                {
                    Id = game.id,
                    Name = game.name,
                    Description = game.description
                });

                return dbGames;
            }
        }

        public static bool DeleteGame(int gameid)
        {
            bool result;

            try
            {
                //deleting the corresponding datasets in table "pairings"
                using (var GameDb = new DataAccessContext())
                {
                    GameDb.game_systems.Remove(GameDb.game_systems.Find(gameid));
                    GameDb.SaveChanges();
                }
                result = true;
            }
            catch
            {
                //TODO: Log exception to log
                result = false;
            }
            return result;
        }

        public static bool ChangeGame(int gameid, string NewName, string NewDescription)
        {
            bool result;

            try
            {
                using (var GameDb = new DataAccessContext())
                {
                    var gameRecord = GameDb.game_systems.Find(gameid);
                    if (gameRecord != null)
                    {
                        gameRecord.name = NewName;
                        gameRecord.description = NewDescription;
                        GameDb.Entry(gameRecord).State = System.Data.Entity.EntityState.Modified;
                        GameDb.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch
            {
                //TODO: Log exception to log
                result = false;
            }

            return result;
        }

        public static bool AddGame(string NewName, string NewDescription)
        {
            bool result;

            try
            {
                using (var GameDb = new DataAccessContext())
                {
                    var newGame = new game_systems
                    {
                        name = NewName,
                        description = NewDescription
                    };

                    var gameRecord = GameDb.game_systems.Add(newGame);
                    GameDb.SaveChanges();
                    result = true;
                }
            }
            catch
            {
                //TODO: Log exception to log
                result = false;
            }

            return result;
        }
    }
}