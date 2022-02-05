using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower = 1000f;
    [SerializeField] float rotationPower = 100f;
    [SerializeField] AudioClip rocketEngine;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem oneEngineParticle;
    [SerializeField] ParticleSystem twoEngineParticle;
    [SerializeField] ParticleSystem threeEngineParticle;
    [SerializeField] ParticleSystem fourEngineParticle;

    Rigidbody rb;
    AudioSource audioSource;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();

        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketEngine);
        }
        if (!mainEngineParticle.isPlaying) { mainEngineParticle.Play(); }
        if (!threeEngineParticle.isPlaying) { threeEngineParticle.Play(); }
        if (!fourEngineParticle.isPlaying) { fourEngineParticle.Play(); }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
        threeEngineParticle.Stop();
        fourEngineParticle.Stop();
    }



    void RotateLeft()
    {
        ApplyRotation(rotationPower);
        if (!oneEngineParticle.isPlaying) { oneEngineParticle.Play(); }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationPower);
        if (!twoEngineParticle.isPlaying) { twoEngineParticle.Play(); }
    }

    void StopRotating()
    {
        oneEngineParticle.Stop();
        twoEngineParticle.Stop();
    }

    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true; // freezing rotation in order to rotate manually
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing to let physics system rotate
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
