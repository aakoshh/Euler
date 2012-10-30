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


