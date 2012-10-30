"""
http://projecteuler.net/index.php?section=problems&id=17

If the numbers 1 to 5 are written out in words:
one, two, three, four, five,
then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.

If all the numbers from 1 to 1000 (one thousand)
inclusive were written out in words, how many letters would be used?


NOTE: Do not count spaces or hyphens. For example, 342
(three hundred and forty-two) contains 23 letters and 115
(one hundred and fifteen) contains 20 letters.
The use of "and" when writing out numbers is in compliance with British usage.
"""

numnames = {1:'one', 2:'two', 3: 'three', 4:'four', 5:'five',
            6:'six', 7:'seven', 8:'eight', 9:'nine', 10:'ten',
            11:'eleven', 12:'twelve', 13:'thirteen', 14:'fourteen', 15:'fifteen',
            16:'sixteen', 17:'seventeen', 18:'eighteen', 19:'nineteen', 20:'twenty',
            30:'thirty', 40:'fourty', 50:'fifty', 60:'sixty', 70:'seventy', 80:'eighty', 90: 'ninety',
            100: 'hundred', 1000: 'thousand'}

nums = sorted(numnames.keys(), reverse=True)

from pe14 import memo

@memo
def articulate(n) :
    for nn in nums :
        d =  n // nn
        r = n % nn
        if d : break
    parts = []
    if nn >= 100 :
        parts.append( numnames[d] )
        parts.append(' ')
    parts.append( numnames[nn] )
    if nn >= 100 and r :
        parts.append( ' and ' )
    elif r :
        parts.append('-')
    if r :
        parts.append( articulate(r) )
    return ''.join(parts)

s = sum( len( articulate(i).replace(' ','').replace('-','') ) for i in xrange(1,1001) )

assert s == 21124
