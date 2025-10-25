->day_1_morning.navigator

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

->winds_down


=navigator
NAVI: Heeey, Chef! Good to see you!
- (loop)
    * [Hello.]
        CHEF: Nice to see you, too.
        ->loop
    * {loop} [Is everything good on our route?]
        NAVI: Sure! 
        NAVI: I mean, I haven't checked it today.
        NAVI: But I don't think much has changed.
        ->loop
    * {loop>1} [So how long 'til we arrive?]
        NAVI: It shouldn't be more than a few days.
        NAVI: Stars willing.
        ->loop
    * [Do you smell this? Is something on fire?]
        NAVI: No, silly!
        NAVI: These are just my <>
    * [Is that a...? {(cough)|(cough-cough)}]
        NAVI: Oh, yes!
        NAVI: My oldest friends, the <>
    * {loop>2}[I'm sorry, I have to go.]
        NAVI: Oh. Okay.
        NAVI: Don't get out of breath! Hihi...
        ->winds_down
- extra-strong cigs!
    TODO adapt the rest of this conversation
->winds_down




=winds_down
-> DONE


== day_1_noon


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