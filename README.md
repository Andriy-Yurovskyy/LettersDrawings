## Overview

Your task is to implement a simple app for teaching children how to hand-write the letters correctly. The idea of the app is to define the correct start/end points and order of strokes of each letter, and then track the touch events to make sure the user is making those strokes correctly.

## Letter definition

A letter is consisting of 1 or more strokes, each consisting of 2 or more key points. For the purpose of this task, all strokes are in straight lines. For example, letter “A” could be defined as (0,0 is in the bottom-left):

- stroke 1: 
    - (0,0)
    - (3,9)
    - (6,0)
- stroke 2:
    - (1,3)
    - (5,3)
## User experience
- The user should see the letter on the screen as a thick dashed outline
- Every point should be a circle with a number inside (according to it’s global order number)
- The user must touch and draw the strokes, each stroke with one touch (i.e. not removing the finger from the screen), and follow the outlined area.
- A valid stroke should start/end/pass through the relevant keypoints within X pixels.
- As user draws, the outlined area should be filled with that stroke
- If the user fails to draw a stroke, this stroke is reset and a message is shown “Please try again”
- For this test task, you do not need to implement the curved letters (S, B, etc), but please think how would you do it and explain in the next meeting.
