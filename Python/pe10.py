"""
http://projecteuler.net/index.php?section=problems&id=10

The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.

Find the sum of all the primes below two million.
"""

from pe7 import primes
from itertools import takewhile
from math import sqrt

#sp = sum( takewhile( lambda p : p < 2*10**6, primes() ) ) 

prime_cache = [2]

def primes_cached() :
    for i in prime_cache :
        yield i
    while True :
        i += 1
        sqrti = sqrt(i)
        for p in takewhile( lambda p : p <= sqrti, prime_cache ) :
            if i % p == 0 : break
        else :
            prime_cache.append(i)
            yield i

#sp = sum( takewhile( lambda p : p < 2*10**6, primes_cached() ) )

def build_sieve( maxp ) :
    #start with the first prime, mark each multiplicant as non-prime
    sieve = list(range(maxp))
    sieve[0] = sieve[1] = 0
    for i in xrange(2,int(sqrt(maxp))+1) :
        if sieve[i] :
            for m in xrange(2*i, len(sieve), i) :
                sieve[m] = 0
    return sieve

sp = sum( build_sieve(2*10**6) )                

assert sp == 142913828922
