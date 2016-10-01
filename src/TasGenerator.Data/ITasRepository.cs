using System.Collections.Generic;
using MongoDB.Bson;
using TasGenerator.Model;

namespace TasGenerator.Data
{
    public interface ITasRepository
    {
        Team Create(Team p);
        //Competition CreateCompetition(Competition p);
        //DrawSolution CreateDrawSolution(DrawSolution p);
        //Competition GetCompetition(string name);
        //IEnumerable<Competition> GetCompetitions();
        //DrawSolution GetDrawSolution(ObjectId id);
        //IEnumerable<DrawSolution> GetDrawSolutions();
        Team GetTeam(int id);
        IEnumerable<Team> GetTeams();
        void Remove(int id);
        //void RemoveCompetition(string name);
        //void RemoveDrawSolution(ObjectId id);
        void Update(int id, Team p);
        //void UpdateCompetition(string name, Competition p);
        //void UpdateDrawSolution(ObjectId id, DrawSolution p);
    }
}