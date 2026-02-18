using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsChallenge4_Tournament
{
    public class Team : IComparable<Team>
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public int CompareTo(Team other)
        {
            if (other == null) return 1;
            int byPoints = other.Points.CompareTo(Points);
            if (byPoints != 0) return byPoints;
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class Match
    {
        public Team Team1 { get; }
        public Team Team2 { get; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public bool Recorded { get; set; }

        public Match(Team team1, Team team2)
        {
            Team1 = team1;
            Team2 = team2;
        }

        public Match Clone()
        {
            var m = new Match(Team1, Team2) { Score1 = Score1, Score2 = Score2, Recorded = Recorded };
            return m;
        }
    }

    public class Tournament
    {
        private readonly List<Team> _teams = new List<Team>();
        private readonly LinkedList<Match> _schedule = new LinkedList<Match>();
        private readonly Stack<Match> _undoStack = new Stack<Match>();
        private readonly List<Match> _recordedMatches = new List<Match>();

        public void AddTeam(Team team)
        {
            if (!_teams.Any(t => t.Name == team.Name))
                _teams.Add(team);
        }

        public void ScheduleMatch(Match match)
        {
            _schedule.AddLast(match);
        }

        public void RecordMatchResult(Match match, int team1Score, int team2Score)
        {
            if (match == null) return;
            match.Score1 = team1Score;
            match.Score2 = team2Score;
            match.Recorded = true;
            match.Team1.Points += team1Score > team2Score ? 3 : (team1Score == team2Score ? 1 : 0);
            match.Team2.Points += team2Score > team1Score ? 3 : (team2Score == team1Score ? 1 : 0);
            _recordedMatches.Add(match);
            _undoStack.Push(match.Clone());
        }

        public void UndoLastMatch()
        {
            if (_undoStack.Count == 0) return;
            var m = _undoStack.Pop();
            m.Team1.Points -= m.Score1 > m.Score2 ? 3 : (m.Score1 == m.Score2 ? 1 : 0);
            m.Team2.Points -= m.Score2 > m.Score1 ? 3 : (m.Score2 == m.Score1 ? 1 : 0);
        }

        public int GetTeamRanking(Team team)
        {
            var sorted = _teams.OrderByDescending(t => t, Comparer<Team>.Default).ToList();
            int idx = sorted.IndexOf(team);
            return idx < 0 ? -1 : idx + 1;
        }

        public List<Team> GetRankings()
        {
            return _teams.OrderByDescending(t => t, Comparer<Team>.Default).ToList();
        }
    }

    class Program
    {
        static void Main()
        {
            var tournament = new Tournament();
            var teamA = new Team { Name = "Team Alpha", Points = 0 };
            var teamB = new Team { Name = "Team Beta", Points = 0 };
            tournament.AddTeam(teamA);
            tournament.AddTeam(teamB);

            var match = new Match(teamA, teamB);
            tournament.ScheduleMatch(match);
            tournament.RecordMatchResult(match, 3, 1);

            var rankings = tournament.GetRankings();
            Console.WriteLine(rankings.Count > 0 ? rankings[0].Name : "None");
            Console.WriteLine(teamA.Points);

            tournament.UndoLastMatch();
            Console.WriteLine(teamA.Points);
        }
    }
}
