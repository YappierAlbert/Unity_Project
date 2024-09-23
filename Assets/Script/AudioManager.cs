using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("---------- SFX Source ----------")]
    public AudioClip Sword;
    public AudioClip Magic;
    public AudioClip Hit;
    public AudioClip Pdead;
    public AudioClip Edead;
    public AudioClip Eshoot;
    public AudioClip Shield;
    public AudioClip Edead2;
    public AudioClip Jump;
    public AudioClip Mclick;
    public AudioClip ChangeWeapon;

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}
