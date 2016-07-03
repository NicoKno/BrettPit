using System;
using System.Data.Entity;
using System.Linq;
using BrettPit.DataAccess;

namespace BrettPit.BusinessLogic
{
    public class EloSetting
    {
        public static void RecalcRatings(int userId, int opponentId, int result, int gameId)
        {
            using (var db = new DataAccessContext())
            {
                var player = db.eloes.Single(dbUser => dbUser.uid == userId && dbUser.gid == gameId);
                var opponent = db.eloes.Single(dbUser => dbUser.uid == opponentId && dbUser.gid == gameId);

                var playerElo = player.elo;
                var opponentElo = opponent.elo;

                var playerResult = 0.0;
                var opponentResult = 0.0;

                switch (result)
                {
                    case 0:
                        playerResult = 0.5;
                        opponentResult = 0.5;
                        break;
                    case 1:
                        playerResult = 0.0;
                        opponentResult = 1.0;
                        break;
                    case 2:
                        playerResult = 1.0;
                        opponentResult = 0.0;
                        break;
                }

                var playerEa = 1 / (1 + Math.Pow(10, (opponentElo - playerElo) / 400.0) );
                var newPlayerElo = playerElo + 20 * (playerResult - playerEa);

                var opponentEa = 1 / (1 + Math.Pow(10, (playerElo - opponentElo) / 400.0) );
                var newOpponentElo = opponentElo + 20 * (opponentResult - opponentEa);

                player.elo = (int)Math.Round(newPlayerElo, MidpointRounding.AwayFromZero);
                opponent.elo = (int) Math.Round(newOpponentElo, MidpointRounding.AwayFromZero);

                db.Entry(player).State = EntityState.Modified;
                db.Entry(opponent).State = EntityState.Modified;

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