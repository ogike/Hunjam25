VAR current_chapter = -> day_1_morning
VAR current_chapter_finished = false

VAR food_to_serve_for_1 = "ENGI"
VAR food_to_serve_for_2 = "NAVI"

//can be: bad, neutral, good
VAR ENGI_prev_food = "neutral"
VAR NAVI_prev_food = "neutral"
VAR OFFI_prev_food = "neutral"

EXTERNAL wait(delayTime) // Wait for x seconds before moving to next line
EXTERNAL set_first_order(character) //set who shall be have the first server plate, ENGI/NAVI/OFFI
EXTERNAL set_first_order(character) //set who shall be have the second server plate, ENGI/NAVI/OFFI

-> current_chapter

==sample

Here be lines.
Sooner or later.
* [Sooner]
* [Later]
    Well...
- The sooner the better.
This is a game jam, after all.


->DONE


== day_1_morning

-> engineer

=officer
OFFI: 
TODO: write the Officer's outline and then the dialogue
->winds_down


=engineer
ENGI: Hi!
ENGI: Another day and I am ready for another concoction!

    * [I'll do my best.]
        ENGI: I'm sure you will.
    * [Good morning to you, too.]
    * (offend)[Did you call my work "concoction"?]
        ENGI: Oh. I did not mean to offend you. 
        ->tinkering
- ENGI: How was your first night on a ship?
    * (travelled_on_ship)[Actually, this wasn't my first.]
        CHEF: I travelled on a ships before.
        CHEF: I just haven't cooked on one.
        ENGI: Aha. I see.
    * [It was fine.]
        ENGI: That's good to hear.
    * [I'd rather not say.]
        ENGI: Oh. Okay.
- (tinkering)ENGI: {offend: Sorry.} I get sucked into all-nighters and tinkering sometimes.
ENGI: There's always something to fix. Or even improve.
ENGI: When I get into the groove I even forget to eat.
ENGI: But I don't want to miss your meals.
ENGI: {offend: Not concoctions. I got it.}
ENGI: I'll leave you to it now.

~ wait (1)
->navigator


=navigator

NAVI: Heeey, Chef! Good to see you! I'm Lead Navigator Navi! :)

CHEF: Oh hey, I'm Chef, how's it--- Oh, god, do you smell this? Is something on fire?

NAVI: Nooo, silly! That's me and my good old buddies, the exxxtra strong cigs!

CHEF: ...I'm not sure if that's relieving. How are you allowed to smoke in here?

NAVI: ... :)

CHEF: Anyway. How's the journey? Any news about how long this should take until arrival?

NAVI: Oh yeah! We should be around 4 days away from the destination. 
NAVI: We are aaall in thiiis together until then, isn't that right?

//Narration: They light another cigarette and shuffle away. Sure.

->winds_down



=winds_down
-> DONE


== day_1_noon
//TODO:
-> winds_down

=day_1_noon_start
current_chapter_finished = false
current_chapter = -> day_1_noon

//idk, samples
VAR food_to_serve_for_1 = "OFFI"
VAR food_to_serve_for_2 = "NAVI"

-> winds_down


=winds_down
->DONE



/*
_______________
Officer OUTLINE

The character is based on Kriszti, effectively.
At the time of writing I have not seen the concept arts for the character, but there were mentions of her being an anthropomrphic cat.

________
DAY 1
____
Morning
Checking in with the chef 
(Could this be the first time they are flying together? Who has more experience on spaceships?)
For the sake of an efficient tutorialisation the chef's work experience is in kitchens but not on ships. So the officer can assure them that it works pretty much the same.


____
Noon
Reassuring     

________
DAY 2
____
Morning
Chatting about the foodstocks, which she herself oversaw the purchase of.

____
Noon
    

________
DAY 3
____
Morning


____
Noon
    

________
DAY 4
____
Morning


____
Noon
    

________
DAY 5
____
Morning


____
Noon
    

________
DAY 6
____
Morning


____
Noon
    

*/

=== function wait(x) ===
~ return 0
=== function set_first_order(x) ===
~ return 0
=== function set_second_order(x) ===
~ return 0