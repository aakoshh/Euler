namespace Euler

// http://projecteuler.net/problem=54

(*
In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:

High Card: Highest value card.
One Pair: Two cards of the same value.
Two Pairs: Two different pairs.
Three of a Kind: Three cards of the same value.
Straight: All cards are consecutive values.
Flush: All cards of the same suit.
Full House: Three of a kind and a pair.
Four of a Kind: Four cards of the same value.
Straight Flush: All cards are consecutive values of same suit.
Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
The cards are valued in the order:
2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.

If two players have the same ranked hands then the rank made up of the highest value wins; for example, a pair of eights beats a pair of fives (see example 1 below). But if two ranks tie, for example, both players have a pair of queens, then highest cards in each hand are compared (see example 4 below); if the highest cards tie then the next highest cards are compared, and so on.

Consider the following five hands dealt to two players:

Hand	 	Player 1	 	Player 2	 	Winner
1	 	5H 5C 6S 7S KD
Pair of Fives
 	2C 3S 8S 8D TD
Pair of Eights
 	Player 2
2	 	5D 8C 9S JS AC
Highest card Ace
 	2C 5C 7D 8S QH
Highest card Queen
 	Player 1
3	 	2D 9C AS AH AC
Three Aces
 	3D 6D 7D TD QD
Flush with Diamonds
 	Player 2
4	 	4D 6S 9H QH QC
Pair of Queens
Highest card Nine
 	3D 6D 7H QD QS
Pair of Queens
Highest card Seven
 	Player 1
5	 	2H 2D 4C 4D 4S
Full House
With Three Fours
 	3C 3D 3S 9S 9D
Full House
with Three Threes
 	Player 1
The file, poker.txt, contains one-thousand random hands dealt to two players. Each line of the file contains ten cards (separated by a single space): the first five are Player 1's cards and the last five are Player 2's cards. You can assume that all hands are valid (no invalid characters or repeated cards), each player's hand is in no specific order, and in each hand there is a clear winner.

How many hands does Player 1 win?
*)

open NUnit.Framework

