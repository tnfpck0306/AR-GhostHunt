# AR-GhostHunt

-*Demo video(Youtube) : https://youtu.be/NGU1wZcqkcI*

<img height = 500px src=https://github.com/tnfpck0306/AR-GhostHunt/assets/76057758/d81157b2-c85e-4fdf-834d-178a5cc2f7fe>
<img height = 500px src=https://github.com/user-attachments/assets/5f0906e7-b40b-48dd-8f77-91f244d9dedf>

AR Ghost Hunt, an AR shooting game that can be enjoyed in a wide place.   
You can use the gun on your phone screen to catch ghosts approaching the player.   
The surrounding ammunition boxes and recovery boxes allow you to escape danger,    
and players can challenge their best records with items and quickness.

## UI Description
<img width = 80% src=https://github.com/tnfpck0306/AR-GhostHunt/assets/76057758/ea6ae209-87d3-461d-99dc-d112967a7d0b>

1. Mark the number of ghosts hunted in the text
2. Ammo left in the magazine & the entire remaining Ammo
3. A button for reloading ammo in the magazine
4. Visual representation of player's remaining health
5. A button for firing gun

## Key Feature
**Ghost** | **Gun shot** | **Ammo**
:-------------------------:|:-------------------------:|:-------------------------:
<img width = 250px src=https://github.com/tnfpck0306/AR-GhostHunt/assets/76057758/c9a4c09f-dc51-4e70-89dc-50de4981ab22> | <img width = 200px src=https://github.com/user-attachments/assets/ed23fc32-3b69-463f-bf22-efaa73117374> | <img width = 200px src=https://github.com/tnfpck0306/AR-GhostHunt/assets/76057758/754339c4-fec1-4190-a1ce-e0dfbdffdb81>
**Health Bar** | **Item** | **High Score**
<img width = 150px src=https://github.com/tnfpck0306/AR-GhostHunt/assets/76057758/460c93b3-df2f-4908-90a8-1ae19e364448> | <img width = 200px src=https://github.com/tnfpck0306/AR-GhostHunt/assets/76057758/82596d8c-d803-44f1-8e44-37d4b6c9b5f5> | <img width = 200px src=https://github.com/tnfpck0306/AR-GhostHunt/assets/76057758/07f5a199-7d4b-4f15-8a2d-4540394f5f6b>

- Ghosts appear around players to track them down
- Ray cast and Reinerenderer show shooting trajectory
- You can check in real time about the remaining ammunition in the magazine and the total ammunition you have
- You can check the amount of health loss caused by ghosts
- If you get close to the item, you can get it, and the ammunition box can replenish the entire ammunition, and the health box can restore the player health
- You can check the highest score among the results of the game

**Player Skill** | **Setting**
  :-------------------------:|:-------------------------:
  <img width = 220px src=https://github.com/user-attachments/assets/42d1ddf2-1ecc-41df-a684-49cfa94637ed> | <img width = 250px src=https://github.com/user-attachments/assets/20975d95-6c61-47fc-b9ff-fd650fad121c>

  - The player can select one of three randomly determined skills. (If you click 1, you can see the detailed description of the selected skill, and if you click 2, you can select the skill.)
  - You can control the background sound and SFX in the setting window after you pause the current game. You can also play again or return to the main menu.

## Level Design
<img src=https://github.com/user-attachments/assets/d1920846-a404-4550-b6a7-28c091c27d7a>

- Ghost (Health 100, Damage 20, Speed 0.5)
- Brown Ghost (Health 150, Damage 40, Speed 0.3)
- Horn Ghost (Health 50, Damage 20, Speed 0.5, BLINK effect)
- Boss Ghost (Health 10 * player kill count, Damage instant kill, Speed 0.5

1. Basically, ghosts appear one by one.  
2. The speed of all ghosts per 5 kills increases by 0.1.  
3. 1 increase in the number of ghosts per 10 kills [over 10 kill -> 2 ghosts / over 20 kill -> 3 ghosts]  
4. Brown ghosts appear every 5 kills.  
5. Horned ghost appears to be one of three after 20 kills.  
6. Boss ghost appears every 25 kills.  

<br/><br/>
- Player (Health 100, Damage 50)
Select a skill for every 10 kills of the player

1. Player Attacks Increase by 20
2. Automatic bullet supply (5 supply every 5 seconds)
3. 50% increase in maximum health
4. Regeneration of health(four per kill)
5. Item Spawn Time Reduction by 15% (25 to 30) -> (21.25 to 25.5)
6. 20% increase item efficiency
7. Reduced ghost speed

## How to use it
*Running the app face-to-face with the phone camera at the user's eye level   
in a spacious place will allow you to play in a more appropriate environment.   
You can also hear spatial sounds better when you wear earphones and help you play better.*

## Environment
Unity `2022.3.4f1`   
AR Foundation `5.0.7`   
Google ARCore XR Plugin `5.0.7`   
