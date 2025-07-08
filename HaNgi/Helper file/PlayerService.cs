using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaNgi
{

    internal class PlayerService
    {

        public static event Action<List<Song>, int> PlayRequest;

        public static void RequestPlay(List<Song> songsToPlay, int startIndex = 0)
        {
            PlayRequest?.Invoke(songsToPlay, startIndex);
        }
    }
}
