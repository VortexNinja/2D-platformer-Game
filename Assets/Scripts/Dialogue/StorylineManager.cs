using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    public class StorylineManager : MonoBehaviour
    {
        public enum state { A, B, C, D, E };
        int progress = 0;
        public state currentState = state.A;

        public static StorylineManager sm;

        public Dictionary<int, string> lines = new Dictionary<int, string>();

        private void Start()
        {
            if(sm == null)
            {
                sm = GetComponent<StorylineManager>();
                AddLines();
            }
        }

        public int getLine()
        {
            int currentLine = progress;
            progress++;
            currentState++;
            return currentLine;
        }

        private void AddLines()
        {
            lines[0] = "Hey! Stop! No ugly people allowed";
            lines[1] = "I should wait till the guard goes away";
            lines[2] = "The ugly person is gone, time to walk away";

        }
    }
}
