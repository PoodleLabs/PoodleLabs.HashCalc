// Poodle Labs' Hash Calc (BST)
// Copyright (C) 2023 Isaac Beizsley (isaac@poodlelabs.com)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Contains("--help") | args.Contains("-help") | args.Contains("help") | args.Contains("h"))
        {
            Console.WriteLine("6 arguments required: NetworkTH/s Joules/TH PricePerCoin BlockReward MiningWatts Price/KWh");
            Console.WriteLine("  - Network TH/s");
            Console.WriteLine("    The mean expected total network hashrate over the mining period denominated in TH/s, eg: '500,000,000'.");
            Console.WriteLine("  - Joules/TH");
            Console.WriteLine("    The weighted mean Joules/Terahash for your mining hardware; an S19J XP, for example, has a value of '21.5'.");
            Console.WriteLine("  - Price Per Coin");
            Console.WriteLine("    The price of 1 BTC in your target currency. '1' for values denominated in Bitcoin, or '100,000' for $100,000.");
            Console.WriteLine("  - Block Reward");
            Console.WriteLine("    The mean expected reward per block in BTC. For example, subsidy-only could be '6.25' or '3.125'.");
            Console.WriteLine("  - Mining Watts");
            Console.WriteLine("    The mean expected watts you expect to devote to mining. For example, a single S19J XP running 24/7 would be '3247'.");
            Console.WriteLine("  - Price KW/h");
            Console.WriteLine("    The mean expected price of your electricity, in your target currency per KW/h. For example, $0.10/KW/h would be '0.1'. Any number is accepted, including '0' and even negatives.");
            return 0;
        }

        if (args.Length != 6)
        {
            Console.WriteLine("Expected 6 arguments: NetworkTH/s Joules/TH PricePerCoin BlockReward MiningWatts Price/KWh");
            return 1;
        }

        if (!decimal.TryParse(args[0], out var networkHashRate) || networkHashRate <= 0)
        {
            Console.WriteLine("Invalid network TH/s.");
            return 2;
        }

        if (!decimal.TryParse(args[1], out var joulesPerTeraHash) || joulesPerTeraHash <= 0)
        {
            Console.WriteLine("Invalid Joules/TH.");
            return 3;
        }

        if (!decimal.TryParse(args[2], out var pricePerCoin) || pricePerCoin <= 0)
        {
            Console.WriteLine("Invalid price per coin.");
            return 4;
        }

        if (!decimal.TryParse(args[3], out var blockReward) || blockReward <= 0)
        {
            Console.WriteLine("Invalid block reward.");
            return 5;
        }

        if (!decimal.TryParse(args[4], out var miningWatts) || miningWatts <= 0)
        {
            Console.WriteLine("Invalid mining watts.");
            return 6;
        }

        if (!decimal.TryParse(args[5], out var priceKwh))
        {
            Console.WriteLine("Invalid price/KWh.");
            return 6;
        }

        var denominatedBlockReward = blockReward * pricePerCoin;
        var ownHashRate = miningWatts / joulesPerTeraHash;

        var hashShare = ownHashRate / networkHashRate;
        var rewardPerBlock = denominatedBlockReward * hashShare;

        var wattsPerBlock = miningWatts / 6;
        var blockCost = wattsPerBlock * (priceKwh / 1000);

        Console.WriteLine($"Hash Rate: {ownHashRate:0.##} TH/s");
        Console.WriteLine();
        void WriteValuesForBlockCount(string periodDescription, decimal blockCount)
        {
            Console.WriteLine($"Expected values per {periodDescription}:");
            if (blockCost != 0)
            {
                var reward = rewardPerBlock * blockCount;
                var electricity = blockCost * blockCount;
                var netIncome = reward - electricity;
                Console.WriteLine($"Gross Mining Reward: {reward:0.#####}");
                Console.WriteLine($"Electricity:         {electricity:0.#####}");
                Console.WriteLine($"Net Income:          {netIncome:0.#####}");
                Console.WriteLine($"Watts Used:          {wattsPerBlock * blockCount:0.#####}");
            }
            else
            {
                Console.WriteLine($"Net Income: {rewardPerBlock * blockCount:0.#####}");
                Console.WriteLine($"Watts Used: {wattsPerBlock * blockCount:0.#####}");
            }


            Console.WriteLine();
        }

        WriteValuesForBlockCount("Block", 1);
        WriteValuesForBlockCount("Hour", 6);
        WriteValuesForBlockCount("Day", 6 * 24);
        WriteValuesForBlockCount("Year", 6 * 24 * 365.2425m);
        return 0;
    }
}
