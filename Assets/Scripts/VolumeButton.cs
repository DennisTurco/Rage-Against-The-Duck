using UnityEngine;

public class VolumeButton : MonoBehaviour
{
    public void VolumeToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
}
