using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questao2.Models;

public class Program
{
    static readonly HttpClient client = new HttpClient();
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");

        // Output expected:
        // Team Paris Saint-Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        int page;

        for (int i = 1; i <= 2; i++)
        {
            var url = "";
            page = 1;
            while (true)
            {
                url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&page={page}&team{i}={team}";

                var response = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<Root>(response);

                foreach (var item in data.Data)
                {
                    if (i == 1)
                    {
                        totalGoals += item.Team1Goals;
                    }
                    else
                    {
                        totalGoals += item.Team2Goals;
                    }
                }


                if (page >= data.Total_Pages)
                    break;

                page++;
            }
        }

        return totalGoals;
    }
}
