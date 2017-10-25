﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DarkRift;
using DarkRift.Server;
using Launcher;
using Network.DarkRiftTags;
using UnityEngine;
using UnityEngine.UI;

namespace Server.Scripts.SLA
{
    public class VotingSLAServer : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private const byte CountdownFrom = 30;
        public GameObject Game;

        private readonly Dictionary<GameMode, int> _votesPerMode = new Dictionary<GameMode, int>();

        private void Awake()
        {
            ServerManager.Instance.Server.Dispatcher.InvokeWait(() =>
            {
                _text.text = "Voting!";
            });

            _votesPerMode[GameMode.Classic] = 0;
            _votesPerMode[GameMode.Team] = 0;
            _votesPerMode[GameMode.Practice] = 0;

            // Send everyone to start voting
            ServerManager.Instance.SendToAll(new TagSubjectMessage(Tags.Voting, VotingSubjects.StartVoting, new DarkRiftWriter()), SendMode.Reliable);

            ServerManager.Instance.Server.Dispatcher.InvokeAsync(() =>
            {
                StartCoroutine(Countdown());
            });

//            ServerManager.Instance.Server.ClientManager.ClientConnected += OnClientConnected;

            foreach (var client in ServerManager.Instance.Players.Keys)
            {
                client.MessageReceived += OnMessageReceived;
            }
        }

        private void OnDisable()
        {
//            ServerManager.Instance.Server.ClientManager.ClientConnected -= OnClientConnected;
        }

//        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
//        {
//            e.Client.MessageReceived += OnMessageReceived;
//        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message = e.Message as TagSubjectMessage;
            if (message == null || message.Tag != Tags.Voting)
                return;

            var client = (Client) sender;

            // Vote got submitted
            if (message.Subject == VotingSubjects.SubmitVote)
            {
                var reader = message.GetReader();
                ServerManager.Instance.Players[client].Vote = (GameMode)reader.ReadByte();
                UpdateVotes();
            }
            else if (message.Subject == VotingSubjects.FinishVoting)
            {
                Debug.Log("Player finished voting.");
                ServerManager.Instance.Players[client].FinishedVoting = true;

                if (ServerManager.Instance.Players.Values.Any(player => !player.FinishedVoting))
                    return;

                Finish();
            }
        }

        public void UpdateVotes()
        {
            byte classicVotes = 0;
            byte teamVotes = 0;
            byte practiceVotes = 0;

            foreach (var player in ServerManager.Instance.Players.Values)
            {
                switch (player.Vote)
                {
                    case GameMode.Classic:
                        classicVotes++;
                        break;
                    case GameMode.Team:
                        teamVotes++;
                        break;
                    case GameMode.Practice:
                        practiceVotes++;
                        break;
                }
            }

            _votesPerMode[GameMode.Classic] = classicVotes;
            _votesPerMode[GameMode.Team] = teamVotes;
            _votesPerMode[GameMode.Practice] = practiceVotes;

            Debug.Log(classicVotes + " - " + teamVotes + " - " + practiceVotes);

            var writer = new DarkRiftWriter();
            writer.Write(classicVotes);
            writer.Write(teamVotes);
            writer.Write(practiceVotes);

            ServerManager.Instance.SendToAll(new TagSubjectMessage(Tags.Voting, VotingSubjects.VoteUpdate, writer), SendMode.Reliable);
        }

        private IEnumerator Countdown()
        {
            for (var i = CountdownFrom; i > 0; i--)
            {
                var writer = new DarkRiftWriter();
                writer.Write(i);
                ServerManager.Instance.SendToAll(new TagSubjectMessage(Tags.Voting, VotingSubjects.Countdown, writer), SendMode.Reliable);

                yield return new WaitForSeconds(1);
            }

            Finish();
        }

        private void Finish()
        {
            var gameMode = SetGameMode();
            var writer = new DarkRiftWriter();
            writer.Write((byte)gameMode);

            ServerManager.Instance.SendToAll(new TagSubjectMessage(Tags.Voting, VotingSubjects.StartGame, writer), SendMode.Reliable);

            ServerManager.Instance.Server.Dispatcher.InvokeWait(() =>
            {
                gameObject.SetActive(false);
                Game.SetActive(true);
            });
        }

        private GameMode SetGameMode()
        {
            var max = new[] { _votesPerMode[GameMode.Classic], _votesPerMode[GameMode.Team], _votesPerMode[GameMode.Practice] }.Max();
            var mostVotes = _votesPerMode.Keys.Where(mode => _votesPerMode[mode] == max).ToList();
            var idx = mostVotes.Count == 1 ? 0 : ServerManager.Instance.Random.Next(0, mostVotes.Count);
            return mostVotes[idx];
        }
    }
}