"""
http://projecteuler.net/index.php?section=problems&id=5

2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.

What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?

"""

def prod( ns ) :
    i = 1
    for n in ns :
        i *= n
    return i

#naive smallest common multiplicant
def scm( ns ) : 
    i = 1
    m = prod(ns)
    while not all( i % d == 0 for d in ns ) and i < m :
        i += 1
    return i

assert scm( range(10,0,-1) ) == 2520


from pe3 import prime_factors
from collections import defaultdict

def factorize( n ) :
    f = defaultdict(int)
    for p in prime_factors(n) :
        f[p] += 1
    return f

def scmf( ns ) :
    f = defaultdict(int)
    for n in ns :
        for p, e in factorize(n).items() :
            f[p] = max(f[p], e)
    return prod( p**e for p, e in f.items() )


assert scmf( range(20,0,-1) ) == 232792560
