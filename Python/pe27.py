# -*- coding: cp1252 -*-
"""
http://projecteuler.net/index.php?section=problems&id=27

Euler published the remarkable quadratic formula:

n² + n + 41

It turns out that the formula will produce 40 primes for the
consecutive values n = 0 to 39.
However, when n = 40, 402 + 40 + 41 = 40(40 + 1) + 41 is divisible by 41,
and certainly when n = 41, 41² + 41 + 41 is clearly divisible by 41.

Using computers, the incredible formula  n²  79n + 1601 was discovered,
which produces 80 primes for the consecutive values n = 0 to 79.
The product of the coefficients, 79 and 1601, is 126479.

Considering quadratics of the form:

n² + an + b, where |a|  1000 and |b|  1000

where |n| is the modulus/absolute value of n
e.g. |11| = 11 and |4| = 4
Find the product of the coefficients, a and b,
for the quadratic expression that produces the maximum
number of primes for consecutive values of n, starting with n = 0.
"""

from pe10 import build_sieve
from itertools import takewhile

def qf(n,a,b) :
    return n**2 + a*n + b

maxab = 1000

sieve = build_sieve(qf(maxab, maxab, maxab)) #at n=a=b it will not be prime that's for sure

is_prime = lambda n : sieve[n] != 0

r = xrange(-maxab+1,maxab)

gen_n = lambda a,b : takewhile( lambda n : is_prime(n),
                                (qf(i,a,b) for i in xrange(maxab)) )

assert len(list(gen_n(1,41))) == 40
assert len(list(gen_n(-79,1601))) == 80   

s = max( (len(list(gen_n(a,b))), a,b) for a in r for b in r )
s = s[1] * s[2]

assert s == -59231
