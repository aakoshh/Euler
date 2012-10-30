"""
http://projecteuler.net/index.php?section=problems&id=38

Take the number 192 and multiply it by each of 1, 2, and 3:

192  1 = 192
192  2 = 384
192  3 = 576
By concatenating each product we get the 1 to 9 pandigital,
192384576.
We will call 192384576 the concatenated product of 192 and (1,2,3)

The same can be achieved by starting with 9 and
multiplying by 1, 2, 3, 4, and 5, giving the pandigital,
918273645, which is the concatenated product of 9 and (1,2,3,4,5).

What is the largest 1 to 9 pandigital 9-digit number
that can be formed as the concatenated product of an
integer with (1,2, ... , n) where n  1?
"""

from itertools import permutations
from pe12 import first

ns = list(xrange(1,10))
digits = list(str(i) for i in xrange(9,0,-1))

def is_concatenated(p):
    digits = str(p)
    for i in xrange(1,5) : #at five digits it would not work
        first_term = int(digits[:i])
        s = ""
        for n in xrange(1,9) :
            s += str(first_term*n)
            if digits == s :
                return True
            if not digits.startswith(s) :
                break
    return False

assert is_concatenated(192384576)
assert is_concatenated(918273645) # 9

# from the largest down
mpd = first( lambda pd : is_concatenated(pd),
             ( int(''.join(p)) for p in permutations(digits)) )

assert mpd == 932718654
