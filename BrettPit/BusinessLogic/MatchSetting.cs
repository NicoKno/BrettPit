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
    }
}