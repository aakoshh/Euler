(ns student.euler.e42
  (:require [clojure.contrib.string :as string]))

(comment "
The nth term of the sequence of triangle numbers is given by, tn = n(n+1)/2; so the first ten triangle numbers are:

1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...

By converting each letter in a word to a number corresponding to its alphabetical position and adding these values we form a word value. 
For example, the word value for SKY is 19 + 11 + 25 = 55 = t10. 
If the word value is a triangle number then we shall call the word a triangle word.

Using words.txt (right click and 'Save Link/Target As...'), a 16K text file containing nearly 
two-thousand common English words, how many are triangle words?")


(defn triangle-numbers [] ;simple def would hold on to head and cache
  (map (fn [n] (/ (* (+ n 1) n) 2))
       (iterate inc 1)))

(assert (= (take 10 (triangle-numbers)) '(1, 3, 6, 10, 15, 21, 28, 36, 45, 55)))


(defn word-value [word]
  (let [base (dec (int \a))]
    (reduce + (map #(- (int %) base) (.toLowerCase word)))))

(assert (= (word-value "SYK") 55))


(def triangle-word?   
  (memoize (fn [word]
             (let [wv (word-value word)
                   ts (take-while #(<= % wv) (triangle-numbers))]
               (not (nil? (some #{wv} ts)))))))

(assert (triangle-word? "SYK"))

(defn get-current-directory []
  (. (java.io.File. ".") getCanonicalPath))

;(def words (re-seq #"[^\",]+" (slurp "words.txt")))

(defn e42 []
  (let [words (string/split #"," (string/replace-str "\"" "" (slurp "src/student/euler/words.txt")))]               
    (count (filter triangle-word? words))))

(assert (= 162 (e42)))
               