VAR next_chapter = -> day_1_morning

//can be: bad, neutral, good
VAR ENGI_prev_food = "neutral"
VAR NAVI_prev_food = "neutral"
VAR OFFI_prev_food = "neutral"

EXTERNAL wait(delayTime) // Wait for x seconds before moving to next line
EXTERNAL set_first_order(character) //set who shall be have the first server plate, ENGI/NAVI/OFFI
EXTERNAL set_second_order(character) //set who shall be have the second server plate, ENGI/NAVI/OFFI

-> next_chapter

/*
==sample

Here be lines.
Sooner or later.
* [Sooner]
* [Later]
    Well...
- The sooner the better.
This is a game jam, after all.


->DONE
*/

== day_1_morning

-> officer

=officer
OFFI: Good morning, Chef! *yawn* 

OFFI: I see you have occupied your workstation early. Have you slept well?

CHEF: Oh, Officer, hi, good morning!

CHEF: I, uh, yes, I've just been bringin' in some potatoes from the storage room, you know -

OFFI: Don't worry about the formalities. Just call me Offi, meow.

CHEF: Yes, ma'am! I mean, yes, Offi! Meow?

OFFI: Meow!

->engineer


=engineer
ENGI: Hi! 

CHEF: Hey! You must be ENGI, right?

ENGI: That's me! The engineer.

ENGI: Another day and I am ready for another...

CHEF: Another what?

ENGI: ...concoction of course! Have to keep those wheels turning.

ENGI: And how are your wheels?

CHEF: My wheels..?

ENGI: Oh! I mean your... spoons?

ENGI: Sorry I get sucked into the all-nighters and tinkering sometimes.

CHEF: I can see that. Do you take breaks at least?

ENGI: The spaceship needs attention at all times!

ENGI: But I like working with you guys so far.

ENGI: For the better - or maybe worse - goals.

ENGI: So I guess these will be my breaks.

CHEF: I'm glad. Maybe try to limit your caffeine intake though.

CHEF: Haven't seen you without a coffee so far.

ENGI: You wound me, that's what fuels me. 

ENGI: I wonder how many days I could go without food, hmm...

CHEF: Maybe don't try that.


~ set_first_order("ENGI")
~ wait (1)
->navigator


=navigator
//there are emojis in this sections that I commented out, as I don't think we'll be able to handle them.
NAVI: Heeey, Chef! Good to see you!

NAVI: I'm Lead Navigator Navi! //:)

CHEF: Oh hey, I'm Chef, how's it...

CHEF: Oh, god, do you smell this? Is something on fire?

NAVI: Nooo, silly!

NAVI: That's me and my good old buddies, the exxxtra strong cigarettes!

CHEF: ...I'm not sure if that's relieving. How are you allowed to smoke in here?

NAVI: Heehee... //:)

NAVI: Let's call it Navigator's Privilege.

CHEF: Anyway. How's the journey?

CHEF: Any news about how long this should take until arrival?

NAVI: Oh, yeah! We should be around 4 days away from the destination. 
NAVI: We are aaall in thiiis together until then, isn't that right?

~ set_second_order("NAVI")
~ next_chapter = -> day_1_noon


-> DONE


== day_1_noon
//TODO:
-> winds_down

=officer_neutral

OFFI: Hey!
OFFI: I see you already got the hang of the job.
OFFI: Thank you for the great meal!
OFFI: Keep it up!

->winds_down

=officer_bad

-> winds_down

=engineer_neutral

ENGI: You were right.
ENGI: They are really not concoctions.
ENGI: A good meal really refills the body.
ENGI: Well done, Chef!

->winds_down

=engineer_bad

ENGI: Thank you for the food, Chef.
CHEF: I hope you liked it.
ENGI: I am feeling a little off...
ENGI: I think I'm gonna go back to my quarters. 
ENGI: Have a nice day!
CHEF: Oh... You too...!
->winds_down

=navigator_neutral


->winds_down

=navigator_bad


->winds_down

=winds_down
->DONE





=== function wait(x) ===
~ return 0
=== function set_first_order(x) ===
~ return 0
=== function set_second_order(x) ===
~ return 0