VAR next_chapter = -> day_1_morning

//can be: bad, neutral, good
VAR OFFI_prev_food = "neutral"
VAR ENGI_prev_food = "neutral"
VAR NAVI_prev_food = "neutral"


EXTERNAL wait(delayTime) // Wait for x seconds before moving to next line
EXTERNAL setDayText(text) // Set the title cards text during fade-in fade-out
EXTERNAL fadeBetweenDays() // Fade in fade out with default values
EXTERNAL fadeToCook() // Fade in fade out with default values, setting title card to "Time to cook" temporarily 

==start
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
~ setDayText("Day 1 - Afternoon")

~ fadeToCook()
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

~ setDayText("Day 2 - Morning")
~ fadeBetweenDays()
-> day_2_morning

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
~ next_chapter = -> day_2_noon
~ setDayText("Day 2 - Afternoon")

~ fadeToCook()
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

~ setDayText("Day 3 - Morning")
~ fadeBetweenDays()

->day_3_morning


== day_3_morning

-> officer

=officer

CHEF: Offi! I'm so happy to see you!

CHEF: How are... you...?

OFFI: *sigh* Nothing good today, I'm afraid.

OFFI: We will need to drop off all our potato cargo on a planet a few parsecs away.

OFFI: It seems like we will need to go without potatoes for the rest of the ride.

OFFI: Do you think you can handle that?

CHEF: Uuuh... Of course.

CHEF: I think I will be able to manage.

OFFI: I'm really-really sorry.

OFFI: None of us really expected this. Corporate orders.

{OFFI_prev_food == "neutral": -> engineer}


OFFI: Oh yeah, another thing.

OFFI: Could you please note down somewhere that I can't eat lactose?

OFFI: I think I haven't let you know beforehand, I'm sorry.

CHEF: Sure, of course!
    ->engineer

=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral

ENGI: Hi CHEF! How did you sleep?

CHEF: Sleep was good. But the morning so far is very bad.

ENGI: Huh? What happened?

CHEF: Didn't you hear?

CHEF: We got orders from above that we need to immediately divert to an another drop off point.

CHEF: This means we won't have...

ENGI: Oh shit! Okay, I have to plan fuel resources, then!

ENGI: Shit.

ENGI: Sorry, talk to you later, CHEF!

->navigator

=engineer_bad


ENGI: Hey CHEF. Sorry if I woke you up in the night going to the toilet.

CHEF: I'm sorry. I might have mixed some gluten in your food..

CHEF: I have to break some bad news. We might run out of potatoes soon.

ENGI: What happened?

CHEF: Didn't you hear?

CHEF: We got orders from above that we need to immediately divert to an another drop-off point.

ENGI: Oh shit! Okay, I have to plan fuel resources, then!

ENGI: Shit.

ENGI: Sorry, talk to you later, CHEF!


-> navigator


=navigator

CHEF: Thick black smoke, wonder who it is?

NAVI: That's gotta be meee! *cough*

NAVI: Ahh, I'm really not doing good, bro.

NAVI: In fact, I'm so DOWN, I ended up downloading a dating app for navigators.

CHEF: Uh-oh...

CHEF: Any luck so far?

NAVI: No, not really.

NAVI: Everyone is soooo lame, and the conversation is never going *anywhere*...

CHEF: Unlike us!

CHEF: Got it?

CHEF: Cuz we are going somewhere?

NAVI: Oh yeah, not sure about that either. Detour. 

NAVI: But you *huuffff* probably already know.

CHEF: Ah, Navi, please stop blowing smoke in the kitch...

CHEF: Ok-bye.

->winds_down

=winds_down
~ next_chapter = -> day_3_noon
~ setDayText("Day 3 - Afternoon")

~ fadeToCook()
->DONE

== day_3_noon


-> officer

=officer

{
- OFFI_prev_food == "neutral":
    -> officer_neutral
- else:
    -> officer_bad
}

=officer_neutral
OFFI: Great job as always, Chef!

OFFI: See you around!
->engineer

=officer_bad

OFFI: Gosh, are you sure there was no milk in this?

CHEF: Mmm, let me see, let me see...

CHEF: Oh no.

CHEF: Oh no, I am so sorry.

OFFI: Hey! No worries.

OFFI: I will poop my intestines out though.

OFFI: I hope you don't mind me saying that.

CHEF: I think... I deserve that after poisoning you.
-> engineer

=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral
ENGI: Great as ever, CHEF!

