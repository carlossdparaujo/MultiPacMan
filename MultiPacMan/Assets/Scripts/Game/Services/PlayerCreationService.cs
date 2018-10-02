using System;
using MultiPacMan.Game.Requests;

namespace MultiPacMan.Game.Services {
    public interface PlayerCreationService {
        void CreatePlayer (PlayerCreationRequest request);
    }
}