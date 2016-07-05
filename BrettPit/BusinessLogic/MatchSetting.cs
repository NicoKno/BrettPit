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
                        Opponent = match.uid2 == uid ? match.users.name : match.users1.name,
                        Player = match.uid2 == uid ? match.users1.name : match.users.name,
                        Result = match.result,
                        Status1 = match.status1,
                        Date = match.timestamp,
                        OwnMatch = match.uid2 == uid
                    }).ToList().Select(match => new MatchModel
                    {
                        Id = match.Id,
                        Game = match.Game,
                        Opponent = match.Opponent,
                        Player = match.Player,
                        Result = GetResult(match.Result, match.OwnMatch),
                        Status = GetStatus(match.Status1, match.OwnMatch),
                        Date = match.Date
                    }).OrderByDescending(match => match.Date) ;

                return matches.ToList();
            }
        }

        private static string GetStatus(int status1, bool ownMatch)
        {
            if (ownMatch)
            {
                if (status1 == 0)
                {
                    return "Decision awaited";
                }

                if (status1 == 1)
                {
                    return "Accepted";
                }

                if (status1 == 2)
                {
                    return "Declined";
                }
            }

            switch (status1)
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

        private static string GetResult(int match, bool ownMatch)
        {
            if (ownMatch)
            {
                switch (match)
                {
                    case 0:
                        return "Draw";
                    case 1:
                        return "You lost";
                    case 2:
                        return "You won";
                }
            }

            switch (match)
            {
                case 0:
                    return "Draw";
                case 1:
                    return "You won";
                case 2:
                    return "You lost";
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
                    status2 = 1,
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

        public static pairings Get(int matchId)
        {
            pairings model;
            using (var db = new DataAccessContext())
            {
                model = db.pairings.Single(dbMatch => dbMatch.id == matchId);
            }

            return model;
        }
    }
}