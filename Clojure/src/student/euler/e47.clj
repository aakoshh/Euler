(ns student.euler.e47)

(comment "
The first two consecutive numbers to have two distinct prime factors are:

14 = 2 * 7
15 = 3 * 5

The first three consecutive numbers to have three distinct prime factors are:

644 = 2^2 * 7 * 23
645 = 3 * 5 * 43
646 = 2 * 17 * 19.

Find the first four consecutive integers to have four distinct primes factors. What is the first of these numbers?")

(defn prime? [n]
  (let [r (int (Math/sqrt n))]
    (loop [d 2]
      (cond
        (= n 1) false
        (> d r) true
        (zero? (rem n d)) false
        :other (recur (inc d))))))

(defn primes []
  (filter prime? (iterate inc 2)))

(def *primes* (primes))

(defn factorize [n]
  "return list of prime factors of n"
  (loop [f []
         ps *primes*
         i n]
    (if (= i 1) f ; found all
      (let [p (first ps)]
        (if (zero? (rem i p))
          (recur (conj f p) ps (/ i p))
          (recur f (rest ps) i))))))


(defn consecutive [n]
  "find n consecutive numbers with n different prime factors"
  (loop [i 2
         c 0]
    (if (= (count (distinct (factorize i))) n)
      (if (= (inc c) n) 
        (- i (dec n)) ; the first consecutive 
        (recur (inc i) (inc c)))
      (recur (inc i) 0))))


(assert (= 14 (consecutive 2)))
(assert (= 644 (consecutive 3)))

(defn e47 []
  (consecutive 4))

(assert (= 134043 (e47)))

        