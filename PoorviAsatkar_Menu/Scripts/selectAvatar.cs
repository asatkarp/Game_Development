using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
    public Image picture; // The Image component of the picture game object
    public Image[] avatars; // An array of Image components for the avatar images

    // This function is called when an avatar image is clicked
    public void OnAvatarClicked(int index)
    {
        // Set the picture game object's sprite to the selected avatar's sprite
        picture.sprite = avatars[index].sprite;
    }
}
