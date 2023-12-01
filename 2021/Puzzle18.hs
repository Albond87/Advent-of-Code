import System.IO
import Data.List.Split

data Pair = N Int | P Pair Pair deriving (Show,Eq)
data Direction = L | R deriving (Show,Eq)

stringToPair :: String -> [Pair] -> (Pair, String)
stringToPair []     first = case first of
                              [p]       -> (p,"")
                              otherwise -> error "parse failed"
stringToPair (x:xs) first | x == '['  = stringToPair xs first
                          | x == ']'  = stringToPair xs ((P (first !! 1) (head first)):(tail (tail first)))
                          | x == ','  = stringToPair xs first
                          | otherwise = stringToPair xs ((N (read [x])):first)

parseFile :: String -> IO [Pair]
parseFile filename = do
    fp <- openFile filename ReadMode
    contents <- hGetContents fp
    let xs = splitOn "\n" contents
    return [fst (stringToPair x []) | x <- xs]

findExplode :: Pair -> Int -> Maybe [Direction]
findExplode (P l r) 4 = case l of
                          P x y -> Just [L]
                          N x   -> case r of
                                     P x y -> Just [R]
                                     N x   -> Nothing
findExplode (P l r) n = case findExplode l (n+1) of
                          Just ds -> Just (L:ds)
                          Nothing -> case findExplode r (n+1) of
                                       Just ds -> Just (R:ds)
                                       Nothing -> Nothing
findExplode _ _       = Nothing

extractPair :: Pair -> [Direction] -> (Pair, Int, Int)
extractPair (P (N x) (N y)) []     = ((N 0), x, y)
extractPair (P l r)         (L:ds) = let (l', x, y) = extractPair l ds in (P l' r, x, y)
extractPair (P l r)         (R:ds) = let (r', x, y) = extractPair r ds in (P l r', x, y)

upToLast :: Eq a => a -> [a] -> Maybe [a]
upToLast e [] = Nothing
upToLast e (x:xs) | x == e    = case upToLast e xs of 
                                  Nothing -> Just []
                                  Just ys -> Just (x:ys)
                  | otherwise = case upToLast e xs of
                                  Nothing -> Nothing
                                  Just ys -> Just (x:ys)


addToEnd :: Pair -> [Direction] -> Int -> Pair
addToEnd (N x)   _      y = N (x+y)
addToEnd (P l r) [L]    y = P (addToEnd l [L] y) r
addToEnd (P l r) [R]    y = P l (addToEnd r [R] y)
addToEnd (P l r) (L:ds) y = P (addToEnd l ds y) r
addToEnd (P l r) (R:ds) y = P l (addToEnd r ds y)

explode :: Pair -> [Direction] -> Pair
explode p ds = let (p',l,r) = extractPair p ds in
               let p'' = (case upToLast R ds of
                            Nothing  -> p'
                            Just ds' -> addToEnd p' (ds' ++ [L,R]) l) in
                          case upToLast L ds of
                            Nothing  -> p''
                            Just ds' -> addToEnd p'' (ds' ++ [R,L]) r

findSplit :: Pair -> Maybe [Direction]
findSplit (N x) = if x > 9 then Just [] else Nothing
findSplit (P l r) = case findSplit l of
                      Just ds -> Just (L:ds)
                      Nothing -> case findSplit r of
                                   Just ds -> Just (R:ds)
                                   Nothing -> Nothing

splitN :: Pair -> [Direction] -> Pair
splitN (N x) [] = P (N (x `div` 2)) (N ((x+1) `div` 2))
splitN (P l r) (L:ds) = P (splitN l ds) r
splitN (P l r) (R:ds) = P l (splitN r ds)

reduce :: Pair -> Pair
reduce p = case findExplode p 1 of
             Just ds -> reduce (explode p ds)
             Nothing -> case findSplit p of
                          Just ds -> reduce (splitN p ds)
                          Nothing -> p

sumNums :: [Pair] -> Pair
sumNums [p] = p
sumNums (p1:p2:ps) = sumNums ((reduce (P p1 p2)):ps)

magnitude :: Pair -> Int
magnitude (N x)   = x
magnitude (P l r) = (3 * (magnitude l)) + (2 * (magnitude r))

run :: String -> IO ()
run filename = do
                input <- parseFile filename
                let total = sumNums input in
                 do
                  print total
                  print (magnitude total)
                  print (maximum [magnitude (reduce (P p1 p2)) | p1 <- input, p2 <- input, p1 /= p2])

main = do
    run "Day18Input.txt"