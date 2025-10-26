VAR next_chapter = -> day_1_morning

//can be: bad, neutral, good
VAR ENGI_prev_food = "neutral"
VAR NAVI_prev_food = "neutral"
VAR OFFI_prev_food = "neutral"

EXTERNAL wait(delayTime) // Wait for x seconds before moving to next line
EXTERNAL set_first_order(character) //set who shall be have the first server plate, ENGI/NAVI/OFFI
EXTERNAL set_second_order(character) //set who shall be have the second server plate, ENGI/NAVI/OFFI

-> day_1_noon

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

-> DONE


== day_1_noon
//TODO:
-> officer

=officer

{
- OFFI_prev_food == "neutral":
    -> officer_neutral
- else:
    -> officer_bad
}

=officer_neutral

OFFI: So yes, as I was saying, they did a really great job with the...

OFFI: Oh hey, thank you for the meal!

OFFI: I see you already got the hang of the job.

OFFI: Keep it up!
->engineer

=officer_bad
-> engineer

=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral

ENGI: Okay I have been wrong, you win.

CHEF: Did we have a bet going on?

ENGI: No, you win in health advices.

CHEF: That's my job after all.

CHEF: Spent all those years collecting debt for it.

ENGI: Yeah...

ENGI: But yes, eating actual food does real wonders.

ENGI: I shouldn't just be living off caffeine, what a surprise.

ENGI: Thank you for the excellent food!

ENGI: Have a nice day!

CHEF: Please leave coffee for the others too!

ENGI: Will do!

->navigator

=engineer_bad

ENGI: Thank you for the food, Chef.
CHEF: I hope you liked it.
ENGI: I am feeling a little off...
ENGI: I think I'm gonna go back to my quarters. 
ENGI: Have a nice day!
CHEF: Oh... You too...!
->navigator

ENGI: Thank you Chef for the food!

CHEF: Glad you liked it!

ENGI: I am feeling a little off right now...

ENGI: I might just go back to my quarters...

ENGI: Have a nice day!

CHEF: Oh...

CHEF: You too!

-> navigator


=navigator

{
    -NAVI_prev_food == "good":
        -> navigator_good
    - NAVI_prev_food == "neutral": 
        -> navigator_neutral
    - else:
        -> navigator_bad
}


=navigator_good

CHEF: Navi, hi!

CHEF: How was the meal?

NAVI: Boss, not gonna lie, it was so perfect. 

NAVI: Like, I never thought you would guess that I like tobacco leaves in my food.

NAVI: But seems like you are just a kitchen genius.

CHEF: I... Eugh... 

CHEF: I just threw up in my mouth now that you bring it up.

NAVI: Genius, I tell you!

-> winds_down

=navigator_neutral

NAVI: CHEF!!!

CHEF: NAVI!!!

NAVI: I HAVE AN IDEA!!!

NAVI: Okay hear me out.

CHEF: Mhm.

NAVI: Tobacco leaves.

CHEF: Mhm?

NAVI: In my food. In my tummy.

CHEF: Huh...?

NAVI: SEE YOU TOMORROW!!!

->winds_down

=navigator_bad

CHEF: Navi, hi!

CHEF: You look a little puzzled. What's up?

NAVI: Nothing, really, the meal was great! //:)

CHEF: ...You sure? 

NAVI: Oh, totally!

NAVI: ...

NAVI: Although... mmm...

NAVI: Could it maybe be a little more... Unhealthy, if this makes sense?

CHEF: Of... course! I think I know what you mean.

CHEF: See you tomorrow?

NAVI: See you tomorrow!

->winds_down

=winds_down
->DONE

== day_2_morning
//TODO:
-> officer

=officer

{
- OFFI_prev_food == "neutral":
    -> officer_neutral
- else:
    -> officer_bad
}

=officer_neutral


->engineer

=officer_bad
-> engineer

=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral

->navigator

=engineer_bad



-> navigator


=navigator

{
    -NAVI_prev_food == "good":
        -> navigator_good
    - NAVI_prev_food == "neutral": 
        -> navigator_neutral
    - else:
        -> navigator_bad
}


=navigator_good

    -> winds_down

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