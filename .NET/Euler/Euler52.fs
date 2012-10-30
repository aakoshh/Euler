namespace Euler
// http://projecteuler.net/problem=52
// It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.
// Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.

open NUnit.Framework

module Euler52 = 
    open System
        
    let digits (x: int) = x.ToString() |> List.ofSeq |> List.sort

    let e52() = 
        Seq.initInfinite (fun x -> x+1)   
        |> Seq.filter (fun x -> let x = float x
                                x <= 0.17*10.0**(Math.Ceiling (Math.Log10 x)))    
        |> Seq.filter (fun x -> let ds = digits x
                                Seq.forall (fun m -> digits (m*x) = ds) [2;3;4;5;6] )
        |> Seq.head


    [<Test>]
    let test() =
        Test.assertEqual (digits 125874) (digits 251748) "Test digits"
        Test.assertEqual (e52()) 142857 "Solution"
