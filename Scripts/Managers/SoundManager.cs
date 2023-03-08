using Final_Project.Scripts.FileManagers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project.Scripts.Managers
{
    public static class SoundManager
    {
        public enum SongType
        {
            MainSong,
            HighScore,
            GameOver
        }

        public static Song MainSong { get; set; }
        public static Song HighScoreSong { get; set; }
        public static Song GameOverSong { get; set; }
        public static SoundEffect JumpEffect { get; set; }
        public static SoundEffect PickupCoinEffect { get; set; }
        public static SoundEffect PickupHealthEffect { get; set; }
        public static SoundEffect PlayerHitEffect { get; set; }
        public static SoundEffect PowerUpEffect { get; set; }
        public static SoundEffect KillEffect { get; set; }

        public static Song CurrentlyPlayingSong { get; set; }

        public static Random random { get; set; } = new Random();

        private static int TimePerJumpSound = 30; //So Player doesn't get annoyed by jump sounds
        private static int JumpTimer = 0;
        public static SongType CurrentSong { get; set; }
        public static void LoadSongs(ContentManager Content)
        {
            MainSong = Content.Load<Song>(FilePaths.MainSongPath);
            HighScoreSong = Content.Load<Song>(FilePaths.HighScoreSongPath);
            GameOverSong = Content.Load<Song>(FilePaths.GameOverSongPath);

        }

        public static void LoadSoundEffects(ContentManager Content)
        {
            JumpEffect = Content.Load<SoundEffect>(FilePaths.JumpEffectPath);
            PickupCoinEffect = Content.Load<SoundEffect>(FilePaths.PickupCoinEffectPath);
            PickupHealthEffect = Content.Load<SoundEffect>(FilePaths.PickupHealthEffectPath);
            PlayerHitEffect = Content.Load<SoundEffect>(FilePaths.PlayerHitEffectPath);
            PowerUpEffect = Content.Load<SoundEffect>(FilePaths.PowerUpEffectPath);
            KillEffect = Content.Load<SoundEffect>(FilePaths.KillEffectPath);
        }
        public static void Update()
        {
            JumpTimer++;
            SetSong();
            PlaySong();
        }

        public static void PlaySoundEffect(SoundEffect sound)
        {
            if (sound == JumpEffect && JumpTimer < TimePerJumpSound)
            {
                return;
            }
            else if (sound == JumpEffect && JumpTimer > TimePerJumpSound)
            {
                JumpTimer = 0;
            }
            sound.Play(0.75f, (((float)random.NextDouble() * 2) - 1), 0);
        }
        private static void PlaySong()
        {
            switch (CurrentSong)
            {
                case SongType.MainSong:
                    {
                        if (!MediaPlayer.Equals(CurrentlyPlayingSong, MainSong))
                        {
                            CurrentlyPlayingSong = MainSong;
                            MediaPlayer.Play(MainSong);
                            MediaPlayer.IsRepeating = true;
                        }
                        break;
                    }
                case SongType.HighScore:
                    {
                        if (!MediaPlayer.Equals(CurrentlyPlayingSong, HighScoreSong))
                        {
                            CurrentlyPlayingSong = HighScoreSong;
                            MediaPlayer.Play(HighScoreSong);
                            MediaPlayer.IsRepeating = false;
                        }
                        break;
                    }
                case SongType.GameOver:
                    {
                        {
                            if (!MediaPlayer.Equals(CurrentlyPlayingSong, GameOverSong))
                            {
                                CurrentlyPlayingSong = GameOverSong;
                                MediaPlayer.Play(GameOverSong);
                                MediaPlayer.IsRepeating = false;
                            }
                            break;
                        }
                    }
            }
        }

        private static void SetSong()
        {
            if (MediaPlayer.State != MediaState.Playing)
            {
                CurrentSong = SongType.MainSong;
            }
        }
    }
}
