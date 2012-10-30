"""
http://projecteuler.net/index.php?section=problems&id=23

A perfect number is a number for which the sum of its proper
divisors is exactly equal to the number. For example,
the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28,
which means that 28 is a perfect number.

A number n is called deficient if the sum of its proper
divisors is less than n and it is called abundant if this sum exceeds n.

As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16,
the smallest number that can be written as the sum of two abundant
numbers is 24. By mathematical analysis, it can be shown that
all integers greater than 28123 can be written as the sum of
two abundant numbers. However, this upper limit cannot be reduced
any further by analysis even though it is known that the greatest
number that cannot be expressed as the sum of two abundant numbers
is less than this limit.

Find the sum of all the positive integers which cannot be written
as the sum of two abundant numbers.
"""

from pe21 import proper_divisors
from pe10 import primes_cached
from pe12 import factorize2, prime_factors2

def factorizer(n) :
    return factorize2( prime_factors2( n, primes_cached() ) )

assert sum(proper_divisors(factorizer(12))) == 16
assert sum(proper_divisors(factorizer(28))) == 28

def is_abundant( n ):
    return n < sum(proper_divisors(factorizer(n)))

ans = [i for i in xrange(30000) if is_abundant(i)]

s = sum(set(xrange(30000)) - set( ans[x]+ans[y] for x in xrange(len(ans)) for y in xrange(x, len(ans))) )

assert s == 4179871
