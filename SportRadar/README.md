# Live Football World Cup Score Board Library

Welcome to the Live Football World Cup Score Board Library! This simple library allows you to manage and track live football matches, providing functionalities to start new matches, update scores, finish matches, and retrieve a summary of ongoing matches.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
  - [Starting a New Match](#starting-a-new-match)
  - [Updating Scores](#updating-scores)
  - [Finishing Matches](#finishing-matches)
  - [Getting Summary](#getting-summary)
- [Example](#example)
- [Limitations](#limitations)

## Installation

To use this library in your C# project, simply add the compiled library to your project references. If you are using a package manager, you can publish the library as a NuGet package.

## Usage

### Starting a New Match

```csharp
var mainService = new MianService();
mainService.StartMatch("HomeTeamName", "AwayTeamName");
```

### Updating Scores
```csharp
mainService.UpdateScore("HomeTeamName", "AwayTeamName", 3, 4);
```

### Finishing Matches
```csharp
mainService.FinishMatch("HomeTeamName", "AwayTeamName");
```

### Getting Summary
```csharp
var summary = mainService.GetSummary();
foreach (var matchSummary in summary)
{
    Console.WriteLine(matchSummary);
}
```

### Example
```csharp
var mainService = new MianService();
mainService.StartMatch("Mexico", "Canada");
mainService.UpdateScore("Mexico", "Canada", 0, 5);
mainService.UpdateScore("Mexico", "Canada", 3, 8);
mainService.FinishMatch("Mexico", "Canada");

mainService.StartMatch("Spain", "Brazil");
mainService.UpdateScore("Spain", "Brazil", 10, 2);

mainService.StartMatch("Germany", "France");
mainService.UpdateScore("Germany", "France", 2, 2);

mainService.StartMatch("Uruguay", "Italy");
mainService.UpdateScore("Uruguay", "Italy", 6, 6);

mainService.StartMatch("Argentina", "Australia");
mainService.UpdateScore("Argentina", "Australia", 3, 1);

var summary = mainService.GetSummary();
foreach (var matchSummary in summary)
{
    Console.WriteLine(matchSummary);
}
```

### Limitations

- Class MatchRepository - missing tests
- No interfaces
- No dependency injection

- Team cannot play multiple matches at the same time. It will be treated as dublicate record even after swapping Home and Away Teams.

"Canada" : "Mexico"
"Canada" : "Germany"
"Germany" : "Canada"

- Scores can be set to any value except negative. 
