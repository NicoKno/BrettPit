using System;
using System.Linq;
using BrettPit.DataAccess;

namespace BrettPit.BusinessLogic
{
    public class EloSetting
    {
        public static void RecalcRatings(int playerOneId, int playerTwoId, int result, int gameId)
        {
            using (var db = new DataAccessContext())
            {
                var playerOne = db.eloes.Single(dbUser => dbUser.uid == playerOneId && dbUser.gid == gameId);
                var playerTwo = db.eloes.Single(dbUser => dbUser.uid == playerTwoId && dbUser.gid == gameId);

                var playerOneElo = playerOne.elo;
                var playerTwoElo = playerTwo.elo;

                var playerOneResult = 0.0;
                var playerTwoResult = 0.0;

                switch (result)
                {
                    case 0:
                        playerOneResult = 0.5;
                        playerTwoResult = 0.5;
                        break;
                    case 1:
                        playerOneResult = 1.0;
                        playerTwoResult = 0.0;
                        break;
                    case 2:
                        playerOneResult = 0.0;
                        playerTwoResult = 1.0;
                        break;
                }

                var playerOneEa = 1 / (1 + Math.Pow(10, (playerTwoElo - playerOneElo) / 400.0) );
                var newPlayerOneElo = playerOneElo + 20 * (playerOneResult - playerOneEa);

                var playerTwoEa = 1 / (1 + Math.Pow(10, (playerOneElo - playerTwoElo) / 400.0) );
                var newPlayerTwoElo = playerTwoElo + 20 * (playerTwoResult - playerTwoEa);

                playerOne.elo = (int)Math.Round(newPlayerOneElo, MidpointRounding.AwayFromZero);
                playerTwo.elo = (int) Math.Round(newPlayerTwoElo, MidpointRounding.AwayFromZero);

                db.SaveChanges();
            }
        }

        public static bool Exists(int uid, int gameId)
        {
            bool result;
            using (var db = new DataAccessContext())
            {
                result = db.eloes.Any(elo => elo.uid == uid && elo.gid == gameId);
            }

            return result;
        }

        public static void CreateElo(int uid, int gameId)
        {
            using (var db = new DataAccessContext())
            {
                var newElo = new eloes
                {
                    elo = 1000,
                    gid = gameId,
                    uid = uid
                };

                db.eloes.Add(newElo);
                db.SaveChanges();
            }
        }
    }
}