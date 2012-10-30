"""
http://projecteuler.net/index.php?section=problems&id=16

215 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.

What is the sum of the digits of the number 21000?

"""

s = sum( int(i) for i in str(2**1000) )

assert s == 1366
