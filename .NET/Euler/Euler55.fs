namespace Euler

(*
If we take 47, reverse and add, 47 + 74 = 121, which is palindromic.

Not all numbers produce palindromes so quickly. For example,

349 + 943 = 1292,
1292 + 2921 = 4213
4213 + 3124 = 7337

That is, 349 took three iterations to arrive at a palindrome.

Although no one has proved it yet, it is thought that some numbers, like 196, 
never produce a palindrome. A number that never forms a palindrome through 
the reverse and add process is called a Lychrel number. 
Due to the theoretical nature of these numbers, and for the purpose of this problem,
 we shall assume that a number is Lychrel until proven otherwise. 
 In addition you are given that for every number below ten-thousand, 
 it will either (i) become a palindrome in less than fifty iterations, 
 or, (ii) no one, with all the computing power that exists, 
 has managed so far to map it to a palindrome. 
 In fact, 10677 is the first number to be shown to require over fifty
  iterations before producing a palindrome: 4668731596684224866951378664 (53 iterations, 28-digits).

Surprisingly, there are palindromic numbers that are themselves Lychrel numbers; the first example is 4994.

How many Lychrel numbers are there below ten-thousand?

NOTE: Wording was modified slightly on 24 April 2007 to emphasise the theoretical nature of Lychrel numbers.
*)


open NUnit.Framework


module Euler55 = 
    
    open Euler.Utils
    open System

    let rev (s: string) = 
        new String(s |> Array.ofSeq |> Array.rev)

    let isPalindrome n = 
        let s = string n
        s = rev s

    let MAXITER = 50

    // a number can be reached by two different sums surely
    // so we can optimize by shortcutting the search paths 
    let rec seekPalindromeIter = memoizeFst <| fun (n: bigint, i) ->
        let rn = bigint.Parse( n |> string |> rev )
        let nxt = n + rn
        if isPalindrome nxt then
            Some(nxt)
        elif i < MAXITER then 
            // the chain can contain numbers over 10000. 
            // it's just that if it started below, then it finishes in 50 steps
            seekPalindromeIter (nxt, i+1)
        else
            None

    let seekPalindrome n = seekPalindromeIter (n, 0)
    
    /// count non-terminating numbers below 10000
    let solve () = 
        [1I .. 9999I] |> List.filter (seekPalindrome >> Option.isNone) |> List.length


    module Tests = 

        [<Test>]
        let TestPalindrome () = 
            Assert.IsTrue( isPalindrome 12321I )
            Assert.IsFalse( isPalindrome 1234I )

        [<Test>]
        let TestSeek () = 
            Assert.AreEqual( Some(7337I), seekPalindrome 349I )
            Assert.AreEqual( Some(121I), seekPalindrome 47I )
            Assert.AreEqual( None, seekPalindrome 4994I )

        [<Test>]
        let TestSolution () = 
            Assert.AreEqual( 249, solve() )


