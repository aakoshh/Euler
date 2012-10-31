namespace Euler

(*
A googol (10**100) is a massive number: one followed by one-hundred zeros; 
100**100 is almost unimaginably large: one followed by two-hundred zeros. 
Despite their size, the sum of the digits in each number is only 1.

Considering natural numbers of the form, ab, where a, b < 100, what is the maximum digital sum?
*)

open NUnit.Framework

module Euler56 = 
    
    let dsum (n: bigint) = 
        n |> string |> Seq.map (string >> int) |> Seq.sum

    let solve () = 
        seq { for a in 1I .. 99I do
                for b in 1 .. 99 do     
                    yield pown a b } 
            |> Seq.map dsum
            |> Seq.max  


    module Tests = 
        
        [<Test>]
        let TestDigitSum() = 
            Assert.AreEqual(6, dsum 123I)

        [<Test>]
        let TestSolution() = 
            Assert.AreEqual(972, solve())