namespace Euler

open Euler
open NUnit.Framework
open Combinatorics

module Euler51 =

    (*
    By replacing the 1st digit of *3, it turns out that six of the nine possible values: 
    13, 23, 43, 53, 73, and 83, are all prime.

    By replacing the 3rd and 4th digits of 56**3 with the same digit, 
    this 5-digit number is the first example having seven primes among the ten generated numbers, 
    yielding the family: 56003, 56113, 56333, 56443, 56663, 56773, and 56993. 
    Consequently 56003, being the first member of this family, 
    is the smallest prime with this property.

    Find the smallest prime which, by replacing part of the number 
    (not necessarily adjacent digits) with the same digit, 
    is part of an eight prime value family.
    *)
    
    (*
    - all members will have the same number of digits -> 
        group primes by number of digits
    - start with the smallest number and try to replace digits with higher ones ->
        max number of family members depends on how close the digit is to 9, so look for at least n
    - when we have a replacable digit, we can 
        a) we use it, then we can try to replace any other occurrence of the same digit to the right with candidates
        b) skip it and try the next replacable digit
    *)

    /// digit of n at a specific exponent (0 is the rightmost digit)
    let digit e n = 
        let i = pown 10 e
        (n / i) % 10

    /// replace the e digit from the right of n with x
    let replaceDigit e x n = 
        let d = digit e n
        let i = pown 10 e
        n - d*i + x*i

    let rec replaceDigits es x n = 
        match es with
        | [] -> n
        | h::t -> replaceDigits t x (replaceDigit h x n)
        

    /// build up the prime numbers with d digits
    let dDigitPrimes d =
        let mini, maxi = pown 10 (d-1), (pown 10 d)-1 
        let sieve = Primes.buildSieve maxi
        let primes = sieve |> Array.filter (fun n -> mini <= n && n <= maxi) |> List.ofArray
        primes, sieve

    /// is the digit d a candidate for replacement if we need f families
    let isCandidate f d = 
        9 - d + 1 >= f

    /// get the exponents that can be replaced with hope to form f size family
    let candidateDigits f n = 
        let s = n |> string
        let nd = s.Length
        s |> Seq.mapi (fun i c -> i, c |> string |> int)
          |> Seq.filter (fun (_,d) -> isCandidate f d)
          |> Seq.map (fun (i,_) -> nd-i-1)
          |> List.ofSeq             
          
    /// try to find f size family that can be generated from p
    let generateFamilies f p = 
        // see which places we can try to replace
        let cd = candidateDigits f p
        // for each combination of the candidate digits
        // see what the leftmost can be replaced to and generate the families with those.
        // no need to test the other digits because if the first digit could be replaced
        // with something smaller we would have already tested that prime number
        seq {
        for cds in powerset cd do // don't match the empty set  
            if not cds.IsEmpty then
                // see what the leftmost digit is and replace every other candidate with it          
                let e = cds |> List.head
                let d = digit e p
                let family = [for r in d .. 9 do // replace everything in the digit set
                                yield p |> replaceDigits cds r]
                yield family }

        
    /// search for f size families in primes with d digits
    let search f d =         
        let primes, sieve = dDigitPrimes d
        // go through the primes and generate family candidates of size f
        // then search all the families so that each member is a prime
        primes
        |> Seq.collect (generateFamilies f) // map every prime to a sequence of lists
        |> Seq.map (List.filter (Primes.isPrime sieve))
        |> Seq.filter (fun family -> family.Length = f)

    
    /// find the smallest prime with and f size family
    let solve f = 
        let rec loop d = 
            let fs = search f d
            if Seq.isEmpty fs then
                loop (d+1)
            else
                fs |> Seq.head |> List.head
        loop 1

    module Tests = 

        [<Test>]
        let TestDigit () = 
            Assert.AreEqual(3, digit 2 12345)

        [<Test>]
        let TestPrimeDigits () = 
            let primes, _ = dDigitPrimes 2
            let expected = [11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71; 73; 79; 83; 89; 97]
            Assert.AreEqual(expected, primes)

        [<Test>]
        let TestDigitCandidacy () = 
            Assert.IsTrue(isCandidate 7 2)
            Assert.IsTrue(isCandidate 7 3)
            Assert.IsFalse(isCandidate 7 4)

        [<Test>]
        let TestCandidates () = 
            let cand = candidateDigits 7 56003
            let expd = [2; 1; 0]
            Assert.AreEqual(expd, cand)

        [<Test>]
        let TestReplaceDigits () = 
            let repl = 56003 |> replaceDigits [2;1] 1
            let expd = 56113
            Assert.AreEqual(expd, repl)

        [<Test>]
        let TestFamilies () = 
            let faml = 13 |> generateFamilies 6 |> List.ofSeq
            let expd = [[13; 23; 33; 43; 53; 63; 73; 83; 93];
                        [13; 14; 15; 16; 17; 18; 19];
                        [11; 22; 33; 44; 55; 66; 77; 88; 99]]
            Assert.AreEqual(expd, faml)
    
        [<Test>]
        let TestSearch2Digit6Primes () = 
            let faml = search 6 2 |> Seq.head
            let expd = [13; 23; 43; 53; 73; 83]
            Assert.AreEqual(expd, faml)

        [<Test>]
        let TestSearch5Digit7Primes () = 
            let faml = search 7 5 |> Seq.head
            let expd = [56003; 56113; 56333; 56443; 56663; 56773; 56993]
            Assert.AreEqual(expd, faml)

        [<Test>]
        let TestSolution6Primes () = 
            Assert.AreEqual(solve 6, 13)

        [<Test>]
        let TestSolution7Primes () = 
            Assert.AreEqual(solve 7, 56003)

        [<Test>]
        let TestSolution8Primes () = 
            Assert.AreEqual(solve 8, 121313)
