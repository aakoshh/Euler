namespace Euler

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

    /// Only use the first member of the passed tuple for memoization,
    /// allowing the second parameter to be used as context (to control recursion).
    let memoizeFst f = 
        let cache = Dictionary<_,_>(HashIdentity.Structural)
        fun (args,ctx) ->
            match cache.TryGetValue(args) with
            | (true, v) -> v
            | _ ->  let v = f(args, ctx) 
                    cache.Add(args, v)
                    v
    