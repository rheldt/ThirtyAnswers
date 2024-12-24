using System.IO;
using System.Media;

namespace ThirtyAnswers.Helpers
{
    public static class AudioHelper
    {
        /// <summary>
        /// Plays the sound that starts a new round.
        /// </summary>
        public static void PlayNewRound()
        {
            Stream stream = Properties.Resources.NewRound;
            SoundPlayer player = new SoundPlayer(stream);
            player.Play();
        }

        /// <summary>
        /// Plays the sound that indicates times up.
        /// </summary>
        public static void PlayTimesUp()
        {
            Stream stream = Properties.Resources.TimesUp;
            SoundPlayer player = new SoundPlayer(stream);
            player.Play();
        }
    }
}