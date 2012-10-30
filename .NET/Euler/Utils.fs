﻿namespace Euler

module Test = 
    let assertEqual x y msg = 
        if x <> y then
            failwith (sprintf "%s failed test. %A should be %A" msg x y)


module Utils = 
    open System.Collections.Generic

    let memoize f = 
        let cache = Dictionary<_,_>(HashIdentity.Structural)
        fun args ->
            match cache.TryGetValue(args) with
            | (true, v) -> v
            | _ ->  let v = f(args)
                    cache.Add(args, v)
                    v
    