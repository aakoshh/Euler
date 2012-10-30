"""
http://projecteuler.net/index.php?section=problems&id=37

The number 3797 has an interesting property.
Being prime itself, it is possible to continuously remove
digits from left to right, and remain prime at each stage:
3797, 797, 97, and 7.
Similarly we can work from right to left:
3797, 379, 37, and 3.

Find the sum of the only eleven primes that are
both truncatable from left to right and right to left.

NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes
"""

from pe10 import primes_cached, prime_cache
from bisect import bisect_left

def index(lst, x):
    'Locate the leftmost value exactly equal to x'
    i = bisect_left(lst, x)
    if i != len(lst) and lst[i] == x:
        return i
    return -1

is_prime = lambda p : index(prime_cache, p) != -1

def is_truncatable(p) :
    if p < 10 : return False
    digits = list(str(p))
    return all( is_prime(int(''.join(digits[:i])))
            and is_prime(int(''.join(digits[i:])))
            for i in xrange(1,len(digits)))

#zip to take exactly 11 elements (takewhile would never find the 12th)
tp = list( p for i,p in zip( xrange(11), ( p for p in primes_cached() if is_truncatable(p)) ))

s = sum(tp)

assert s == 748317
    
