# Hash Calc

A simple tool for calculating expected income, electricity cost and energy usage on a per-block, per-hour, per-day and per-year basis for arbitrarty mining setups given expected network hash rates, block rewards, and some information about a mining setup.

Obviously these values are exploratory, and cannot be guaranteed. If the values you input are wrong, expect wrong output.

## Usage

You need the .NET SDK installed, with the ability to target .NET 7.0, and can either run `./run.ps1 [NetworkTH/s] [Joules/TH] [PricePerCoin] [BlockReward] [MiningWatts] [Price/KWh]`, or, if you don't have/want to use PowerShell, `dotnet run --project ./src/PoodleLabs.HashCalc/PoodleLabs.HashCalc.csproj -- [NetworkTH/s] [Joules/TH] [PricePerCoin] [BlockReward] [MiningWatts] [Price/KWh]`.

For example:
```
./run.ps1 500000000 21.5 100000 3.125 3247 0.1
```

An argument of `--help`, `-help`, `help`, or `h` will print some information about the required parameters.
