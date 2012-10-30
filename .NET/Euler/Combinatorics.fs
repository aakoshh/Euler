namespace Euler

open NUnit.Framework

module Combinatorics = 
    open Euler.Utils

    let rec powerset lst = seq {
        match lst with
        | [] -> yield []
        | h::t ->
            for ps in powerset t do
                yield ps
                yield h::ps }


    let rec fact = memoize(fun x -> 
        match x with
        | _ when x <= 1I -> 1I
        | x -> x * fact(x-1I))

    let combine n k = (fact n) / (fact (n-k)) / (fact k)


    module Tests = 

        [<Test>]
        let TestPowerSet () = 
            let ps = powerset [1;2] |> Seq.toList
            let ex = [[]; [1]; [2]; [1; 2]]
            Assert.AreEqual(ex, ps)

        [<Test>]
        let TestCombine () = 
            Assert.AreEqual(10I, (combine 5I 3I))
            Assert.AreEqual(1144066I, (combine 23I 10I))



