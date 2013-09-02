namespace Euler

open NUnit.Framework

module Primes =

    /// Build a sieve of Erathostenes up to n
    let buildSieve n = 
        let arr = Array.init (n+1) id // one more so that we can answer n too
        arr.[1] <- 0 // one is not a prime
        for i in 2 .. n do
            if arr.[i] <> 0 then
                for j in (i+i) .. i .. n do
                    arr.[j] <- 0
        arr

    /// Check that a given n is prime using a pre-build sieve
    let isPrime (sieve: int[]) n = sieve.[n] <> 0

    /// Get just the numbers from a sieve
    let toSet sieve = sieve |> Array.filter ((<>) 0) |> Set.ofArray

    /// Check if a number is prime naively
    let isPrimeNaive n = 
        let rec loop i = 
            if i * i > n then true
            elif n % i = 0 then false
            else loop (i+1)
        if n <= 1 then false else loop 2



    /// Tests for prime numbers
    module Tests = 

        [<Test>]
        let TestBuildSieve() = 
            let sieve = buildSieve 10
            let primes = [|0; 0; 2; 3; 0; 5; 0; 7; 0; 0; 0|]
            Assert.AreEqual(primes, sieve)


        [<Test>]
        let TestIsPrimeWithSieve() = 
            let sieve = buildSieve 10
            Assert.IsTrue( isPrime sieve 5 )
            Assert.IsFalse( isPrime sieve 8 )

        [<Test>]
        let IsPrimeNaiveWorks() = 
            let primes = [false; false; true; true; false; true; false; true; false; false; false]
            let isprim = List.init 11 isPrimeNaive
            Assert.AreEqual(primes, isprim)