module Euler54 =   
    open System.IO  
    open Microsoft.FSharp.Reflection
    // implicitly ordered by appearance!
    type Rank = 
    | Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten | Jack | Queen | King | Ace

    type Suit = 
    | Spades | Hearts | Diamonds | Clubs

    type Card = Rank * Suit

    type Hand = 
    | Hand of Card * Card * Card * Card * Card    // with cards ordered by rank
    
    // assign a numeric value to each rank    
    let rankValues = FSharpType.GetUnionCases typeof<Rank>
                     |> Array.map (fun info -> (info.Name, info.Tag + 2))
                     |> Map.ofArray
    
    let rankName (x:'a) = 
        match FSharpValue.GetUnionFields(x, typeof<'a>) with
        | case, _ -> case.Name  

    let value rank = rankValues.[ rankName rank ]

    // ranking hands. active pattern has limit of 7
    // http://cs.hubfs.net/topic/Some/0/59530 describes combinations of patterns with monads and arrows
    // compose multiple patterns on the same hand
    let (>>>) f g = (fun x -> 
        match f x with 
        | Some _ -> g x 
        | None -> None)

    let rankArray hand = match hand with Hand((r1,_),(r2,_),(r3,_),(r4,_),(r5,_)) -> [|r1;r2;r3;r4;r5|]

    // RoyalFlush|StraighFlush|FourOfKind|FullHouse|Flush|Straight|ThreeOfKind|TwoPairs|OnePair|HighCard 
    let (|SameSuit|_|) = function
        | Hand((_,s1),(_,s2),(_,s3),(_,s4),(r5,s5))
            when s1 = s2 && s1 = s3 && s1 = s4 && s1 = s5 -> Some(r5)
        | _ -> None

    let (|Consecutive|_|) hand =
        let ranks  = rankArray hand
        let values = ranks |> Array.map value
        let first = values.[0]
        let consecutive = 
            values.[1..4] |> Array.mapi (fun i v -> v - first = (i+1))
                          |> Array.forall id
        if consecutive then Some(ranks.[4]) else None

    let (|RoyalFlush|_|) hand =
        match hand with 
        | Hand((Ten,_),(Jack,_),(Queen,_),(King,_),(Ace,_))
            -> (|SameSuit|_|) hand           
        | _ -> None

    let (|StraightFlush|_|) = (|SameSuit|_|) >>> (|Consecutive|_|)
        
    let (|FourOfKind|_|) = function
        | Hand((r1,_),(r2,_),(r3,_),(r4,_),(r5,_)) // abbbb or aaaab
            when r2 = r3 && r3 = r4 && (r1 = r2 || r4 = r5) -> Some(r3)     
        | _ -> None       
       
    let (|FullHouse|_|) = function
        | Hand((r1,_),(r2,_),(r3,_),(r4,_),(r5,_))  
            when r1 = r2 && r3 = r4 && r4 = r5 -> Some(r1, r3) // aabbb
        | Hand((r1,_),(r2,_),(r3,_),(r4,_),(r5,_))  
            when r1 = r2 && r2 = r3 && r4 = r5 -> Some(r1, r4) // aaabb
        | _ -> None 

    let (|Flush|_|) = (|SameSuit|_|)

    let (|Straight|_|) = (|Consecutive|_|)

    let (|ThreeOfKind|_|) = function
        | Hand((r1,_),(r2,_),(r3,_),(_,_),(_,_)) 
            when r1 = r2 && r2 = r3  -> Some(r1)   
        | Hand((_,_),(r2,_),(r3,_),(r4,_),(_,_))
            when r2 = r3 && r3 = r4  -> Some(r2)  
        | Hand((_,_),(_,_),(r3,_),(r4,_),(r5,_)) 
            when r3 = r4 && r4 = r5  -> Some(r3)    
        | _ -> None 

    let pairs hand = 
        let ranks = rankArray hand
        [ for i in 1..4 do if ranks.[i-1] = ranks.[i] then yield ranks.[i] ]

    let (|TwoPairs|_|) hand = 
        match pairs hand with
        | [a;b] -> Some(a,b)
        | _ -> None

    let (|OnePair|_|) hand = 
        match pairs hand with
        | [a] -> Some(a)
        | _ -> None

    // return the ranks in reverse order for ties
    let (|HighCard|_|) hand = 
        let ranks = rankArray hand       
        let valueRanks = ranks |> Set.ofArray
        valueRanks |> Set.toList |> List.sort |> List.rev |> Some
    

    // return hand rank as number and list of card values descending
    let evaluate hand = 
        let hv, crs = 
            match hand with // I could define a rank type to enumerate all these and return a descriptive value
            | RoyalFlush r      -> 10,[r]
            | StraightFlush r   -> 9, [r]
            | FourOfKind r      -> 8, [r]
            | FullHouse (l,h)   -> 7, [h;l]
            | Flush r           -> 6, [r]
            | Straight r        -> 5, [r]
            | ThreeOfKind r     -> 4, [r]
            | TwoPairs (l,h)    -> 3, [h;l]
            | OnePair r         -> 2, [r]
            | HighCard rs       -> 1, rs
            | _ -> failwith (sprintf "%A not matched" hand)
        hv, crs |> List.map value

    let compare hand1 hand2 = 
        let h1v, c1v = evaluate hand1
        let h2v, c2v = evaluate hand2
        let comp a b = 
            if a > b then 1
            elif a < b then 2
            else 0
        let hc = comp h1v h2v
        if hc <> 0 then 
            hc
        else // rank is tied first compare the components
            let compareRanks rs1 rs2 = 
                List.map2 (fun r1 r2 -> comp r1 r2) rs1 rs2 
                |> List.filter (fun c -> c > 0) 
            let ccs = compareRanks c1v c2v   
            if ccs.Length > 0 then ccs.Head        
            else // the card ranks are tied too, so compare the highest value cards on the whole hand
                let getranks hand = match hand with HighCard rs -> rs | _ -> failwith "no rank list"
                let hc1 = getranks hand1  
                let hc2 = getranks hand2
                let ccs = compareRanks hc1 hc2
                if ccs.Length > 0 then ccs.Head
                else failwith (sprintf "no difference between %A (%A, %A) and %A (%A, %A)" hand1 h1v c1v hand2 h2v c2v)

    
    // converting text file
    module Convert = 
        let rankMap = Map.ofList [ '2',Two; '3',Three; '4',Four; '5',Five; '6',Six; '7',Seven; '8',Eight; '9',Nine; 
                                   'T',Ten; 'J',Jack; 'Q',Queen; 'K',King; 'A',Ace ]
        let suitMap = Map.ofList [ 'S',Spades; 'H',Hearts; 'D',Diamonds; 'C',Clubs ]
                 
        //convert abbreviation to card
        let toCard (rs: string) = (rankMap.[rs.[0]], suitMap.[rs.[1]])

        let toArr (rss: string) = rss.Split( [|' '|] )

        // convert an array of abbreviations to 5 cards
        let toHand (rss: string[]) = 
            rss |> Array.map toCard 
                |> Array.sort
                |> (fun cards -> 
                    match cards with 
                    | [|a;b;c;d;e|] -> Hand(a,b,c,d,e)
                    | _ -> failwith (sprintf "Invalid hand %A" cards) ) 

        // read rows in file as player 1 vs player 2 hands
        let readHands fn =        
            File.ReadLines(fn)
            |> Seq.map toArr
            |> Seq.map (fun cards -> cards.[0..4] |> toHand,  
                                     cards.[5..9] |> toHand )                                               
    //
    let fileName = Path.Combine(__SOURCE_DIRECTORY__, "poker.txt")

    let e54 () = 
        Convert.readHands fileName 
        |> Seq.map (fun (h1,h2) -> compare h1 h2)
        |> Seq.filter (fun w -> w = 1)
        |> Seq.length

    open Test
    open Convert

    [<Test>]
    let test() = 
        Test.assertEqual (toCard "5H") (Five, Hearts) "toCard"
        Test.assertEqual (value Three) 3 "value"        
        ["5H 5C 6S 7S KD","2C 3S 8S 8D TD",2;
         "5D 8C 9S JS AC","2C 5C 7D 8S QH",1;
         "2D 9C AS AH AC","3D 6D 7D TD QD",2;
         "4D 6S 9H QH QC","3D 6D 7H QD QS",1;
         "2H 2D 4C 4D 4S","3C 3D 3S 9S 9D",2;
         "TH 8H 5C QS TC","9H 4D JC KS JS",2]
        |> List.map (fun (h1,h2,w) ->
                        let hh1 = h1 |> toArr |> toHand
                        let hh2 = h2 |> toArr |> toHand
                        let c = (compare hh1 hh2)
                        Test.assertEqual c w "winner") |> ignore
        Test.assertEqual ("TH JH QH KH AH" |> toArr |> toHand |> evaluate) (10,[14]) "royal flush"
        Test.assertEqual ("3H 4H 5H 6H 7H" |> toArr |> toHand |> evaluate) (9,[7]) "straight flush"
        Test.assertEqual ("6H 6H 6C 6H 2H" |> toArr |> toHand |> evaluate) (8,[6]) "4 of a kind"
        Test.assertEqual ("6H 4H 5C 3H 2H" |> toArr |> toHand |> evaluate) (5,[6]) "straight" 
        Test.assertEqual (e54()) 376 "solution"       
