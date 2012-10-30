namespace Euler

// http://projecteuler.net/problem=53
(*
There are exactly ten ways of selecting three from five, 12345:

123, 124, 125, 134, 135, 145, 234, 235, 245, and 345

In combinatorics, we use the notation, 5C3 = 10.

In general,

nCr =	
n!
r!(nr)!
,where r  n, n! = n(n1)...321, and 0! = 1.
It is not until n = 23, that a value exceeds one-million: 23C10 = 1144066.

How many, not necessarily distinct, values of  nCr, for 1  n  100, are greater than one-million?
*)
open NUnit.Framework

module Euler53 = 
    open Combinatorics

    let e53() = 
        seq{ for n in [1I..100I] do
                for r in 1I..n do
                    if (combine n r) > 10I**6 then
                        yield (n,r) } 
        |> Seq.length

    open Test

    [<Test>]
    let test() = 
        Test.assertEqual (e53()) 4075 "e53 solution"
    
                                

