# -*- coding: cp1250 -*-
"""
http://projecteuler.net/index.php?section=problems&id=4

A palindromic number reads the same both ways.
The largest palindrome made from the product of two 2-digitnumbers is 9009 = 91 × 99.

Find the largest palindrome made from the product of two 3-digit numbers.
"""

mp = max( x*y for x in xrange(100,1000) \
              for y in xrange(x,1000) \
        if str(x*y) == str(x*y)[::-1] )

assert mp == 906609
