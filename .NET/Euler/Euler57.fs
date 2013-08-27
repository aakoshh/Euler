namespace Euler

(*
It is possible to show that the square root of two can be expressed as an infinite continued fraction.

√ 2 = 1 + 1/(2 + 1/(2 + 1/(2 + ... ))) = 1.414213...

By expanding this for the first four iterations, we get:

1 + 1/2 = 3/2 = 1.5
1 + 1/(2 + 1/2) = 7/5 = 1.4
1 + 1/(2 + 1/(2 + 1/2)) = 17/12 = 1.41666...
1 + 1/(2 + 1/(2 + 1/(2 + 1/2))) = 41/29 = 1.41379...

The next three expansions are 99/70, 239/169, and 577/408, but the eighth expansion, 1393/985, 
is the first example where the number of digits in the numerator exceeds the number of digits in the denominator.

In the first one-thousand expansions, how many fractions contain a numerator with more digits than denominator?
*)

module Euler57 = 

    open Utils

    let rec gcd a b = 
        if b = 0I then a else gcd b (a % b)


    type Fraction = { Nominator: bigint; Denominator: bigint }
    with 

        member x.Simplify() = 
            let d = gcd x.Nominator x.Denominator
            { Nominator = x.Nominator / d
              Denominator = x.Denominator / d }

        member x.Reciproc() = 
            { Nominator = x.Denominator 
              Denominator = x.Nominator }
        
        member x.Add(y: Fraction) = 
            { Nominator = x.Nominator * y.Denominator + y.Nominator * x.Denominator
              Denominator = x.Denominator * y.Denominator }.Simplify()

        member x.Mul(y: Fraction) = 
            { Nominator = x.Nominator * y.Nominator
              Denominator = x.Denominator * y.Denominator }.Simplify()

        member x.Div(y: Fraction) = 
            x.Mul(y.Reciproc())


    let frac a b = { Nominator = a; Denominator = b } 
    let (+) (a: Fraction) b = a.Add(b)
    let (/) (a: Fraction) b = a.Div(b)

    let one, two = frac 1I 1I, frac 2I 1I


    let rec expand = memoize <| fun n -> 
        if n = 0 then
            one / two
        else
            one / (two + expand (n-1))


    let expansion n = 
        one + (expand n)


    let solve () = 
        { 0 .. 999 } 
            |> Seq.map expansion
            |> Seq.filter (fun f -> (string f.Nominator).Length > (string f.Denominator).Length)
            |> Seq.length


    module Tests = 

        open NUnit.Framework
        
        [<Test>]
        let AddingFractionsWorks() = 
            let s = (frac 1I 1I) + (frac 1I 2I)
            Assert.AreEqual(frac 3I 2I, s)

        [<Test>]
        let AddingAndDividingWorks() = 
            let s = one + one / (two + one / two)
            Assert.AreEqual(frac 7I 5I, s)

        [<Test>]
        let FirstExpansionsAreCorrect() = 
            [3,2; 7,5; 17,12; 41,29; 99,70; 239,169; 577,408; 1393,985]
            |> List.map (fun (a,b) -> 
                frac (bigint a) (bigint b))
            |> List.iteri (fun i f -> 
                Assert.AreEqual (f, expansion i))
        
        [<Test>]
        let FinalSolutionIsCorrect() = 
            Assert.AreEqual(153, solve())