CHEF: Thank you! I'm happy you enjoyed it.
->navigator

=engineer_bad

ENGI: Thank you CHEF!

ENGI: Gotta run!

CHEF: Okay? Bye?

-> navigator


=navigator

{
    -NAVI_prev_food == "good":
        -> navigator_neutral
    - NAVI_prev_food == "neutral": 
        -> navigator_neutral
    - else:
        -> winds_down
}


=navigator_neutral

NAVI: Thanks for the bangin' meal again!

NAVI: You're so great!
    ->winds_down


=winds_down
~ setDayText("Day 4 - Morning")
~ fadeBetweenDays()
->day_4_morning

== day_4_morning
-> officer

=officer

OFFI: *sigh* First the potatoes, now the coffee machine ran out...

OFFI: I will need to talk to Engi about his caffeine consumption

OFFI: Again.

OFFI: Really not looking forward to it.

CHEF: I have been trying my best to keep him from slowly killing himself with it too.

OFFI: I think we all have.

OFFI: To no avail.
->engineer

=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral

ENGI: Hi! I have good news.

ENGI: I think I figured out the fuel management.

ENGI: We will last until we reach the new drop-off point.

CHEF: Whew!

CHEF: I was already in a bit of a pinch getting the ingredients ready today. Thank you!

ENGI: Couldn't have made it without your cooking!

ENGI: Cheers to another good lunch!

CHEF: Did you just give a toast with a coffee mug?

->navigator

=engineer_bad

ENGI: Hey.

ENGI: Ship is not doing good.

ENGI: I'm not doing good.

CHEF: What's u-

ENGI: I'm just gonna sit down and chill a little, okay? 

ENGI: Thanks.
-> navigator


=navigator
NAVI: Ayyo! Hey, I heard some rumors that we are running low on some food.

NAVI: We will still have lunch today, right?

CHEF: Oh, I'm... really trying my best.

CHEF: It's a little tough without the potatoes though.

CHEF: Also, to be fair, I didn't account for the detour when we embarked on the journey.

CHEF: I'm a little sad these days.

NAVI: Totally got you. I know I'm supposed to be on top of this all, but...

NAVI: I just really wish there were more of us, you know?

CHEF: Maybe on the next route. Fingers crossed?

NAVI: Knocking on wood, bro. Keep it up! I believe in us!

    ->winds_down

=winds_down
~ next_chapter = -> day_4_noon
~ setDayText("Day 4 - Afternoon")

~ fadeToCook()
->DONE

== day_4_noon
//TODO:
-> officer

=officer

OFFI: Sorry, I need to run.
{OFFI_prev_food == "bad": -> engineer}

OFFI: It was a great, meal.

OFFI: I'll talk to you later.
->engineer


=engineer

{
    - ENGI_prev_food == "neutral":
        -> engineer_neutral
    - else:
        -> engineer_bad
}

=engineer_neutral

ENGI: CHEF! I think I can do it!

ENGI: I figured out new ways to optimize the holding capacity.

ENGI: I have to take a look at the control room.

ENGI: Then I will check out the heating system and vents.

ENGI: I think something got a little cooked.

ENGI: In the wrong way. In the smoke way.

CHEF: That one is not the ship's fault, I think.

ENGI: Will report back soon! 

CHEF: Please do!
->navigator

=engineer_bad

ENGI: Hey. Did I do something to piss you off?

CHEF: What do you mean?

ENGI: Nothing, just my gluten allergy.

ENGI: Sorry for being cranky.

ENGI: But we might land today, so that's good.

ENGI: Good luck with cleaning!

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

NAVI: Hey Chef, thanks for the meal!

NAVI: You're always a bright spot in my day!

    ->winds_down

=navigator_bad

NAVI: Ah man, it's gonna be tough sitting in one place all day like this...

CHEF: Nooo...

CHEF: Are you not doing well?

NAVI: Well, sadly I threw up while eating. Not sure what happened, the food wasn't even bad...

NAVI: I'm so sorry for letting you down, Chef!!!

NAVI: I swear I will train my tummy better!

CHEF: ...Are you kidding me? I'M sorry! 

NAVI: No, I AM sorry!
    ->winds_down

=winds_down

~ setDayText("End of the game\nThank you for playing!")
~ fadeBetweenDays()

->DONE




=== function wait(x) ===
~ return 0
=== function setDayText(x) ===
~ return 0
=== function fadeBetweenDays() ===
~ return 0