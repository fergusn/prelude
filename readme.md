## Overview
Prelude is a library with a number of usefull utility fucntions and classes that encourage functional composition in C#.

## Examples

```csharp
using static Prelude;

// Option
var options = new[] { Some(2), Some(4), None };
var ab = from a in options[0]
         from b in options[1]
         where b != 0
         select a / b;

var c = ab.GetOrElse(0); 

// Task 
var tasks = new[] { Task.FromResult(5), Task.FromResult(6) };
var xy = await from x in tasks[0]
               from y in tasks[1]
               select x + y;

var z = xy.Timeout(2.minutes())
          .RecoverWith(ex => 0);

// Try
var attempts = new [] { Try(() => 2), Try(() => 3), Try(() => 4) };

var xyz = from x in attempts[0]
          from y in attempts[1]
          from z in attempts[2]
          select x + y + z;

// Timespan
await 5.seconds();

// Try


// Collections
var xs = List(1, 2, 3);
var ys = Map(Pair(1, "One"), Pair(2, "Two"));

```