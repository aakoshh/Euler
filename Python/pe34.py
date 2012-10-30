"""
http://projecteuler.net/index.php?section=problems&id=34

145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.

Find the sum of all numbers which are equal to the sum of the
factorial of their digits.

Note: as 1! = 1 and 2! = 2 are not sums they are not included.
"""

from itertools import count

def gen_fact() :
    f = 1
    for i in count(1) :
        f *= i
        yield i,f

from pe15 import fact
from math import log10, ceil

def gen_curious() :
    maxdigits = int(ceil(log10(fact(9)))) #sum cannot keep pace above
    for i in xrange(10, 10**maxdigits) :
        if i == sum( fact(int(d)) for d in str(i) ) :
            yield i

cn = list(gen_curious())            
s = sum(cn)
            
assert s == 40730
