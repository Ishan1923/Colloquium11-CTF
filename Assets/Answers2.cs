using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answers2 : MonoBehaviour
{
    public Hashtable questions = new Hashtable();
    public Hashtable hints = new Hashtable();

    void Start(){


        questions["2"]="isteCTF{you_knew_about_extensions}";
        questions["3"]="isteCTF{se@rch3d_@_l0t_343fdr43efscd2}";
        questions["4"] = "isteCTF{its_c@ll3d_ra1l_fenc3_cip4er}";
        questions["23"] = "isteCTF{5dw8f36nmg2a9hj.doc}"; 
        questions["16"] = "isteCTF{l34rn_h0w_t0_decrypt_all_types_0f_fil3s}";
        questions["11"] = "isteCTF{w3b_to_c0mment5}";
        questions["15"] = "isteCTF{blackberrygrapepassionfruit}";
        questions["25"] = "isteCTF{adlibs}";
        questions["27"] = "isteCTF{blue_steganography_hurts_me}";
        questions["28"] = "isteCTF{102001017}";
        questions["32"] = "isteCTF{40.4230672,-3.6687656}";
        questions["33"] = "isteCTF{aceupmysleeve}";



        hints["2"]="What you see isn't all there is to it. The secret might be hidden in plain sight, but it's the details you don't normally see that could hold the key. Consider how things are presented—they might not be what they seem.";
        hints["3"]="The archive holds the key, but finding it requires precision. Search carefully within the contents, and pay close attention to what you uncover. It's not just about extracting files—it's about finding the right clue.";

        hints["4"] = "What appears to be a simple image might be hiding more than you think. Use 7-Zip to reveal a hidden file, and once you have it, consider alternative methods to unlock the message inside.";
        hints["23"] = "A file's hash can help identify it in an online malware database. Try searching for it and check the details."; 
        hints["16"] = "you might want to use a BIG wordlist";
        hints["11"] = "Sometimes, the information you need is hidden within the page itself. Pay attention to scripts that might store key information for the site's functionality. A little digging could reveal what you're looking for.";
        hints["15"] = "Think beyond the usual fruit basket—your list needs to be extensive. Once you have it, standardize the names and explore every possible way they can be combined.";
        hints["25"] = "Modern cryptographic schemes don't require the encoder to remain secret, but this one does.";
        hints["27"] = "It can be solved by guessing the proper length of the flag, counting all the differences in each section to make a character, and seeing if that matches our flag format.";
        hints["28"] = "This challenge is a great way to learn what tools and techniques can be used to find specific information in large files.";
        hints["32"] = "The Professor's plan revolved around a location where wealth and strategy intersect. To find it, you might want to take a closer look at the world from above";
        hints["33"] = "Google the problem's name with 'cipher,' and let the adventure of discovery begin.";


        

        

    
    }

    
}
