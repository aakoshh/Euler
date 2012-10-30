"""
http://projecteuler.net/index.php?section=problems&id=9
A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
a2 + b2 = c2

For example, 32 + 42 = 9 + 16 = 25 = 52.

There exists exactly one Pythagorean triplet for which a + b + c = 1000.
Find the product abc.

"""
from pe5 import prod

def find( k ) :
    # a + b > c; c = k - a - b
    # b > k/2 - a
    for a in xrange(1, k/3) :
        for b in xrange( k/2-a, k/2 ) :
            c = k - a - b
            if a**2 + b**2 == c**2 :
                return a,b,c

assert prod(find(1000)) == 31875000
