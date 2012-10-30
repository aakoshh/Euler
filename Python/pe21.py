"""
http://projecteuler.net/index.php?section=problems&id=21

Let d(n) be defined as the sum of proper divisors of n
(numbers less than n which divide evenly into n).
If d(a) = b and d(b) = a, where a  b, then a and b are
an amicable pair and each of a and b are called amicable numbers.

For example, the proper divisors of 220 are
1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110;
therefore d(220) = 284.
The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.

Evaluate the sum of all the amicable numbers under 10000.
"""
from pe10 import build_sieve
from pe12 import num_divisors2, factorize2, prime_factors2
from pe5 import prod
from pe14 import memo

def make_factorizer( maxn ) :
    primes = [ p for p in build_sieve(maxn) if p ]
    return lambda n : factorize2( prime_factors2( n, primes ) )

maxn = 10000
factorizer = make_factorizer( maxn )

from itertools import product

def proper_divisors(factors) :
    multiplicants = [ [p**i for i in xrange(e+1)] for p,e in factors.items() ]
    orig_n = prod( m[-1] for m in multiplicants )
    return filter( lambda pd : pd < orig_n, (prod(ns) for ns in product(*multiplicants)) )

@memo
def d(n) :
    return sum( proper_divisors( factorizer( n ) ) )


assert d(220) == 284

def is_amicable(a) :
    b = d(a)
    return a != b and d(b) == a 

s = sum( i for i in xrange(10000) if is_amicable(i) )

assert s == 31626
