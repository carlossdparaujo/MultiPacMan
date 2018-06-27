using System;
using MultiPacMan.Game.Requests;

namespace MultiPacMan.Game.Services {
    public interface PlayerCreationService {
        void SendCreationMessage (PlayerCreationRequest request);
    }
}