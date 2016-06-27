using System;
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
                var dbGame = db.game_systems.Find(1);

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
        public static IEnumerable<UserScoreModel> GetScoreForAllUsers(int gameId)
        {
            using (var db = new DataAccessContext())
            {
                var dbUserScores = db.eloes.Where(elo => elo.game_systems.id == gameId).Select(elo => new UserScoreModel
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
    }
}