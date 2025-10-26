VAR next_chapter = -> day_1_morning

//can be: bad, neutral, good
VAR ENGI_prev_food = "neutral"
VAR NAVI_prev_food = "neutral"
VAR OFFI_prev_food = "neutral"

EXTERNAL wait(delayTime) // Wait for x seconds before moving to next line
EXTERNAL set_first_order(character) //set who shall be have the first server plate, ENGI/NAVI/OFFI
EXTERNAL set_second_order(character) //set who shall be have the second server plate, ENGI/NAVI/OFFI

-> next_chapter

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
OFFI: Good morning! (yawn)
OFFI: I see you have occupied your workstation early.
OFFI: Have you slept well?
    * [Yes, I did. Thank you.]//Confirm
    * [It will take some getting used to.] //disagree
        
    * [I started early and hauled in some potatoes...]//deflect
        OFFI: Okay. ->enough
- CHEF: How about you?
OFFI: I have my complaints, but I won't bore you with them.
- (enough) OFFI: You have a lot to do here.
OFFI: Enough on your plate. Pardon the pun.
OFFI: Anyway, carry on!
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

~ set_first_order("ENGI")
~ wait (1)
->navigator


=navigator
NAVI: Heeey, Chef! Good to see you!
- (opts)
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
        NAVI: Are you okay? You look a little...
        ->loop
    * {loop<2}[Do you smell this? Is something on fire?]
        NAVI: No, silly!
        NAVI: These are just my <>
    * [Is that a...? {(cough)|(cough-cough)}]
        NAVI: Oh, yes!
        NAVI: My oldest friends, the <>
    * {loop>2}[I'm sorry, I have to go.]
        NAVI: Oh. Okay.
        NAVI: Don't get out of breath! Heehee...
        ->winds_down
- extra-strong cigs!->cigs
- (loop)->opts
- (cigs)
    * [But those are dangerous!]
        NAVI: Yeah. So is flying a ship.
    * [How can you even smoke in here?]
        NAVI: Being a navigator... has a few advantages.
        * * [Like what?]
        * * [If you say so...]
- NAVI: Heeheehee...
NAVI: Anyway, I'll not keep you from your kitchen duties.

~ set_second_order("NAVI")
~ next_chapter = -> day_1_noon

->winds_down

=winds_down

-> DONE


== day_1_noon
//TODO:
-> winds_down

=officer_neutral


->winds_down

=officer_bad

-> winds_down


=winds_down
->DONE





=== function wait(x) ===
~ return 0
=== function set_first_order(x) ===
~ return 0
=== function set_second_order(x) ===
~ return 0