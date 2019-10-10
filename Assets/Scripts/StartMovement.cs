using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMovement : MonoBehaviour {

    enum State {
        settingUp,
        begun,
        ended
    }
    State state { get; set; } = State.settingUp;
    List<GameObject> particles { get; set; } = new List<GameObject>();
    public void StartMove() {

        if (state == State.begun)
            return;

        state = State.begun;

        var partCount = GameObject.Find("ParticlesCount").GetComponent<InputField>().text;

        if (partCount != "") {
            foreach (var ch in partCount) {
                if (ch < 48 || ch > 57)
                    return;
            }
            CreatingParticles(int.Parse(partCount));
        }
    }

    void CreatingParticles(int partCount) {

        if (state == State.settingUp)
            return;

        var particleRes = Resources.Load<GameObject>("Prefabs/Particle");

        var leftPos = GameObject.Find("WallL").transform.position.x;
        var rightPos = GameObject.Find("WallR").transform.position.x;
        var topPos = GameObject.Find("WallT").transform.position.y;
        var downPos = GameObject.Find("WallD").transform.position.y;

        var rnd = new System.Random();

        var secs = float.Parse(GameObject.Find("StepInSecs").GetComponent<InputField>().text);

        for (int i = 0; i < partCount; ++i) {

            var particle = GameObject.Instantiate(particleRes);

            particles.Add(particle);

            var startVel = new Vector2(rnd.Next(0, 100), rnd.Next(0, 100)) / 10;
            var color = new Color(rnd.Next(0, 1001) / 1000f, rnd.Next(0, 1001) / 1000f, rnd.Next(0, 1001) / 1000f);

            particle.transform.position = new Vector2(rnd.Next((int)(leftPos * 1000), (int)(rightPos * 1000)) / 1000f,
                rnd.Next((int)(downPos * 1000), (int)(topPos * 1000)) / 1000f);

            particle.AddComponent<ParticlePhys>().SetUp(secs, startVel, color);

        }
    }

    public void PauseStartMove() {

        if (state == State.settingUp)
            return;

        foreach (var part in particles)
            part.GetComponent<ParticlePhys>().StartPause();
    }
    public void StopAll() {
        
        if (state == State.settingUp)
            return;

        foreach(var part in particles) {
            part.GetComponent<ParticlePhys>().DeleteMe();
        }
        state = State.ended;
    }

}
