"""
http://projecteuler.net/index.php?section=problems&id=14

The following iterative sequence is defined for the set of positive integers:

n  n/2 (n is even)
n  3n + 1 (n is odd)

Using the rule above and starting with 13, we generate the following sequence:

13  40  20  10  5  16  8  4  2  1
It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.

Which starting number, under one million, produces the longest chain?

NOTE: Once the chain starts the terms are allowed to go above one million.
"""

"""
def memo(function=None,keyargs=None) :
    def decorator(f) :
        cache = {}
        def wrapper(*args) :
            keys = args if not keyargs else args[:keyargs]
            if keys not in cache :
                cache[keys] = f(*args)
            return cache[keys]
        wrapper.cache = cache
        return wrapper
    if function :
        return decorator(function)
    return decorator
"""
def memo(f) :
    cache = {}
    def wrapper(*args) :
        if args not in cache :
            cache[args] = f(*args)
        return cache[args]
    wrapper.cache = cache
    return wrapper

def nextseq(n) :
    return n/2 if n % 2 == 0 else 3 * n + 1

def chain(n) :
    yield n
    while n != 1 :
        n = nextseq(n)
        yield n

@memo #allow for sequences to merge
def chainrec(n) :
    if n == 1 :
        return [1]
    n = nextseq(n)
    return [n] + chainrec(n)        

@memo
def chain_length(n) :
    if n == 1 :
        return 1 
    n = nextseq(n)
    return chain_length(n) + 1

cache = {}
def chain_length_norec(n) :
    if n not in cache :        
        s = 1; ni = n
        while ni != 1 :            
            ni = nextseq(ni)
            if ni in cache :
                s += cache[ni]
                break
            else :
                s += 1
        cache[n] = s
    return cache[n]


import sys
sys.setrecursionlimit(2000)
s = max( (chain_length_norec(i), i) for i in xrange(1,10**6) ) 

assert s[1] == 837799        
