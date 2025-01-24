using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{

    internal class RoundManager
    {
        MobManager mobManager;

        int roundNumber = 1;
        int roundLength = 20;
        float timeRemaining = 20;
        float spawnDelay = 10;
        float spawnCounter = 10;

        public RoundManager(MobManager mobManager)
        {
            this.mobManager = mobManager;
            timeRemaining = roundLength;
        }

        public void Update()
        {
            timeRemaining -= Globals.ElapsedSeconds;

            spawnCounter -= Globals.ElapsedSeconds;

            if (timeRemaining <= 0)
            {
                newRound();
            }

            if (spawnCounter <= 0)
            {
                mobManager.newBear();
                spawnCounter = spawnDelay;
            }
        }

        void newRound()
        {
            roundNumber++;
            roundLength += 10;
            timeRemaining = roundLength;
            spawnDelay = spawnDelay * 0.8f;

            string roundCountString = "Round " + roundNumber;

            Globals.ScreenGUI.UpdateAttribute("text", "roundCount", roundCountString);
        }
    }
}
