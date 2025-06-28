using System.Collections.Generic;
using UnityEngine;

namespace _1v1.lol_cheat
{
    public class Utils : MonoBehaviour
    {
        public static List<Character> GetTargets()
        {
            List<Character> players = new List<Character>();

            foreach (Character player in FindObjectsOfType<Character>())
            {
                if (!player.photonView.IsMine)
                {
                    players.Add(player);
                }
            }

            return players;
        }
    }
}
