using System;
using System.Collections.Generic;
using System.Linq;
using BrettPit.DataAccess;
using BrettPit.Models;

namespace BrettPit.BusinessLogic
{
    public class MatchSetting
    {
        public static IEnumerable<MatchModel> GetMatches(int gameId, int uid)
        {
            using (var db = new DataAccessContext())
            {
                var matches = db.pairings.Where(match => match.uid1 == uid || match.uid2 == uid)
                    .Where(match => match.game_system_id == gameId)
                    .Select(match => new {
                        Id = match.id,
                        Game = match.game_systems.name,
                        Opponent = match.users.name,
                        Player = match.users1.name,
                        Result = match.result,
                        Status = match.status1,
                        Date = match.timestamp
                    }).ToList().Select(match => new MatchModel
                    {
                        Id = match.Id,
                        Game = match.Game,
                        Opponent = match.Opponent,
                        Player = match.Player,
                        Result = GetResult(match.Result),
                        Status = GetStatus(match.Status),
                        Date = match.Date
                    }).OrderByDescending(match => match.Date) ;

                return matches.ToList();
            }
        }

        private static string GetStatus(int match)
        {
            switch (match)
            {
                case 0:
                    return "Open";
                case 1:
                    return "Accepted";
                case 2:
                    return "Declined";
            }

            return string.Empty;
        }

        private static string GetResult(int match)
        {
            switch (match)
            {
                case 0:
                    return "Draw";
                case 1:
                    return "Opponent won";
                case 2:
                    return "You won";
            }

            return string.Empty;
        }

        public static void ChangeMatchState(int gameId, int matchId, int status)
        {
            using (var db = new DataAccessContext())
            {
                var acceptedMatch = db.pairings.FirstOrDefault(match => match.id == matchId);

                if (acceptedMatch != null)
                {
                    acceptedMatch.status1 = status;
                    db.Entry(acceptedMatch).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public static void Save(int opponentId, int result, int currentUserId, int gameId)
        {
            using (var db = new DataAccessContext())
            {
                var pairing = new pairings
                {
                    status1 = 0,
                    game_system_id = gameId,
                    result = result,
                    timestamp = DateTime.Now,
                    uid1 = opponentId,
                    uid2 = currentUserId
                };
                db.pairings.Add(pairing);
                db.SaveChanges();
            }
        }
    }
}