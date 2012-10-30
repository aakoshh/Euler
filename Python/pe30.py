"""
http://projecteuler.net/index.php?section=problems&id=30

Surprisingly there are only three numbers that can be written
as the sum of fourth powers of their digits:

1634 = 14 + 64 + 34 + 44
8208 = 84 + 24 + 04 + 84
9474 = 94 + 44 + 74 + 44
As 1 = 14 is not a sum it is not included.

The sum of these numbers is 1634 + 8208 + 9474 = 19316.

Find the sum of all the numbers that can be written
as the sum of fifth powers of their digits.
"""

from math import ceil, log10

def power_numbers( e ) :
    max_digits = int(ceil(log10(9**e))+1) # the first time the increment of a digit is greater than the increment of term     
    return ( n for n in xrange(10, 10**max_digits) if n == sum( int(i)**e for i in str(n) ) )

assert sum(power_numbers(4)) == 19316

assert sum(power_numbers(5)) == 443839
