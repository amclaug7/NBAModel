using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MathNet.Numerics;

namespace NBAFormulaFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=LAPTOP-BSO196BK\SQLEXPRESS;Initial Catalog=NBAStats;Integrated Security=True");

            double[] test1 = new double[1104];
            double[] test2 = new double[1104];
            double bestMSE = 10000000000000;

            for (double i = 1.25; i <= 1.35; i += .01)
            {
                for (double j = 1.95; j <= 2.05; j += .01)
                {
                    for (double k = 14.85; k <= 14.95; k += .01)
                    {
                        string query = $"select ({i}*sum(WS_48*(MP/3936))+{j}*sum(BPM*(MP/3936))+{k}*sum(TSPercent*(MP/3936))) as TeamEstWins, " +
                            "Wins " +
                            "from advancedstats, standings " +
                            "where advancedstats.year = standings.year " +
                            "and advancedstats.Tm = standings.TeamCode " +
                            "group by wins, team, standings.year;";

                        SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
                        DataTable dtbl = new DataTable();
                        sda.Fill(dtbl);

                        int z = 0;

                        foreach (DataRow row in dtbl.Rows)
                        {
                            test1[z] = double.Parse(row["TeamEstWins"].ToString());
                            test2[z] = double.Parse(row["Wins"].ToString());

                            z++;
                        }

                        double mse = Distance.MSE(test1, test2);

                        if (mse < bestMSE)
                        {
                            bestMSE = mse;
                            Console.Clear();
                            Console.WriteLine(bestMSE + " " + i + " " + j + " " + k);
                        }
                    }
                }
            }
        }
    }
}
    


/*
for (int x = 0; x< 18518; x++)
            {
                Console.Write(x.ToString() + " ");
                Console.Write(playerStatGrid[x, 0] + " ");
                Console.Write(playerStatGrid[x, 1] + " ");
                Console.Write(playerStatGrid[x, 2] + " ");
                Console.Write(playerStatGrid[x, 3] + " ");
                Console.Write(playerStatGrid[x, 4] + " ");
                Console.Write(playerStatGrid[x, 5] + " ");
                Console.Write(playerStatGrid[x, 6] + " ");
                Console.Write(playerStatGrid[x, 7] + " ");
                Console.Write(playerStatGrid[x, 8] + " ");
                Console.Write(playerStatGrid[x, 9] + " ");
                Console.Write(playerStatGrid[x, 10] + " ");
                Console.Write(playerStatGrid[x, 11] + " ");
                Console.Write(playerStatGrid[x, 12] + " ");
                Console.Write(playerStatGrid[x, 13] + " ");
                Console.Write(playerStatGrid[x, 14] + " ");
                Console.Write(playerStatGrid[x, 15] + " ");
                Console.Write(playerStatGrid[x, 16] + " ");
                Console.Write(playerStatGrid[x, 17] + " ");
                Console.Write(playerStatGrid[x, 18] + " ");
                Console.Write(playerStatGrid[x, 19] + " ");
                Console.Write(playerStatGrid[x, 20] + " ");
                Console.Write(playerStatGrid[x, 21] + " ");
                Console.Write(playerStatGrid[x, 22] + " ");
                Console.Write(playerStatGrid[x, 23] + " ");
                Console.Write(playerStatGrid[x, 24] + " ");
                Console.Write(playerStatGrid[x, 25] + " ");
                Console.Write(playerStatGrid[x, 26] + " ");
                Console.Write(playerStatGrid[x, 27] + " ");
                Console.Write(playerStatGrid[x, 28] + " ");
                Console.Write(playerStatGrid[x, 29] + " ");
                Console.Write(playerStatGrid[x, 30] + " ");
                Console.Write(playerStatGrid[x, 31] + " ");
            }*/

