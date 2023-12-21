# Hash Calc

A simple tool for calculating expected income, electricity cost and energy usage on a per-block, per-hour, per-day and per-year basis for arbitrarty mining setups given expected network hash rates, block rewards, and some information about a mining setup.

Obviously these values are exploratory, and cannot be guaranteed. If the values you input are wrong, expect wrong output.

## Usage

You need the .NET SDK installed, with the ability to target .NET 7.0, and can either run `./run.ps1 [NetworkTH/s] [Joules/TH] [PricePerCoin] [BlockReward] [MiningWatts] [Price/KWh]`, or, if you don't have/want to use PowerShell, `dotnet run --project ./src/PoodleLabs.HashCalc/PoodleLabs.HashCalc.csproj -- [NetworkTH/s] [Joules/TH] [PricePerCoin] [BlockReward] [MiningWatts] [Price/KWh]`.

An argument of `--help`, `-help`, `help`, or `h` will print some information about the required parameters.

For example:
```
./run.ps1 500000000 21.5 100000 3.125 3247 0.1
```

For a network hashrate of 500m TH/s, for a single S19J XP with an electricity cost of $0.10 KW/h and a Bitcoin price of $100,000, outputs:

```
Hash Rate: 151.02 TH/s

Expected values per Block:
Gross Mining Reward: 0.09439
Electricity:         0.05412
Net Income:          0.04027
Watts Used:          541.16667

Expected values per Hour:
Gross Mining Reward: 0.56634
Electricity:         0.3247
Net Income:          0.24164
Watts Used:          3247

Expected values per Day:
Gross Mining Reward: 13.59209
Electricity:         7.7928
Net Income:          5.79929
Watts Used:          77928

Expected values per Year:
Gross Mining Reward: 4964.41004
Electricity:         2846.26175
Net Income:          2118.14828
Watts Used:          28462617.54
```
