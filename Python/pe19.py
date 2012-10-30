"""
http://projecteuler.net/index.php?section=problems&id=19

You are given the following information, but you may prefer to do some research for yourself.

1 Jan 1900 was a Monday.
Thirty days has September,
April, June and November.
All the rest have thirty-one,
Saving February alone,
Which has twenty-eight, rain or shine.
And on leap years, twenty-nine.
A leap year occurs on any year evenly divisible by 4,
but not on a century unless it is divisible by 400.
How many Sundays fell on the first of the month during
the twentieth century (1 Jan 1901 to 31 Dec 2000)?
"""

def is_leap_year(y) :
    return y % 4 == 0 and y % 100 != 0 or y % 400 == 0

m30 = set([9,4,6,11])

def num_days(y,m) :
    if m == 2 :
        return 29 if is_leap_year(y) else 29
    else :
        return 30 if m in m30 else 31

from itertools import cycle, dropwhile, takewhile

def dates() :
    wd = cycle([1,2,3,4,5,6,7])
    y = 1900
    while True :
        for m in xrange(1,13) :
            for d in xrange(1,num_days(y,m)+1) :
                yield d, m, y, wd.next()
        y += 1


s = len(
    filter( lambda d : d[-1] == 7 and d[0] == 1,
    dropwhile( lambda d : d[2] < 1901,
        takewhile( lambda d : d[2] < 2001,
            dates() ))))

assert s == 171
