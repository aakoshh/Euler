// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.
#r "nunit.framework.dll"
#load "Primes.fs"
#load "Combinatorics.fs"
#load "Euler51.fs"
open Euler

Euler51.search 8 6 |> Seq.head
