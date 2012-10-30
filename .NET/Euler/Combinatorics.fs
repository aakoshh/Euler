namespace Euler

open NUnit.Framework

module Combinatorics = 
    
    let rec powerset lst = seq {
        match lst with
        | [] -> yield []
        | h::t ->
            for ps in powerset t do
                yield ps
                yield h::ps }


    module Tests = 

        [<Test>]
        let TestPowerSet () = 
            let ps = powerset [1;2] |> Seq.toList
            let ex = [[]; [1]; [2]; [1; 2]]
            Assert.AreEqual(ex, ps)

