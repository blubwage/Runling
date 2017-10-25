﻿namespace Network.DarkRiftTags
{
    public class RoomSubjects
    {
        public const ushort Create = 0;
        public const ushort Join = 1;
        public const ushort Leave = 2;
        public const ushort GetOpenRooms = 3;
        public const ushort GetOpenRoomsFailed = 4;
        public const ushort CreateFailed = 5;
        public const ushort CreateSuccess = 6;
        public const ushort JoinFailed = 7;
        public const ushort JoinSuccess = 8;
        public const ushort PlayerJoined = 9;
        public const ushort LeaveSuccess = 10;
        public const ushort PlayerLeft = 11;
        public const ushort ChangeColor = 12;
        public const ushort ChangeColorSuccess = 13;
        public const ushort ChangeColorFailed = 14;
        public const ushort StartGame = 15;
        public const ushort StartGameSuccess = 16;
        public const ushort StartGameFailed = 17;
        public const ushort ServerReady = 18;
    }
}