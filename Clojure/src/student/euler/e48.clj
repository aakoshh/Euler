(ns student.euler.e48
  (:use [clojure.contrib.math :only (expt)]))

(comment "
The series, 11 + 22 + 33 + ... + 1010 = 10405071317.

Find the last ten digits of the series, 11 + 22 + 33 + ... + 10001000.")


(defn exp-sum [n]
  (reduce + (map #(expt % %) (range 1 (inc n)))))

(assert (= (exp-sum 10)  10405071317))


(defn e48 []
  (apply str (take-last 10 (str (exp-sum 1000)))))

(assert (= "9110846700" (e48)))

; (apply str (take-last 10 (str (reduce + (for [x (range 1 1001)] (expt x x))))))