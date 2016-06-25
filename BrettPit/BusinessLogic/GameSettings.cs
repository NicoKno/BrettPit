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

        //public static GamesModel GetAll()
        //{
        //    GamesModel resultGames = null;

        //    using (var db = new DataAccessContext())
        //    {
        //        resultGames = new GamesModel(db.game_systems.ToList());

        //        //int count = games.Count();
        //        //if (games != null)
        //        //{
        //        //    resultGame = new GameModel
        //        //    {
        //        //        Id = games[0].id,
        //        //        Name = games[0].name,
        //        //        Description = games[0].description
        //        //    };
        //        //}

        //            //var dbGame = db.game_systems.Find(1);

        //            //if (dbGame != null)
        //            //{
        //            //    resultGame = new GameModel
        //            //    {
        //            //        Id = dbGame.id,
        //            //        Name = dbGame.name,
        //            //        Description = dbGame.description
        //            //    };
        //            //}
        //        }

        //    return resultGame;
        //}

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