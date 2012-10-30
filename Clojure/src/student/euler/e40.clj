(ns student.euler.e40
  (:require [clojure.contrib.string :as string])
  (:use     [clojure.contrib.math :only (expt)]))

;An irrational decimal fraction is created by concatenating the positive integers:
;0.123456789101112131415161718192021...
;It can be seen that the 12th digit of the fractional part is 1.
;If dn represents the nth digit of the fractional part, find the value of the following expression.
;d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000




(defn nth-digit [n]
  (loop [d 1 ; length
         x 1]; last value
    (if (>= d n)
      (read-string (string/take 1 (string/drop (- d n) (string/reverse (str x))))) ; last character within bounds
      (recur (+ d (count (str (inc x)))) (inc x)))))

(defn test-nth-digit []
  (let [test-vec (map-indexed (fn [i v] [(inc i) (read-string (str v))]) 
                              (vec "123456789101112131415161718192021"))]
    (every? (fn [[i d]] (= d (nth-digit i))) test-vec))) 

(assert (test-nth-digit))

(defn e40 []
  (reduce * (map nth-digit (map (partial expt 10) (range 7)))))

(assert (= (e40) 210))