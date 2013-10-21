namespace Euler

(*
XOR decryption

Each character on a computer is assigned a unique code and the preferred standard is ASCII 
(American Standard Code for Information Interchange). 
For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.

A modern encryption method is to take a text file, convert the bytes to ASCII, 
then XOR each byte with a given value, taken from a secret key. 
The advantage with the XOR function is that using the same encryption key on the cipher text, 
restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.

For unbreakable encryption, the key is the same length as the plain text message, 
and the key is made up of random bytes. 
The user would keep the encrypted message and the encryption key in different locations, 
and without both "halves", it is impossible to decrypt the message.

Unfortunately, this method is impractical for most users, 
so the modified method is to use a password as a key. 
If the password is shorter than the message, which is likely, 
the key is repeated cyclically throughout the message. 
The balance for this method is using a sufficiently long password key for security, 
but short enough to be memorable.

Your task has been made easy, as the encryption key consists of three lower case characters. 
Using cipher1.txt (right click and 'Save Link/Target As...'), 
a file containing the encrypted ASCII codes, 
and the knowledge that the plain text must contain common English words,
decrypt the message and find the sum of the ASCII values in the original text.
*)


module Euler59 = 
    open System.IO

    let asciiToChar i = 
        char i

    let charToAscii (c: char) = 
        int c

    /// Cycle through the encryption key values to pad them up to any input length.
    let cycle (values: 'a[]) = 
        let n = Array.length values
        let rec loop i = 
            seq {
                yield values.[i]
                let i = (i+1) % n
                yield! loop i
            }
        loop 0

    /// Encrypt / Decrypt using XOR
    let cipher key data = 
        let pairs = Seq.zip data (cycle key) 
        pairs |> Seq.map (fun (d,k) -> d ^^^ k)


    /// Create n letter password candidates.
    let rec candidates n = 
        seq {
            if n = 0 then
                yield []
            else
                for p in candidates (n-1) do
                    for i in (charToAscii 'a') .. (charToAscii 'z') do
                        yield i :: p 
        }

    /// Not sure what can be in the original data.
    let isEnglishChar i =
        let c = asciiToChar i
        System.Char.IsLetter(c)
        || System.Char.IsPunctuation(c)
        || System.Char.IsWhiteSpace(c)

    /// Instead of demanding all characters to be english, here we just order by the most english content.
    let englishRatio txt = 
        let eng, tot = txt |> Seq.fold (fun (e,t) i -> 
                                let e' = if isEnglishChar i then e+1 else e
                                e', t+1) (0,0)
        (float eng) / (float tot)

    /// Try to guess the best solution for some encrypted text with n lower letter character passwords
    let guess n (data: int[]) = 
        let best = 
            candidates n
                |> Seq.map (fun keys -> 
                    let keys = keys |> Array.ofList
                    let decrypted = data |> cipher keys |> Array.ofSeq
                    decrypted)
                |> Seq.sortBy (fun txt -> -(englishRatio txt))                                       
                |> Seq.head
                |> Array.map asciiToChar
        best


    let solve() =         
        let fileName = Path.Combine(__SOURCE_DIRECTORY__, "cipher1.txt")
        let data = File.ReadAllText(fileName).Split(',') |> Array.map int
        let original = data |> guess 3
        original |> Seq.sumBy charToAscii



    module Tests = 

        open NUnit.Framework
        
        [<Test>]
        let DecryptionWorks() = 
            let data = "Here some common english words." |> Array.ofSeq // testing the password would require longer text
            let keys = "abc" |> Seq.map charToAscii |> Array.ofSeq
            let encrypted = data |> Seq.map charToAscii |> cipher keys |> Array.ofSeq
            let decrypted = encrypted |> cipher keys |> Seq.map asciiToChar |> Array.ofSeq
            Assert.AreEqual(data, decrypted) 
        

        [<Test>]
        let SolutionIsCorrect() = 
            Assert.AreEqual(107359, solve())