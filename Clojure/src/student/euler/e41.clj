(ns student.euler.e41
  (:require [clojure.contrib.string :as string])
  (:use [clojure.contrib.math :only (sqrt expt)]
        [clojure.contrib.combinatorics :only (permutations)]))  

; We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. 
; For example, 2143 is a 4-digit pandigital and is also prime.
; What is the largest n-digit pandigital prime that exists?


(defn sieve [maxnum]
  "Build a list of primes up to maxnum"
  (let [bound (inc maxnum)]
    (loop [i 2
           candidates (apply sorted-set (range i bound))] ; every number
      (if (< i maxnum)
        (if (candidates i) ; still a prime
          (recur (inc i) (apply disj candidates (rest (range i bound i)))) ; remove every multiplicant
          (recur (inc i) candidates ))
        candidates))))

(assert (= #{2 3 5 7 11 13 17 19} (sieve 20)))
; takes too long to build for 987654321 
         

(defn prime? [n]
  (let [r (int (sqrt n))]
    (loop [d 2]
      (cond
        (= n 1) false
        (> d r) true
        (zero? (rem n d)) false
        :other (recur (inc d))))))
        
(assert (every? prime? #{2 3 5 7 11 13 17 19}))
(assert (not-any? prime? #{1 4 6 8 10 12 14 16 18}))

(defn list-to-int [int-lst]
  (read-string (string/join "" int-lst)))

(defn list-to-int2 [int-lst] ; returns number in reverse order
  (reduce + (map-indexed (fn [i d] (* d (expt 10 (inc i)))) int-lst)))

(defn list-to-int3 [int-lst] 
  (Integer/parseInt (string/join "" int-lst)))


(defn pandigitals [n]
  "1 to n pandigital numbers"
  (map list-to-int3 (permutations (range 1 (inc n)))))

(defn max-num [xs]
  (if (seq xs) 
    (apply max xs)
    0))

(defn e41 []
  (max-num (map #(max-num (filter prime? (pandigitals %))) (range 1 10))))


(assert (= 7652413 (e41)))
