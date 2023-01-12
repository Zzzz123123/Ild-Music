﻿using System;
using System.Collections;
using System.Collections.Generic;
using IldMusic.VLCSharp;
using ShareInstances.PlayerResources;

namespace VlcPlayerTest
{
    class Program
    {
        private static VLCSharpPlayer _player = new();
        private static Track track1 = new(pathway:"/home/ollar7788/Music/Eminem.mp3");
        private static Track track2 = new(pathway:"/home/ollar7788/Music/feeling_myself.mp3");
        private static Track track3 = new(pathway:"/home/ollar7788/Music/stay_alive.mp3");

        private static Playlist playlist = new(new List<Track>() {track1, track2, track3});

        static void Main(string[] args)
        {
            //_player.SetTrackInstance(track1);
            _player.SetPlaylistInstance(playlist);


            while (true)
            {

                var cmd = Console.ReadLine();
                Process(cmd);
            }
        }


        private static void Process(string cmd)
        {
            if (cmd == "play")
            {
                _player.Pause_ResumePlayer();
            }
            if (cmd == "pause")
            {
                _player.Pause_ResumePlayer();
            }
            if (cmd == "stop")
            {
               _player.StopPlayer();
            }
            if (cmd == "prev")
            {
                _player.DropPrevious();
            }
            if (cmd == "next")
            {
                _player.DropNext();
            }
        }

    }
}