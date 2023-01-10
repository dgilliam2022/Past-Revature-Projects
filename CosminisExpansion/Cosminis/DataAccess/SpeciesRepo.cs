/*            *This code could be use to expand our project in case we decide to implement "stats" going forward*
using DataAccess.Entities;
using CustomExceptions;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
 
public class SpeciesRepo : ISpeciesDAO
{
    private readonly wearelosingsteamContext _context;

    public SpeciesRepo(wearelosingsteamContext context)
    {
        _context = context;
    }
             
    public Species GenerateBaseStats(int speciesId)
    {
        Species speciesInstance = new Species();

        Random randomStat = new Random();

        int baseStr = -7 + randomStat.Next(10, 27); 
        int baseDex = -7 + randomStat.Next(10, 27);
        int baseInt = -7 + randomStat.Next(10, 27);

        speciesInstance.BaseStr = speciesInstance.BaseStr + baseStr;
        speciesInstance.BaseDex = speciesInstance.BaseDex + baseDex;
        speciesInstance.BaseInt = speciesInstance.BaseInt + baseInt;

        speciesInstance.FoodElementIdFk = speciesId;

        return speciesInstance;
    }
}
*/