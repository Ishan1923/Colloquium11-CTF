using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answers : MonoBehaviour
{
    public Hashtable questions = new Hashtable();
    public Hashtable hints = new Hashtable();

    void Start(){
        questions["1"] = "isteCTF{morsecodeftw}";
        questions["30"] = "isteCTF{bellaciaowinner}";
        questions["12"] = "isteCTF{console_da_best}";
        questions["29"] = "isteCTF{ctf_winner_austin}";
        questions["26"]="isteCTF{fixed_it_is}";
        questions["18"]="isteCTF{ai_can_be_hacked}";
        questions["5"]="isteCTF{Blurry}";
        questions["6"]="isteCTF{music}";
        questions["7"]="isteCTF{n1ce_@nd_ea5y}";
        questions["8"]="isteCTF{654773}";
        questions["9"]="isteCTF{m0nk3_1s_c00l}";
        questions["13"]="isteCTF{istefh}";
        questions["14"]="isteCTF{22022026}";
        questions["10"]="isteCTF{doyouknowhowtoplaymusicwithyourkeyboard}";
        questions["17"]="isteCTF{fly_with_me}";
        questions["19"]="isteCTF{collecctf}";
        questions["20"]="isteCTF{whitespace_is_a_language}";
        questions["21"]="isteCTF{rick_astley}";
        questions["22"]="isteCTF{jsissmth}";
        questions["24"]="isteCTF{i_luv_d1n0}";


        hints["1"]="To hear the secret, both earphones are crucial. Trust your ears.";
        hints["30"]="True art speaks beyond what meets the eye. Look deeper, explore beyond the surface.";
        hints["12"]="Sometimes, the path to discovery lies in the scripts running behind the scenes.";
        hints["29"]="Sometimes, the most intriguing secrets are the ones hidden in plain sight. Look closely at all the files—some may not be immediately visible. A certain spellbinding detail might just unlock what you're searching for";
        hints["26"]="Even damaged packages can still reveal their contents—look for ways to extract, not just repair.";
        hints["18"]="A simple logic trick in code can mask and reveal data. Find the key, flip the bits, and the truth appears.";
        hints["5"]="At first glance, the images might seem identical, but the truth is in the details. Consider using an online tool that can highlight the subtle differences between them—what seems the same might not be.";
        hints["6"]="The melody is only the surface. Look beyond the notes—the key to the secret lies in how they are arranged. Decode the hidden pattern and the message will be revealed.";
        hints["7"]="The secret is hidden in the image itself. Use an image steganography tool to reveal the message concealed within.";
        hints["8"]="Carefully examine the flow of the assembly instructions. The value at [rbp-0x4] is manipulated based on a comparison. Track the changes step by step, and you'll uncover the final value.";
        hints["9"]="Think about how binary digits might represent letters or symbols.";
        hints["13"]="Not every path leads to progress. Find the right destination, and something new will be revealed.";
        hints["14"]="The secret lies in time itself. These numbers represent seconds from a certain reference point in the past—uncover the exact moment, and you'll find the flag.";
        hints["10"]="Convert the melody to its key notes and use a reference to find their place on the keyboard";
        hints["17"]="Sometimes, the full picture only comes into focus once you start uncovering the hidden details. Keep looking closer.";
        hints["19"]="Not everything is as it seems. Look beyond the surface—what's concealed can still be found.";
        hints["20"]="Even emptiness can hold meaning. Look deeper—what seems like nothing might just be something in disguise.";
        hints["21"]="the masterpiece lies in its properties";
        hints["22"]="Messy code can be deceiving. Clean it up, focus on the right pattern, and filter out the distractions—what remains will guide you.";
        hints["24"]="Amidst the unchanging echoes, a hidden message disrupts the pattern. A careful analysis may reveal what the noise is trying to conceal.";


    
    }

}
