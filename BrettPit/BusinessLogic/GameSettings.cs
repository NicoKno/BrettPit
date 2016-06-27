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
                    resultGame = new GameModel();
                    //resultGame.Game = dbGame;
                    resultGame.Id = dbGame.id;
                    resultGame.Name = dbGame.name;
                    resultGame.Description = dbGame.description;
                }
            }

            return resultGame;
        }
    }
}