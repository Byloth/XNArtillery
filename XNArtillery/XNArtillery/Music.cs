using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

using ByloEngine;

namespace XNArtillery
{
    class Music 
    {
        private const int maximumSong = 4;
        private const float standardVolume = 0.75F;

        private Song[] songs;

        public Music()
        {
            songs = new Song[maximumSong];

            MediaPlayer.IsVisualizationEnabled = true;
            MediaPlayer.Volume = standardVolume;
        }

        public void LoadContent()
        {
            for (int i = 0; i < maximumSong; i += 1)
            {
                songs[i] = Global.ThisGame.Content.Load<Song>("Sounds/Soundtracks/Soundtrack" + (i + 1));
            }
        }

        public void Update()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(songs[Randomize.Integer(maximumSong)]);
            }
        }
    }
}
