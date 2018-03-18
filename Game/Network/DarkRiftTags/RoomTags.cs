﻿namespace Game.Scripts.Network.DarkRiftTags
{
    public class RoomTags
    {
        private const ushort Shift = Tags.Room * Tags.TagsPerPlugin;

        public const ushort Create = 0 + Shift;
        public const ushort Join = 1 + Shift;
        public const ushort Leave = 2 + Shift;
        public const ushort GetOpenRooms = 3 + Shift;
        public const ushort GetOpenRoomsFailed = 4 + Shift;
        public const ushort CreateFailed = 5 + Shift;
        public const ushort CreateSuccess = 6 + Shift;
        public const ushort JoinFailed = 7 + Shift;
        public const ushort JoinSuccess = 8 + Shift;
        public const ushort PlayerJoined = 9 + Shift;
        public const ushort LeaveSuccess = 10 + Shift;
        public const ushort PlayerLeft = 11 + Shift;
        public const ushort ChangeColor = 12 + Shift;
        public const ushort ChangeColorSuccess = 13 + Shift;
        public const ushort ChangeColorFailed = 14 + Shift;
        public const ushort StartGame = 15 + Shift;
        public const ushort StartGameSuccess = 16 + Shift;
        public const ushort StartGameFailed = 17 + Shift;
        public const ushort ServerReady = 18 + Shift;
    }
}