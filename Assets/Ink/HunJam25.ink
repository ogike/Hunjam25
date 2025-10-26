VAR next_chapter = -> day_1_morning

//can be: bad, neutral, good
VAR OFFI_prev_food = "bad"
VAR ENGI_prev_food = "bad"
VAR NAVI_prev_food = "bad"


EXTERNAL wait(delayTime) // Wait for x seconds before moving to next line

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

~ wait(1)
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

ENGI: HAM-25 needs attention at all times!

ENGI: A spaceship is a delicate machine!

ENGI: But I like working with you guys so far.

ENGI: For the better - or maybe worse - goals.

ENGI: So I guess these will be my breaks.

CHEF: I'm glad. Maybe try to limit your caffeine intake though.

CHEF: Haven't seen you without a coffee so far.

ENGI: You wound me, that's what fuels me. 

ENGI: I wonder how many days I could go without food, hmm...

CHEF: Maybe don't try that.

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

~ next_chapter = -> day_1_noon
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

//event template for morning

== day_2_morning

-> officer

=officer

OFFI: ... So we should be solid for the next 5 days at least. 

OFFI: About the flour...

OFFI: I think that should last us around a millennia, if not more.

CHEF: Great! Everything going according to plan then.

OFFI: That's right.

OFFI: Oh. I had a weird dream last night about the cheese...

OFFI: That we were running out...

OFFI: Everyone was so sad, and I didn't want to let anybody down!

OFFI: Even though I can't personally...

OFFI: NAVI!!!

OFFI: What did I tell you about smoking in the dining area?

CHEF: Well, talk to you later, Offi!
    -> engineer

=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral

ENGI: HAM-25 is ready for the day! 

CHEF: Oh, where did all this energy come from?

ENGI: From my stomach! Yesterday's meal was amazing.

CHEF: I am glad! How much coffee did you drink in the end?

ENGI: I was so nourished by the food I only needed half of my caffeine stash.

ENGI: And yet!

ENGI: I worked on HAM-25 all night long!

ENGI: I fixed the broken pipes near the bathroom...

ENGI: padded out one of the chambers...

ENGI: switched out the microchips in... 

ENGI: Ahh! But I don't want to bore you with this, you must be so busy!

ENGI: So thank you again, can't wait for today's meal!

->navigator

=engineer_bad


ENGI: Good morning... 

CHEF: You seem down. Is everything okay?

ENGI: I am not the smartest in the bunch...

ENGI: I realized this yesterday on the toilet.

CHEF: Hmmm?

ENGI: Sorry to make your work a little harder but I forgot to mention.

ENGI: I have a weak stomach for gluten. 

ENGI: Could you please take that into consideration?

CHEF: Of course, anything for the crew!

ENGI: Thank you! 

ENGI: Your food yesterday tasted amazing either way. 

-> navigator


=navigator

NAVI: ...So I was telling her, hey, girl, you should totally try the tobacco salad.

NAVI: She's definitely not having it though. I think she is annoyed with me.

NAVI: Good morning, Chef!

CHEF: Oh hey, Navi!

NAVI: Who's annoyed with you?

NAVI: Offi, maybe...? She was a little standoffish last night...

CHEF: Chief, I think cats just can't smile.

NAVI: Oh! Right.

NAVI: Sometimes I just forget that she's a cat. Yeah!

    ->winds_down

=winds_down
->DONE


== day_2_noon
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

OFFI: Sorry for running off earlier, that was so rude of me.

OFFI: So, have you been getting good sleep nowadays?

CHEF: Surprisingly, yes!

CHEF: Even though the beds aren't the most comfortable.

OFFI: I can't wait to see the day when the company actually starts caring about keeping us healthy.

OFFI: Until then, we will cozy up on the thinnest mattresses.

CHEF: So be it, Officer! So be it.

->engineer

=officer_bad

OFFI: Hey look I don't have a lot of time to tell you this.

OFFI: I'm so sorry.

OFFI: I think I forgot to let you know that I can't eat dairy.

CHEF: Oh no!

CHEF: I should have known, cats can't drink milk!

OFFI: Oh, no! I mean...

OFFI: Catpeople can. I'm just lactose intolerant.

CHEF: Oooh..!

OFFI: *rumble*

OFFI: I have to go now but I'll see you tomorrow! 

CHEF: I'M SO SORRY OFFI!

-> engineer

=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral

ENGI: Thank you for the amazing meal as always! 

ENGI: I am so happy that we are soon finished with this cargo job. 

ENGI: What are your plans after we landed?

CHEF: I miss sleeping in my own bed. 

CHEF: And you?

ENGI: I need to see my cat as soon as possible.

ENGI: Absolutely important for my soul.

CHEF: Understandable!

ENGI: Alright! I am ready to take on the day and work on the dear HAM-25.

ENGI: Thanks again!
->navigator

=engineer_bad

ENGI: Thanks for the meal, Chef.

CHEF: What's wrong?

ENGI: I am feeling a little under the weather.

ENGI: Maybe the food got mixed together with gluten?

ENGI: Or might just be because I miss my cat from home so much...

CHEF: Oh, it must have been my mistake... I'll try to be more careful next time.

ENGI: I think I'll have some rest in my quarters and watch some cat videos.

ENGI: Have a nice day!

CHEF: You too!

-> navigator


=navigator

{
    -NAVI_prev_food == "good":
        -> navigator_neutral
    - NAVI_prev_food == "neutral": 
        -> navigator_neutral
    - else:
        -> navigator_bad
}


=navigator_neutral

NAVI: I am SOOOO reenergized now. I'm so thankful for your work! 

NAVI: I don't even know how I survived on vending machine food alone last year!

    ->winds_down


=navigator_bad
CHEF: Hey, Navi! How's the tummy doing today?

NAVI: Mmm... I liked the food!

NAVI: It tasted great, don't get me wrong!

CHEF: ...? What's wrong?

NAVI: Uhm... Nothing! //:)

CHEF: Do you have a tummyache?

NAVI: No no!

NAVI: I already threw up so it doesn't hurt anymore!

CHEF: Thats worse!!!

CHEF: Why wouldn't you start with that?

NAVI: I didn't want to freak you out //:(

NAVI: Are you emetophobic? I'm so sorry I forgot to ask!

NAVI: *rumble*

NAVI: Ough, sorry, gotta go!
    ->winds_down

=winds_down
->DONE



=== function wait(x) ===
~ return 0