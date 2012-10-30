(ns student.problems-99)
; http://www.terrancedavis.com/clojure/


(defn get-my-last [vs] 
  (last vs)) 
(assert (= (get-my-last '(a b c d)) 'd))

(defn get-last-two [vs]
  (let [rev (reverse vs)] 
    (list (second rev) (first rev))))
(assert (= (get-last-two '(a b c d)) '(c d)))
;(source take-last)


(defn value-at [vs i]
  (nth vs i))
(assert (= (value-at, '(a b c d), 2) 'c))


(defn palindrome? [lst]
  (= lst (reverse lst)))
(assert (palindrome? [1 2 3 2 1]))
(assert (not (palindrome? [1 2 3])))


(defn flatten-list [ [x & xs] ]
  (if x
    (let [tail (flatten-list xs)]      
      (if (seq? x)
        (concat (flatten-list x) tail)
        (cons x tail)))
    '()))
(assert (= (flatten-list '(a (b (c d) e))) '(a b c d e)))
; (source flatten)



  
(require 'clojure.set)
(defn hanoi-towers
  "Move a given number of rings from 
   source to destination tower.
   To move from n from 1 to 2, we must 
   first move n-1 to 3, then the n-th to 2, 
   then n-1 to 2. Return list of moves."
  ([from to rings]
    (if (zero? rings) ; ready
      []      
      (let [other ((vec (clojure.set/difference #{0 1 2} #{from to})) 0)]
        (concat 
          (hanoi-towers from other (dec rings)) 
          [[from to rings]]
          (hanoi-towers other to (dec rings)))))))

(defn hanoi-towers-sim [towers moves]
  "Apply the moves on the towers. Towers should be vector of lists of rings."
  (loop [t towers
         m moves]    
    (if (empty? m) 
      t ; final state. else apply the first move
      (let [move (first m) 
            from (move 0)
            to   (move 1)
            ring (move 2)
            tfrom (t from)
            tto  (t to)]
        (println move t)
        (if (not= (first tfrom) ring)  (println "unexpected ring " (first tfrom) " on " from ". expected " ring))
        (if-let [ftto (first tto)] (if (< ftto ring) (println "smaller ring " ftto " on " to ". expected < " ring)))
        (recur (assoc t 
                      from (rest tfrom)
                      to (cons ring tto))
               (rest m))))))
            
; (hanoi-towers-sim ['(1 2 3) '() '()] (hanoi-towers 0 2 3))
; (hanoi-towers-sim ['(1 2 3 4 5 6) '() '()] (hanoi-towers 0 2 6))


(defn remove-duplicates [ xs ]
  "Drop subsequent duplicates from a list"
  (loop [[x & xs] xs
         acc []]
    (cond 
      (not x) acc
      (= x (last acc)) (recur xs acc)
      :other (recur xs (conj acc x)))))
    
  
  
; (assert (= (remove-duplicates '(a a a a b c c a a d e e e e)) '( (a b c a d e))))   
  
  
  