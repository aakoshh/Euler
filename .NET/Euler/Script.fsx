// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.
#r "nunit.framework.dll"
#load "Utils.fs"
#load "Primes.fs"
#load "Combinatorics.fs"
#load "Euler58.fs"
open Euler
open Utils


Euler.Euler58.solve()

