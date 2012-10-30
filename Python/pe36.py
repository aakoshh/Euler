"""
http://projecteuler.net/index.php?section=problems&id=36

The decimal number, 585 = 10010010012 (binary),
is palindromic in both bases.

Find the sum of all numbers, less than one million,
which are palindromic in base 10 and base 2.

(Please note that the palindromic number, in either base,
may not include leading zeros.)
"""

def dec2bin(n):
    if not n : return '0'
    parts = []
    while n > 0:
        parts.append( str(n % 2) )
        n = n >> 1
    return ''.join(reversed(parts))

def is_palindrome(n):
    return list(str(n)) == list(reversed(str(n)))

s = sum( x for x in xrange(10**6) if all(is_palindrome(n) for n in (x, dec2bin(x))) )

assert s == 872187
