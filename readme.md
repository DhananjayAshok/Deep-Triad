# Deep Triad
#### A 3D Tic Tac Toe AI System

## The Game

1. Objective: Be the first of three players to form a line of 3 (of your own pieces)

2. Turns: At the start of every game the players are randomly assigned a turn, each player gets one turn every round in that order.

3. Placement: There is gravity in this game. ![Screenshot](Documentation/Bottom.png) What that means is just like Connect 4 you can not place a piece in the cells of the higher layers unless there is piece in the layers below.
![Screenshot](Documentation/SecondLayer.png)

4. Winning Lines: Any consecutive line of 3 pieces wins the game. This could be just on the lower layers,![Screenshot](Documentation/GroundWin.png) through the middle ![Screenshot](Documentation/MiddleWin.png) or on one of the side face of the cube. ![Screenshot](Documentation/FaceWin.png)


## The AI

### Hyperion the Greedy
![Screenshot](Documentation/Hyperion.png)

Hyperion has only one goal. **Attack**. The strategy that Hyperion follows disregards any long term blocking of the opponent and impliments a greedy algorithm to minimize an aggressive *Heuristic Function* and win the game.

Hyperion Algorithm
* If there is a move that will win the game, play it.
* If the next player has a move that would win them the game, block it.
* Else calculate the most aggressive move possible, play it.

Calculating the most aggressive move - Hyperion assigns a score to each move and picks the move with the highest score
Aggression score for each move i.e The Aggresive Heuristic Function.
* For each consecutive 3 spots (winning line) that isn't occupied by an opponent do the following
++ If after the move only one spot is occupied then add 1
++ If 2 spots are occupied then add 4 ( Having 2 in a row in one line should be more than twice as good as having one.
++ If 3 spots are occupied then add 1000 ( This doesn't matter however since if any move would put 3 in a row the algorithm would play it regardless of score)
* Then instead of adding the scores of the lines to calculate the score of a move, Hyperion takes the product. This is because the product will place a higher weight on getting a double attack which is very effective

The strategy results in some clear preferences for Hyperion
1. For the first move Hyperion will always play a corner
2. Hyperion will almost always occupy the centre position if given the chance as it connects to the most winning lines.
3. A game with 3 Hyperion players is deterministic, the first player will always win

This means the algorithm has an efficiency of **O(1)** with respect to the number of moves played already.
![Screenshot](Documentation/HyperionEfficiency.png)


### Zenith the Wise
![Screenshot](Documentation/Zenith.png)

*Zenith sees all*. Zenith follows a more rounded strategy than its Aggressive counterpart. There are 2 primary differences between the two
1. Zenith uses a different Heuristic Function. One that not just considers the number of attacks it has available to it, but also the number of attacks the other opponents have available to them. 
2. Instead of taking a greedy approach my plan is to use the power Deep Learning with ML Agents to implement a Q-Deep Learning Algorithm over the game.




### 
