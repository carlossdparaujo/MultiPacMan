using System;

namespace MultiPacMan.Game.Requests {
    public interface Request {
        object[] asData ();
    }
}