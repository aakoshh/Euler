namespace Euler

(*
Starting with 1 and spiralling anticlockwise in the following way, a square spiral with side length 7 is formed.

37 36 35 34 33 32 31
38 17 16 15 14 13 30
39 18  5  4  3 12 29
40 19  6  1  2 11 28
41 20  7  8  9 10 27
42 21 22 23 24 25 26
43 44 45 46 47 48 49

It is interesting to note that the odd squares lie along the bottom right diagonal, 
but what is more interesting is that 8 out of the 13 numbers lying along both diagonals are prime; 
that is, a ratio of 8/13 ≈ 62%.

If one complete new layer is wrapped around the spiral above, a square spiral with side length 9 will be formed. 
If this process is continued, what is the side length of the square spiral for which the ratio of primes 
along both diagonals first falls below 10%?
*)

module Euler58 =     
    open Primes
    open Utils

    let isPrime = memoize isPrimeNaive
    
    /// Enumerate the diagonal elements of the layers of the square.
    let corners = 
        // lower right corner is always a square number of n+2: 3,5,7,9,...
        let rec loop n = seq {
            let a = n * n
            let b = (n-2)*(n-2) + 1
            let d = (a - b) / 4 + 1
            yield a-3*d, a-2*d, a-d, a
            yield! loop (n+2)
        }
        loop 3
    
    
    /// Enumerate the subsequent number of primes on the diagonals of squares of given lengths.
    let primeRatios = 
        corners |> Seq.scan (fun (nom,denom,len) (a,b,c,d) -> 
            let ps    = [a;b;c;d] |> List.filter isPrime 
            let len   = d - c + 1
            let denom = denom + 4            
            let nom   = nom + (ps |> List.length)
            nom, denom, len)
            (0, 1, 1)


    let solve() = 
        let rec loop next = 
            let n,d,l = next()
            if d > 10 * n then l else loop next
        primeRatios |> Seq.skip 1 |> makeNext |> loop


    module Tests = 

        open NUnit.Framework
        
        [<Test>]
        let CornersCanBeEnumerated() = 
            let cs = corners |> Seq.take 3 |> List.ofSeq
            Assert.AreEqual([(3, 5, 7, 9); (13, 17, 21, 25); (31, 37, 43, 49)], cs)

        [<Test>]
        let RatiosCanBeEnumerated() = 
            let rs = primeRatios |> Seq.take 4 |> List.ofSeq
            Assert.AreEqual([(0,1,1);(3,5,3);(5,9,5);(8,13,7)], rs)

        [<Test>]
        let SolutionIsCorrect() = 
            Assert.AreEqual(26241, solve())